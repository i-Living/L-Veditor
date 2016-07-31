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

namespace L_Veditor.States
{
    public class MultiSelectionState : State
    {
        private int tempB, tempE;
        private ItemList _itemlist;
        private SelectionList _selectionlist;
        private Events.MyEventHandler _myHandler;
        private Scene _scene;
        private Item _myItem;
        private Selection sel;
        private Point marker;
        private ItemGroup group;
        private bool itemsCaptured = false;
        Point begin = new Point(), end = new Point();

        public MultiSelectionState(Scene scene, Events.MyEventHandler myEventH, SelectionList selectList)
        {
            _scene = scene;
            _itemlist = scene.MyList;
            _selectionlist = selectList;
            _myHandler = myEventH;
            if (selectList.ActiveSel != null)
                selectList.ActiveSel.DrawMarker(scene.Ploter); 
            itemsCaptured = false;
        }
        public override void MouseDown(int X, int Y)
        {
            for (int i = _selectionlist.Count-1; i >= 0; i--)
            {
                if (_selectionlist[i].Captured(X, Y))
                {
                    tempB = X;
                    tempE = Y;
                    itemsCaptured = true;
                    return;
                }
            }
            for (int i = _itemlist.Count - 1; i >= 0; i--)
            {
                if (_itemlist[i].Captured(X, Y))
                {
                    _selectionlist.Add(new Selection(_itemlist[i]));
                    _selectionlist.ActiveSel = _selectionlist[0];
                    _myHandler.StateCont.ActiveState = _myHandler.SetSlState();
                    _myHandler.selectstate.MouseDown(X, Y);
                    return;
                }
            }
            itemsCaptured = false;
            begin.X = X;
            begin.Y = Y;
            _selectionlist.Clear();
        }
        public override void MouseMove(int X, int Y)
        {
            if (itemsCaptured)
            {
                for (int i = 0; i < _selectionlist.Count; i++)
                {
                    _selectionlist[i].Item.MoveTo(X, Y, tempB, tempE);
                }
                tempB = X;
                tempE = Y;
                _scene.Draw();
                _selectionlist.DrawMarkers(_scene.Ploter);
                return;
            }
            else
            {
                end.X = X;
                end.Y = Y;
                _scene.Draw();
                _scene.DrawSelection(begin, end);
            }
        }
        public override void MouseUp(int X, int Y)
        {
            if(!itemsCaptured)
            {
                for (int i = 0; i < _itemlist.Count; i++)
                {
                    if (_itemlist[i].Select(begin, end))
                    {
                        _selectionlist.Add(new Selection(_itemlist[i]));
                    }
                }
                _selectionlist.ActiveSelL = _selectionlist;
            }
                
            _scene.Draw();
            _selectionlist.DrawMarkers(_scene.Ploter);
        }
    }
}
