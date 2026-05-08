using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

// NHQTools
using NHQTools.Themes;
using NHQTools.Helpers;
using NHQTools.Utilities;
using NHQTools.Extensions;
using NHQTools.FileFormats;
using NHQTools.FileFormats.Pff;

namespace NovaPff
{
    public partial class Main : BaseFormTheme
    {
        private bool _isExiting;
        private bool _savedState = true;

        private PffFile _pff;
        private readonly PffGrid _pffGrid;
        private readonly HashSet<string> _pffFileChanged; // Set of filenames that have been changed/imported

        private readonly AppSettings _settings;
        private readonly MainStatusStrip _mainStatusStrip;
        private readonly ToolTip _toolTips;

        private SoundPlayer _audioPlayer;
        private MemoryStream _audioStream;
        private PffEntry _audioPlayingEntry;
        private int _lastFilterIndex; // Last valid FilterMagic selection

        private bool _isLightTheme; // Determines if we should use light or dark accent colors

        private readonly Font _fontRegular; // DeadSpace toggle fonts (maybe this should be an image)
        private readonly Font _fontStrikeout; // DeadSpace toggle fonts (maybe this should be an image)
        private readonly Font _fontBold; // Group headers in filter dropdown

        //////////////////////////////////////////////////////////////////////////////////////
        #region Main Load / Close
        public Main()
        {
            InitializeComponent();

            // Apply settings before anything else
            _settings = AppSettings.Load();
            _settings.PropertyChanged += SettingsChanged;

            // Theme
            ThemeResources = Properties.Resources.ResourceManager;
            InitialTheme = _settings.Theme;

            // Tooltips helper
            _toolTips = AccessibilityTooltipHelper.Apply(this);

            // Setup fonts only once to prevent redundant object creation
            _fontRegular = new Font(StatusDeadSpaceToggle.Font, FontStyle.Regular);
            _fontStrikeout = new Font(StatusDeadSpaceToggle.Font, FontStyle.Strikeout);
            _fontBold = new Font(FilterMagic.Font, FontStyle.Bold);

            // Imported files tracker, reset on open/save
            _pffFileChanged = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Group helper for status strip
            _mainStatusStrip = new MainStatusStrip(StatusProgressBar, StatusFileName, StatusVersion,
                StatusSize, StatusEntries, StatusDeadSpace, StatusDeadSpaceToggle);

            // Wires DataGrid click events
            _pffGrid = new PffGrid(DataGrid, () => _isLightTheme, _pffFileChanged);
            _pffGrid.CellClickAction += GridCellClickAction;
            _pffGrid.CellEditAction += GridCellEditAction;

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void MainLoad(object sender, EventArgs e)
        {
            // Determine if we're in a light or dark theme for accent colors
            // Will update via SettingsChanged event
            _isLightTheme = Common.IsLightColor(BackColor);

            // Title
            Text = $@"{Config.AppName} v{Config.AppVersion} (Build {Config.BuildDate})";

            // Status strip
            StatusDeadSpaceToggle.Font = _settings.ShowDeadSpaceEntries ? _fontRegular : _fontStrikeout;

            // Dropdown filter
            FilterMagic.DrawMode = DrawMode.OwnerDrawFixed;
            FilterMagic.DrawItem += (fs, fe) => PffFilter.DrawItem(fs, fe, _fontBold);

            // Must be called after the form is fully initialized
            VisibilityHelper.CaptureInitialVisibility(this);

            // DataGrid helper for rebuilding row cache on sort
            DataGrid.Sorted += (ss, se) =>
            {
                _pffGrid.RebuildRowCache();
                DataGrid.ClearSelection();
                DataGrid.CurrentCell = null; // Removes focus rectangle
            };

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void MainClosing(object sender, FormClosingEventArgs e)
        {
            if (_isExiting)
            {
                _settings.Theme = ThemeManager.CurrentTheme.ToString();
                AppSettings.Save(_settings);
                return;
            }

            e.Cancel = true;

            SaveAndContinue(nextAction:() => {
                _isExiting = true;
                BeginInvoke(new Action(Close));
            });

        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region State / Filter Management
        private void MainBusy(bool isBusy)
        {
            StatusFileName.Visible = !isBusy;

            // Always keep progress bar enabled when visible
            // Reset the progress bar image so animation restarts
            StatusProgressBar.Visible = isBusy;
            StatusProgressBar.Image = Properties.Resources.loading_bar;
            StatusProgressBar.Enabled = true;

            // Disable MainStatusStrip items except progress bar
            MainStatusStrip.Items.OfType<ToolStripItem>()
                .Where(x => x != StatusProgressBar)
                .ForEach(x => x.Enabled = !isBusy);

            Controls.OfType<Control>()
                .Where(x => x != MainStatusStrip)
                .ForEach(x => x.Enabled = !isBusy);

            VisibilityHelper.ToggleVisibility(!isBusy);
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void MarkUnsaved() => _savedState = false;

        //////////////////////////////////////////////////////////////////////////////////////
        private void MarkSaved()
        {
            _savedState = true;

            // Clear the files changed tracker because once we save, all files are now existing
            _pffFileChanged.Clear(); 
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void SaveAndContinue(Action nextAction)
        {
            // If no file is open or there are no changes, just do the nextAction
            if (_pff == null || _savedState)
            {
                nextAction();
                return;
            }

            var result = CustomMessageBox.Show(this, @"Do you want to save changes first?",
                @"Save Changes",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    SaveFile(saveCompleteAction: nextAction);
                    break;
                case DialogResult.No:
                    nextAction?.Invoke();
                    break;
            }

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void ResetWorkspace()
        {
            _pff = null;
            DataGrid.DataSource = null;
            MarkSaved();
            StopAudio();
            ResetFilters();
            ResetStatusStrip();
            VisibilityHelper.ResetToInitial();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void ResetFilters()
        {
            TextBoxFilter.Text = string.Empty;

            if (FilterMagic.Items.Count > 0)
                FilterMagic.SelectedIndex = 0;

            _pffGrid.ResetSort();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void ResetStatusStrip()
        {
            _mainStatusStrip.ResetAll();

            // Reset forecolor to default in case it was changed by an import or filter (On close event)
            _mainStatusStrip.Entries.ForeColor = _isLightTheme ? SystemColors.ControlText : Color.White;
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void RefreshFilterMagic(bool rebuildCache = false)
        {
            if(rebuildCache)
                _pff.RebuildFilterCache();

            PffFilter.BuildList(FilterMagic, _pff);

        }
        //////////////////////////////////////////////////////////////////////////////////////
        private void RefreshStatusStrip()
        {
            long totalBytes = _pff.TotalDataSize;
            long totalDeadSpaceBytes = _pff.TotalDeadSpaceSize;

            _mainStatusStrip.FileName.Value = _pff.SourceFile?.Name ?? "New.pff";

            _mainStatusStrip.Version.Value = $"{_pff.VersionStrExt}";
            _mainStatusStrip.Size.Value = totalBytes.ToFileSize();

            var totalRows = _pff.EntryCount;
            var totalFiltered = _pff.EntryCount - DataGrid.Rows.Count;

            if (totalFiltered > 0) {
                _mainStatusStrip.Entries.Value = $"{totalRows - totalFiltered:N0} ({totalFiltered:N0} Filtered)";
                _mainStatusStrip.Entries.ForeColor = _isLightTheme ? Color.DarkMagenta : Color.HotPink;
            } 
            else
            {
                _mainStatusStrip.Entries.Value = totalRows.ToString("N0");
                _mainStatusStrip.Entries.ForeColor = _isLightTheme ? SystemColors.ControlText : Color.White;
            }


            _mainStatusStrip.DeadSpace.Value = totalDeadSpaceBytes.ToFileSize();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void StopAudio()
        {
            if (_audioPlayer == null)
                return;

            _audioPlayer.Stop();
            _audioPlayer.Dispose();
            _audioStream?.Dispose();

            _audioPlayer = null;
            _audioStream = null;
            _audioPlayingEntry = null;
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Events and Keypresses
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.S:
                    if(_pff == null)
                        return true;
                    MenuSaveClick(null, null);
                    return true;
                case Keys.Control | Keys.Shift | Keys.S:
                    if (_pff == null)
                        return true;
                    MenuSaveAsClick(null, null);
                    return true;
                case Keys.Control | Keys.O:
                    MenuOpenClick(null, null);
                    return true;
                case Keys.Control | Keys.N:
                    MenuNewClick(null, null);
                    return true;
                case Keys.Delete:
                    if (_pff != null && DataGrid.SelectedRows.Count > 0)
                        DeleteEntries(_pffGrid.GetTargetEntries(true));
                    return true;
                default:
                    // Pass all other keys to the base class
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void FilterChanged(object sender, EventArgs e)
        {
            // If group header was selected, revert to previous valid selection
            if (FilterMagic.SelectedItem is PffFilter f && f.GroupName)
            {
                FilterMagic.SelectedIndex = _lastFilterIndex;
                return;
            }

            _lastFilterIndex = FilterMagic.SelectedIndex;
            GridRefresh();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void SettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(AppSettings.ShowDeadSpaceEntries):
                    StatusDeadSpaceToggle.Font = _settings.ShowDeadSpaceEntries ? _fontRegular : _fontStrikeout;
                    break;
                case nameof(AppSettings.Theme):
                    ThemeManager.Apply(_settings.Theme);
                    _isLightTheme = Common.IsLightColor(BackColor);
                    break;
                case nameof(AppSettings.ShowFileSizeInBytes):
                case nameof(AppSettings.ShowEpochTimestamp):
                    break;
            }

            GridRefresh();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        // _settings will update ui automatically via SettingsChanged, which will trigger GridRefresh
        private void DeadSpaceToggleClick(object sender, EventArgs e) => _settings.ShowDeadSpaceEntries = !_settings.ShowDeadSpaceEntries;
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Menu Click
        private void MenuLogoClick(object sender, EventArgs e) => Common.LaunchWebBrowser(Config.AppUrlSuffix);

        //////////////////////////////////////////////////////////////////////////////////////
        private void MenuSettingsClick(object sender, EventArgs e)
        {
            using (var form = new Settings(_settings))
                form.ShowDialog(this);

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void MenuCloseClick(object sender, EventArgs e) => SaveAndContinue(nextAction:ResetWorkspace);

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region New Pff
        private void MenuNewClick(object sender, EventArgs e) => NewFile();

        //////////////////////////////////////////////////////////////////////////////////////
        private void NewFile(Action nextAction = null)
        {

            SaveAndContinue(() =>
            {
                using (var newFile = new New())
                {

                    if (newFile.ShowDialog(this) != DialogResult.OK)
                        return;

                    var gameInfo = newFile.SelectedGame;

                    PffWorker(
                        workerTask:() => PffFile.Create(gameInfo),
                        onStart: ResetWorkspace,
                        onSuccess: () =>
                        {
                            MarkUnsaved(); // Mark new file as unsaved
                            nextAction?.Invoke();

                            RefreshFilterMagic(rebuildCache:false);

                            // refocus main window
                            Activate();
                        }

                    );

                } 
        
            });

        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Open Pff
        private void MenuOpenClick(object sender, EventArgs e) => OpenFile();

        //////////////////////////////////////////////////////////////////////////////////////
        private void OpenFile(string fileName = null)
        {

            SaveAndContinue(() =>
            {
                var targetFile = fileName;

                // If no file was dropped (MenuOpenClick), show the dialog
                if (targetFile == null)
                {
                    OpenFileDialog.InitialDirectory = _settings.LastOpenDirectory ?? string.Empty;

                    if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    targetFile = OpenFileDialog.FileName;
                }

                // Update settings
                _settings.LastOpenDirectory = Path.GetDirectoryName(targetFile);

                // Run the worker
                PffWorker(
                    workerTask: () => PffFile.Open(new FileInfo(targetFile)),
                    onStart: ResetWorkspace, // ResetWorkspace calls MarkSaved
                    onSuccess: () => RefreshFilterMagic(rebuildCache: false));

            });
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Save Pff
        private void MenuSaveClick(object sender, EventArgs e) => SaveFile(saveAsDialog: false);

        //////////////////////////////////////////////////////////////////////////////////////
        private void MenuSaveAsClick(object sender, EventArgs e) => SaveFile(saveAsDialog: true);

        //////////////////////////////////////////////////////////////////////////////////////
        private void SaveFile(bool? saveAsDialog = null, Action saveCompleteAction = null)
        {
            if (!AppSettings.Get(_settings.PreserveDeadSpaceSuppress,
                    _settings.PreserveDeadSpace, 
                    out var preserveDeadSpace))
            {
                var mbr = CustomMessageBox.ShowCheckBox(
                    owner: this,
                    text: "Would you like to preserve \"Dead Space\" entries when saving?" + Environment.NewLine + Environment.NewLine + "It is completely safe to remove these entries as they serve no purpose.",
                    caption: "Confirm Action",
                    checkBoxText: "Don't ask again",
                    checkBoxResultAction: (dr, isChecked) =>
                    {
                        _settings.PreserveDeadSpace = dr == DialogResult.Yes;
                        _settings.PreserveDeadSpaceSuppress = dr != DialogResult.Cancel && isChecked;
                    },
                    defaultCheckState: false,
                    MessageBoxButtons.YesNoCancel
                );

                if (mbr == DialogResult.Cancel)
                    return;

                preserveDeadSpace = mbr == DialogResult.Yes;

            }

            // if saveAsDialog is TRUE or there is no source file because a new file was created, force Save As dialog
            var saveAsFileName = _pff?.SourceFile?.FullName;

            if (saveAsFileName == null || saveAsDialog == true)
            {

                SaveAsFileDialog.FileName = _pff?.SourceFile?.Name ?? "New.pff";
                SaveAsFileDialog.InitialDirectory = _settings.LastSaveDirectory ?? string.Empty;

                using (new CenteredDialogHelper.CenterDialog(this))
                {
                    if (SaveAsFileDialog.ShowDialog(this) != DialogResult.OK)
                        return;

                }

                _settings.LastSaveDirectory = Path.GetDirectoryName(SaveAsFileDialog.FileName);

                saveAsFileName = SaveAsFileDialog.FileName;
                saveAsFileName = saveAsFileName.EndsWith(".pff", StringComparison.OrdinalIgnoreCase)
                    ? saveAsFileName
                    : saveAsFileName + ".pff";
            }

            PffWorker(
                workerTask:() => _pff.Save(saveAsFileName, preserveDeadSpace), 
                onSuccess: () =>
                {
                    MarkSaved();
                    saveCompleteAction?.Invoke();
                }
             );
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Drag and Drop (Import / Open)
        private void GridDragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files == null || files.Length == 0)
                return;

            // Check the first file to determine if we're opening or adding
            var firstFile = files[0];

            if (Path.GetExtension(firstFile).Equals(".pff", StringComparison.OrdinalIgnoreCase))
                OpenFile(firstFile);
            else if (_pff == null)
                NewFile(nextAction:() => ImportEntries(files));
            else
                ImportEntries(files);
            
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Import Entries
        private void MenuImportClick(object sender, EventArgs e)
        {
            if (TryGetImportFiles(out var files))
                ImportEntries(files);
           
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void ImportEntries(string[] files)
        {
            var importList = files.ToList();

            // Skip conflict check if previous message was supressed and Yes was selected
            var autoOverwrite = _settings.ImportOverwriteSuppress && _settings.ImportOverwriteExisting;

            var conflicts = autoOverwrite
                ? new List<string>() // Dummy list to skip the check
                : importList.Where(f => _pff.GetEntry(Path.GetFileName(f)) != null).ToList();

            // Determine if we should overwrite existing files
            // Only show if there are conflicts and the user hasn't suppressed it previously
            if (conflicts.Any())
            {
                var msg = $"{conflicts.Count:N0} of the {files.Length:N0} selected file(s) already exist in this PFF.\n\nOverwrite them?";

                var mbr = CustomMessageBox.ShowCheckBox(
                    owner: this,
                    text: msg,
                    caption: "Confirm Overwrite",
                    checkBoxText: "Don't ask again",
                    checkBoxResultAction: (dr, isChecked) =>
                    {
                        _settings.ImportOverwriteExisting = dr == DialogResult.Yes;
                        _settings.ImportOverwriteSuppress = dr != DialogResult.Cancel && isChecked;
                    },
                    defaultCheckState: false,
                    buttons: MessageBoxButtons.YesNoCancel
                );

                switch (mbr)
                {
                    case DialogResult.Cancel: // Includes if the dialog was closed without choosing an option
                        return;
                    case DialogResult.No:
                        importList = importList.Except(conflicts).ToList();
                        break;
                }
            }

            if (importList.Count == 0)
            {
                // See if we had any files submitted in the first place before showing a message.
                if (files.Length > 0)
                    ShowInfo(@"No files to import after skipping conflicts.", @"Import Aborted");
            
                return;
            }

            // Tallys
            var selectedCount = importList.Count;
            var importedTotal = 0;
            var importedNames = new List<string>();
            var fatalError = false;

            // Worker
            PffWorker(workerTask:() =>
            {

                foreach (var file in importList)
                {
                    try
                    {
                        var fInfo = new FileInfo(file);

                        // If we reached this line, we either have no conflict OR we have permission to overwrite.
                        _pff.ImportEntry(fInfo, overwriteExisting: true);

                        // Collect for UI update on the UI thread
                        importedNames.Add(fInfo.Name);

                        importedTotal++;

                    }
                    catch (ArgumentNullException ex)
                    {
                        fatalError = true;
                        Invoke((MethodInvoker)(() =>
                        {
                            ShowError(
                                $"{ex.Message}{Environment.NewLine}{Environment.NewLine}Import cannot continue.",
                                "Import Error");

                        }));
                        break;
                    }
                    catch (Exception ex)
                    {

                        var continueImport = false;

                        // Invoke on the UI thread
                        Invoke((MethodInvoker)delegate
                        {
                            continueImport = Confirm(
                                $"{ex.Message}{Environment.NewLine}{Environment.NewLine}This file will be skipped. Continue importing remaining files?",
                                @"Import Error",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Error);
                        });

                        if (!continueImport)
                            break;

                    }

                } // End foreach

                return null;

            },
            onComplete: () =>
            {
                if (importedTotal > 0)
                {
                    // Merge imported names on the UI thread
                    foreach (var name in importedNames)
                        _pffFileChanged.Add(name);

                    MarkUnsaved();
                    RefreshFilterMagic(rebuildCache:true);
                    GridRefresh(); // will update status strip
                }

                // Show how many files were skipped due to existing files
                if (fatalError || _settings.ImportResultSuppress)
                    return;

                var msg = $@"Imported {importedTotal:N0} of {selectedCount:N0} file(s).";

                CustomMessageBox.ShowCheckBox(
                    owner: this,
                    text: msg,
                    caption: "Import Results",
                    checkBoxText: "Don't show again",
                    checkBoxResultAction: (dialogResult, isChecked) =>
                    {
                        _settings.ImportResultSuppress = isChecked;
                    },
                    defaultCheckState: false,
                    MessageBoxButtons.OK
                );

            }); // PffWorker

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private bool TryGetImportFiles(out string[] files)
        {
            files = null;
            FileImportDialog.InitialDirectory = _settings.LastImportDirectory ?? string.Empty;
            FileImportDialog.Multiselect = true;

            using (new CenteredDialogHelper.CenterDialog(this))
            {
                if (FileImportDialog.ShowDialog(this) != DialogResult.OK)
                    return false;
            }

            files = FileImportDialog.FileNames;
            _settings.LastImportDirectory = Path.GetDirectoryName(FileImportDialog.FileName);
            return true;
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Export Entries
        private void MenuExportClick(object sender, EventArgs e)
        {

            if (_pff == null || _pff.EntryCount == 0)
                return;

            // Alter for when the menu item was clicked with a single row selected
            if (DataGrid.SelectedRows.Count == 1)
            {

                if (!_settings.ExportMenuSingleSuppress)
                {
                    CustomMessageBox.ShowCheckBox(
                        this,
                        "You have selected a single item. To export just one item, use the icon in the row.\n\n" +
                        "Tip: Hold CTRL to unselect.\n\n" +
                        "WARNING: Suppressing this will automatically unselect the row and export ALL files next time.",
                        "Export Warning",
                        "Don't show again",
                        (result, isChecked) => _settings.ExportMenuSingleSuppress = isChecked,
                        false,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Abort so user can change selection or use the row icon
                    return; 
                }

                // If the warning was supressed, clear the selection and export all
                DataGrid.ClearSelection();
            }

            if (!TryGetExportPath(out var path)) 
                return;
  
            var exportEntries = _pffGrid.GetTargetEntries(DataGrid.SelectedRows.Count > 0)
                .Select(m => m.Entry);

            ExportEntries(path, exportEntries.ToList());
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void GridExportClick(PffEntry entry)
        {
            // Do not combine with MenuExportClick because this is for exporting a single entry
            // If we combine with MenuExportClick, it will always warn the user or export all.
            if (TryGetExportPath(out var path))
                ExportEntries(path, new List<PffEntry> { entry });
           
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void ExportEntries(string destDir, List<PffEntry> entries)
        {
            // Filter invalid entries
            var exportList = entries
                .Where(e => !e.DeadSpace && e.DataSize > 0)
                .ToList();

            if (exportList.Count == 0)
            {
                ShowError(@"Unable to export dead space or 0 byte files.", @"Export Error");
                return;
            }

            // Skip conflict check if previous message was supressed and Yes was selected
            var autoOverwrite = _settings.ExportOverwriteSuppress && _settings.ExportOverwriteExisting;

            var conflicts = autoOverwrite
                ? new List<PffEntry>() // Dummy list to skip the check
                : exportList.Where(e => File.Exists(Path.Combine(destDir, e.FileNameStr))).ToList();

            if (conflicts.Any())
            {
  
                if (_settings.ExportOverwriteSuppress && !_settings.ExportOverwriteExisting)
                {
                    exportList = exportList.Except(conflicts).ToList();
                }
                else
                {
                    var mbr = CustomMessageBox.ShowCheckBox(
                        owner: this,
                        text: $"{conflicts.Count:N0} file(s) already exist in the destination folder.\n\nOverwrite them?",
                        caption: "Confirm Overwrite",
                        checkBoxText: "Don't ask again",
                        checkBoxResultAction: (dr, isChecked) =>
                        {
                            _settings.ExportOverwriteExisting = dr == DialogResult.Yes;
                            _settings.ExportOverwriteSuppress = dr != DialogResult.Cancel && isChecked;
                        },
                        defaultCheckState: false,
                        buttons: MessageBoxButtons.YesNoCancel,
                        icon: MessageBoxIcon.Warning
                    );

                    switch (mbr)
                    {
                        case DialogResult.Cancel: // Includes if the dialog was closed without choosing an option
                            return;
                        case DialogResult.No:
                        {
                            // Remove the conflicting entries from the export list
                            exportList = exportList.Except(conflicts).ToList();
                            if (exportList.Count == 0) 
                                return;
                            break;
                        }

                    } // case DialogResult

                } // End CustomMessageBox

            } // End conflicts.Any()

            // Tallys
            var selectedCount = exportList.Count;
            var exportedCount = 0;
            var fatalError = false;

           if(selectedCount == 0)
            {
                // See if we had any files submitted in the first place before showing a message.
                if (entries.Count > 0)
                    ShowInfo(@"No files to export after skipping conflicts.", @"Export Aborted");

                return;
            }

            // Pff Worker
            PffWorker(workerTask:() =>
            {
                foreach (var entry in exportList)
                {

                    try
                    {
                        // If we reached this line, we either have no conflict OR we have permission to overwrite.
                        _pff.ExportEntry(destDir, entry);
                        exportedCount++;
                    }
                    catch (Exception ex) when (ex is DirectoryNotFoundException || ex is ArgumentNullException)
                    {
                        fatalError = true;
                        Invoke((MethodInvoker)(() =>
                        {
                            ShowError(
                                $"{ex.Message}{Environment.NewLine}{Environment.NewLine}Export cannot continue.",
                                "Export Error");

                        }));
                        break;
                    }
                    catch (Exception ex)
                    {
                        var continueExport = false;

                        // Invoke the confirmation dialog on the UI thread
                        Invoke((MethodInvoker)(() =>
                        {
                            continueExport = Confirm(
                                $"{ex.Message}{Environment.NewLine}{Environment.NewLine}This file will be skipped. Continue exporting remaining files?",
                                "Export Error",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Error);
                        }));

                        if (!continueExport) 
                            break;

                    }
                }
                return null;
            },
            onComplete: () =>
            {
                if (fatalError || _settings.ExportResultSuppress)
                    return;
     
                CustomMessageBox.ShowCheckBox(
                    this,
                    $"Exported {exportedCount:N0} of {selectedCount:N0} file(s).",
                    "Export Results",
                    "Don't show again",
                    (dialogResult, isChecked) => _settings.ExportResultSuppress = isChecked,
                    false,
                    MessageBoxButtons.OK);
               
            }); // End PffWorker
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private bool TryGetExportPath(out string selectedPath)
        {
            selectedPath = null;
            FolderBrowserDialog.SelectedPath = _settings.LastExportDirectory ?? string.Empty;
            FolderBrowserDialog.ShowNewFolderButton = true;
            FolderBrowserDialog.Description = @"Select a destination folder for exported files.";

            using (new CenteredDialogHelper.CenterDialog(this))
            {
                if (FolderBrowserDialog.ShowDialog(this) != DialogResult.OK)
                    return false;
            }

            selectedPath = FolderBrowserDialog.SelectedPath;
            _settings.LastExportDirectory = selectedPath;
            return true;
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Delete Entries
        private void MenuDeleteClick(object sender, EventArgs e) =>
            DeleteEntries(_pffGrid.GetTargetEntries(true));

        //////////////////////////////////////////////////////////////////////////////////////
        private void GridDeleteClick(PffEntry entry, int rowIndex)
        {
            DeleteEntries(new[] {
                new PffGrid.EntryMatch
                {
                    RowIndex = rowIndex,
                    Entry = entry
                }
            });

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void DeleteEntries(IEnumerable<PffGrid.EntryMatch> matches)
        {
            // Prevents the "Collection was modified" error if the grid updated during the loop
            var deletedCount = 0;

            foreach (var match in matches.ToList())
            {
                // Delete from the imports list ***BEFORE*** marking as dead space
                // because SetEntryDeadSpace renames the file
                _pffFileChanged.Remove(match.Entry.FileNameStr);

                // Stop audio
                if (_audioPlayingEntry == match.Entry)
                    StopAudio();

                _pff.SetEntryDeadSpace(match.Entry);

                DataGrid.InvalidateRow(match.RowIndex);

                deletedCount++;
            }

            if (deletedCount <= 0)
                return;

            MarkUnsaved();
            RefreshStatusStrip();
            DataGrid.ClearSelection();
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region View Entries
        private void GridViewClick(PffEntry entry)
        {

            switch (entry.FileType)
            {
                case FileType.BFC when !entry.FileNameStr.EndsWith(".wav", StringComparison.OrdinalIgnoreCase):
                case FileType.BMP:
                case FileType.DDS:
                case FileType.FNT:
                case FileType.JPG:
                case FileType.PCX:
                case FileType.PNG:
                case FileType.R16:
                case FileType.TGA:
                    var imgTypes = new HashSet<FileType>
                    {
                        FileType.BFC, FileType.BMP, FileType.DDS, FileType.FNT, FileType.JPG,
                        FileType.PCX, FileType.PNG, FileType.R16, FileType.TGA
                    };

                    var imgList = new List<PffEntry>();
                    var imgRowIndex = 0;

                    foreach (var match in _pffGrid.GetTargetEntries(false))
                    {
                        if (!imgTypes.Contains(match.Entry.FileType))
                            continue;

                        if (match.Entry.Id == entry.Id)
                            imgRowIndex = imgList.Count;

                        imgList.Add(match.Entry);
                    }

                    using (var viewer = new ViewImage(_settings, entry, imgList, imgRowIndex))
                    {
                        viewer.ShowDialog(this);
                        if (viewer.EntryDataSaved)
                        {
                            MarkUnsaved();

                            // Mark as changed to update row color
                            _pffFileChanged.Add(entry.FileNameStr);

                            GridRefresh();

                        }
                    }
                    break;
                case FileType.CBIN:
                case FileType.RTXT:
                case FileType.SCR:
                case FileType.TXT:
                    using (var viewer = new ViewText(_settings, entry))
                    {
                        viewer.ShowDialog(this);

                        if (viewer.EntryDataSaved)
                        {
                            MarkUnsaved();

                            // Mark as changed to update row color
                            _pffFileChanged.Add(entry.FileNameStr);

                            GridRefresh();
                        }

                    }
                    break;

                case FileType.PAK:

                    if (!AppSettings.Get(_settings.ExportPakAlphaPngSuppress,
                            _settings.ExportPakAlphaPng, 
                            out var exportPng))
                    {

                        var mbr = CustomMessageBox.ShowCheckBox(
                            owner: this,
                            text: "Would you like to export a transparent PNG along with the textures?",
                            caption: "Confirm Action",
                            checkBoxText: "Don't ask again",
                            checkBoxResultAction: (dr, isChecked) =>
                            {
                                _settings.ExportPakAlphaPng = dr == DialogResult.Yes;
                                _settings.ExportPakAlphaPngSuppress = dr != DialogResult.Cancel && isChecked;
                            },
                            defaultCheckState: false,
                            MessageBoxButtons.YesNoCancel
                        );

                        if (mbr == DialogResult.Cancel)
                            break;

                        exportPng = mbr == DialogResult.Yes;
            
                    }

                    if (!TryGetExportPath(out var path))
                        break;

                    try
                    {
                        var entryData = _pff.GetEntryData(entry);

                        var pal = _pff.GetEntry(entry.FileNameStr.Replace(".pak", ".pal", StringComparison.OrdinalIgnoreCase));
                        var palData = pal != null ? _pff.GetEntryData(pal) : null;

                        var pakFiles = Pak.Unpack(entry.FileNameStr, entryData, palData, exportPng);

                        var exportDir = Path.Combine(path, Path.GetFileNameWithoutExtension(entry.FileNameStr));

                        Directory.CreateDirectory(exportDir);

                        foreach (var pakFile in pakFiles)
                            File.WriteAllBytes(Path.Combine(exportDir, pakFile.Key), pakFile.Value);

                    }
                    catch (Exception ex) { ShowError(ex.Message, "PAK Extract Failed");  }

                    break;
                case FileType.BFC when entry.FileNameStr.EndsWith(".wav", StringComparison.OrdinalIgnoreCase):
                case FileType.WAV:

                    byte[] unpackedWav;

                    try
                    {
                        unpackedWav = entry.FileType == FileType.BFC ? Bfc.Unpack(_pff.GetEntryData(entry)) : null;
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Failed to unpack {FileType.BFC} container:\n\n{ex.Message}", "Unpack Error");
                        return;
                    }

                    // Pass along entry so we can keep track of what is playing
                    PlayAudio(entry, unpackedWav ?? _pff.GetEntryData(entry));
                    break;

            }
        }

        private void PlayAudio(PffEntry entry, byte[] audioBytes)
        {
            // Do not StopAudio() before checking if the same entry is playing
            // as StopAudio() clears the _audioPlayingEntry
            if (_audioPlayingEntry == entry)
            {
                StopAudio();
                return;
            }

            StopAudio(); // Stop any currently playing audio

            // For cleanup if it fails
            MemoryStream wavStream = null;
            SoundPlayer wavPlayer = null;

            try
            {

                wavStream = new MemoryStream(Wav.ToWav(audioBytes));
                wavPlayer = new SoundPlayer(wavStream);
                wavPlayer.Play();

                // Only assign on success
                _audioStream = wavStream;
                _audioPlayer = wavPlayer;
                _audioPlayingEntry = entry;
            }
            catch (Exception ex)
            {
                wavPlayer?.Dispose();
                wavStream?.Dispose();
                ShowError($"Failed to play audio:\n{ex.Message}", "Audio Error");
            }

        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region DataGrid Events
        private void GridCellClickAction(PffEntry entry, string columnName, int rowIndex)
        {
            switch (columnName)
            {
                case PffGrid.Columns.FileView:
                    GridViewClick(entry);
                    break;
                case PffGrid.Columns.FileExport:
                    GridExportClick(entry);
                    break;
                case PffGrid.Columns.FileDelete:
                    GridDeleteClick(entry, rowIndex);
                    break;

            }

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void GridCellEditAction(PffEntry entry, string newName, int rowIndex)
        {

            if (_pff == null || entry == null)
                return;

            if (!_pff.RenameEntry(entry, newName))
            {
                // Revert the binding property back to the authoritative name
                // FileNameEdit was already overwritten by the DataGridView binding
                // before OnCellEndEdit fired, so we must manually reset it
                entry.FileNameEdit = entry.FileNameStr;
                DataGrid.InvalidateRow(rowIndex);
                return;
            }

            // Highlight the renamed entry as modified
            _pffFileChanged.Add(entry.FileNameStr);

            MarkUnsaved();
            DataGrid.InvalidateRow(rowIndex);
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void GridRefresh(PffFile pff = null)
        {
            _pff = pff ?? _pff;

            if (_pff == null)
                return;

            _pffGrid.Refresh(_pff.Entries, _settings, TextBoxFilter, FilterMagic);

            // Do not move to GridCellFormatting, as that fires on every cell redraw
            // Including scrolling, sorting, etc...
            RefreshStatusStrip();

        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region MessageBox
        private DialogResult ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon) =>
            CustomMessageBox.Show(this, message, title, buttons, icon);

        //////////////////////////////////////////////////////////////////////////////////////
        private void ShowInfo(string message, string title = "Information") => CustomMessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

        //////////////////////////////////////////////////////////////////////////////////////
        private DialogResult ShowError(string message, string title = "Error") =>
            CustomMessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        //////////////////////////////////////////////////////////////////////////////////////
        private bool Confirm(string message, string title = "Confirm", MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel, MessageBoxIcon icon = MessageBoxIcon.Question) =>
            CustomMessageBox.Show(this, message, title, buttons, icon) == DialogResult.Yes;
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region PFF Background Worker
        private void PffWorker(Func<PffFile> workerTask, Action onStart = null, Action onComplete = null, Action onSuccess = null)
        {
            onStart?.Invoke();

            MainBusy(true);

            var worker = new BackgroundWorker();

            worker.DoWork += (s, args) =>
            {
                args.Result = workerTask();
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                ((BackgroundWorker)s).Dispose();

                MainBusy(false);

                if (args.Error != null)
                {
                    ShowError(args.Error.Message);
                }
                else if (args.Result is PffFile result)
                {
                    // Fires only when workerTask returned a PffFile (no exception)
                    GridRefresh(result);
                    onSuccess?.Invoke();
                }

                onComplete?.Invoke();
            };

            worker.RunWorkerAsync();

        }
        #endregion

    }

}