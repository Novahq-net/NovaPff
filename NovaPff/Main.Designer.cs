using System.Windows.Forms;

namespace NovaPff
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                _toolTips?.Dispose();
                _fontRegular?.Dispose();
                _fontStrikeout?.Dispose();
                _fontBold?.Dispose();
                StopAudio();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusProgressBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusEntries = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusDeadSpaceToggle = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusDeadSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.SaveAsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.FileImportDialog = new System.Windows.Forms.OpenFileDialog();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.PbHoverClose = new System.Windows.Forms.PictureBox();
            this.PbHoverSettings = new System.Windows.Forms.PictureBox();
            this.PbHoverExport = new System.Windows.Forms.PictureBox();
            this.PbHoverImport = new System.Windows.Forms.PictureBox();
            this.PbHoverDelete = new System.Windows.Forms.PictureBox();
            this.PbHoverSaveAs = new System.Windows.Forms.PictureBox();
            this.PbHoverSave = new System.Windows.Forms.PictureBox();
            this.PbHoverNew = new System.Windows.Forms.PictureBox();
            this.PbHoverOpen = new System.Windows.Forms.PictureBox();
            this.MenuNovahq = new System.Windows.Forms.PictureBox();
            this.FilterMagic = new System.Windows.Forms.ComboBox();
            this.PanelFilter = new System.Windows.Forms.Panel();
            this.TextBoxFilter = new System.Windows.Forms.TextBox();
            this.DataGrid = new NHQTools.Extensions.NonSelectingDataGridView();
            this.FileId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileDeadSpace = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileDataOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileCrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileView = new System.Windows.Forms.DataGridViewImageColumn();
            this.FileExport = new System.Windows.Forms.DataGridViewImageColumn();
            this.FileDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.MainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverSaveAs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuNovahq)).BeginInit();
            this.PanelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.DefaultExt = "pff";
            this.OpenFileDialog.Filter = "NovaLogic PFF Files (*.pff)|*.pff";
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.AutoSize = false;
            this.MainStatusStrip.BackColor = System.Drawing.Color.LightGray;
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusProgressBar,
            this.StatusFileName,
            this.StatusVersion,
            this.StatusSize,
            this.StatusEntries,
            this.StatusDeadSpaceToggle,
            this.StatusDeadSpace});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 616);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.MainStatusStrip.Size = new System.Drawing.Size(856, 20);
            this.MainStatusStrip.SizingGrip = false;
            this.MainStatusStrip.TabIndex = 5;
            this.MainStatusStrip.Tag = "";
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.AutoSize = false;
            this.StatusProgressBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StatusProgressBar.Image = global::NovaPff.Properties.Resources.loading_bar;
            this.StatusProgressBar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusProgressBar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.StatusProgressBar.Margin = new System.Windows.Forms.Padding(5, 4, -3, 5);
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(170, 11);
            this.StatusProgressBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusProgressBar.Visible = false;
            // 
            // StatusFileName
            // 
            this.StatusFileName.AutoSize = false;
            this.StatusFileName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusFileName.Margin = new System.Windows.Forms.Padding(0, 3, 0, 5);
            this.StatusFileName.Name = "StatusFileName";
            this.StatusFileName.Size = new System.Drawing.Size(170, 12);
            this.StatusFileName.Text = "Ready...";
            this.StatusFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusVersion
            // 
            this.StatusVersion.AutoSize = false;
            this.StatusVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.StatusVersion.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.StatusVersion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusVersion.Margin = new System.Windows.Forms.Padding(0, 3, 0, 5);
            this.StatusVersion.Name = "StatusVersion";
            this.StatusVersion.Size = new System.Drawing.Size(150, 12);
            this.StatusVersion.Tag = "";
            this.StatusVersion.Text = "--";
            // 
            // StatusSize
            // 
            this.StatusSize.AutoSize = false;
            this.StatusSize.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.StatusSize.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.StatusSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusSize.Margin = new System.Windows.Forms.Padding(0, 3, 0, 5);
            this.StatusSize.Name = "StatusSize";
            this.StatusSize.Size = new System.Drawing.Size(150, 12);
            this.StatusSize.Text = "--";
            // 
            // StatusEntries
            // 
            this.StatusEntries.AutoSize = false;
            this.StatusEntries.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.StatusEntries.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.StatusEntries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusEntries.Margin = new System.Windows.Forms.Padding(0, 3, 0, 5);
            this.StatusEntries.Name = "StatusEntries";
            this.StatusEntries.Size = new System.Drawing.Size(200, 12);
            this.StatusEntries.Text = "--";
            // 
            // StatusDeadSpaceToggle
            // 
            this.StatusDeadSpaceToggle.AutoSize = false;
            this.StatusDeadSpaceToggle.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.StatusDeadSpaceToggle.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.StatusDeadSpaceToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusDeadSpaceToggle.LinkColor = System.Drawing.SystemColors.Highlight;
            this.StatusDeadSpaceToggle.Margin = new System.Windows.Forms.Padding(4, 2, 0, 5);
            this.StatusDeadSpaceToggle.Name = "StatusDeadSpaceToggle";
            this.StatusDeadSpaceToggle.Size = new System.Drawing.Size(20, 13);
            this.StatusDeadSpaceToggle.Text = "👁";
            this.StatusDeadSpaceToggle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.StatusDeadSpaceToggle.Click += new System.EventHandler(this.DeadSpaceToggleClick);
            // 
            // StatusDeadSpace
            // 
            this.StatusDeadSpace.AutoSize = false;
            this.StatusDeadSpace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusDeadSpace.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.StatusDeadSpace.LinkColor = System.Drawing.SystemColors.Highlight;
            this.StatusDeadSpace.Margin = new System.Windows.Forms.Padding(0, 3, 0, 5);
            this.StatusDeadSpace.Name = "StatusDeadSpace";
            this.StatusDeadSpace.Size = new System.Drawing.Size(150, 12);
            this.StatusDeadSpace.Text = "--";
            this.StatusDeadSpace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FileImportDialog
            // 
            this.FileImportDialog.Multiselect = true;
            this.FileImportDialog.Title = "Import Files";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewImageColumn1.FillWeight = 4.5F;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::NovaPff.Properties.Resources.datagrid_file_magnifying_glass;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.ToolTipText = "FileViewImg";
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewImageColumn2.FillWeight = 4.5F;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::NovaPff.Properties.Resources.datagrid_file_xmark;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn2.ToolTipText = "FileDeleteImg";
            // 
            // PbHoverClose
            // 
            this.PbHoverClose.AccessibleName = "Close";
            this.PbHoverClose.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.PbHoverClose.BackColor = System.Drawing.Color.Transparent;
            this.PbHoverClose.ErrorImage = null;
            this.PbHoverClose.Image = global::NovaPff.Properties.Resources.folder_xmark;
            this.PbHoverClose.InitialImage = null;
            this.PbHoverClose.Location = new System.Drawing.Point(161, 7);
            this.PbHoverClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PbHoverClose.Name = "PbHoverClose";
            this.PbHoverClose.Size = new System.Drawing.Size(28, 22);
            this.PbHoverClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbHoverClose.TabIndex = 16;
            this.PbHoverClose.TabStop = false;
            this.PbHoverClose.Tag = "folder-xmark";
            this.PbHoverClose.Visible = false;
            this.PbHoverClose.Click += new System.EventHandler(this.MenuCloseClick);
            // 
            // PbHoverSettings
            // 
            this.PbHoverSettings.AccessibleName = "Settings";
            this.PbHoverSettings.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.PbHoverSettings.BackColor = System.Drawing.Color.Transparent;
            this.PbHoverSettings.ErrorImage = null;
            this.PbHoverSettings.Image = global::NovaPff.Properties.Resources.gear;
            this.PbHoverSettings.InitialImage = null;
            this.PbHoverSettings.Location = new System.Drawing.Point(5, 7);
            this.PbHoverSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PbHoverSettings.Name = "PbHoverSettings";
            this.PbHoverSettings.Size = new System.Drawing.Size(28, 22);
            this.PbHoverSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbHoverSettings.TabIndex = 15;
            this.PbHoverSettings.TabStop = false;
            this.PbHoverSettings.Tag = "gear";
            this.PbHoverSettings.Click += new System.EventHandler(this.MenuSettingsClick);
            // 
            // PbHoverExport
            // 
            this.PbHoverExport.AccessibleName = "Export All / Selected File(s)";
            this.PbHoverExport.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.PbHoverExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PbHoverExport.BackColor = System.Drawing.Color.Transparent;
            this.PbHoverExport.ErrorImage = null;
            this.PbHoverExport.Image = global::NovaPff.Properties.Resources.file_export;
            this.PbHoverExport.InitialImage = null;
            this.PbHoverExport.Location = new System.Drawing.Point(800, 41);
            this.PbHoverExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PbHoverExport.Name = "PbHoverExport";
            this.PbHoverExport.Size = new System.Drawing.Size(26, 19);
            this.PbHoverExport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbHoverExport.TabIndex = 14;
            this.PbHoverExport.TabStop = false;
            this.PbHoverExport.Tag = "file-export";
            this.PbHoverExport.Visible = false;
            this.PbHoverExport.Click += new System.EventHandler(this.MenuExportClick);
            // 
            // PbHoverImport
            // 
            this.PbHoverImport.AccessibleName = "Import File(s)";
            this.PbHoverImport.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.PbHoverImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PbHoverImport.BackColor = System.Drawing.Color.Transparent;
            this.PbHoverImport.ErrorImage = null;
            this.PbHoverImport.Image = global::NovaPff.Properties.Resources.file_import;
            this.PbHoverImport.InitialImage = null;
            this.PbHoverImport.Location = new System.Drawing.Point(767, 41);
            this.PbHoverImport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PbHoverImport.Name = "PbHoverImport";
            this.PbHoverImport.Size = new System.Drawing.Size(26, 19);
            this.PbHoverImport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbHoverImport.TabIndex = 13;
            this.PbHoverImport.TabStop = false;
            this.PbHoverImport.Tag = "file-import";
            this.PbHoverImport.Visible = false;
            this.PbHoverImport.Click += new System.EventHandler(this.MenuImportClick);
            // 
            // PbHoverDelete
            // 
            this.PbHoverDelete.AccessibleName = "Delete Selected File(s)";
            this.PbHoverDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.PbHoverDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PbHoverDelete.BackColor = System.Drawing.Color.Transparent;
            this.PbHoverDelete.ErrorImage = null;
            this.PbHoverDelete.Image = global::NovaPff.Properties.Resources.file_xmark;
            this.PbHoverDelete.InitialImage = null;
            this.PbHoverDelete.Location = new System.Drawing.Point(827, 41);
            this.PbHoverDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PbHoverDelete.Name = "PbHoverDelete";
            this.PbHoverDelete.Size = new System.Drawing.Size(26, 19);
            this.PbHoverDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbHoverDelete.TabIndex = 12;
            this.PbHoverDelete.TabStop = false;
            this.PbHoverDelete.Tag = "file-xmark";
            this.PbHoverDelete.Visible = false;
            this.PbHoverDelete.Click += new System.EventHandler(this.MenuDeleteClick);
            // 
            // PbHoverSaveAs
            // 
            this.PbHoverSaveAs.AccessibleName = "Save As";
            this.PbHoverSaveAs.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.PbHoverSaveAs.BackColor = System.Drawing.Color.Transparent;
            this.PbHoverSaveAs.ErrorImage = null;
            this.PbHoverSaveAs.Image = global::NovaPff.Properties.Resources.floppy_disk_pen;
            this.PbHoverSaveAs.InitialImage = null;
            this.PbHoverSaveAs.Location = new System.Drawing.Point(127, 7);
            this.PbHoverSaveAs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PbHoverSaveAs.Name = "PbHoverSaveAs";
            this.PbHoverSaveAs.Size = new System.Drawing.Size(28, 22);
            this.PbHoverSaveAs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbHoverSaveAs.TabIndex = 9;
            this.PbHoverSaveAs.TabStop = false;
            this.PbHoverSaveAs.Tag = "floppy-disk-pen";
            this.PbHoverSaveAs.Visible = false;
            this.PbHoverSaveAs.Click += new System.EventHandler(this.MenuSaveAsClick);
            // 
            // PbHoverSave
            // 
            this.PbHoverSave.AccessibleName = "Save";
            this.PbHoverSave.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.PbHoverSave.BackColor = System.Drawing.Color.Transparent;
            this.PbHoverSave.ErrorImage = null;
            this.PbHoverSave.Image = global::NovaPff.Properties.Resources.floppy_disk;
            this.PbHoverSave.InitialImage = null;
            this.PbHoverSave.Location = new System.Drawing.Point(96, 7);
            this.PbHoverSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PbHoverSave.Name = "PbHoverSave";
            this.PbHoverSave.Size = new System.Drawing.Size(28, 22);
            this.PbHoverSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbHoverSave.TabIndex = 8;
            this.PbHoverSave.TabStop = false;
            this.PbHoverSave.Tag = "floppy-disk";
            this.PbHoverSave.Visible = false;
            this.PbHoverSave.Click += new System.EventHandler(this.MenuSaveClick);
            // 
            // PbHoverNew
            // 
            this.PbHoverNew.AccessibleName = "New";
            this.PbHoverNew.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.PbHoverNew.BackColor = System.Drawing.Color.Transparent;
            this.PbHoverNew.ErrorImage = null;
            this.PbHoverNew.Image = global::NovaPff.Properties.Resources.file_plus;
            this.PbHoverNew.InitialImage = null;
            this.PbHoverNew.Location = new System.Drawing.Point(31, 7);
            this.PbHoverNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PbHoverNew.Name = "PbHoverNew";
            this.PbHoverNew.Size = new System.Drawing.Size(28, 22);
            this.PbHoverNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbHoverNew.TabIndex = 7;
            this.PbHoverNew.TabStop = false;
            this.PbHoverNew.Tag = "file-plus";
            this.PbHoverNew.Click += new System.EventHandler(this.MenuNewClick);
            // 
            // PbHoverOpen
            // 
            this.PbHoverOpen.AccessibleName = "Open";
            this.PbHoverOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.PbHoverOpen.BackColor = System.Drawing.Color.Transparent;
            this.PbHoverOpen.ErrorImage = null;
            this.PbHoverOpen.Image = global::NovaPff.Properties.Resources.folder_open;
            this.PbHoverOpen.InitialImage = null;
            this.PbHoverOpen.Location = new System.Drawing.Point(63, 7);
            this.PbHoverOpen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PbHoverOpen.Name = "PbHoverOpen";
            this.PbHoverOpen.Size = new System.Drawing.Size(28, 22);
            this.PbHoverOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbHoverOpen.TabIndex = 6;
            this.PbHoverOpen.TabStop = false;
            this.PbHoverOpen.Tag = "folder-open";
            this.PbHoverOpen.Click += new System.EventHandler(this.MenuOpenClick);
            // 
            // MenuNovahq
            // 
            this.MenuNovahq.AccessibleName = "Visit Novahq.net";
            this.MenuNovahq.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.MenuNovahq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuNovahq.BackColor = System.Drawing.Color.Transparent;
            this.MenuNovahq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MenuNovahq.ErrorImage = null;
            this.MenuNovahq.Image = global::NovaPff.Properties.Resources.nhq_logo;
            this.MenuNovahq.InitialImage = null;
            this.MenuNovahq.Location = new System.Drawing.Point(735, 7);
            this.MenuNovahq.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MenuNovahq.Name = "MenuNovahq";
            this.MenuNovahq.Size = new System.Drawing.Size(117, 23);
            this.MenuNovahq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuNovahq.TabIndex = 2;
            this.MenuNovahq.TabStop = false;
            this.MenuNovahq.Tag = "nhq-logo";
            this.MenuNovahq.Click += new System.EventHandler(this.MenuLogoClick);
            // 
            // FilterMagic
            // 
            this.FilterMagic.BackColor = System.Drawing.SystemColors.Window;
            this.FilterMagic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterMagic.FormattingEnabled = true;
            this.FilterMagic.Location = new System.Drawing.Point(215, 39);
            this.FilterMagic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FilterMagic.Name = "FilterMagic";
            this.FilterMagic.Size = new System.Drawing.Size(145, 20);
            this.FilterMagic.TabIndex = 2;
            this.FilterMagic.Visible = false;
            this.FilterMagic.SelectionChangeCommitted += new System.EventHandler(this.FilterChanged);
            // 
            // PanelFilter
            // 
            this.PanelFilter.BackColor = System.Drawing.SystemColors.Window;
            this.PanelFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelFilter.Controls.Add(this.TextBoxFilter);
            this.PanelFilter.Location = new System.Drawing.Point(5, 39);
            this.PanelFilter.Name = "PanelFilter";
            this.PanelFilter.Size = new System.Drawing.Size(203, 20);
            this.PanelFilter.TabIndex = 0;
            this.PanelFilter.Tag = "TextBoxWrapper";
            this.PanelFilter.Visible = false;
            // 
            // TextBoxFilter
            // 
            this.TextBoxFilter.AccessibleDescription = "Filter... (* for wildcard)";
            this.TextBoxFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBoxFilter.Location = new System.Drawing.Point(4, 3);
            this.TextBoxFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextBoxFilter.MaxLength = 15;
            this.TextBoxFilter.Name = "TextBoxFilter";
            this.TextBoxFilter.Size = new System.Drawing.Size(193, 13);
            this.TextBoxFilter.TabIndex = 0;
            this.TextBoxFilter.TextChanged += new System.EventHandler(this.FilterChanged);
            // 
            // DataGrid
            // 
            this.DataGrid.ActionColumns = ((System.Collections.Generic.HashSet<string>)(resources.GetObject("DataGrid.ActionColumns")));
            this.DataGrid.AllowDrop = true;
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.AllowUserToDeleteRows = false;
            this.DataGrid.AllowUserToOrderColumns = true;
            this.DataGrid.AllowUserToResizeColumns = false;
            this.DataGrid.AllowUserToResizeRows = false;
            this.DataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 7.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileId,
            this.FileDeadSpace,
            this.FileName,
            this.FileSize,
            this.FileTimestamp,
            this.FileDataOffset,
            this.FileCrc,
            this.FileView,
            this.FileExport,
            this.FileDelete});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 7.5F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.DataGrid.EnableHeadersVisualStyles = false;
            this.DataGrid.Location = new System.Drawing.Point(0, 66);
            this.DataGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.NoHighlightColumns = ((System.Collections.Generic.HashSet<string>)(resources.GetObject("DataGrid.NoHighlightColumns")));
            this.DataGrid.RowHeadersVisible = false;
            this.DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGrid.Size = new System.Drawing.Size(860, 550);
            this.DataGrid.TabIndex = 3;
            this.DataGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.GridDragDrop);
            // 
            // FileId
            // 
            this.FileId.HeaderText = "FileId";
            this.FileId.Name = "FileId";
            this.FileId.ReadOnly = true;
            this.FileId.Visible = false;
            // 
            // FileDeadSpace
            // 
            this.FileDeadSpace.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FileDeadSpace.HeaderText = "Dead Space";
            this.FileDeadSpace.Name = "FileDeadSpace";
            this.FileDeadSpace.ReadOnly = true;
            this.FileDeadSpace.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FileDeadSpace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.FileDeadSpace.Visible = false;
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileName.FillWeight = 20.14721F;
            this.FileName.HeaderText = "File";
            this.FileName.MaxInputLength = 15;
            this.FileName.Name = "FileName";
            this.FileName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FileSize
            // 
            this.FileSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FileSize.DefaultCellStyle = dataGridViewCellStyle2;
            this.FileSize.FillWeight = 20.14721F;
            this.FileSize.HeaderText = "Size";
            this.FileSize.Name = "FileSize";
            this.FileSize.ReadOnly = true;
            this.FileSize.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FileTimestamp
            // 
            this.FileTimestamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FileTimestamp.DefaultCellStyle = dataGridViewCellStyle3;
            this.FileTimestamp.FillWeight = 20.14721F;
            this.FileTimestamp.HeaderText = "Modified";
            this.FileTimestamp.Name = "FileTimestamp";
            this.FileTimestamp.ReadOnly = true;
            this.FileTimestamp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FileDataOffset
            // 
            this.FileDataOffset.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FileDataOffset.DefaultCellStyle = dataGridViewCellStyle4;
            this.FileDataOffset.FillWeight = 15.11041F;
            this.FileDataOffset.HeaderText = "Offset";
            this.FileDataOffset.Name = "FileDataOffset";
            this.FileDataOffset.ReadOnly = true;
            this.FileDataOffset.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FileCrc
            // 
            this.FileCrc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FileCrc.DefaultCellStyle = dataGridViewCellStyle5;
            this.FileCrc.FillWeight = 15.11041F;
            this.FileCrc.HeaderText = "CRC";
            this.FileCrc.Name = "FileCrc";
            this.FileCrc.ReadOnly = true;
            this.FileCrc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FileView
            // 
            this.FileView.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileView.FillWeight = 4.5F;
            this.FileView.HeaderText = "";
            this.FileView.Image = global::NovaPff.Properties.Resources.datagrid_file_unknown;
            this.FileView.Name = "FileView";
            this.FileView.ReadOnly = true;
            this.FileView.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FileView.ToolTipText = "View";
            // 
            // FileExport
            // 
            this.FileExport.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileExport.FillWeight = 4.5F;
            this.FileExport.HeaderText = "";
            this.FileExport.Image = global::NovaPff.Properties.Resources.datagrid_file_export;
            this.FileExport.Name = "FileExport";
            this.FileExport.ReadOnly = true;
            this.FileExport.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FileExport.ToolTipText = "Export";
            // 
            // FileDelete
            // 
            this.FileDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileDelete.FillWeight = 4.5F;
            this.FileDelete.HeaderText = "";
            this.FileDelete.Image = global::NovaPff.Properties.Resources.datagrid_file_xmark;
            this.FileDelete.Name = "FileDelete";
            this.FileDelete.ReadOnly = true;
            this.FileDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FileDelete.ToolTipText = "Remove";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(856, 636);
            this.Controls.Add(this.PanelFilter);
            this.Controls.Add(this.FilterMagic);
            this.Controls.Add(this.PbHoverClose);
            this.Controls.Add(this.PbHoverSettings);
            this.Controls.Add(this.PbHoverExport);
            this.Controls.Add(this.PbHoverImport);
            this.Controls.Add(this.PbHoverDelete);
            this.Controls.Add(this.PbHoverSaveAs);
            this.Controls.Add(this.PbHoverSave);
            this.Controls.Add(this.PbHoverNew);
            this.Controls.Add(this.PbHoverOpen);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.DataGrid);
            this.Controls.Add(this.MenuNovahq);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainClosing);
            this.Load += new System.EventHandler(this.MainLoad);
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverSaveAs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbHoverOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuNovahq)).EndInit();
            this.PanelFilter.ResumeLayout(false);
            this.PanelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox MenuNovahq;
        private OpenFileDialog OpenFileDialog;
        private StatusStrip MainStatusStrip;
        private ToolStripStatusLabel StatusEntries;
        private ToolStripStatusLabel StatusDeadSpace;
        private ToolStripStatusLabel StatusVersion;
        private ToolStripStatusLabel StatusSize;
        private PictureBox PbHoverOpen;
        private PictureBox PbHoverNew;
        private PictureBox PbHoverSave;
        private PictureBox PbHoverSaveAs;
        private PictureBox PbHoverDelete;
        private PictureBox PbHoverImport;
        private PictureBox PbHoverExport;
        private ToolStripStatusLabel StatusFileName;
        private ToolStripStatusLabel StatusProgressBar;
        private NHQTools.Extensions.NonSelectingDataGridView DataGrid;
        private SaveFileDialog SaveAsFileDialog;
        private FolderBrowserDialog FolderBrowserDialog;
        private OpenFileDialog FileImportDialog;
        private ToolStripStatusLabel StatusDeadSpaceToggle;
        private PictureBox PbHoverSettings;
        private PictureBox PbHoverClose;
        private DataGridViewImageColumn dataGridViewImageColumn1;
        private DataGridViewImageColumn dataGridViewImageColumn2;
        private DataGridViewTextBoxColumn FileId;
        private DataGridViewCheckBoxColumn FileDeadSpace;
        private DataGridViewTextBoxColumn FileName;
        private DataGridViewTextBoxColumn FileSize;
        private DataGridViewTextBoxColumn FileTimestamp;
        private DataGridViewTextBoxColumn FileDataOffset;
        private DataGridViewTextBoxColumn FileCrc;
        private DataGridViewImageColumn FileView;
        private DataGridViewImageColumn FileExport;
        private DataGridViewImageColumn FileDelete;
        private ComboBox FilterMagic;
        private Panel PanelFilter;
        private TextBox TextBoxFilter;
    }
}

