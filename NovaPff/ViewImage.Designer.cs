namespace NovaPff
{
    partial class ViewImage
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
                PicBoxView?.Dispose();
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
            this.BtnClose = new System.Windows.Forms.Button();
            this.PicBoxView = new System.Windows.Forms.PictureBox();
            this.panelPictureBox = new System.Windows.Forms.Panel();
            this.LabelError = new System.Windows.Forms.Label();
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnPrev = new System.Windows.Forms.Button();
            this.BtnNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxView)).BeginInit();
            this.panelPictureBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Location = new System.Drawing.Point(683, 551);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 25);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.CloseClick);
            // 
            // PicBoxView
            // 
            this.PicBoxView.BackColor = System.Drawing.Color.Transparent;
            this.PicBoxView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicBoxView.Location = new System.Drawing.Point(12, 9);
            this.PicBoxView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PicBoxView.Name = "PicBoxView";
            this.PicBoxView.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.PicBoxView.Size = new System.Drawing.Size(711, 508);
            this.PicBoxView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicBoxView.TabIndex = 3;
            this.PicBoxView.TabStop = false;
            // 
            // panelPictureBox
            // 
            this.panelPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPictureBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelPictureBox.Controls.Add(this.LabelError);
            this.panelPictureBox.Controls.Add(this.PicBoxView);
            this.panelPictureBox.Location = new System.Drawing.Point(23, 18);
            this.panelPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelPictureBox.Name = "panelPictureBox";
            this.panelPictureBox.Padding = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.panelPictureBox.Size = new System.Drawing.Size(735, 526);
            this.panelPictureBox.TabIndex = 4;
            // 
            // LabelError
            // 
            this.LabelError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelError.AutoSize = true;
            this.LabelError.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelError.Location = new System.Drawing.Point(26, 491);
            this.LabelError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelError.Name = "LabelError";
            this.LabelError.Size = new System.Drawing.Size(56, 17);
            this.LabelError.TabIndex = 5;
            this.LabelError.Text = "#error#";
            this.LabelError.Visible = false;
            // 
            // BtnExport
            // 
            this.BtnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnExport.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnExport.Enabled = false;
            this.BtnExport.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExport.Location = new System.Drawing.Point(23, 551);
            this.BtnExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(124, 25);
            this.BtnExport.TabIndex = 12;
            this.BtnExport.Text = "Export image";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExportClick);
            // 
            // BtnPrev
            // 
            this.BtnPrev.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnPrev.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnPrev.Enabled = false;
            this.BtnPrev.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrev.Location = new System.Drawing.Point(341, 551);
            this.BtnPrev.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnPrev.Name = "BtnPrev";
            this.BtnPrev.Size = new System.Drawing.Size(69, 25);
            this.BtnPrev.TabIndex = 13;
            this.BtnPrev.Text = "Previous";
            this.BtnPrev.UseVisualStyleBackColor = false;
            this.BtnPrev.Click += new System.EventHandler(this.BtnPrevClick);
            // 
            // BtnNext
            // 
            this.BtnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnNext.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnNext.Enabled = false;
            this.BtnNext.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNext.Location = new System.Drawing.Point(418, 551);
            this.BtnNext.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(69, 25);
            this.BtnNext.TabIndex = 14;
            this.BtnNext.Text = "Next";
            this.BtnNext.UseVisualStyleBackColor = false;
            this.BtnNext.Click += new System.EventHandler(this.BtnNextClick);
            // 
            // ViewImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnClose;
            this.ClientSize = new System.Drawing.Size(784, 586);
            this.Controls.Add(this.BtnNext);
            this.Controls.Add(this.BtnPrev);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.panelPictureBox);
            this.Controls.Add(this.BtnClose);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Name = "ViewImage";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "#ViewImage#";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewImageClosing);
            this.Load += new System.EventHandler(this.ViewImageLoad);
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxView)).EndInit();
            this.panelPictureBox.ResumeLayout(false);
            this.panelPictureBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.PictureBox PicBoxView;
        private System.Windows.Forms.Panel panelPictureBox;
        private System.Windows.Forms.Label LabelError;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Button BtnNext;
    }
}