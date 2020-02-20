using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soeleman.WinApp
{
    public partial class FormSubNumber :
        Form
    {
        public FormSubNumber()
        {
            this.InitializeComponent();
            this.InitGrid();
        }

        private void InitGrid()
        {
            this.dataGridView1.AutoGenerateColumns = false;

            this.dataGridView1.Columns.AddRange(
                new DataGridViewTextBoxMajorMinorColumn
                {
                    DataPropertyName = "MajorMinor",
                    HeaderText = @"Data No 1"
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "D2P",
                    HeaderText = @"Data No 2"
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "D3P",
                    HeaderText = @"Data No 3"
                });

            this.dataGridView1.DataSource = DataRepo.DataSubNumber();
        }
    }
}
