using System.Globalization;
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

            for (var count = 0; count <= exten.Rows.Count - 1; count++)
            {
                exten.Rows[count].HeaderCell.Value = string.Format((count + 1).ToString(CultureInfo.InvariantCulture), "0");
            }
        }
    }
}
