using System;
using System.Windows.Forms;

// NHQTools Libraries
using NHQTools.Themes;

namespace NovaPff
{
    public partial class Settings : BaseFormTheme
    {
        private readonly AppSettings _settings;

        ////////////////////////////////////////////////////////////////////////////////////
        public Settings(AppSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        ////////////////////////////////////////////////////////////////////////////////////
        private void SettingsLoad(object sender, EventArgs e)
        {
            SelectTheme.DataSource = Enum.GetValues(typeof(ThemeManager.Themes));
            SelectTheme.SelectedItem = ThemeManager.CurrentTheme;

            CheckBoxShowDeadSpaceEntries.Checked = _settings.ShowDeadSpaceEntries;
            CheckBoxShowEpochTimestamps.Checked = _settings.ShowEpochTimestamp;
            CheckBoxShowFileSizeInBytes.Checked = _settings.ShowFileSizeInBytes;
        }

        ////////////////////////////////////////////////////////////////////////////////////
        private void SaveClick(object sender, EventArgs e)
        {

            _settings.ShowDeadSpaceEntries = CheckBoxShowDeadSpaceEntries.Checked;
            _settings.ShowEpochTimestamp = CheckBoxShowEpochTimestamps.Checked;
            _settings.ShowFileSizeInBytes = CheckBoxShowFileSizeInBytes.Checked;

            if (SelectTheme.SelectedItem is ThemeManager.Themes selectedTheme)
                _settings.Theme = selectedTheme.ToString();

            if (CheckBoxResetDialogs.Checked)
            {
                _settings.ImportOverwriteSuppress = false;
                _settings.ImportResultSuppress = false;
                _settings.ExportOverwriteSuppress = false;
                _settings.ExportResultSuppress = false;
                _settings.PreserveDeadSpaceSuppress = false;
                _settings.ExportMenuSingleSuppress = false;
            }

            DialogResult = DialogResult.OK;
        }

        ////////////////////////////////////////////////////////////////////////////////////
        private void CancelClick(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

    }

}
