using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using L_Veditor.Items;
using L_Veditor.Drawing;

namespace L_Veditor
{
    public interface IEventController
    {
        void MouseMove(int X, int Y);
        void MouseUp(int X, int Y);
        void MouseDown(int X, int Y);
    }
}
