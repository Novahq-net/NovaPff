using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Resources for grid icons
using NovaPff.Properties;

// NHQTools Libraries
using NHQTools.Extensions;
using NHQTools.FileFormats;
using NHQTools.FileFormats.Pff;

namespace NovaPff
{
    public class PffGrid
    {

        // Public
        public event Action<PffEntry, string, int> CellClickAction; // Raised when icon cells are clicked
        public event Action<PffEntry, string, int> CellEditAction;  // Raised when FileName edit is committed

        // Private
        private readonly Dictionary<int, int> _rowCache = new Dictionary<int, int>();
        private readonly Bitmap _emptyCellBitmap = new Bitmap(1, 1);

        private readonly DataGridView _grid;
        private readonly Func<bool> _isLightTheme;  // Func<bool> Allows dynamic theme changes without needing to re-init the grid.
        private readonly HashSet<string> _pffFileChanged;

        //////////////////////////////////////////////////////////////////////////////////////
        public PffGrid(DataGridView grid, Func<bool> isLightTheme, HashSet<string> pffFileChanged)
        {
            _grid = grid;
            _isLightTheme = isLightTheme;
            _pffFileChanged = pffFileChanged; 

            // Configure column defaults
            ConfigureColumns();

            // Grid events
            _grid.DataBindingComplete += OnDataBindingComplete;
            _grid.CellToolTipTextNeeded += OnCellToolTipTextNeeded;
            _grid.RowPrePaint += OnRowPrePaint;
            _grid.CellFormatting += OnCellFormatting;
            _grid.CellClick += OnCellClick;
            _grid.SelectionChanged += OnSelectionChanged;
            _grid.CellMouseEnter += OnCellMouseEnter;
            _grid.CellMouseLeave += OnCellMouseLeave;
            _grid.MouseLeave += OnMouseLeave;
            _grid.CellBeginEdit += OnCellBeginEdit;
            _grid.CellEndEdit += OnCellEndEdit;
            _grid.DragEnter += OnDragEnter;
        }

        //////////////////////////////////////////////////////////////////////////////////////
        #region Column Setup

        public static class Columns
        {
            // Magic strings are bad
            public const string FileId = "FileId";
            public const string FileName = "FileName";
            public const string FileSize = "FileSize";
            public const string FileTimestamp = "FileTimestamp";
            public const string FileDataOffset = "FileDataOffset";
            public const string FileDeadSpace = "FileDeadSpace";
            public const string FileCrc = "FileCrc";
            public const string FileView = "FileView";
            public const string FileExport = "FileExport";
            public const string FileDelete = "FileDelete";

            public static readonly HashSet<string> ImageCol = new HashSet<string>
            {
                FileView,
                FileExport,
                FileDelete
            };

        }

