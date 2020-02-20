using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Soeleman
{
    public sealed class DataGridViewTextBoxMajorMinorCell :
        DataGridViewImageCell
    {
        private const string Fmt = "0.##";
        private static readonly Image EmptyImage;

        static DataGridViewTextBoxMajorMinorCell()
        {
            EmptyImage = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        }

        public DataGridViewTextBoxMajorMinorCell()
        {
            this.ValueType = typeof(string);
        }

        protected override object GetFormattedValue(
            object value,
            int rowIndex,
            ref DataGridViewCellStyle cellStyle,
            TypeConverter valueTypeConverter,
            TypeConverter formattedValueTypeConverter,
            DataGridViewDataErrorContexts context)
        {
            return EmptyImage;
        }

        protected override void Paint(
            Graphics g,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            var mm = ((String)value).Split('|');
            var majorValue = Convert.ToDecimal(mm[0]);
            var minorValue = Convert.ToDecimal(mm[1]);

            var minorString = minorValue.ToString(Fmt);

            var minorFont =
                new Font(
                    cellStyle.Font.FontFamily,
                    cellStyle.Font.Size - 1);

            var minorCtl =
                TextRenderer.MeasureText(minorString, minorFont);

            base.Paint(
                g,
                clipBounds,
                cellBounds,
                rowIndex,
                cellState,
                value,
                formattedValue,
                errorText,
                cellStyle,
                advancedBorderStyle,
                paintParts & ~DataGridViewPaintParts.ContentForeground);

            g.DrawString(
                majorValue.ToString(Fmt),
                cellStyle.Font,
                new SolidBrush(cellStyle.ForeColor),
                cellBounds.X + 6,
                cellBounds.Y + 4);

            g.DrawString(
                minorString,
                minorFont,
                new SolidBrush(Color.HotPink),
                cellBounds.X + (cellBounds.Width - (minorCtl.Width - 2)),
                cellBounds.Y);
        }
    }
}