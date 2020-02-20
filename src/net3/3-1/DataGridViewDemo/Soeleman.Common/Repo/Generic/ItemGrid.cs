using System;

namespace Soeleman
{
    public sealed class ItemGrid
    {
        public Guid Id { get; set; }

        public String ItemString { get; set; }

        public Int32 ItemInt32 { get; set; }

        public Boolean ItemBoolean { get; set; }

        public Decimal ItemDecimal { get; set; }

        public DateTime ItemDateTime { get; set; }
    }
}