        private void ConfigureColumns()
        {
            // Column alignments, must be set before applying themes
            var fileNameCol = _grid.Columns[Columns.FileName];
            if (fileNameCol != null)
                fileNameCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Column tags for theming, must be set before applying themes
            var fileViewCol = _grid.Columns[Columns.FileView];
            if (fileViewCol != null)
                fileViewCol.Tag = "datagrid-file-magnifying-glass";

            var fileExportCol = _grid.Columns[Columns.FileExport];
            if (fileExportCol != null)
                fileExportCol.Tag = "datagrid-file-export";

            var fileDeleteCol = _grid.Columns[Columns.FileDelete];
            if (fileDeleteCol != null)
                fileDeleteCol.Tag = "datagrid-file-xmark";

            // Configure action columns for NonSelectingDataGridView
            if (!(_grid is NonSelectingDataGridView noSelect))
                return;

            noSelect.ActionColumns = Columns.ImageCol;
            noSelect.NoHighlightColumns = Columns.ImageCol;
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private static void MapColumn(DataGridView grid, string columnName, string entryPropertyName)
        {
            var col = grid.Columns[columnName];

            if (col != null)
                col.DataPropertyName = entryPropertyName;

        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Data Binding
        public void BindDataSource(IEnumerable<PffEntry> entries, AppSettings settings, HashSet<string> importedFiles, TextBox filterText = null, ComboBox filterMagic = null)
        {

            // Preserve current sort column and order
            var sortCol = _grid.SortedColumn;
            var sortOrder = _grid.SortOrder;

            // Do not auto generate columns
            _grid.AutoGenerateColumns = false;

            // Map PffDataGridCols.Property to properties within PffEntry
            MapColumn(_grid, Columns.FileId, "Id");
            MapColumn(_grid, Columns.FileDeadSpace, "DeadSpace");
            MapColumn(_grid, Columns.FileName, "FileNameEdit"); // Dummy property to allow editing FileNameStr without modifying the actual property until commit, see OnCellBeginEdit and OnCellEndEdit
            // *** If DataSizeStr is renamed, this will need to be updated also in EntryList.OnComparison for sorting to work correctly ***
            MapColumn(_grid, Columns.FileSize, settings.ShowFileSizeInBytes ? "DataSize" : "DataSizeStr"); // See note above if DataSizeStr is renamed
            MapColumn(_grid, Columns.FileTimestamp, settings.ShowEpochTimestamp ? "Timestamp" : "DateTimeLocalStr");
            MapColumn(_grid, Columns.FileDataOffset, "DataOffset");
            MapColumn(_grid, Columns.FileCrc, "CrcStr");

            // Make sure image columns are not data bound
            foreach (var col in Columns.ImageCol.Select(colName => _grid.Columns[colName]).Where(col => col != null))
                col.DataPropertyName = null;

            // Extract all values before filtering to avoid multiple enumeration (causes UI to feel laggy)
            var hideDeadSpace = !settings.ShowDeadSpaceEntries;

            // Magic filter
            var filterItem = filterMagic?.SelectedItem as PffFilter;
            var hasTypeFilter = filterItem?.TypeVal is FileType;
            var hasExtFilter = filterItem?.TypeVal is string;
            var targetType = filterItem?.TypeVal is FileType ft ? ft : FileType.Unknown;
            var targetExt = filterItem?.TypeVal as string;

            // string.Empty to satisfy non-nullable filterText param
            var txt = filterText?.Text ?? string.Empty;
            var hasTxtFilter = !string.IsNullOrWhiteSpace(txt);
            Regex txtWildcardRx = null;

            // Only setup Regex if we actually have a wildcard    
            if (hasTxtFilter && txt.Contains("*"))
            {
                var pattern = Regex.Escape(txt).Replace("\\*", ".*");
                txtWildcardRx = new Regex(pattern, RegexOptions.IgnoreCase);
            }

            var data = entries.Where(x =>
                (!hideDeadSpace || !x.DeadSpace) &&
                (!hasTypeFilter || x.FileType == targetType) &&
                (!hasExtFilter || string.Equals(x.FileTypeExt, targetExt, StringComparison.OrdinalIgnoreCase)) &&
                (!hasTxtFilter || (txtWildcardRx?.IsMatch(x.FileNameStr) ?? x.FileNameStr.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0))
            );

            // Pin imported entries to the top for the default unsorted view.
            // When a sort column is triggered, EntryList.OnComparison will keep
            // imported entries pinned to the top regardless of sort direction.
            var list = data.ToList();

            if (importedFiles.Count > 0)
                list = list.OrderByDescending(x => importedFiles.Contains(x.FileNameStr)).ToList();

            // Wrap the datasource in a SortableBindingList
            _grid.DataSource = new EntryList(list, importedFiles);

            RebuildRowCache();

            if (sortCol == null || sortOrder == SortOrder.None)
                return;

            var direction = (sortOrder == SortOrder.Ascending)
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            _grid.Sort(sortCol, direction);

        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Refresh / Reset Sort
        public void Refresh(IEnumerable<PffEntry> entries, AppSettings settings, TextBox filterText, ComboBox filterMagic)
        {
            BindDataSource(entries, settings, _pffFileChanged, filterText, filterMagic);
            _grid.ClearSelection();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        public void ResetSort()
        {
            foreach (DataGridViewColumn col in _grid.Columns)
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Row Cache / Find Row by Id
        public void RebuildRowCache()
        {
            _rowCache.Clear();

            if (_grid == null || _grid.Rows.Count == 0) 
                return;

            foreach (DataGridViewRow row in _grid.Rows)
            {
                if (row.IsNewRow) 
                    continue;

                var idObj = row.Cells[Columns.FileId].Value;

                switch (idObj)
                {
                    case int obj:
                    {
                        _rowCache[obj] = row.Index;
                        break;
                    }
                    case uint obj:
                        // So when we forget that PffEntry.Id is uint and not int, we can still cache it without changing the dictionary type
                        _rowCache[(int)obj] = row.Index;
                        break;
                }
            }

        }


        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Entry Helpers

        public class EntryMatch
        {
            public int RowIndex { get; set; }
            public PffEntry Entry { get; set; }
        }

        //////////////////////////////////////////////////////////////////////////////////////
        public IEnumerable<EntryMatch> GetTargetEntries(bool selectedRows)
        {
            // Cast to IEnumerable to allow iterating either collection type
            var rows = selectedRows
                ? (System.Collections.IEnumerable)_grid.SelectedRows
                : _grid.Rows;

            foreach (DataGridViewRow row in rows)
            {
                if (!(row.DataBoundItem is PffEntry entry) || entry.DeadSpace) 
                    continue;

                yield return new EntryMatch
                {
                    RowIndex = row.Index,
                    Entry = entry
                };

            }

        }

        //////////////////////////////////////////////////////////////////////////////////////
        public void InvalidateEntryRow(PffEntry entry)
        {
            var rowIndex = GetEntryRowId(entry.Id);

            if (rowIndex >= 0)
                _grid.InvalidateRow(rowIndex);

        }

        //////////////////////////////////////////////////////////////////////////////////////
        public int GetEntryRowId(int entryId)
        {
            if (_rowCache.TryGetValue(entryId, out var rowIndex))
                return rowIndex;

            return -1;
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region Grid Events
        private void OnDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.DataSource is EntryList source)
            {
                for (var i = 0; i < source.Count; i++)
                {
                    if (source[i].DeadSpace)
                        grid.Rows[i].ReadOnly = true;
                }
            }

            _grid.ClearSelection();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private static void OnCellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex < 0) 
                return;

            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex].Name != Columns.FileCrc) 
                return;

            if (!(grid.Rows[e.RowIndex].DataBoundItem is PffEntry entry)) 
                return;

            // Calculate only on hover
            if (entry.CrcStr.Contains("*"))
            {
                e.ToolTipText = "CRC Mismatch!" + Environment.NewLine + Environment.NewLine +
                                "Stored: " + entry.CrcRead + Environment.NewLine +
                                "Computed: " + entry.CrcComputed + Environment.NewLine + Environment.NewLine +
                                "This does not seem to have any real impact.";
            }

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void OnRowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (!(grid.Rows[e.RowIndex].DataBoundItem is PffEntry entry)) 
                return;

            if (entry.DeadSpace)
            {
                var color = _isLightTheme() ? Color.Silver : Color.DimGray;
                grid.Rows[e.RowIndex].DefaultCellStyle.ForeColor = color;
            }
            else if (_pffFileChanged.Contains(entry.FileNameStr))
            {
                var color = _isLightTheme() ? Color.DarkBlue : Color.CornflowerBlue;
                grid.Rows[e.RowIndex].DefaultCellStyle.ForeColor = color;
                grid.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = color;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Fires whenever DataSource is changed or InvalidateRow is called

            var grid = (DataGridView)sender;

            if (e.RowIndex < 0 || !(grid.Rows[e.RowIndex].DataBoundItem is PffEntry entry))
                return;

            var light = _isLightTheme();

            switch (entry.DeadSpace)
            {
                // DeadSpace remove images
                case true when Columns.ImageCol.Contains(grid.Columns[e.ColumnIndex].Name):
                    e.Value = _emptyCellBitmap;
                    e.FormattingApplied = true;
                    return;

                // Swap icon based on file type
                case false when grid.Columns[e.ColumnIndex].Name == Columns.FileView:
                    Bitmap icon;

                    var switchType = entry.FileType;

                    if (switchType == FileType.BFC)
                    {

                        if(entry.FileNameStr.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                            switchType = FileType.WAV;

                        if (entry.FileNameStr.EndsWith(".dds", StringComparison.OrdinalIgnoreCase))
                            switchType = FileType.DDS;

                        if (entry.FileNameStr.EndsWith(".tga", StringComparison.OrdinalIgnoreCase) || entry.FileNameStr.EndsWith(".mdt", StringComparison.OrdinalIgnoreCase))
                            switchType = FileType.TGA;

                    }

                    switch (switchType)
                    {
                        case FileType.BFC:
                        case FileType.PAK:
                            icon = light ? Resources.datagrid_file_zipper : Resources.datagrid_file_zipper_dark;
                            break;
                        //case FileType.TDI:
                        //case FileType.TDO:
                        //        icon = light ? Resources.datagrid_file_vector : Resources.datagrid_file_vector_dark;
                        //        break;
                        case FileType.BMP:
                        case FileType.DDS:
                        case FileType.FNT:
                        case FileType.JPG:
                        case FileType.PCX:
                        case FileType.PNG:
                        case FileType.R16:
                        case FileType.TGA:
                            icon = light ? Resources.datagrid_file_image : Resources.datagrid_file_image_dark;
                            break;
                        case FileType.TXT:
                        case FileType.RTXT:
                        case FileType.CBIN:
                        case FileType.SCR:
                            icon = light ? Resources.datagrid_file_lines : Resources.datagrid_file_lines_dark;
                            break;
                        case FileType.WAV:
                        case FileType.MP3:
                            icon = light ? Resources.datagrid_file_audio : Resources.datagrid_file_audio_dark;
                            break;
                        case FileType.BMS:
                        case FileType.Unknown:
                        default:
                            icon = light ? Resources.datagrid_file_binary : Resources.datagrid_file_binary_dark;
                            break;
                    }

                    e.Value = icon;
                    e.FormattingApplied = true;

                    return;

                // CRC
                case false when grid.Columns[e.ColumnIndex].Name == Columns.FileCrc && entry.CrcStr.Contains("*"):
                {
                    var crcColor = light ? Color.Red : Color.IndianRed;
                    e.CellStyle.ForeColor = crcColor;
                    e.CellStyle.SelectionForeColor = crcColor;
                    break;
                }

            }

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void OnCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            var col = grid.Columns[e.ColumnIndex];

            // Ignore clicks on dead space rows
            if (row.Get<bool>(Columns.FileDeadSpace))
                return;

            if (!Columns.ImageCol.Contains(col.Name))
                return;

            if (!(row.DataBoundItem is PffEntry entry))
                return;

            // Raise the event so the form can handle the action
            CellClickAction?.Invoke(entry, col.Name, e.RowIndex);
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private static void OnSelectionChanged(object sender, EventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.SelectedRows.Count == 0)
                return;

            var row = grid.SelectedRows[0];

            // Ignore selection of dead space rows
            if (row.Get<bool>(Columns.FileDeadSpace))
                row.Selected = false;

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void OnCellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            var colName = grid.Columns[e.ColumnIndex].Name;

            // Swap cursor to hand if over an image column and the row is not dead space
            if (Columns.ImageCol.Contains(colName) && !row.Get<bool>(Columns.FileDeadSpace))
                _grid.Cursor = Cursors.Hand;

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void OnCellMouseLeave(object sender, DataGridViewCellEventArgs e) => _grid.Cursor = Cursors.Default;

        //////////////////////////////////////////////////////////////////////////////////////
        private void OnMouseLeave(object sender, EventArgs e) => _grid.Cursor = Cursors.Default; // Fix issue when leaving cell quickly to outside of grid

        //////////////////////////////////////////////////////////////////////////////////////
        private string _editOrigFileName;
        private void OnCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var grid = (DataGridView)sender;

            // Only allow in the FileName col
            if (grid.Columns[e.ColumnIndex].Name != Columns.FileName)
            {
                e.Cancel = true;
                return;
            }

            // Block editing dead space entries
            if (grid.Rows[e.RowIndex].DataBoundItem is PffEntry entry && entry.DeadSpace)
            {
                e.Cancel = true;
                return;
            }

            // Capture original name before the grid overwrites FileNameStr
            _editOrigFileName = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

            grid.ClearSelection();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex].Name != Columns.FileName)
                return;

            if (!(grid.Rows[e.RowIndex].DataBoundItem is PffEntry entry))
                return;

            var newName = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

            // Compare against the captured name, the OnCellEndEdit event already changed
            // the FileNameStr property
            if (!string.Equals(newName, _editOrigFileName, StringComparison.OrdinalIgnoreCase))
                CellEditAction?.Invoke(entry, newName, e.RowIndex);

            _editOrigFileName = null; // trust issues

            grid.ClearSelection();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private static void OnDragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files == null || files.Length == 0)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
      
            e.Effect = DragDropEffects.Copy;
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        #region EntryList : SortableBindingList
        public class EntryList : SortableBindingList<PffEntry>
        {
            // Sorting override to ensure consistent sorting when size and/or offset are the
            // same by using the entry ID as a tiebreaker

            private readonly HashSet<string> _importedFiles;

            public EntryList(IList<PffEntry> list, HashSet<string> importedFiles) : base(list)
            {
                _importedFiles = importedFiles;
            }

            protected override int OnComparison(PffEntry lhs, PffEntry rhs)
            {
                // Always pin newly imported entries to the top, regardless of sort direction.
                // Because Compare() inverts the result for descending, we need to compensate here:
                //   Ascending: return -1 to place imported first (Compare keeps as -1)
                //   Descending: return +1 to place imported first (Compare inverts to -1)
                var lhsImported = _importedFiles.Contains(lhs.FileNameStr);
                var rhsImported = _importedFiles.Contains(rhs.FileNameStr);

                switch (lhsImported)
                {
                    case true when !rhsImported:
                        return SortDirectionCore == ListSortDirection.Descending ? 1 : -1;
                    case false when rhsImported:
                        return SortDirectionCore == ListSortDirection.Descending ? -1 : 1;
                }

                int result;

                // If the grid is datasize by the string representation, swap it for the bytes value
                if (SortPropertyCore != null && SortPropertyCore.Name == "DataSizeStr")
                    result = lhs.DataSize.CompareTo(rhs.DataSize);
                else
                    result = base.OnComparison(lhs, rhs);

                // If values are equal, sort by ID to prevent jumping rows
                if (result == 0)
                    result = lhs.Id.CompareTo(rhs.Id);

                return result;
            }
        }
        #endregion

    }

}