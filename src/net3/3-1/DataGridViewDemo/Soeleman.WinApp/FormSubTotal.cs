using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Soeleman.WinApp
{
    public partial class FormSubTotal :
        Form
    {
        private readonly Type decType = typeof(decimal);

        public FormSubTotal()
        {
            this.InitializeComponent();

            this.InitGrid1();

            var dt = DataRepo.DataSubTotal();

            this.dataGridView1.DataSource = dt;

            this.InitGrid2(dt.Copy());

            this.dataGridView1.CellClick += (g, e) =>
            {
                var row = this.dataGridView1.Rows[e.RowIndex];
                var lastColumnIndex = this.dataGridView1.ColumnCount - 1;

                row.Cells[lastColumnIndex].Value = row
                    .Cells
                    .Cast<DataGridViewCell>()
                    .Select((s, i) => i != lastColumnIndex ? s.Value : decimal.Zero)
                    .Sum(s => (decimal)s);
            };

            this.dataGridView1.SelectionChanged += (s, e) =>
            {
                var row = this.dataGridView1.CurrentRow;

                if (row == null)
                {
                    return;
                }
                
                var subTotal =
                    (decimal) row.Cells["Nilai1"].Value +
                    (decimal) row.Cells["Nilai2"].Value +
                    (decimal) row.Cells["Nilai3"].Value;

                this.labelSelectedRow.Text = $@"Selected Row Total: {subTotal.ToString(CultureInfo.InvariantCulture)}";
            };
        }

        private void InitGrid1()
        {
            this.dataGridView1.AutoGenerateColumns = false;

            for (var i = 1; i < 4; i++)
            {
                var nameCol = "Nilai" + i;

                this.dataGridView1.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = nameCol,
                        Name = nameCol,
                        HeaderText = nameCol
                    });
            }

            this.dataGridView1.Columns
                .Add("SubTotal", "Sub Total");
        }

        private void InitGrid2(DataTable dt)
        {
            var columns = string.Join(
                " + ",
                dt.Columns
                    .Cast<DataColumn>()
                    .Select(s => s.ColumnName));

            dt.Columns.Add(
                new DataColumn("Hasil", this.decType, columns));

            this.dataGridView2.DataSource = dt;

            //var total = this.dataGridView2
            //    .Rows
            //    .Cast<DataGridViewRow>()
            //    .Sum(s =>
            //        (decimal) s.Cells["Nilai1"].Value +
            //        (decimal) s.Cells["Nilai2"].Value +
            //        (decimal) s.Cells["Nilai3"].Value);

            //var total = dt.Compute("SUM(Hasil)", string.Empty);

            var total = dt
                .AsEnumerable()
                .Sum(s => s.Field<decimal>("Hasil"));

            this.labelTotalRow.Text = $@"Total: {total.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
