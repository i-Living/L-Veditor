using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using L_Veditor.Drawing;

namespace L_Veditor.Items
{
    class MyLine : Item
    {
        public MyLine (Point begin, Point end)
        {
            _begin = begin;
            _end = end;
            base.Marker = new Point[2];
        }
        public override void Draw(Ploter ploter)
        {
            base.Draw(ploter);
            ploter.MyLine(_begin, _end);
            ReWriteMarker();
        }
        public override bool Captured(int x, int y)
        {
            Int64 sqr12 = Convert.ToInt64(Math.Pow(_end.X - _begin.X, 2) + Math.Pow(_end.Y -  _begin.Y, 2));
            Int64 hlf12 = Convert.ToInt64(Math.Sqrt(sqr12));
            if ((Math.Abs((_begin.Y - _end.Y) * (x - _begin.X) + (_end.X - _begin.X) * (y -  _begin.Y)) <= hlf12) &&
                ((Math.Abs(2 * x -  _begin.X - _end.X) * (_end.X -  _begin.X) + (2 * y - _begin.Y - _end.Y) * (_end.Y - _begin.Y)) <= sqr12))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void Resize(Item item, int marker, int X, int Y)
        {
            MarkerFocus = marker;
            if (MarkerFocus == 0)
            {
                item._begin.X = X;
                item._begin.Y = Y;
                return;
            }
            if (MarkerFocus == 1)
            {
                item._end.X = X;
                item._end.Y = Y;
                return;
            }
        }
    }
}
