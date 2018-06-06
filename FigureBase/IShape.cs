using System.Drawing;
using System.Drawing.Drawing2D;

namespace FigureBase
{
    public interface IShape
    {
        GraphicsPath GraphicsPath { get; }
        void CreateFigure();
        Pen Pen { get; }
    }
}
