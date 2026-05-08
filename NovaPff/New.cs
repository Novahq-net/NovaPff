using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

// NHQTools Libraries
using NHQTools.Themes;
using NHQTools.FileFormats.Pff;

namespace NovaPff
{
    public partial class New : BaseFormTheme
    {

        public PffGameInfo SelectedGame { get; private set; }

        public New() => InitializeComponent();

        //////////////////////////////////////////////////////////////////////////////////////
        private void NewLoad(object sender, EventArgs e)
        {
            GameSelect.DataSource = PffGames.All.OrderBy(g => g.GameName).ToList();
            GameSelect.DisplayMember = "GameNameExt"; // Must match the Property Name in PffGames
            GameSelect.DrawItem += GameSelectDrawItem;
        }

        //////////////////////////////////////////////////////////////////////////////////////
        private static void GameSelectDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            if (!(sender is ComboBox combo))
                return;

            if (!(combo.Items[e.Index] is PffGameInfo item))
                return;

            // Only draw the selection highlight for items in the dropdown list, not when closed but focused
            var isDropDownItem = (e.State & DrawItemState.ComboBoxEdit) == 0;

            if (isDropDownItem)
                e.DrawBackground();
            else
                // Draw normal background for the closed combo display
                using (var bgBrush = new SolidBrush(combo.BackColor))
                    e.Graphics.FillRectangle(bgBrush, e.Bounds);

            var textLeft = item.GameName;
            var textRight = $"(PFF{item.VersionNumber}-{item.EntryRecordLength})";

            // Use dropdown ForeColor for the static area
            // Otherwise use e.ForeColor for dropdown items
            var textColor = isDropDownItem ? e.ForeColor : combo.ForeColor;

            using (var brush = new SolidBrush(textColor))
            using (var formatLeft = new StringFormat())
            {
                formatLeft.Alignment = StringAlignment.Near;
                formatLeft.LineAlignment = StringAlignment.Center;

                using (var formatRight = new StringFormat())
                {
                    formatRight.Alignment = StringAlignment.Far;
                    formatRight.LineAlignment = StringAlignment.Center;

                    // Draw Left text with padding so it doesn't touch the left border
                    var rectLeft = new RectangleF(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                    e.Graphics.DrawString(textLeft, e.Font, brush, rectLeft, formatLeft);

                    // Draw Right text with padding so it doesn't touch the right border
                    var rectRight = new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 8, e.Bounds.Height);
                    e.Graphics.DrawString(textRight, e.Font, brush, rectRight, formatRight);

                }

            }

            // Draw the focus rectangle ONLY if the item has focus in selection mode
            if (isDropDownItem)
                e.DrawFocusRectangle();

        }

        //////////////////////////////////////////////////////////////////////////////////////
        private void CancelClick(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        //////////////////////////////////////////////////////////////////////////////////////
        private void OkClick(object sender, EventArgs e) => SelectedGame = GameSelect.SelectedItem as PffGameInfo;
    }

}