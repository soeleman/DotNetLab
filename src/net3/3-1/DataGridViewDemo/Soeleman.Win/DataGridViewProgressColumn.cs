using System.Windows.Forms;

namespace Soeleman
{
    public sealed class DataGridViewProgressColumn :
        DataGridViewImageColumn
    {
        public DataGridViewProgressColumn()
        {
            this.CellTemplate = new DataGridViewProgressCell();
        }
    }
}
