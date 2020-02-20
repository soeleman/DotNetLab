using System.Collections;
using System.Collections.Generic;

namespace Soeleman
{
    public sealed class GenericResult<T> :
        DataResult
        where T : class
    {
        public GenericResult()
        {
            this.Records = new List<T>();
        }

        public List<T> Records { get; set; }

        protected override IList MyList()
        {
            return this.Records;
        }
    }
}