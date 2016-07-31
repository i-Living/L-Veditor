using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using L_Veditor.Drawing;
using L_Veditor.Items;

namespace L_Veditor
{
    /// <summary>
    /// Controls the drawing tools.
    /// </summary>
    public class Scene
    {
        Ploter ploter;
        ItemList myItemList;

        public Ploter Ploter
        {
            get { return ploter; }
            set { ploter = value; }
        }
        public ItemList MyList
        {
            get { return myItemList; }
            set { myItemList = value; }
        }
        public Scene()
        {
            ploter = new Ploter();
            myItemList = new ItemList();
        }
        public void Draw()
        {
            ploter.Clear();
            myItemList.Draw(ploter);
        }
        public void Clear()
        {
            ploter.Clear();
        }
        public void DrawSelection(Point begin, Point end)
        {
            ploter.MySelection(begin, end);
        }
    }
}
