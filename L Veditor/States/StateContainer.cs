using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace L_Veditor
{
    public class StateContainer : IEventController
    {
        State activeState = null;
        public State ActiveState
        {
            set { activeState = value; }
            get { return activeState; }
        }
        public void SetActiveState(State State)
        {
            this.activeState = State;
        }
        public void MouseMove(int X, int Y)
        {
            if (activeState != null) activeState.MouseMove(X, Y);
        }

        public void MouseUp(int X, int Y)
        {
            if (activeState != null) activeState.MouseUp(X, Y);
        }

        public void MouseDown(int X, int Y)
        {
            if (activeState != null) activeState.MouseDown(X, Y);
        }
    }
}
