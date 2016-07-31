using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace L_Veditor.Drawing
{
    /// <summary>
    /// Drawing figures and selection by coordinates
    /// </summary>
    public class Ploter
    {
        private Pen myPen, mySelectionPen;
        private Graphics myGraphics;

        public Graphics MyGraphics
        {
            get { return myGraphics; }
            set { myGraphics = value; }
        }

        public Ploter()
        {
            myPen = new Pen(Color.Black, 1);
            mySelectionPen = new Pen(Color.Black, 1);
            float[] Intervals = { 5, 10, 5, 10 };
            mySelectionPen.DashPattern = Intervals;
        }

        public void MyLine(Point begin, Point end)
        {
            if (myGraphics == null)
            {
                return;
            }
            myGraphics.DrawLine(myPen, begin.X, begin.Y, end.X, end.Y);
        }
        public void MyRectangle(Point begin, Point end)
        {
            if (myGraphics == null)
            {
                return;
            }
            int X = begin.X < end.X ? begin.X : end.X;
            int Y = begin.Y < end.Y ? begin.Y : end.Y;
            int width = Math.Abs(begin.X - end.X);
            int heigh = Math.Abs(begin.Y - end.Y);
            myGraphics.DrawRectangle(myPen, X, Y, width, heigh);
        }
        public void MyElipse(Point begin, Point end)
        {
            if (myGraphics == null)
            {
                return;
            }
            int X = begin.X < end.X ? begin.X : end.X;
            int Y = begin.Y < end.Y ? begin.Y : end.Y;
            int width = Math.Abs(begin.X - end.X);
            int heigh = Math.Abs(begin.Y - end.Y);
            myGraphics.DrawEllipse(myPen, X, Y, width, heigh); 
        }
        public void MyCurve(Point[] begin, Point[] end)
        {
            if (myGraphics == null)
            {
                return;
            }
            for (int i = 0; i < begin.Count(); i++)
                myGraphics.DrawLine(myPen, begin[i], end[i]);
        }
        public void MyPolyLine(Point[] begin, Point[] end)
        {
            if (myGraphics == null)
            {
                return;
            }
            for (int i = 0; i < begin.Count(); i++)
                myGraphics.DrawLine(myPen, begin[i], end[i]);
        }
        public void Clear()
        {
            myGraphics.Clear(Color.White);
        }

        public void MySelection(Point begin, Point end)
        {
            if (myGraphics == null)
            {
                return;
            }
            int X = begin.X < end.X ? begin.X : end.X;
            int Y = begin.Y < end.Y ? begin.Y : end.Y;
            int width = Math.Abs(begin.X - end.X);
            int heigh = Math.Abs(begin.Y - end.Y);
            myGraphics.DrawRectangle(mySelectionPen, X, Y, width, heigh);
        }
    }
}
