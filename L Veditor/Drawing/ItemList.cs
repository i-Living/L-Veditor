using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L_Veditor.Drawing;

namespace L_Veditor.Drawing
{
    /// <summary>
    /// Keeps all created figures
    /// </summary>
    public class ItemList : List<Item>
    {
        public Item ActiveItem { get; set; }
        public bool Capture(int x, int y)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Captured(x, y))
                {
                    this.ActiveItem = this[i];
                    return true;
                }
            }
            return false;
        }
        public void Draw(Ploter ploter)
        {
            for (int i = 0; i < this.Count(); i++)
            {
                this[i].Draw(ploter);
            }
        }
        public void Delete(Item item)
        {
            this.Remove(item);
        }
        public void Delete(SelectionList selectionlist)
        {
            for (int i = 0; i < selectionlist.Count; i++)
            {
                this.Remove(selectionlist[i].Item);
            }
        }
        public Item Find(Item item)
        {
            foreach (Item i in this)
            {
                if (item == i)
                    return i;
            }
            return item;
        }
    }
}
