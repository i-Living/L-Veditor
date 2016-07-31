using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using L_Veditor.Events;
using L_Veditor.Items;
using L_Veditor.Drawing;


namespace L_Veditor
{
    /// <summary>
    /// Creates new figures and groups
    /// </summary>
    public class Factory
    {
        private string _figure;
        private ItemGroup group;

        internal ItemGroup Group
        {
            get { return group; }
            set { group = value; }
        }

        public string Figure
        {
            get { return _figure; }
            set { _figure = value; }
        }

        public Factory()
        {
            _figure = string.Empty;
        }
        
        public void NewGroup(SelectionList slist, ItemList itemlist)
        {
            group = new ItemGroup();
            group.Add(slist, itemlist);
            slist.Clear();
            slist.ActiveSel = new Selection(group);
        }

        public Item NewFigure(Point begin, Point end)
        {
            Item CurentFigure;
            switch (_figure)
            {
                case "Line":
                    CurentFigure = new MyLine(begin, end);
                    break;
                case "Rectangle":
                    CurentFigure = new MyRectangle(begin, end);
                    break;
                case "Ellipse":
                    CurentFigure = new MyEllipse(begin, end);
                    break;
                case "PolyLine":
                    CurentFigure = new MyPolyLine(begin, end);
                    break;
                case "Curve":
                    CurentFigure = new MyCurve(begin, end);
                    break;
                default:
                    CurentFigure = null;
                    break;
            }
            return CurentFigure;
        }
    }
}
