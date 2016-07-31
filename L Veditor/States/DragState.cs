using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using L_Veditor.Drawing;
using L_Veditor.Items;
using L_Veditor;
using L_Veditor.Events;

namespace L_Veditor
{
    public class DragState : State
    {
        private readonly State _state;
        private Ploter _pl;
        private Item _myItem;
        private ItemList mylist;
        private Scene _scene;
        private Events.MyEventHandler _myEventH;
        SelectionList _selectList;
        Selection sel;

        public DragState(Scene scene, Events.MyEventHandler myEventH, SelectionList SelectList)
        {
            _scene = scene;
            mylist = scene.MyList;
            _pl = scene.Ploter;
            _myEventH = myEventH;
            _selectList = SelectList;
        }
        public override void MouseMove(int X, int Y)
        {
            _myItem = mylist[mylist.Count - 1];
            _myItem._end.X = X;
            _myItem._end.Y = Y;
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
            sel = new Selection(_myItem);
            _selectList.Add(sel);
            _selectList.ActiveSel = sel;
            _myEventH.StateCont.ActiveState = _myEventH.SetSlState();
            _selectList.ActiveSel.DrawMarker(_scene.Ploter);           
        }
        public override void MouseDown(int X, int Y)
        {
        }
    }
}
