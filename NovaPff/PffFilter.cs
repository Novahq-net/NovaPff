using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

// NHQTools Libraries
using NHQTools.FileFormats;
using NHQTools.FileFormats.Pff;

namespace NovaPff
{
    internal class PffFilter
    {
        public string TypeStr { get; }
        public object TypeVal { get; }  // null = All, FileType = type filter, string = extension filter
        public bool GroupName { get; }

        private PffFilter(string typeStr, object typeVal, bool groupName)
        {
            TypeStr = typeStr;
            TypeVal = typeVal;
            GroupName = groupName;
        }

        public static PffFilter All() => new PffFilter("All", null, false);
        public static PffFilter GroupHeader(string name) => new PffFilter(name, null, true);
        public static PffFilter FileMagic(FileType type) => new PffFilter(type.ToString(), type, false);
        public static PffFilter FileExt(string ext) => new PffFilter(ext.ToUpperInvariant(), ext, false);

        public override string ToString() => TypeStr;

        //////////////////////////////////////////////////////////////////////////////////////
        #region ComboBox Helpers

        public static void BuildList(ComboBox filterMagic, PffFile pff)
        {
            var items = new List<PffFilter> { All() };

            // Detected Types
            var magicList = pff.DistinctFileTypes;
            if (magicList != null && magicList.Count > 0)
            {
                items.Add(GroupHeader("── Detected Types ──"));

                // Pin Unknown
                if (magicList.Contains(FileType.Unknown))
                    items.Add(FileMagic(FileType.Unknown));

                foreach (var ft in magicList.Where(t => t != FileType.Unknown).OrderBy(t => t.ToString()))
                    items.Add(FileMagic(ft));
            }

            // Extensions
            var extList = pff.DistinctFileTypeExtensions;
            if (extList != null && extList.Count > 0)
            {
                items.Add(GroupHeader("── Extensions ──"));

                foreach (var ext in extList.OrderBy(e => e))
                    items.Add(FileExt(ext));

            }

            filterMagic.DataSource = items;
            filterMagic.SelectedIndex = 0;
        }

        //////////////////////////////////////////////////////////////////////////////////////
        public static void DrawItem(object sender, DrawItemEventArgs e, Font boldFont)
        {
            if (e.Index < 0)
                return;

            var combo = (ComboBox)sender;
            if (!(combo.Items[e.Index] is PffFilter item))
                return;

            // Removes the bright blue highlight on focused item after selection
            var isDropDownItem = (e.State & DrawItemState.ComboBoxEdit) == 0;

            if (isDropDownItem && !item.GroupName)
                e.DrawBackground();
            else
                using (var bgBrush = new SolidBrush(combo.BackColor))
                    e.Graphics.FillRectangle(bgBrush, e.Bounds);

            var font = item.GroupName ? boldFont : e.Font;
            var color = item.GroupName ? SystemColors.GrayText : isDropDownItem ? e.ForeColor : combo.ForeColor;
            var padding = item.GroupName ? 0 : 6; // left padding

            var bounds = new RectangleF(e.Bounds.X + padding, e.Bounds.Y, e.Bounds.Width - padding, e.Bounds.Height);

            using (var brush = new SolidBrush(color))
            using (var sf = new StringFormat())
            {
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(item.TypeStr, font, brush, bounds, sf);
            }

            if (isDropDownItem && (e.State & DrawItemState.Focus) != 0 && !item.GroupName)
                e.DrawFocusRectangle();
        }

        #endregion

    }

}