using System;
using System.Windows.Forms;

namespace Soeleman
{
    public sealed class DataGridViewCalendarColumn :
        DataGridViewColumn
    {
        public DataGridViewCalendarColumn()
            : base(new DataGridViewCalendarCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;

            set
            {
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewCalendarCell)))
                {
                    throw new InvalidCastException(@"Must be a CalendarCell");
                }

                base.CellTemplate = value;
            }
        }
    }
}
