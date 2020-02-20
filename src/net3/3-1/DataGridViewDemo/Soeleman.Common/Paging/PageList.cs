using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace Soeleman
{
    public partial class PageList :
        IListSource
    {
        private readonly int pageSize;

        public PageList(
            int pageSize,
            long totalRecord)
        {
            this.TotalRecord = totalRecord;
            this.pageSize = pageSize;
        }

        public long TotalRecord { get; set; }

        public bool ContainsListCollection { get; protected set; }

        public IList GetList()
        {
            return Enumerable
                .Range(0, (int)Math.Ceiling((double)this.TotalRecord / this.pageSize))
                .Select(s => new PageItem(this.pageSize, s * this.pageSize))
                .ToList();
        }
    }
}