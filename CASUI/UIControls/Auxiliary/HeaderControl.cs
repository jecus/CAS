using System;
using System.Drawing;
using System.Windows.Forms;
using CAS.UI.Management;
using Controls.AvButtonT;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering;

namespace CAS.UI.UIControls.Auxiliary
{
    ///<summary>
    /// �����, ����������� ������� ���������
    ///</summary>
    public partial class HeaderControl : Panel
    {

        #region Fields
        private int oldWidth = 0;
        private readonly ActionControl actionControl = new ActionControl();
        private readonly ContextActionControl contextActionControl = new ContextActionControl();
        private readonly Icons icons = new Icons();

        #endregion

        #region Constructor
        ///<summary>
        /// ��������� ����� ������
        ///</summary>
        public HeaderControl()
        {
            InitializeComponent();
            Dock = DockStyle.Top;
            TabIndex = 0;
            // 
            // contextActionControl
            // 
            contextActionControl.CloseReflectionType = ReflectionTypes.CloseSelected;
            contextActionControl.Height = Height;
            contextActionControl.Location = new Point(Width - contextActionControl.Width, 0);
            contextActionControl.TabIndex = 2;
            // 
            // actionControl
            // 
            actionControl.EditDisplayerText = "Edit operator";
            actionControl.EditReflectionType = ReflectionTypes.DisplayInNew;
            actionControl.EditDisplayerRequested += actionControl_EditDisplayerRequested;
            actionControl.ReloadRised += actionControl_ReloadRised;
            actionControl.Location = new Point(500, 0);
            actionControl.Height = Height;
            actionControl.TabIndex = 1;

            Controls.Add(actionControl);
            Controls.Add(contextActionControl);
        }

        #endregion

        #region Properties

        #region public ReflectionTypes EditReflectionType

        /// <summary>
        /// ��� ����������� ���� ��������������
        /// </summary>
        public ReflectionTypes EditReflectionType
        {
            get
            {
                return actionControl.EditReflectionType;
            }
            set
            {
                actionControl.EditReflectionType = value;
            }
        }

        #endregion

        #region public string EditDisplayerText

        /// <summary>
        /// ��������� ��������� ���� ��������������
        /// </summary>
        public string EditDisplayerText
        {
            get
            {
                return actionControl.EditDisplayerText;
            }
            set
            {
                actionControl.EditDisplayerText = value;
            }
        }

        #endregion

        #region public AvButton ButtonReload

        /// <summary>
        /// ���������� ������ Reload
        /// </summary>
        public AvButtonT ButtonReload
        {
            get
            {
                return actionControl.ButtonReload;
            }
        }

        #endregion

        #region public RichReferenceButton ButtonEdit

        /// <summary>
        /// ���������� ������ Edit/Save
        /// </summary>
        public RichReferenceButton ButtonEdit
        {
            get
            {
                return actionControl.ButtonEdit;
            }
        }

        #endregion

        #region public ActionControl ActionControl
        
        /// <summary>
        /// ���������� ActionControl (������ Reload, Edit)
        /// </summary>
        public ActionControl ActionControl
        {
            get
            {
                return actionControl;
            }
        }

        #endregion

        #region public ContextActionControl ContextActionControl

        /// <summary>
        /// ���������� ContextActionControl (������ Help, Close, Print)
        /// </summary>
        public ContextActionControl ContextActionControl
        {
            get
            {
                return contextActionControl;
            }
        }

        #endregion
        
        #region public bool ActionControlSplitterVisible

        /// <summary>
        /// ��������� ��� ������������� ��������, ������������ ����� �� ���������� ����������� ��� ������
        /// </summary>
        public bool ActionControlSplitterVisible
        {
            get
            {
                return actionControl.splitViewer1.SplitterImage != null;
            }
            set
            {
                actionControl.splitViewer1.SplitterImage = icons.SeparatorLine; 
            }
        }

        #endregion

        #endregion

        #region Methods

        #region protected override void OnSizeChanged(EventArgs e)

        /// <summary>
        /// �����, ����������� ��� ����������� ����������� ������� �������� ����������
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (Width != oldWidth)
            {
                oldWidth = Width;
                contextActionControl.Left = Width - contextActionControl.Width;
            }
        }

        #endregion

        #region protected override void OnControlAdded(ControlEventArgs e)

        /// <summary>
        /// �����, �������������� ������� ���������� ������ �������� ����������
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.Top = 0;
            e.Control.Height = Height;
            if (e.Control is AircraftHeaderControl || e.Control is AbstractOperatorHeaderControl)
                e.Control.TabIndex = 0;
            base.OnControlAdded(e);
        }

        #endregion

        #region private void actionControl_EditDisplayerRequested(object sender, ReferenceEventArgs e)

        private void actionControl_EditDisplayerRequested(object sender, ReferenceEventArgs e)
        {
            if (EditDisplayerRequested != null) EditDisplayerRequested(sender, e);
        }

        #endregion

        #region private void actionControl_ReloadRised(object sender, EventArgs e)

        private void actionControl_ReloadRised(object sender, EventArgs e)
        {
            if (ReloadRised != null) ReloadRised(sender, e);
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// ������� ������� ��������������
        /// </summary>
        public event EventHandler<ReferenceEventArgs> EditDisplayerRequested;
        /// <summary>
        /// ������� ������� ����������
        /// </summary>
        public event EventHandler ReloadRised;

        #endregion
    }
}
