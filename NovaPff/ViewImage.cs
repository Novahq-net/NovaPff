using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

// NHQTools Libraries
using NHQTools.Themes;
using NHQTools.Helpers;
using NHQTools.Utilities;
using NHQTools.Extensions;
using NHQTools.FileFormats;
using NHQTools.FileFormats.Pff;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace NovaPff
{
    public partial class ViewImage : BaseFormTheme
    {
        // Public
        public bool EntryDataSaved { get; private set; }

        // Private
        private readonly PffFile _pff;
        private readonly Encoding _enc;
        private readonly AppSettings _settings;
        private readonly IReadOnlyList<PffEntry> _imgRows;

        private PffEntry _entry;
        private FormatDef _def;
        private byte[] _fileData;
        private FileType _fileType;
        private FileType? _containerType;
        private int _imgRowIndex;

        // UI
        private bool _isLightTheme;

        ////////////////////////////////////////////////////////////////////////////////////
        #region Constructor / Load / Error

        public ViewImage(AppSettings settings, PffEntry entry, IReadOnlyList<PffEntry> imgRows = null, int imgRowIndex = 0)
        {
            InitializeComponent();

            _settings = settings;
            _imgRows = imgRows;
            _imgRowIndex = imgRowIndex;

            _pff = entry.Pff;
            _enc = entry.Pff.Enc;

            LoadEntry(entry);
        }

        private void ViewImageLoad(object sender, EventArgs e)
        {
            _isLightTheme = Common.IsLightColor(panelPictureBox.BackColor);
            RefreshView();
        }

        private void LoadEntry(PffEntry entry)
        {

            byte[] unpackedData = null;
            FileType? unpackedType = null;

            if (entry.FileType == FileType.BFC)
            {
                try
                {
                    unpackedData = Bfc.Unpack(_pff.GetEntryData(entry));
                    unpackedType = Definitions.DetectType(unpackedData, entry.FileNameStr);

                    // Change the fileType to the unpacked type so we
                    // can load the correct viewer
                    if (unpackedType != FileType.DDS && unpackedType != FileType.TGA)
                        throw new Exception("Unpacked data is not a supported file type for this viewer.");

                    unpackedType = unpackedType.Value;

                }
                catch (Exception ex)
                {
                    ViewImageError($"Failed to unpack BFC container:{Environment.NewLine}{Environment.NewLine}{ex.Message}");
                    return;
                }

            }

            _entry = entry;
            _fileType = unpackedType ?? entry.FileType;
            _fileData = unpackedData ?? _pff.GetEntryData(entry);
            _containerType = unpackedType == null ? (FileType?)null : entry.FileType;
            _def = Definitions.GetFormatDef(_fileType);
        }

        private void RefreshView()
        {
            BtnExport.Enabled = false;
            LabelError.Visible = false;
            
            PicBoxView.Image?.Dispose(); // Dispose previous image if exists
            PicBoxView.Image = null;
            PicBoxView.SizeMode = PictureBoxSizeMode.Zoom;

            // Update navigation buttons
            BtnPrev.Enabled = _imgRows != null && _imgRowIndex > 0;
            BtnNext.Enabled = _imgRows != null && _imgRowIndex < _imgRows.Count - 1;

            if (_def?.ToBmpDelegate == null)
            {
                ViewImageError("A method does not exist to handle this image.");
                return;
            }

            try
            {
                var image = _def.ToBmpDelegate(_fileData);
                var bpp = Image.GetPixelFormatSize(image.PixelFormat);

                var container = _containerType.HasValue ? $"{_containerType.Value} > " : "";

                Text = $@"{_entry.FileNameStr}  |  {container}{_fileType}   |   {_fileData.Length:N0}B  |  {_fileData.Length.ToFileSize()}   |   {image.Width}x{image.Height}px  |  {bpp}bpp";
                PicBoxView.Image = image;

                BtnExport.Enabled = true;
            }
            catch (Exception ex)
            {
                ViewImageError(ex.Message);
            }

        }

        private void ViewImageError(string message)
        {
            Text = @"Error!";
            LabelError.Text = $@"Error: {message}";
            LabelError.Visible = true;
            LabelError.ForeColor = _isLightTheme ? Color.Red : Color.IndianRed;
            PicBoxView.Image = SystemIcons.Error.ToBitmap();
            PicBoxView.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        #region Navigation

        private void BtnPrevClick(object sender, EventArgs e) => Navigate(-1);

        private void BtnNextClick(object sender, EventArgs e) => Navigate(1);

        private void Navigate(int direction)
        {
            if (_imgRows == null)
                return;

            var next = _imgRowIndex + direction;

            if (next < 0 || next >= _imgRows.Count)
                return;

            _imgRowIndex = next;
            LoadEntry(_imgRows[_imgRowIndex]);
            RefreshView();
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        #region Form Closing

        private void CloseClick(object sender, EventArgs e) => Close();

        private void ViewImageClosing(object sender, FormClosingEventArgs e) { }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        #region Export

        private void BtnExportClick(object sender, EventArgs e)
        {
            using (var dg = new SaveFileDialog())
            {
                var name = _entry.FileNameStr;
                var ext = Path.GetExtension(name);
                var extUpper = ext.TrimStart('.').ToUpperInvariant();

                dg.Title = @"Export Image";
                dg.Filter = $@"{extUpper} Files (*{ext})|*{ext}|All Files (*.*)|*.*";
                dg.DefaultExt = ext.TrimStart('.');
                dg.FileName = name;

                if (!string.IsNullOrEmpty(_settings.LastExportImageDirectory))
                    dg.InitialDirectory = _settings.LastExportImageDirectory;

                if (dg.ShowDialog(this) != DialogResult.OK)
                    return;

                _settings.LastExportImageDirectory = Path.GetDirectoryName(dg.FileName);

                try
                {
                    File.WriteAllBytes(dg.FileName, _fileData);
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show(this, "Failed to export image:\n" + ex.Message,
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
        #endregion

    }

}