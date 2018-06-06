using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FigureBase
{
    public abstract class AbstractShape : IShape, ICloneable
    {
        public GraphicsPath GraphicsPath { get; protected set; }
        public Pen Pen { get; protected set; }

        private void Initialize()
        {
            this.GraphicsPath = new GraphicsPath();
            this.Pen = this.Pen = new Pen(Color.Black, 1F) { DashStyle = DashStyle.Solid };
        }

        protected AbstractShape()
        {
            this.Initialize();
        }

        public virtual void CreateFigure()
        {
            this.GraphicsPath.Reset();
        }

        public abstract override string ToString();
        public abstract object Clone();
    }
}
