using System;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Soeleman
{
    public sealed class DataTableRepo :
        IDataRepository
    {
        private static DataTable gridDataTable = new DataTable();

        public DataResult GetData(
            int pageSize,
            int currentPage)
        {
            InitGridDataTable();

            return new DataTableResult
            {
                Records = gridDataTable
                    .AsEnumerable()
                    .Skip(currentPage)
                    .Take(pageSize)
                    .CopyToDataTable(),
                TotalRecord = gridDataTable.Rows.Count
            };
        }

        public void AddNewData()
        {
            var rnd = new Random(100000);
            var dat = DateTime.Now;
            var itemRandom = rnd.Next();
            var mod = itemRandom % 2;
            var dec = itemRandom * 0.2M;

            dat = dat.AddDays(0 - itemRandom / (int)dec);

            gridDataTable.Rows.Add(
                    Guid.NewGuid(),
                    itemRandom.ToString(CultureInfo.InvariantCulture),
                    rnd.Next(),
                    mod == 0,
                    dec,
                    dat);
        }

        private static void InitGridDataTable()
        {
            if (!gridDataTable.Rows.Count.Equals(0))
            {
                return;
            }

            gridDataTable.Rows.Clear();
            gridDataTable = GenerateGridDataTable();
        }

        private static DataTable GenerateGridDataTable()
        {
            var rnd = new Random(100000);
            var dat = DateTime.Now;

            var dt = new DataTable();

            dt.Columns.Add(@"Id", typeof(Guid));
            dt.Columns.Add(@"ItemString", typeof(string));
            dt.Columns.Add(@"ItemInt32", typeof(int));
            dt.Columns.Add(@"ItemBoolean", typeof(bool));
            dt.Columns.Add(@"ItemDecimal", typeof(decimal));
            dt.Columns.Add(@"ItemDateTime", typeof(DateTime));

            for (var i = 0; i < 1000; i++)
            {
                var itemRandom = rnd.Next();

                var mod = itemRandom % 2;
                var dec = itemRandom * 0.2M;
                dat = dat.AddDays(0 - (itemRandom / (int)dec));

                dt.Rows.Add(
                    Guid.NewGuid(),
                    itemRandom.ToString(CultureInfo.InvariantCulture),
                    i,
                    mod == 0,
                    dec,
                    dat);
            }

            return dt;
        }
    }
}