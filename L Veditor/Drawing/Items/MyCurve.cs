using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using L_Veditor.Drawing;

namespace L_Veditor.Items
{
    class MyCurve : Item
    {
        private int count;
        private Point[] B;
        private Point[] E;
        private double[,] Gcoord;
        public MyCurve(Point begin, Point end)
        {
            _begin = begin;
            _end = end;
            B = new Point[1];
            E = new Point[1];
            B[0] = begin;
            E[0] = end;
            count = 0;
            base.Marker = new Point[4];
        }
        public override void Draw(Ploter ploter)
        {
            ploter.MyCurve(B.ToArray(), E.ToArray());
            ReWriteMarker();
        }
        public void FindMinMax()
        {
            int minX = 9999, minY = 9999, maxX = 0, maxY = 0;
            for (int i = 0; i < count; i++)
            {
                if (minX > B[i].X)
                    minX = B[i].X;
                if (minX > E[i].X)
                    minX = E[i].X;
                if (minY > B[i].Y)
                    minY = B[i].Y;
                if (minY > E[i].Y)
                    minY = E[i].Y;
                if (maxX < B[i].X)
                    maxX = B[i].X;
                if (maxX < E[i].X)
                    maxX = E[i].X;
                if (maxY < B[i].Y)
                    maxY = B[i].Y;
                if (maxY < E[i].Y)
                    maxY = E[i].Y;
            }
            _begin.X = minX;
            _begin.Y = minY;
            _end.X = maxX;
            _end.Y = maxY;
        }
        public override bool Captured(int x, int y)
        {
            for (int i = 1; i < count; i++)
            {
                Int64 sqr12 = Convert.ToInt64(Math.Pow(E[i].X - B[i].X, 2) + Math.Pow(E[i].Y - B[i].Y, 2));
                Int64 hlf12 = Convert.ToInt64(Math.Sqrt(sqr12));
                if ((Math.Abs((B[i].Y - E[i].Y) * (x - B[i].X) + (E[i].X - B[i].X) * (y - B[i].Y)) <= hlf12) &&
                    ((Math.Abs(2 * x - B[i].X - E[i].X) * (E[i].X - B[i].X) + (2 * y - B[i].Y - E[i].Y) * (E[i].Y - B[i].Y)) <= sqr12))
                {
                    return true;
                }
            }
            return false;
            //if ((x >= _begin.X) && (y >= _begin.Y) && (x <= _end.X) && (y <= _end.Y))
            //    return true;
            //else
            //    return false;
        }
        public void AddPoint(Point point)
        {
            count++;
            Array.Resize(ref B, count);
            Array.Resize(ref E, count);
            if (count > 1)
                B[count - 1] = E[count - 2];
            else
                B[count - 1] = E[count - 1];
            E[count - 1] = point;
            if (point.X < _begin.X)
            {
                _begin.X = point.X;
            }
            if (point.Y < _begin.Y)
            {
                _begin.Y = point.Y;
            }
            if (point.X > _end.X)
            {
                _end.X = point.X;
            }
            if (point.Y > _end.Y)
            {
                _end.Y = point.Y;
            }
            Gcoord = new double[count, 4];
            UpdateCoord();
        }
        private void UpdateCoord()
        {
            for (int i = 0; i < count; i++)
            {
                Gcoord[i, 0] = (double)(B[i].X - this._begin.X) / (this._end.X - this._begin.X);
                Gcoord[i, 1] = (double)(E[i].X - this._end.X) / (this._end.X - this._begin.X);
                Gcoord[i, 2] = (double)(B[i].Y - this._begin.Y) / (this._end.Y - this._begin.Y);
                Gcoord[i, 3] = (double)(E[i].Y - this._end.Y) / (this._end.Y - this._begin.Y);
            }
        }
        private void MakeC()
        {
            int beginX = 0; int beginY = 0; int endX = 0; int endY = 0;
            for (int i = 0; i < count; i++)
            {
                beginX = (int)(Gcoord[i, 0] * (this._end.X - this._begin.X) + this._begin.X);
                endX = (int)(Gcoord[i, 1] * (this._end.X - this._begin.X) + this._end.X);
                beginY = (int)(Gcoord[i, 2] * (this._end.Y - this._begin.Y) + this._begin.Y);
                endY = (int)(Gcoord[i, 3] * (this._end.Y - this._begin.Y) + this._end.Y);
                B[i].X = beginX;
                B[i].Y = beginY;
                E[i].X = endX;
                E[i].Y = endY;
            }
        }
        public override void Resize(Item item, int marker, int X, int Y)
        {
            if (marker == 0)
            {
                item._begin.X = X;
                item._begin.Y = Y;
                MakeC();
                return;
            }
            if (marker == 1)
            {
                item._end.X = X;
                item._begin.Y = Y;
                MakeC();
                return;
            }
            if (marker == 2)
            {
                item._end.X = X;
                item._end.Y = Y;
                MakeC();
                return;
            }
            if (marker == 3)
            {
                item._begin.X = X;
                item._end.Y = Y;
                MakeC();
                return;
            }
        }
        public override void MoveTo(int X, int Y, int beginX, int beginY)
        {
            for (int i = 0; i < count; i++)
            {
                B[i].X += X - beginX;
                B[i].Y += Y - beginY;
                E[i].X += X - beginX;
                E[i].Y += Y - beginY;
            }
            FindMinMax();
        }
    }
}
