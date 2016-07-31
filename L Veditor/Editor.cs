using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using L_Veditor.Drawing;
using L_Veditor.Items;
using L_Veditor.Events;

namespace L_Veditor
{
    /// <summary>
    /// Creates all base classes
    /// </summary>
    public class Editor
    {
        Scene scene;
        SelectionList selectList;
        MyEventHandler myEventH;
        Factory myFactory;
        DrawingPlace myCanvas;        

        public DrawingPlace GetList
        {
            get { return myCanvas; }
        }

        public Editor()
        {
            scene = new Scene();
            selectList = new SelectionList();
            myFactory = new Factory();
            myEventH = new MyEventHandler(scene, myFactory, selectList);
            myCanvas = new DrawingPlace(scene, myEventH, myFactory);
        }
    }
}
