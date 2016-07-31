using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L_Veditor.Drawing;
using System.Drawing;

namespace L_Veditor
{
    /// <summary>
    /// Implements selection of figures
    /// </summary>
    public class Selection
    {
        private Item SItem;

        Point p = new Point(-1, -1);
        public Item Item
        {
            get { return SItem; }
            set { SItem = value; }
        }
        public Selection(Item Item)
        {
            this.SItem = Item;
        }
        public bool Captured(int x, int y)
        {
            if (SItem.Captured(x, y))
                return true;
            else
                return false;
        }
        public bool MarkerCaptured(int X, int Y)
        {
            if (SItem.MarkerCaptured(X, Y) != -1)
                return true;
            else
                return false;
        }
        public void DrawMarker(Ploter Ploter)
        {
            for (int i = 0; i < SItem.Marker.Count(); i++)
            {
                Point temp1 = new Point();
                Point temp2 = new Point();
                temp1.X = SItem.Marker[i].X + SItem.MarkerSize;
                temp1.Y = SItem.Marker[i].Y + SItem.MarkerSize;
                temp2.X = SItem.Marker[i].X - SItem.MarkerSize;
                temp2.Y = SItem.Marker[i].Y - SItem.MarkerSize;
                Ploter.MyRectangle(temp1, temp2);
            }
        }
    }
}
