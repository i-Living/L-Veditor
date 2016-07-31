using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using L_Veditor;

namespace L_Veditor
{
    public partial class Form1 : Form
    {
        Editor myEditor;

        public Form1()
        {
            InitializeComponent();
            myEditor = new Editor();
            myEditor.GetList.New(this);
            Controls.Add(myEditor.GetList);
            Controls.Add(myEditor.GetList.CreateMenu());
        }
    }
}
