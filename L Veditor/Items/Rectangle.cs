using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_Veditor.Items
{
    class Rectangle : Item
    {
        public Rectangle (int[] x, int[] y)
        {
            _x = new int[2];
            _y = new int[2];
            _x[0] = x[0];
            _y[0] = y[0];
            _x[1] = x[1];
            _y[1] = y[1];
        }
    }
}
