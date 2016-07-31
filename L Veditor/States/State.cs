using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using L_Veditor.States;
using L_Veditor.Events;

namespace L_Veditor
{
    public abstract class State : IEvent
    {
        //public CreateState crstate { get; set; }
        //public DragState drstate { get; set; }
        //public SingleSelectionState selectstate { get; set; }

        public virtual void MouseMove(int X, int Y)
        {

        }
        public virtual void MouseUp(int X, int Y)
        {

        }
        public virtual void MouseDown(int X, int Y)
        {

        }
    }
}
