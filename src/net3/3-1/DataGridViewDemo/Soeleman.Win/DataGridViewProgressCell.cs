using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Soeleman
{
    public sealed class DataGridViewProgressCell :
        DataGridViewImageCell
    {
        // Used to make custom cell consistent with a DataGridViewImageCell
        private static readonly Image EmptyImage;

        static DataGridViewProgressCell()
        {
            EmptyImage = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        }

        public DataGridViewProgressCell()
        {
            this.ValueType = typeof(Int32);
        }

        // Method required to make the Progress Cell consistent with the default Image Cell. 
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override Object GetFormattedValue(
            Object value,
            Int32 rowIndex,
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
            Int32 rowIndex,
            DataGridViewElementStates cellState,
            Object value,
            Object formattedValue,
            String errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            var progressVal = (Int32)value;
            var percentage = progressVal / 100.0f; // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.

            //Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
            Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);

            // Draws the cell grid
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

            if (percentage > 0.0)
            {
                // Draw the progress bar and the text
                g.FillRectangle(
                    new SolidBrush(
                        Color.FromArgb(163, 189, 242)),
                        cellBounds.X + 2,
                        cellBounds.Y + 2,
                        Convert.ToInt32((percentage * cellBounds.Width) - 4),
                        cellBounds.Height - 4);

                g.DrawString(
                    progressVal + "%",
                    cellStyle.Font,
                    foreColorBrush,
                    cellBounds.X + 6,
                    cellBounds.Y + 5);
            }
            else
            {
                // draw the text
                var dataGridViewRow = this.DataGridView.CurrentRow;

                var dg =
                    dataGridViewRow != null &&
                    dataGridViewRow.Index == rowIndex
                        ? new SolidBrush(cellStyle.SelectionForeColor)
                        : foreColorBrush;

                g.DrawString(
                    progressVal + "%",
                    cellStyle.Font,
                    dg,
                    cellBounds.X + 6,
                    cellBounds.Y + 2);
            }
        }
    }
}
