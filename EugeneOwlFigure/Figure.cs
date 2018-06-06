using System.Drawing.Drawing2D;

namespace EugeneOwlFigure
{
    public abstract class Figure
    {
        public abstract GraphicsPath GetPath();

        public abstract void SetManualParameters(int[] values);
    }
}
