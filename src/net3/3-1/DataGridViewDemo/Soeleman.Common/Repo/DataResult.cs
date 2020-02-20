using System.Collections;

namespace Soeleman
{
    public abstract class DataResult
    {
        protected DataResult()
        {
            this.TotalRecord = 0;
        }

        public long TotalRecord { get; set; }

        public IList GetList()
        {
            return this.MyList();
        }

        protected abstract IList MyList();
    }
}
