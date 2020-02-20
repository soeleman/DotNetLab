using System.Collections;
using System.ComponentModel;
using System.Data;

namespace Soeleman
{
    public sealed class DataTableResult :
        DataResult
    {
        public DataTableResult()
        {
            this.Records = new DataTable();
        }

        public DataTable Records { get; set; }

        protected override IList MyList()
        {
            return ((IListSource)this.Records).GetList();
        }
    }
}