using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_Veditor.Events
{
    public interface IEvent
    {
        void MouseMove(int X, int Y);
        void MouseUp(int X, int Y);
        void MouseDown(int X, int Y);
    }
}
