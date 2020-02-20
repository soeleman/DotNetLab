using System;
using System.Windows.Forms;

namespace Soeleman
{
    public sealed class DataGridViewCalendarEditingControl :
        DateTimePicker,
        IDataGridViewEditingControl
    {
        private bool valueChanged;

        public DataGridViewCalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Short;
        }

        public bool RepositionEditingControlOnValueChange => false;

        public Cursor EditingPanelCursor => this.Cursor;

        public int EditingControlRowIndex { get; set; }

        public DataGridView EditingControlDataGridView { get; set; }

        public bool EditingControlValueChanged
        {
            get => this.valueChanged;

            set => this.valueChanged = value;
        }

        public object EditingControlFormattedValue
        {
            get => this.Value.ToShortDateString();

            set
            {
                var s = value as string;

                if (s == null)
                {
                    return;
                }

                try
                {
                    this.Value = DateTime.Parse(s);
                }
                catch
                {
                    this.Value = DateTime.Now;
                }
            }
        }

        public Object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return this.EditingControlFormattedValue;
        }

        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        public bool EditingControlWantsInputKey(
            Keys key,
            bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed. 
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        public void PrepareEditingControlForEdit(
            bool selectAll)
        {
            // No preparation needs to be done.
        }

        protected override void OnValueChanged(
            EventArgs eventArgs)
        {
            this.valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventArgs);
        }
    }
}
