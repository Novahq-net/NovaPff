namespace NovaPff
{
    partial class Settings
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
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.CheckBoxShowDeadSpaceEntries = new System.Windows.Forms.CheckBox();
            this.CheckBoxShowFileSizeInBytes = new System.Windows.Forms.CheckBox();
            this.CheckBoxShowEpochTimestamps = new System.Windows.Forms.CheckBox();
            this.CheckBoxResetDialogs = new System.Windows.Forms.CheckBox();
            this.GroupDisplay = new System.Windows.Forms.GroupBox();
            this.GroupTheme = new System.Windows.Forms.GroupBox();
            this.SelectTheme = new System.Windows.Forms.ComboBox();
            this.GroupDisplay.SuspendLayout();
            this.GroupTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnSave.Location = new System.Drawing.Point(157, 202);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 25);
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.SaveClick);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnCancel.Location = new System.Drawing.Point(240, 202);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 25);
            this.BtnCancel.TabIndex = 4;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // CheckBoxShowDeadSpaceEntries
            // 
            this.CheckBoxShowDeadSpaceEntries.AutoSize = true;
            this.CheckBoxShowDeadSpaceEntries.BackColor = System.Drawing.SystemColors.Control;
            this.CheckBoxShowDeadSpaceEntries.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CheckBoxShowDeadSpaceEntries.Location = new System.Drawing.Point(21, 26);
            this.CheckBoxShowDeadSpaceEntries.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CheckBoxShowDeadSpaceEntries.Name = "CheckBoxShowDeadSpaceEntries";
            this.CheckBoxShowDeadSpaceEntries.Size = new System.Drawing.Size(175, 16);
            this.CheckBoxShowDeadSpaceEntries.TabIndex = 0;
            this.CheckBoxShowDeadSpaceEntries.Text = "Display Dead Space Entires";
            this.CheckBoxShowDeadSpaceEntries.UseVisualStyleBackColor = false;
            // 
            // CheckBoxShowFileSizeInBytes
            // 
            this.CheckBoxShowFileSizeInBytes.AutoSize = true;
            this.CheckBoxShowFileSizeInBytes.BackColor = System.Drawing.SystemColors.Control;
            this.CheckBoxShowFileSizeInBytes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CheckBoxShowFileSizeInBytes.Location = new System.Drawing.Point(21, 47);
            this.CheckBoxShowFileSizeInBytes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CheckBoxShowFileSizeInBytes.Name = "CheckBoxShowFileSizeInBytes";
            this.CheckBoxShowFileSizeInBytes.Size = new System.Drawing.Size(162, 16);
            this.CheckBoxShowFileSizeInBytes.TabIndex = 1;
            this.CheckBoxShowFileSizeInBytes.Text = "Display File Size in Bytes";
            this.CheckBoxShowFileSizeInBytes.UseVisualStyleBackColor = false;
            // 
            // CheckBoxShowEpochTimestamps
            // 
            this.CheckBoxShowEpochTimestamps.AutoSize = true;
            this.CheckBoxShowEpochTimestamps.BackColor = System.Drawing.SystemColors.Control;
            this.CheckBoxShowEpochTimestamps.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CheckBoxShowEpochTimestamps.Location = new System.Drawing.Point(21, 68);
            this.CheckBoxShowEpochTimestamps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CheckBoxShowEpochTimestamps.Name = "CheckBoxShowEpochTimestamps";
            this.CheckBoxShowEpochTimestamps.Size = new System.Drawing.Size(167, 16);
            this.CheckBoxShowEpochTimestamps.TabIndex = 2;
            this.CheckBoxShowEpochTimestamps.Text = "Display Epoch Timestamp";
            this.CheckBoxShowEpochTimestamps.UseVisualStyleBackColor = false;
            // 
            // CheckBoxResetDialogs
            // 
            this.CheckBoxResetDialogs.AutoSize = true;
            this.CheckBoxResetDialogs.BackColor = System.Drawing.SystemColors.Control;
            this.CheckBoxResetDialogs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CheckBoxResetDialogs.Location = new System.Drawing.Point(14, 175);
            this.CheckBoxResetDialogs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CheckBoxResetDialogs.Name = "CheckBoxResetDialogs";
            this.CheckBoxResetDialogs.Size = new System.Drawing.Size(209, 16);
            this.CheckBoxResetDialogs.TabIndex = 2;
            this.CheckBoxResetDialogs.Text = "Reset \"Don\'t show again\" dialogs";
            this.CheckBoxResetDialogs.UseVisualStyleBackColor = false;
            // 
            // GroupDisplay
            // 
            this.GroupDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.GroupDisplay.Controls.Add(this.CheckBoxShowDeadSpaceEntries);
            this.GroupDisplay.Controls.Add(this.CheckBoxShowEpochTimestamps);
            this.GroupDisplay.Controls.Add(this.CheckBoxShowFileSizeInBytes);
            this.GroupDisplay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GroupDisplay.Location = new System.Drawing.Point(14, 70);
            this.GroupDisplay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupDisplay.Name = "GroupDisplay";
            this.GroupDisplay.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupDisplay.Size = new System.Drawing.Size(298, 99);
            this.GroupDisplay.TabIndex = 1;
            this.GroupDisplay.TabStop = false;
            this.GroupDisplay.Text = "PFF Files";
            // 
            // GroupTheme
            // 
            this.GroupTheme.BackColor = System.Drawing.SystemColors.Control;
            this.GroupTheme.Controls.Add(this.SelectTheme);
            this.GroupTheme.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GroupTheme.Location = new System.Drawing.Point(14, 11);
            this.GroupTheme.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupTheme.Name = "GroupTheme";
            this.GroupTheme.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupTheme.Size = new System.Drawing.Size(298, 54);
            this.GroupTheme.TabIndex = 0;
            this.GroupTheme.TabStop = false;
            this.GroupTheme.Text = "Theme";
            // 
            // SelectTheme
            // 
            this.SelectTheme.BackColor = System.Drawing.SystemColors.Window;
            this.SelectTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectTheme.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SelectTheme.FormattingEnabled = true;
            this.SelectTheme.Location = new System.Drawing.Point(20, 21);
            this.SelectTheme.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectTheme.Name = "SelectTheme";
            this.SelectTheme.Size = new System.Drawing.Size(140, 20);
            this.SelectTheme.TabIndex = 0;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(328, 239);
            this.Controls.Add(this.GroupTheme);
            this.Controls.Add(this.GroupDisplay);
            this.Controls.Add(this.CheckBoxResetDialogs);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnSave);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsLoad);
            this.GroupDisplay.ResumeLayout(false);
            this.GroupDisplay.PerformLayout();
            this.GroupTheme.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.CheckBox CheckBoxShowDeadSpaceEntries;
        private System.Windows.Forms.CheckBox CheckBoxShowFileSizeInBytes;
        private System.Windows.Forms.CheckBox CheckBoxShowEpochTimestamps;
        private System.Windows.Forms.CheckBox CheckBoxResetDialogs;
        private System.Windows.Forms.GroupBox GroupDisplay;
        private System.Windows.Forms.GroupBox GroupTheme;
        private System.Windows.Forms.ComboBox SelectTheme;
    }
}