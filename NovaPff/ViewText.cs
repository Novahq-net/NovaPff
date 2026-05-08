using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

// NHQTools Libraries
using NHQTools.Themes;
using NHQTools.Helpers;
using NHQTools.Utilities;
using NHQTools.Extensions;
using NHQTools.FileFormats;
using NHQTools.FileFormats.Pff;

namespace NovaPff
{
    public partial class ViewText : BaseFormTheme
    {
        // Public
        public bool EntryDataSaved { get; private set; } // For the caller

        // Private
        private readonly PffFile _pff;
        private readonly Encoding _enc;
        private readonly PffEntry _entry;
        private readonly FormatDef _def;

        private readonly AppSettings _settings;

        private readonly byte[] _fileData;
        private readonly FileType _fileType;

        // UI
        private bool _isLightTheme;

        private readonly Timer _debounceTimer;
        private readonly ToolTip _statusToolTip = new ToolTip();
        
        private string _originalText;
        private SerializeFormat _currentFormat;

        // Constants
        private const int ValidationDebounceMs = 500;
        private const int MaxErrorTooltipLines = 10;
        private readonly SerializeFormat[] _autoValidateFormats = { SerializeFormat.JSON, SerializeFormat.INI };

        // Validation
        private bool _textModified;

        private bool TextModified
        {
            get => _textModified;
            set
            {
                _textModified = value;
                UpdateSaveButton();
            }
        }

        private bool _isValid = true;
       
        ////////////////////////////////////////////////////////////////////////////////////
        #region Constructor / Loading / Error

        public ViewText(AppSettings settings, PffEntry entry)
        {
            InitializeComponent();

            _pff = entry.Pff;
            _entry = entry;
            _enc = entry.Pff.Enc;
            _fileType = entry.FileType;
            _fileData = _pff.GetEntryData(entry);
            _settings = settings ?? new AppSettings();

            // Def
            _def = Definitions.GetFormatDef(_fileType);

            // Serializer
            _currentFormat = _def.SerializeFormats[0];

            // UI
            PicBoxNoResults.Image = SystemIcons.Error.ToBitmap();

            if (!string.IsNullOrEmpty(_def.Notes))
            {
                PicBoxHelp.Visible = true;
                PicBoxHelp.Image = SystemIcons.Question.ToBitmap();
            }

            LabelValidationStatus.Parent = TextBoxData;

            // Serializer Dropdown
            SelectSerializer.DataSource = _def.SerializeFormats;
            SelectSerializer.SelectedItem = _currentFormat;

            // Validator Timer
            _debounceTimer = new Timer { Interval = ValidationDebounceMs };
            _debounceTimer.Tick += OnValidationTimerTick;

        }

        private void ViewTextLoad(object sender, EventArgs e)
        {
            // UI Theme
            _isLightTheme = Common.IsLightColor(TextBoxData.BackColor);

            _debounceTimer.Stop();
            LabelValidationStatus.Visible = false; // keep this here because we resuse this method

            if (_def.TextSerializer == null)
            {
                ViewTextError($"No converters defined for {_entry.FileType}");
                return;
            }

            try
            {
                Text = $@"{_entry.FileNameStr}  |  {_fileType}  |  {_fileData.Length:N0}B  |  {_fileData.Length.ToFileSize()}";

                var rawText = _def.TextSerializer.ToTxt(_fileData, _currentFormat, _enc);
                _originalText = rawText.Replace("\0", "");

                TextBoxData.Text = _originalText;
                TextBoxData.SelectionStart = 0;

                BtnExport.Enabled = true;

                UpdateValidationStatus("Loading...", Color.DimGray, null);

                if (!_autoValidateFormats.Contains(_currentFormat))
                    return;

                LabelValidationStatus.Visible = true;
                _debounceTimer.Start();
            }
            catch (Exception ex)
            { ViewTextError(ex.Message); }
        }

        private void ViewTextError(string message)
        {
            Text = @"Error!";
            TextBoxData.Text = $@"Error: {message}";
            TextBoxData.ForeColor = _isLightTheme ? Color.Red : Color.IndianRed;
            TextBoxData.ReadOnly = true;

            TextModified = false;
            DisableControls();
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        #region UI State

        private void DisableControls()
        {
            BtnSave.Enabled = false;
            BtnExport.Enabled = false;
            TextBoxSearch.Enabled = false;
            BtnSearch.Enabled = false;
        }

        private void TextDataTextChanged(object sender, EventArgs e)
        {
            if (_autoValidateFormats.Contains(_currentFormat))
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            }

            TextModified = !string.Equals(TextBoxData.Text, _originalText);
        }

        private void SelectSerializerChangeCommitted(object sender, EventArgs e)
        {
            if (!(SelectSerializer.SelectedItem is SerializeFormat newFormat) || newFormat == _currentFormat)
                return;

            if (TextModified)
            {
                var result = CustomMessageBox.Show(this, "Changing format discards changes. Continue?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    SelectSerializer.SelectedItem = _currentFormat;
                    return;
                }
            }

            _currentFormat = newFormat;
            ViewTextLoad(sender, e);
        }

        private void PicBoxHelpClick(object sender, EventArgs e) =>
            CustomMessageBox.ShowAutoWidth(this, _def.Notes, "Format Help", MessageBoxButtons.OK, MessageBoxIcon.Information);

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        #region Save

        private void UpdateSaveButton() => BtnSave.Enabled = _textModified && _isValid;

