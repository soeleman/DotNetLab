using System;
using System.Windows.Forms;

namespace Soeleman
{
    public static class DataGridViewExtension
    {
        public static void AutoNumberRowsForGridView(
            this DataGridView exten)
        {
            if (exten == null)
            {
                return;
            }

            var totalDigit = Math.Floor(Math.Log10(exten.Rows.Count) + 1);
            var stringFormat = $"D{totalDigit}";

            for (var count = 0; count <= exten.Rows.Count - 1; count++)
            {
                exten.Rows[count].HeaderCell.Value = (count + 1).ToString(stringFormat);
            }

            //exten.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }
    }
}
