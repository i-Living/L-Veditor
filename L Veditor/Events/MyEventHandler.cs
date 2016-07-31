using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using L_Veditor.Items;
using L_Veditor.Drawing;
using L_Veditor.States;


namespace L_Veditor.Events
{
    public class MyEventHandler : IEventHandlerBase
    {
        private bool _keyPressed;
        private StateContainer StCont;
        private CreateState crState;
        public SingleSelectionState selectstate;
        private MultiSelectionState mselectstate;
        private DragState drstate;
        private SelectionList slist;
        private CurveDragState curvestate;
        private PolyLineDragState polylinestate;

        public SelectionList GetSelectionList
        {
            get { return slist; }
        }
        
        public StateContainer StateCont
        {
            get { return StCont; }
            set { StCont = value; }
        }

        public StateContainer EventController { get { return StCont; } }

        public MyEventHandler(Scene scene, Factory figuretype, SelectionList selectList)
        {
            StCont = new StateContainer();
            StCont.SetActiveState(new MultiSelectionState(scene, this, selectList));
            slist = selectList;
            crState = new CreateState(scene, this, figuretype, selectList);
            drstate = new DragState(scene, this, selectList);
            selectstate = new SingleSelectionState(scene, this, figuretype, selectList);
            mselectstate = new MultiSelectionState(scene, this, selectList);
            curvestate = new CurveDragState(scene, this, figuretype, selectList);
            polylinestate = new PolyLineDragState(scene, this, figuretype, selectList);
        }

        public CreateState SetCrState()
        {
            return crState;
        }
        public CurveDragState SetCurveDrState()
        {
            return curvestate;
        }
        public PolyLineDragState SetPLDrState()
        {
            return polylinestate;
        }
        public DragState SetDrState()
        {
            return drstate;
        }
        public SingleSelectionState SetSlState()
        {
            return selectstate;
        }
        public MultiSelectionState SetMSlState()
        {
            return mselectstate;
        }
        public bool KeyPressed
        {
            get { return _keyPressed; }
            set { _keyPressed = value; }
        }
        public void EndEdit()
        {
            polylinestate.Isdrawing = false;
        }
    }
}
