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
    public class CreateState : State
    {
        private Ploter pl;
        private Item myItem;
        private ItemList mylist;
        private Scene _scene;
        private Factory _figuretype;
        private MyEventHandler _myEventH;

        public CreateState(Scene scene, MyEventHandler myEventH, Factory figuretype, SelectionList SelectList)
        {
            _scene = scene;
            mylist = scene.MyList;
            pl = scene.Ploter;
            _figuretype = figuretype;
            _myEventH = myEventH;
        }

        public override void MouseMove(int X, int Y)
        {
        }
        public override void MouseUp(int X, int Y)
        {
        }
        public override void MouseDown(int X, int Y)
        {
            Point begin = new Point(), end = new Point();
            begin.X = X;
            begin.Y = Y;
            end.X = X;
            end.Y = Y;
            myItem = _figuretype.NewFigure(begin, end);
            mylist.Add(myItem);
            switch (_figuretype.Figure)
            {
                case "PolyLine":
                    _myEventH.StateCont.ActiveState = _myEventH.SetPLDrState();
                    break;
                case "Curve":
                    _myEventH.StateCont.ActiveState = _myEventH.SetCurveDrState();
                    break;
                default:
                    _myEventH.StateCont.ActiveState = _myEventH.SetDrState();
                    break;
            }
        }
    }
}
