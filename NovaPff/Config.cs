using System;
using System.Reflection;

namespace NovaPff
{
    public static class Config
    {
        public static readonly string AppName;
        public static readonly string AppVersion;
        public static readonly string AppUrl;
        public static readonly string AppUrlSuffix;
        public static readonly string BuildDate;

        static Config()
        {
            var assembly = typeof(Config).Assembly;
            var assemblyName = assembly.GetName();

            var titleAttr = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                assembly, typeof(AssemblyTitleAttribute));

            AppName = titleAttr?.Title ?? assemblyName.Name;
            AppVersion = $"{assemblyName.Version.Major}.{assemblyName.Version.Minor}.{assemblyName.Version.Build}";
            AppUrl = "https://novahq.net/";
            BuildDate = Properties.Resources.BuildDate?.Trim();
            AppUrlSuffix = $"{AppUrl}?app={Uri.EscapeDataString(AppName.Replace(" ", ""))}-v{AppVersion}b{BuildDate}";

        }

    }

}