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
    /// 
    /// </summary>
    public class DrawingPlace : Control
    {
        private PictureBox MyPictureBox;
        private Point begin, end;
        private ItemList myItemList;
        private State state;
        private Scene _scene;
        private Events.MyEventHandler _myEventH;
        private Factory _figuretype;
        private ItemGroup group;
        public PictureBox Plotter
        {
            get { return MyPictureBox; }
        }

        public DrawingPlace(Scene scene, Events.MyEventHandler myEventH, Factory figuretype)
        {
            this.Dock = DockStyle.Fill;
            _myEventH = myEventH;
            _scene = scene;
            _figuretype = figuretype;
        }


        public void New(Form Form)
        {
            MyPictureBox = new PictureBox();
            this.Size = Form.Size;
            this.Controls.Add(MyPictureBox);
            MyPictureBox.Dock = DockStyle.Fill;
            MyPictureBox.BackColor = Color.White;
            this.Controls.Add(MyPictureBox);
            MyPictureBox.MouseDown += new MouseEventHandler(pict_MouseDown);
            MyPictureBox.MouseMove += new MouseEventHandler(pict_MouseMove);
            MyPictureBox.MouseUp += new MouseEventHandler(pict_MouseUp);
            _scene.Ploter.MyGraphics = Graphics.FromHwnd(MyPictureBox.Handle);
            myItemList = _scene.MyList;
            _myEventH.StateCont.ActiveState = _myEventH.SetMSlState();
        }

        private void pict_MouseWheel(object sender, MouseEventArgs e)
        {
            
        }
        private void pict_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _myEventH.StateCont.ActiveState = _myEventH.SetMSlState();
                _myEventH.EndEdit();
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                begin.X = e.X;
                begin.Y = e.Y;
            }
            _myEventH.KeyPressed = true;
            _myEventH.StateCont.MouseDown(begin.X, begin.Y);
        }

        private void pict_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                end.X = e.X;
                end.Y = e.Y;
            }
            _myEventH.StateCont.MouseUp(end.X, end.Y);
            _myEventH.KeyPressed = false;
        }

        private void pict_MouseMove(object sender, MouseEventArgs e)
        {
            if (_myEventH.KeyPressed == true)
            {
                end.X = e.X;
                end.Y = e.Y;
                _myEventH.StateCont.MouseMove(end.X, end.Y);
            }
            return;
        }

        public MenuStrip CreateMenu()
        {
            //Основные пункты меню
            MenuStrip MainMenu = new MenuStrip();
            MainMenu.Dock = DockStyle.Top;
            ToolStripMenuItem File = new ToolStripMenuItem("Файл");
            MainMenu.Items.Add(File);
            ToolStripMenuItem Tools = new ToolStripMenuItem("Инструменты");
            MainMenu.Items.Add(Tools);
            ToolStripMenuItem Action = new ToolStripMenuItem("Действие");
            MainMenu.Items.Add(Action);

            //Выбо фигур 
            ToolStripMenuItem newLine = new ToolStripMenuItem("Линия");
            newLine.Click += new System.EventHandler(LineMenu_Click);
            Tools.DropDownItems.Add(newLine);

            ToolStripMenuItem newRectangle = new ToolStripMenuItem("Прямоугольник");
            newRectangle.Click += new System.EventHandler(RectangleMenu_Click);
            Tools.DropDownItems.Add(newRectangle);

            ToolStripMenuItem newEllipse = new ToolStripMenuItem("Эллипс");
            newEllipse.Click += new System.EventHandler(EllipseMenu_Click);
            Tools.DropDownItems.Add(newEllipse);
            ToolStripMenuItem newPolyLine = new ToolStripMenuItem("Ломаная");
            newPolyLine.Click += new System.EventHandler(PolyLineMenu_Click);
            Tools.DropDownItems.Add(newPolyLine);
            ToolStripMenuItem newCurve = new ToolStripMenuItem("Кривая");
            newCurve.Click += new System.EventHandler(CurveMenu_Click);
            Tools.DropDownItems.Add(newCurve);

            //Выбор дейтсвия
            ToolStripMenuItem Select = new ToolStripMenuItem("Выделить");
            Select.Click += new System.EventHandler(Select_Click);
            Action.DropDownItems.Add(Select);
            ToolStripMenuItem MSelect = new ToolStripMenuItem("Выделить несколько");
            MSelect.Click += new System.EventHandler(MSelect_Click);
            Action.DropDownItems.Add(MSelect);
            ToolStripMenuItem Group = new ToolStripMenuItem("Группировать");
            Group.Click += new System.EventHandler(Group_Click);
            Action.DropDownItems.Add(Group);
            ToolStripMenuItem UnGroup = new ToolStripMenuItem("Разгрууппировать");
            UnGroup.Click += new System.EventHandler(UnGroup_Click);
            Action.DropDownItems.Add(UnGroup);
            ToolStripMenuItem Delete = new ToolStripMenuItem("Удалить");
            Delete.Click += new System.EventHandler(Delete_Click);
            Action.DropDownItems.Add(Delete);
            
            return MainMenu;
        }

        private void MSelect_Click(object sender, EventArgs e)
        {
            _myEventH.StateCont.ActiveState = _myEventH.SetMSlState();
            //_figuretype.Figure = "";
        }
        private void Select_Click(object sender, EventArgs e)
        {
            _myEventH.StateCont.ActiveState = _myEventH.SetSlState();
            //_figuretype.Figure = "";
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            _myEventH.StateCont.ActiveState = _myEventH.SetMSlState();
            if (_myEventH.GetSelectionList.ActiveSelL == null)
            {
                _scene.MyList.Delete(_myEventH.GetSelectionList.ActiveSel.Item);
                _myEventH.GetSelectionList.Clear();
                _scene.Draw();
                return;
            }
            if (_myEventH.GetSelectionList.ActiveSelL.Count > 1)
            {
                _scene.MyList.Delete(_myEventH.GetSelectionList.ActiveSelL);
                _myEventH.GetSelectionList.Clear();
            }
            else
            {
                _scene.MyList.Delete(_myEventH.GetSelectionList.ActiveSel.Item);
                _myEventH.GetSelectionList.Clear();
            }
            _scene.Draw();
        }
        private void UnGroup_Click(object sender, EventArgs e)
        {
            try
            {
                _figuretype.Group.UnGroup(_myEventH.GetSelectionList.ActiveSel.Item, myItemList);
                _myEventH.GetSelectionList.Clear();
                _scene.Draw();
            }
            catch
            {
            }
        }
        private void Group_Click(object sender, EventArgs e)
        {
            _figuretype.NewGroup(_myEventH.GetSelectionList, myItemList);
            _scene.Draw();
        }
        private void PolyLineMenu_Click(object sender, EventArgs e)
        {      
            _myEventH.StateCont.ActiveState = _myEventH.SetCrState();
            _figuretype.Figure = "PolyLine";
        }
        private void CurveMenu_Click(object sender, EventArgs e)
        {
            _myEventH.StateCont.ActiveState = _myEventH.SetCrState();
            _figuretype.Figure = "Curve";
        }

        private void EllipseMenu_Click(object sender, EventArgs e)
        {
            _myEventH.StateCont.ActiveState =  _myEventH.SetCrState();
            _figuretype.Figure = "Ellipse";
        }

        private void RectangleMenu_Click(object sender, EventArgs e)
        {
            _myEventH.StateCont.ActiveState = _myEventH.SetCrState();
            _figuretype.Figure = "Rectangle";
        }

        private void LineMenu_Click(object sender, EventArgs e)
        {
            _myEventH.StateCont.ActiveState = _myEventH.SetCrState();
            _figuretype.Figure = "Line";
        }

    }
}
