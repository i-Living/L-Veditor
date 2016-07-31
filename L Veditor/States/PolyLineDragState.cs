using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using L_Veditor.Drawing;
using L_Veditor;
using L_Veditor.Events;
using L_Veditor.Items;

namespace L_Veditor.States
{
    public class PolyLineDragState : State
    {
        private Ploter pl;
        private Item myItem;
        private ItemList mylist;
        private Scene _scene;
        private Factory _figuretype;
        private Events.MyEventHandler _myEventH;
        private MyPolyLine myPolyLine;
        SelectionList _selectList;
        Selection sel;
        private bool isdrawing = false;

        public bool Isdrawing
        {
            get { return isdrawing; }
            set { isdrawing = value; }
        }

        public PolyLineDragState(Scene scene, Events.MyEventHandler myEventH, Factory figuretype, SelectionList SelectList)
        {
            _scene = scene;
            mylist = scene.MyList;
            pl = scene.Ploter;
            _myEventH = myEventH;
            _figuretype = figuretype;
            _selectList = SelectList;
        }
        public override void MouseDown(int X, int Y)
        {
            //if (!isdrawing)
            //{
            //    Point begin = new Point(), end = new Point();
            //    begin.X = X;
            //    begin.Y = Y;
            //    end.X = X;
            //    end.Y = Y;
            //    myItem = _figuretype.NewFigure(begin, end);
            //    mylist.Add(myItem);
            //    isdrawing = true;
            //    _scene.Draw();
            //    return;
            //}            
            myPolyLine = (MyPolyLine)mylist[mylist.Count - 1];
            Point temp = new Point(X, Y);
            myPolyLine.Add(temp);
            _scene.Draw();
        }
        public override void MouseMove(int X, int Y)
        {
        }
        public override void MouseUp(int X, int Y)
        {
            _scene.Draw();
            sel = new Selection(mylist[mylist.Count - 1]);
            _selectList.Add(sel);
            _selectList.ActiveSel = sel;
            _selectList.ActiveSel.DrawMarker(_scene.Ploter);
        }
    }
}
