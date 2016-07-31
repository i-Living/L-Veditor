using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using L_Veditor.Drawing;


namespace L_Veditor
{
    /// <summary>
    /// Allows to select multiple figures
    /// </summary>
    public class SelectionList : List<Selection>
    {
        public Selection ActiveSel { get; set; }
        public SelectionList ActiveSelL { get; set; }

        public void DrawMarkers(Ploter pl)
        {
            for(int i = 0; i < this.Count; i++)
            {
                this[i].DrawMarker(pl);
            }
        }
    }
}
