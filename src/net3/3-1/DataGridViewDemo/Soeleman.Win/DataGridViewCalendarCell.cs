using System;
using System.Windows.Forms;

namespace Soeleman
{
    public class DataGridViewCalendarCell :
        DataGridViewTextBoxCell
    {
        public DataGridViewCalendarCell()
        {
            this.Style.Format = "d";
        }

        public override Type EditType => typeof(DataGridViewCalendarEditingControl);

        public override Type ValueType => typeof(DateTime);

        public override Object DefaultNewRowValue => DateTime.Now;

        public override void InitializeEditingControl(
            int rowIndex,
            object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            
            if (!(this.DataGridView.EditingControl is DataGridViewCalendarEditingControl ctl))
            {
                return;
            }

            if (this.Value == null)
            {
                var defaultNewRowValue = this.DefaultNewRowValue;

                if (defaultNewRowValue != null)
                {
                    ctl.Value = (DateTime)defaultNewRowValue;
                }
            }
            else
            {
                ctl.Value = (DateTime)this.Value;
            }
        }
    }
}
