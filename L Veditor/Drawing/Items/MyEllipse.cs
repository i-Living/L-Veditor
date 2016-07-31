using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using L_Veditor.Drawing;

namespace L_Veditor.Items
{
   class MyEllipse  : Item
    {
        public MyEllipse (Point begin, Point end)
        {
            _begin = begin;
            _end = end;
            base.Marker = new Point[4];
        }
        public override void Draw(Ploter ploter)
        {
            base.Draw(ploter);
            ploter.MyElipse(_begin, _end);
            ReWriteMarker();
        }
        public override bool Captured(int x, int y)
        {
            int cX = Math.Abs((_begin.X + _end.X) / 2);
            int cY = Math.Abs((_begin.Y + _end.Y) / 2);
            int a = Math.Abs(_end.X - cX);
            int b = Math.Abs(_end.Y - cY);
            if ((Math.Pow((x - cX), 2) / Math.Pow(a, 2) + Math.Pow((y - cY), 2) / Math.Pow(b, 2)) <= 1)
                return true;
            else
                return false;
        }
    }
}
