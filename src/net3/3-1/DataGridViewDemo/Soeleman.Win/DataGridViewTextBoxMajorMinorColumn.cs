using System.Windows.Forms;

namespace Soeleman
{
    public sealed class DataGridViewTextBoxMajorMinorColumn
        : DataGridViewImageColumn
    {
        public DataGridViewTextBoxMajorMinorColumn()
        {
            this.CellTemplate = new DataGridViewTextBoxMajorMinorCell();
        }
    }
}
