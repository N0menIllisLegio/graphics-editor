using FigureBase;

namespace EugeneCross
{
    class PluginAdapter : AbstractShape
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PluginAdapter()
        {

        }

        public PluginAdapter(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override void CreateFigure()
        {
            base.CreateFigure();
            Cross cross = new Cross(X, Y);
            GraphicsPath = cross.GetPath();
        }

        public override string ToString()
        {
            return "Cross";
        }

        public override object Clone()
        {
            return new PluginAdapter(X, Y);
        }
    }
}
