namespace NovaPff
{
    partial class ViewText
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
                _debounceTimer?.Dispose();
                _statusToolTip?.Dispose();
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
            this.TextBoxData = new System.Windows.Forms.TextBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.LabelValidationStatus = new System.Windows.Forms.Label();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.SelectSerializer = new System.Windows.Forms.ComboBox();
            this.PanelFilter = new System.Windows.Forms.Panel();
            this.PicBoxNoResults = new System.Windows.Forms.PictureBox();
            this.TextBoxSearch = new System.Windows.Forms.TextBox();
            this.PicBoxHelp = new System.Windows.Forms.PictureBox();
            this.BtnExport = new System.Windows.Forms.Button();
            this.PanelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxNoResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // TextBoxData
            // 
            this.TextBoxData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxData.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxData.Location = new System.Drawing.Point(14, 12);
            this.TextBoxData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextBoxData.MaxLength = 0;
            this.TextBoxData.Multiline = true;
            this.TextBoxData.Name = "TextBoxData";
            this.TextBoxData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxData.Size = new System.Drawing.Size(756, 525);
            this.TextBoxData.TabIndex = 0;
            this.TextBoxData.TextChanged += new System.EventHandler(this.TextDataTextChanged);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Location = new System.Drawing.Point(695, 549);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 25);
            this.BtnClose.TabIndex = 7;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.CloseClick);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnSave.Enabled = false;
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Location = new System.Drawing.Point(612, 549);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 25);
            this.BtnSave.TabIndex = 6;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.SaveClick);
            // 
            // LabelValidationStatus
            // 
            this.LabelValidationStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelValidationStatus.BackColor = System.Drawing.Color.Transparent;
            this.LabelValidationStatus.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelValidationStatus.Location = new System.Drawing.Point(628, 502);
            this.LabelValidationStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelValidationStatus.Name = "LabelValidationStatus";
            this.LabelValidationStatus.Size = new System.Drawing.Size(117, 15);
            this.LabelValidationStatus.TabIndex = 0;
            this.LabelValidationStatus.Text = "#status#";
            this.LabelValidationStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LabelValidationStatus.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnSearch.Enabled = false;
            this.BtnSearch.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSearch.Location = new System.Drawing.Point(365, 549);
            this.BtnSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(60, 25);
            this.BtnSearch.TabIndex = 4;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.SearchClick);
            // 
            // SelectSerializer
            // 
            this.SelectSerializer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectSerializer.BackColor = System.Drawing.SystemColors.Window;
            this.SelectSerializer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectSerializer.FormattingEnabled = true;
            this.SelectSerializer.Location = new System.Drawing.Point(473, 552);
            this.SelectSerializer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectSerializer.Name = "SelectSerializer";
            this.SelectSerializer.Size = new System.Drawing.Size(86, 20);
            this.SelectSerializer.TabIndex = 5;
            this.SelectSerializer.SelectionChangeCommitted += new System.EventHandler(this.SelectSerializerChangeCommitted);
            // 
            // PanelFilter
            // 
            this.PanelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PanelFilter.BackColor = System.Drawing.SystemColors.Window;
            this.PanelFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelFilter.Controls.Add(this.PicBoxNoResults);
            this.PanelFilter.Controls.Add(this.TextBoxSearch);
            this.PanelFilter.Location = new System.Drawing.Point(184, 549);
            this.PanelFilter.Name = "PanelFilter";
            this.PanelFilter.Size = new System.Drawing.Size(176, 25);
            this.PanelFilter.TabIndex = 8;
            this.PanelFilter.Tag = "TextBoxWrapper";
            // 
            // PicBoxNoResults
            // 
            this.PicBoxNoResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PicBoxNoResults.BackColor = System.Drawing.Color.Transparent;
            this.PicBoxNoResults.Location = new System.Drawing.Point(156, 4);
            this.PicBoxNoResults.Name = "PicBoxNoResults";
            this.PicBoxNoResults.Size = new System.Drawing.Size(15, 15);
            this.PicBoxNoResults.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicBoxNoResults.TabIndex = 9;
            this.PicBoxNoResults.TabStop = false;
            this.PicBoxNoResults.Visible = false;
            // 
            // TextBoxSearch
            // 
            this.TextBoxSearch.AccessibleDescription = "Search Contents...";
            this.TextBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBoxSearch.Location = new System.Drawing.Point(5, 5);
            this.TextBoxSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextBoxSearch.MaxLength = 15;
            this.TextBoxSearch.Name = "TextBoxSearch";
            this.TextBoxSearch.Size = new System.Drawing.Size(145, 13);
            this.TextBoxSearch.TabIndex = 3;
            this.TextBoxSearch.TextChanged += new System.EventHandler(this.SearchTextChanged);
            // 
            // PicBoxHelp
            // 
            this.PicBoxHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PicBoxHelp.BackColor = System.Drawing.Color.Transparent;
            this.PicBoxHelp.Cursor = System.Windows.Forms.Cursors.Help;
            this.PicBoxHelp.Location = new System.Drawing.Point(732, 17);
            this.PicBoxHelp.Name = "PicBoxHelp";
            this.PicBoxHelp.Size = new System.Drawing.Size(15, 15);
            this.PicBoxHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicBoxHelp.TabIndex = 10;
            this.PicBoxHelp.TabStop = false;
            this.PicBoxHelp.Visible = false;
            this.PicBoxHelp.Click += new System.EventHandler(this.PicBoxHelpClick);
            // 
            // BtnExport
            // 
            this.BtnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnExport.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnExport.Enabled = false;
            this.BtnExport.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExport.Location = new System.Drawing.Point(14, 549);
            this.BtnExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(124, 25);
            this.BtnExport.TabIndex = 11;
            this.BtnExport.Text = "Export text to file";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExportClick);
            // 
            // ViewText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnClose;
            this.ClientSize = new System.Drawing.Size(784, 586);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.PicBoxHelp);
            this.Controls.Add(this.PanelFilter);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.LabelValidationStatus);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.TextBoxData);
            this.Controls.Add(this.SelectSerializer);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.MinimizeBox = false;
            this.Name = "ViewText";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "#ViewText#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewTextClosing);
            this.Load += new System.EventHandler(this.ViewTextLoad);
            this.PanelFilter.ResumeLayout(false);
            this.PanelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxNoResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxHelp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxData;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label LabelValidationStatus;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.ComboBox SelectSerializer;
        private System.Windows.Forms.Panel PanelFilter;
        private System.Windows.Forms.TextBox TextBoxSearch;
        private System.Windows.Forms.PictureBox PicBoxNoResults;
        private System.Windows.Forms.PictureBox PicBoxHelp;
        private System.Windows.Forms.Button BtnExport;
    }
}