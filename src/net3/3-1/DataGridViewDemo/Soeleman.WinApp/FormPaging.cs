using System;
using System.Globalization;
using System.Windows.Forms;

namespace Soeleman.WinApp
{
    public partial class FormPaging :
        Form
    {
        private const int PageSize = 10;
        private const int BeginCurrentPage = 0;

        private readonly DataRepository repository = new DataRepository(new GenericRepo());

        public FormPaging()
        {
            this.InitializeComponent();

            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.BindingSource = this.bindingSource1;

            InitDataGridView(this.dataGridView1);

            this.BindDataGridView();

            this.bindingNavigatorDeleteItem.Enabled = false;
            this.bindingNavigatorDeleteItem.Visible = false;

            this.bindingNavigatorAddNewItem.Enabled = true;
            this.bindingNavigatorAddNewItem.Click += (s, a) =>
            {
                repository.AddNewData();
                this.BindDataGridView();
            };
        }

        private static void InitDataGridView(
            DataGridView exten)
        {
            var decimalValue = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ItemDecimal",
                HeaderText = "Decimal",
                DefaultCellStyle =
                {
                    Format = "N0",
                    FormatProvider = new CultureInfo("en-us")
                }
            };

            exten.Columns.Clear();

            exten.AllowUserToAddRows = false;
            exten.AutoGenerateColumns = false;

            exten.RowHeadersVisible = true;
            exten.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            //exten.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            exten.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", Visible = false });
            exten.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ItemString", HeaderText = "String" });
            exten.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ItemInt32", HeaderText = "Int32" });
            exten.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "ItemBoolean", HeaderText = "Boolean" });

            exten.Columns.Add(decimalValue);

            //exten.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ItemDateTime", HeaderText = "DateTime" });
            exten.Columns.Add(new DataGridViewCalendarColumn { DataPropertyName = "ItemDateTime", HeaderText = "DateTime" });

            exten.DataBindingComplete += (s, e) => exten.AutoNumberRowsForGridView();
        }

        private void BindingSourceCurrentChanged(
            object sender,
            EventArgs e)
        {
            this.BindDataGridView();
        }

        private void BindDataGridView()
        {
            var pageItem = this.bindingSource1.Current as PageItem;
            var pageList = this.bindingSource1.DataSource as PageList;

            var dataResult = pageItem == null
                ? this.repository.GetData(PageSize, BeginCurrentPage)
                : this.repository.GetData(pageItem.PageSize, pageItem.CurrentPage);

            if ((pageList != null && pageList.TotalRecord != dataResult.TotalRecord) ||
                (pageItem == null))
            {
                this.bindingSource1.CurrentChanged -= this.BindingSourceCurrentChanged;
                this.bindingSource1.DataSource = new PageList(PageSize, dataResult.TotalRecord);
                this.bindingSource1.CurrentChanged += this.BindingSourceCurrentChanged;
            }

            this.dataGridView1.DataSource = dataResult.GetList();
        }
    }
}

//private void InitDataGridView()
//{
//    this.dataGridView1.Columns.Clear();
//    //this.dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
//    this.dataGridView1.RowHeadersVisible = true;
//    this.dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
//    this.dataGridView1.AllowUserToAddRows = false;
//    this.dataGridView1.AutoGenerateColumns = false;
//    this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", Visible = false });
//    this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ItemString", HeaderText = "String" });
//    this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ItemInt32", HeaderText = "Int32" });
//    this.dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "ItemBoolean", HeaderText = "Boolean" });
//    var decimalValue =
//        new DataGridViewTextBoxColumn
//        {
//            DataPropertyName = "ItemDecimal",
//            HeaderText = "Decimal",
//            DefaultCellStyle =
//                {
//                    Format = "N0",
//                    FormatProvider = new CultureInfo("en-us")
//                }
//        };
//    this.dataGridView1.Columns.Add(decimalValue);
//    //this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ItemDateTime", HeaderText = "DateTime" });
//    this.dataGridView1.Columns.Add(new DataGridViewCalendarColumn { DataPropertyName = "ItemDateTime", HeaderText = "DateTime" });
//    //this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
//    this.dataGridView1.DataBindingComplete += (s, e) => this.dataGridView1.AutoNumberRowsForGridView();
//    //this.dataGridView1.DataBindingComplete += (s, e) =>
//    //{
//    //    this.dataGridView1.AutoNumberRowsForGridView();
//    //    //var totalRecord = this.dataGridView1.Rows.Count;
//    //    //var totalDigit = Math.Floor(Math.Log10(totalRecord) + 1);
//    //    //var stringFormat = $"D{totalDigit}";
//    //    //for (var count = 0; count <= totalRecord - 1; count++)
//    //    //{
//    //    //    this.dataGridView1.Rows[count].HeaderCell.Value = (count + 1).ToString(stringFormat);
//    //    //}
//    //    //this.dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
//    //};
//    //this.dataGridView1.CellFormatting += (s, e) =>
//    //{
//    //    var idx = e.RowIndex;
//    //    var row = dataGridView1.Rows[idx];
//    //    var totalDigit = Math.Floor(Math.Log10(this.dataGridView1.Rows.Count) + 1);
//    //    var stringFormat = $"D{totalDigit}";
//    //    row.HeaderCell.Value = (idx + 1).ToString(stringFormat);
//    //    row.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
//    //    //int idx = e.RowIndex;
//    //    //DataGridViewRow row = dataGridView1.Rows[idx];
//    //    //long newNo = idx;
//    //    //if (!_RowNumberStartFromZero)
//    //    //{
//    //    //    newNo += 1;
//    //    //}
//    //    //long oldNo = -1;
//    //    //if (row.HeaderCell.Value != null)
//    //    //{
//    //    //    if (IsNumeric(row.HeaderCell.Value))
//    //    //    {
//    //    //        oldNo = System.Convert.ToInt64(row.HeaderCell.Value);
//    //    //    }
//    //    //}
//    //    //if (newNo != oldNo)
//    //    //{
//    //    //    row.HeaderCell.Value = newNo.ToString();
//    //    //    row.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
//    //    //}
//    //    //this.dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
//    //};
//}
