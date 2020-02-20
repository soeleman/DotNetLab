using System;
using System.Data;

namespace Soeleman
{
    public static class DataRepo
    {
        public static DataTable DataSubNumber()
        {
            var decType = typeof(decimal);
            var dt = new DataTable();

            dt.Columns.AddRange(
                new[]
                {
                    new DataColumn("D1P", decType),
                    new DataColumn("D1S", decType),
                    new DataColumn("D2P", decType),
                    new DataColumn("D3P", decType)
                });

            dt.Columns.Add(
                new DataColumn(
                    "MajorMinor",
                    typeof(string),
                    "D1P + '|' + D1S"));

            dt.Rows.Add(21.3838m, 20.01m, 18.20m, 1.11m);
            dt.Rows.Add(0.8888m, 0.11m, 19.21m, 0.18m);
            dt.Rows.Add(22.2222m, 18.10m, 12.22m, 1.8m);
            dt.Rows.Add(38.2808m, 33.81m, 1.23m, 0.01m);
            dt.Rows.Add(1.2334m, 0.02m, 8.24m, 3.11m);

            return dt;
        }

        public static DataTable DataSubTotal()
        {
            var decType = typeof(decimal);
            var dt = new DataTable();

            dt.Columns.AddRange(
                new[]
                {
                    new DataColumn("Nilai1", decType),
                    new DataColumn("Nilai2", decType),
                    new DataColumn("Nilai3", decType)
                });

            dt.Rows.Add(1, 4, 2);
            dt.Rows.Add(2, 9, 7);

            return dt;
        }
    }
}
