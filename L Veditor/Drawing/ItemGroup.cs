using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using L_Veditor.Drawing;
using L_Veditor.Items;

namespace L_Veditor.Drawing
{
    /// <summary>
    /// Allows to work with group like with Item
    /// </summary>
    class ItemGroup : Item
    {
        private List<Item> MyGroup;
        private Point GBegin, GEnd;
        private double[,] Gcoord;
        private int Nmarker;
        public ItemGroup()
        {
            MyGroup = new List<Item>(); 
            _begin.X = 9999;
            _end.X = 0; 
            _begin.Y = 9999;
            _end.Y = 0;
            base.Marker = new Point[4];
            Nmarker = 0;
        }
        public override void Draw(Ploter ploter)
        {
            ploter.MySelection(_begin, _end);
            for (int i = 0; i < MyGroup.Count; i++)
            {
                MyGroup[i].Draw(ploter);
            }
            ReWriteMarker();
        }
        public void Add(SelectionList slist, ItemList itemlist)
        {
            for (int i = 0; i < slist.Count; i++)
            {
                MyGroup.Add(slist[i].Item);
                itemlist.Remove(MyGroup[i]); 
            }
            MakeSelection();
            itemlist.Add(this);

            Gcoord = new double[MyGroup.Count, 4];
            for (int i = 0; i < MyGroup.Count; i++)
            {
                Gcoord[i, 0] = (double)(MyGroup[i]._begin.X - this._begin.X) / (this._end.X - this._begin.X);
                Gcoord[i, 1] = (double)(MyGroup[i]._end.X - this._end.X)     / (this._end.X - this._begin.X);
                Gcoord[i, 2] = (double)(MyGroup[i]._begin.Y - this._begin.Y) / (this._end.Y - this._begin.Y);
                Gcoord[i, 3] = (double)(MyGroup[i]._end.Y - this._end.Y)     / (this._end.Y - this._begin.Y);
            }
        }

        public override bool Captured(int x, int y)
        {
            for (int i = 0; i < MyGroup.Count; i++)
            {
                if (MyGroup[i].Captured(x,y))
                    return true;
            }
            return false;
        }
        public override void MoveTo(int X, int Y, int beginX, int beginY)
        {
            for (int i = 0; i < MyGroup.Count; i++)
            {
                MyGroup[i].MoveTo(X, Y, beginX, beginY);
            }
            _begin.X += X - beginX;
            _begin.Y += Y - beginY;
            _end.X += X - beginX;
            _end.Y += Y - beginY;
            beginX = X;
            beginY = Y;
        }
        public void MakeSelection()
        {
            int minX = 9999, minY = 9999, maxX = 0, maxY = 0;
            for(int i = 0; i < MyGroup.Count; i++)
            {
                if (minX > MyGroup[i]._begin.X)
                    minX = MyGroup[i]._begin.X;
                if (minX > MyGroup[i]._end.X)
                    minX = MyGroup[i]._end.X;
                if (minY > MyGroup[i]._begin.Y)
                    minY = MyGroup[i]._begin.Y;
                if (minY > MyGroup[i]._end.Y)
                    minY = MyGroup[i]._end.Y;
                if (maxX < MyGroup[i]._begin.X)
                    maxX = MyGroup[i]._begin.X;
                if (maxX < MyGroup[i]._end.X)
                    maxX = MyGroup[i]._end.X;
                if (maxY < MyGroup[i]._begin.Y)
                    maxY = MyGroup[i]._begin.Y;
                if (maxY < MyGroup[i]._end.Y)
                    maxY = MyGroup[i]._end.Y;
            }
            _begin.X = minX;
            _begin.Y = minY;
            _end.X = maxX;
            _end.Y = maxY;
        }
        private void MakeC(int marker, int X, int Y)
        {
            int beginX = 0; int beginY = 0; int endX = 0; int endY = 0;
            for (int i = 0; i < MyGroup.Count; i++)
            {
                beginX = (int)(Gcoord[i, 0] * (this._end.X - this._begin.X) + this._begin.X);
                endX =   (int)(Gcoord[i, 1] * (this._end.X - this._begin.X) + this._end.X);
                beginY = (int)(Gcoord[i, 2] * (this._end.Y - this._begin.Y) + this._begin.Y);
                endY = (int)(Gcoord[i, 3] * (this._end.Y - this._begin.Y) + this._end.Y);
                MyGroup[i]._begin.X = beginX;
                MyGroup[i]._begin.Y = beginY;
                MyGroup[i]._end.X = endX;
                MyGroup[i]._end.Y = endY;
                if ((MyGroup[i] is MyCurve) || (MyGroup[i] is MyPolyLine) || (MyGroup[i] is ItemGroup))
                {
                    switch(marker)
                    {
                        case 0:
                            MyGroup[i].Resize(MyGroup[i], marker, beginX, beginY);
                            break;
                        case 1:
                            MyGroup[i].Resize(MyGroup[i], marker, endX, beginY);
                            break;
                        case 2:
                            MyGroup[i].Resize(MyGroup[i], marker, endX, endY);
                            break;
                        case 3:
                            MyGroup[i].Resize(MyGroup[i], marker, beginX, endY);
                            break;
                    }
                }
                //else
                //{
                //    MyGroup[i]._begin.X = beginX;
                //    MyGroup[i]._begin.Y = beginY;
                //    MyGroup[i]._end.X = endX;
                //    MyGroup[i]._end.Y = endY;
                //}
            }
        }
        public override void Resize(Item item, int marker, int X, int Y)
        {
            if (marker == 0)
            {
                Nmarker = 0;
                this._begin.X = X;
                this._begin.Y = Y;
                MakeC(marker, X, Y);
                return;
            }
            if (marker == 1)
            {
                Nmarker = 1;
                this._end.X = X;
                this._begin.Y = Y;
                MakeC(marker, X, Y);
                return;
            }
            if (marker == 2)
            {
                Nmarker = 2;
                this._end.X = X;
                this._end.Y = Y;
                MakeC(marker, X, Y);
                return;
            }
            if (marker == 3)
            {
                Nmarker = 3;
                this._begin.X = X;
                this._end.Y = Y;
                MakeC(marker, X, Y);
                return;
            }
        }
        public void UnGroup(Item group, ItemList itemlist)
        {
            try
            {
                ItemGroup gr = (ItemGroup)group;
                for (int i = 0; i < gr.MyGroup.Count; i++)
                {
                    itemlist.Add(gr.MyGroup[i]);
                }
                itemlist.Remove(group);
                gr.MyGroup.Clear();
            }
            catch
            {
            }
        }
    }
}
