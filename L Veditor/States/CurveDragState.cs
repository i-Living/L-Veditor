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
    public class CurveDragState : State
    {
        private Ploter pl;
        private Item myItem;
        private ItemList mylist;
        private Scene _scene;
        private Factory _figuretype;
        private Events.MyEventHandler _myEventH;
        private MyCurve mycurve;
        SelectionList _selectList;
        Selection sel;

        public CurveDragState(Scene scene, Events.MyEventHandler myEventH, Factory figuretype, SelectionList SelectList)
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
            //Point begin = new Point(), end = new Point();
            //begin.X = X;
            //begin.Y = Y;
            //end.X = X;
            //end.Y = Y;
            //myItem = _figuretype.NewFigure(begin, end);
            //mylist.Add(myItem);
        }
        public override void MouseMove(int X, int Y)
        {
            Point temp = new Point(X, Y);
            mycurve = (MyCurve)mylist[mylist.Count - 1];
            mycurve.AddPoint(temp);
            _scene.Draw();
        }
        public override void MouseUp(int X, int Y)
        {
            if ((X == mylist[mylist.Count - 1]._begin.X) && (Y == mylist[mylist.Count - 1]._begin.Y))
            {
                mylist.RemoveAt(mylist.Count - 1);
                _scene.Draw();
                _myEventH.StateCont.ActiveState = _myEventH.SetCrState();
                return;
            }
            _scene.Draw();
            myItem = mycurve;
            sel = new Selection(myItem);
            _selectList.Add(sel);
            _selectList.ActiveSel = sel;
            _myEventH.StateCont.ActiveState = _myEventH.SetSlState();
            _selectList.ActiveSel.DrawMarker(_scene.Ploter);  
        }
    }
}
