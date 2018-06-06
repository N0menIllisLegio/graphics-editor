using System.Collections.Generic;
using System.Windows.Forms;
using FigureBase;

namespace BaseProject
{
    class Collection
    {
        public List<object> collection = new List<object>();

        public void DrawAll(PictureBox pictureBox)
        {
            foreach (var figure in collection)
            {
                DrawTool.DrawFigure(pictureBox, (AbstractShape)figure);
            }
        }
    }
}