        private void SaveClick(object sender, EventArgs e)
        {
            if (!TryGetBinaryData(out var newBytes, out var errorMsg))
            {
                HandleSaveError(errorMsg);
                return;
            }

            try
            {
                _pff.SetEntryData(_entry, newBytes);
                _originalText = TextBoxData.Text;
                TextModified = false;
                EntryDataSaved = true;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Failed to write data to entry:\n" + ex.Message,
                    "Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleSaveError(string fullErrorMsg)
        {
            var lineNum = 0;

            if (_currentFormat == SerializeFormat.JSON)
            {
                Json.ExceptionHandler(new Exception(fullErrorMsg), (msg, line) =>
                {
                    fullErrorMsg = msg;
                    lineNum = line;
                });
            }

            var result = CustomMessageBox.Show(this,
                $"Syntax Error on Line {lineNum}:\n{fullErrorMsg}\n\nRevert to last saved state?",
                "Save Failed", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (result == DialogResult.Yes)
            {
                TextBoxData.Text = _originalText;
                TextModified = false;
            }
            else if (lineNum > 0)
            {
                HighlightResult(lineNum);
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        #region Validation
        private bool TryGetBinaryData(out byte[] result, out string errorMsg)
        {
            // Attempts to convert the current text back to binary.
            // Returns true if valid, false if a syntax error occurred.

            result = null;
            errorMsg = string.Empty;

            try
            {
                result = _def.TextSerializer.FromTxt(TextBoxData.Text, _currentFormat, _enc);
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        private void OnValidationTimerTick(object sender, EventArgs e)
        {
            _debounceTimer.Stop();

            if (TryGetBinaryData(out _, out var errorMsg))
            {
                SetValidationResult(true, "Valid", null);
            }
            else
            {
                var msg = string.Join(Environment.NewLine,
                    errorMsg.Split('\n').Take(MaxErrorTooltipLines));
                SetValidationResult(false, "Syntax Error", msg);
            }

            UpdateSaveButton();
        }

        private void SetValidationResult(bool isValid, string statusText, string toolTip)
        {
            _isValid = isValid;

            var color = isValid
                ? (_isLightTheme ? Color.Green : Color.LightGreen)
                : (_isLightTheme ? Color.Red : Color.IndianRed);

            UpdateValidationStatus(statusText, color, toolTip);
        }

        private void UpdateValidationStatus(string text, Color color, string tooltip)
        {
            LabelValidationStatus.Text = text;
            LabelValidationStatus.ForeColor = color;
            LabelValidationStatus.Cursor = tooltip == null ? Cursors.Default : Cursors.Help;

            _statusToolTip.SetToolTip(LabelValidationStatus, tooltip);

            if (tooltip != null)
                _statusToolTip.ToolTipIcon = ToolTipIcon.Error;
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        #region Search
        
        private void SearchTextChanged(object sender, EventArgs e)
        {
            BtnSearch.Enabled = !string.IsNullOrWhiteSpace(TextBoxSearch.Text);
            PicBoxNoResults.Visible = false;
        }

        private void SearchClick(object sender, EventArgs e)
        {
            var query = TextBoxSearch.Text;
            PicBoxNoResults.Visible = false;

            if (string.IsNullOrEmpty(query))
                return;

            var index = FindNext(query);

            if (index != -1)
            {
                TextBoxData.Select(index, query.Length);
                TextBoxData.ScrollToCaret();
                TextBoxData.Focus();
            }
            else
            {
                PicBoxNoResults.Visible = true;
            }
        }

        private int FindNext(string query)
        {
            var startPos = TextBoxData.SelectionStart + TextBoxData.SelectionLength;
            var text = TextBoxData.Text;

            var index = text.IndexOf(query, startPos, StringComparison.OrdinalIgnoreCase);

            // Wrap around
            if (index == -1)
                index = text.IndexOf(query, 0, StringComparison.OrdinalIgnoreCase);

            return index;
        }

        private void HighlightResult(int lineNum)
        {
            try
            {
                var lineIndex = lineNum - 1;

                if (lineIndex < 0 || lineIndex >= TextBoxData.Lines.Length)
                    return;

                var start = TextBoxData.GetFirstCharIndexFromLine(lineIndex);
                var length = TextBoxData.Lines[lineIndex].Length;

                TextBoxData.Select(start, length);
                TextBoxData.ScrollToCaret();
                TextBoxData.Focus();
            }
            catch { /* Ignore */ }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        #region Export

        private void BtnExportClick(object sender, EventArgs e)
        {
            using (var dg = new SaveFileDialog())
            {
                var name = _entry.FileNameStr;
                var ext = name == name.ToUpperInvariant() ? ".TXT" : ".txt";

                dg.Title = @"Export Text";
                dg.Filter = @"Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                dg.DefaultExt = "txt";
                dg.FileName = name + ext;

                if (!string.IsNullOrEmpty(_settings.LastExportTextDirectory))
                    dg.InitialDirectory = _settings.LastExportTextDirectory;

                if (dg.ShowDialog(this) != DialogResult.OK)
                    return;

                _settings.LastExportTextDirectory = Path.GetDirectoryName(dg.FileName);

                try
                {
                    File.WriteAllText(dg.FileName, TextBoxData.Text, _enc);
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show(this, "Failed to export text:\n" + ex.Message,
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        #endregion  

        ////////////////////////////////////////////////////////////////////////////////////
        #region Form Closing 
        private void CloseClick(object sender, EventArgs e) => Close();

        private void ViewTextClosing(object sender, FormClosingEventArgs e)
        {
            if (!TextModified)
                return;

            var result = CustomMessageBox.Show(this, "You have unsaved changes. Save before closing?",
                "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    SaveClick(sender, e);
                    if (TextModified)
                        e.Cancel = true;
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;

            }
        }

        #endregion

    }

}