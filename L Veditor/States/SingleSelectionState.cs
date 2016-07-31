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
    public class SingleSelectionState : State
    {
        private int tempB, tempE;
        private ItemList _itemlist;
        private SelectionList _selectionlist;
        private Events.MyEventHandler _myEventH;
        private Scene _scene;
        private Item _myItem;
        private Selection sel;
        private Factory _figuretype;
        private int marker;
        private bool markerCaptured = false;
        private bool itemCaptured = false;

        public SingleSelectionState(Scene scene, Events.MyEventHandler myEventH, Factory figuretype, SelectionList selectList)
        {
            _scene = scene;
            _itemlist = scene.MyList;
            _selectionlist = selectList;
            _myEventH = myEventH;
            _figuretype = figuretype;
        }
        public override void MouseDown(int X, int Y)
        {
            for (int i = _itemlist.Count - 1; i >= 0; i--)
            {
                if (_itemlist[i].MarkerCaptured(X, Y) != -1)
                {
                    markerCaptured = true;
                    marker = _itemlist[i].MarkerCaptured(X, Y);
                    return;
                }
                if (_itemlist[i].Captured(X, Y))
                {
                    tempB = X;
                    tempE = Y;
                    itemCaptured = true;
                    _selectionlist.Clear();
                    _selectionlist.Add(new Selection(_itemlist[i]));
                    _selectionlist.ActiveSel = _selectionlist[0];
                    _selectionlist.ActiveSel.DrawMarker(_scene.Ploter);
                    return;
                }
            }
            marker = -1;
            itemCaptured = false;
            _selectionlist.Clear();
            _scene.Draw();
            _myEventH.StateCont.ActiveState = _myEventH.SetMSlState();
        }

        public override void MouseMove(int X, int Y)
        {
            if(marker != -1)
            {
                _myItem = _selectionlist.ActiveSel.Item;
                _myItem.Resize(_myItem, marker, X, Y);
                _scene.Draw();
                _selectionlist.ActiveSel.DrawMarker(_scene.Ploter);
                return;
            }
            if (itemCaptured)
            {
                _myItem = _selectionlist.ActiveSel.Item;
                _myItem.MoveTo(X, Y, tempB, tempE);
                tempB = X;
                tempE = Y;
                _scene.Draw();
                _selectionlist.ActiveSel.DrawMarker(_scene.Ploter);
                return;
            }
        }
        public override void MouseUp(int X, int Y)
        {
            marker = -1;
            markerCaptured = false;
            itemCaptured = false;
        }
    }
}
