using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

// NHQTools Libraries
using NHQTools.Themes;

namespace NovaPff
{
    public class AppSettings : INotifyPropertyChanged
    {
        // INotifyPropertyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;

        // PropertyChanged event
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // This config file
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".cfg");

        ////////////////////////////////////////////////////////////////////////////////////
        #region Persisted Settings

        // Folder paths
        public string LastOpenDirectory { get; set; } = string.Empty;
        public string LastSaveDirectory { get; set; } = string.Empty;
        public string LastImportDirectory { get; set; } = string.Empty;
        public string LastExportDirectory { get; set; } = string.Empty;
        public string LastExportImageDirectory { get; set; } = string.Empty;
        public string LastExportTextDirectory { get; set; } = string.Empty;

        // Misc
        public bool PreserveDeadSpace { get; set; }
        public bool ExportOverwriteExisting { get; set; }
        public bool ExportPakAlphaPng { get; set; }
        public bool ImportOverwriteExisting { get; set; }

        // Suppress
        public bool PreserveDeadSpaceSuppress { get; set; }
        public bool ExportOverwriteSuppress { get; set; }
        public bool ExportResultSuppress { get; set; }
        public bool ExportMenuSingleSuppress { get; set; }
        public bool ExportPakAlphaPngSuppress { get; set; }
        public bool ImportOverwriteSuppress { get; set; }
        public bool ImportResultSuppress { get; set; }


        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        #region Settings with change notification
        private bool _showDeadSpaceEntries;
        public bool ShowDeadSpaceEntries
        {
            get => _showDeadSpaceEntries;
            set => SetField(ref _showDeadSpaceEntries, value);
        }

        ////////////////////////////////////////////////////////////////////////////////////
        private bool _showFileSizeInBytes;
        public bool ShowFileSizeInBytes
        {
            get => _showFileSizeInBytes;
            set => SetField(ref _showFileSizeInBytes, value);
        }

        ////////////////////////////////////////////////////////////////////////////////////
        private bool _showEpochTimestamp;
        public bool ShowEpochTimestamp
        {
            get => _showEpochTimestamp;
            set => SetField(ref _showEpochTimestamp, value);
        }

        ////////////////////////////////////////////////////////////////////////////////////
        private string _theme = nameof(ThemeManager.Themes.SystemDefault);
        public string Theme
        {
            get => _theme;
            set
            {
                // *** Intentionally skip equality check ***
                // SystemDefault theme must re-fire when the OS theme changes
                _theme = value;
                OnPropertyChanged();
            }

        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        #region Resolve Helpers

        // If setting was previously suppressed, use the value saved in config.
        // Otherwise, return false so we can prompt the user.
        public static bool Get(bool suppress, bool savedSetting, out bool resolved)
        {
            if (suppress)
            {
                resolved = savedSetting;
                return true;
            }

            resolved = false;
            return false;
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////
        public static AppSettings Load()
        {
            if (!File.Exists(FilePath))
                return new AppSettings();

            try
            {
                using (var fs = File.OpenRead(FilePath))
                {
                    var xs = new XmlSerializer(typeof(AppSettings));
                    return (AppSettings)xs.Deserialize(fs);
                }
            }
            catch
            {
                return new AppSettings(); // Fall back
            }

        }

        ////////////////////////////////////////////////////////////////////////////////////
        public static void Save(AppSettings settings)
        {
            try
            {
                var tempPath = FilePath + ".tmp";

                using (var fs = File.Create(tempPath))
                {
                    var xs = new XmlSerializer(typeof(AppSettings));
                    xs.Serialize(fs, settings);
                }

                // Write to temp file first and then move to final location to
                // avoid leaving a corrupted save file if something goes wrong
                File.Copy(tempPath, FilePath, true);
                File.Delete(tempPath);

            } catch { /* Ignore */ }

        }

        ////////////////////////////////////////////////////////////////////////////////////
        private void SetField(ref bool field, bool value, [CallerMemberName] string propertyName = null)
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged(propertyName);
        }

    }

}