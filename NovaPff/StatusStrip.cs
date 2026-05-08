using System.Windows.Forms;

// NHQTools Libraries
using NHQTools.Helpers;

namespace NovaPff
{
    public class MainStatusStrip : StatusStripGroup
    {
        // Magic strings are bad
        public BoundStatusLabel ProgressBar { get; }
        public BoundStatusLabel FileName { get; }
        public BoundStatusLabel Version { get; }
        public BoundStatusLabel Size { get; }
        public BoundStatusLabel Entries { get; }
        public BoundStatusLabel DeadSpace { get; }
        public BoundStatusLabel DeadSpaceToggle { get; }

        // Binds UI elements to a group and sets up default states for each element
        public MainStatusStrip(
            ToolStripStatusLabel progressBar,
            ToolStripStatusLabel fileName,
            ToolStripStatusLabel version,
            ToolStripStatusLabel size,
            ToolStripStatusLabel entries,
            ToolStripStatusLabel deadSpace,
            ToolStripStatusLabel deadSpaceToggle)
        {
            ProgressBar = Bind(progressBar);
            FileName = Bind(fileName, prefix: "", "Ready...");
            Version = Bind(version, prefix: "Version: ", defaultValue: "--");
            Size = Bind(size, prefix: "Size: ", defaultValue: "--");
            Entries = Bind(entries, prefix: "Entries: ", defaultValue: "--");
            DeadSpace = Bind(deadSpace, prefix: "Dead Space: ", defaultValue: "--");
            DeadSpaceToggle = Bind(deadSpaceToggle, prefix: "", defaultValue: "👁");

        }

    }

}