using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using L_Veditor.Drawing;
using L_Veditor.Items;

namespace L_Veditor
{
    /// <summary>
    /// Keeps figure data
    /// </summary>
    public abstract class Item
    {
        private const int markerSize = 5;
        public Point _begin;
        public Point _end;
        protected int count;
        private bool selected;
        public int MarkerFocus;
        public abstract bool Captured(int x, int y);
        public Point[] Marker;

        public int MarkerSize
        {
            get { return markerSize; }
        }
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public virtual void ReWriteMarker()
        {
            if (this is MyLine)
            {
                Marker[0] = new Point(_begin.X, _begin.Y);
                Marker[1] = new Point(_end.X, _end.Y);
            }
            else
            {
                Marker[0] = new Point(_begin.X, _begin.Y);
                Marker[1] = new Point(_end.X, _begin.Y);
                Marker[2] = new Point(_end.X, _end.Y);
                Marker[3] = new Point(_begin.X, _end.Y);
            }
        }
        public int MarkerCaptured(int X, int Y)
        {
            for (int i = 0; i <= Marker.Count()-1; i++)
            {
                if ((X >= Marker[i].X - markerSize) && (Y <= Marker[i].Y + markerSize) && (X <= Marker[i].X + markerSize) && (Y >= Marker[i].Y - markerSize))
                {
                    MarkerFocus = i;
                    return MarkerFocus;
                }
            }
            return -1;
        }
        public virtual void Draw(Ploter ploter)
        {
            
        }
        public virtual void MoveTo(int X, int Y, int beginX, int beginY)
        {
            _begin.X += X - beginX;
            _begin.Y += Y - beginY;
            _end.X += X - beginX;
            _end.Y += Y - beginY;
        }
       
        public virtual void Resize(Item item, int marker, int X, int Y)
        {
            MarkerFocus = marker;
            if(MarkerFocus == 0)
            {
                item._begin.X = X;
                item._begin.Y = Y;
                return;
            }
            if (MarkerFocus == 1)
            {
                item._end.X = X;
                item._begin.Y = Y;
                return;
            }
            if (MarkerFocus == 2)
            {
                item._end.X = X;
                item._end.Y = Y;
                return;
            }
            if (MarkerFocus == 3)
            {
                item._begin.X = X;
                item._end.Y = Y;
                return;
            }
        }
        public bool Select(Point begin, Point end)
        {
            if ((_begin.X > begin.X) && (_end.X < end.X) && (_end.Y < end.Y) && _begin.Y > begin.Y)
            {
                return true;
            }
            else
                return false;
        }
    }
}
