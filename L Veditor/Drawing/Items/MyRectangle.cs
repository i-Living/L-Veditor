using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using L_Veditor.Drawing;

namespace L_Veditor.Items
{
    class MyRectangle : Item
    {
        public MyRectangle (Point begin, Point end)
        {
            _begin = begin;
            _end = end;
            base.Marker = new Point[4];
        }
        public override void Draw(Ploter ploter)
        {
            base.Draw(ploter);
            ploter.MyRectangle(_begin, _end);
            ReWriteMarker();
        }
        public override bool Captured(int x, int y)
        {
            if ((x >= _begin.X) && (y >= _begin.Y) && (x <= _end.X) && (y <= _end.Y))
                return true;
            else
                return false;
        }
    }
}
