using System;
using System.ComponentModel;
using System.Windows.Forms;
using CAS.UI.Management;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering;

namespace CAS.UI.UIControls.Auxiliary
{
    /// <summary>
    /// ����� ��� ����������� ���� ������ � �������� ������� � ��������� 
    /// </summary>
    public partial class ContextActionControl : UserControl
    {

        #region Fields

        private bool showPrintButton = false;
        private readonly Icons icons = new Icons();

        #endregion
        
        #region Constructor

        /// <summary>
        /// ������� ����� ������ ��� ����������� ���� ������ � �������� ������� � ���������
        /// </summary>
        public ContextActionControl()
        {
            InitializeComponent();
            splitViewer1.SplitterImage = icons.SeparatorLine;
            splitViewer1[0] = buttonHelp;
            splitViewer1[1] = buttonCloseTab;
            CloseReflectionType = ReflectionTypes.CloseSelected;
            buttonCloseTab.DisplayerRequested += referenceButtonClosetab_DisplayerRequested;
        }

        #endregion

        #region Properties

        #region public ReflectionTypes CloseReflectionType

        ///<summary>
        /// ��� ����������� ����
        ///</summary>
        public ReflectionTypes CloseReflectionType
        {
            get
            {
                return buttonCloseTab.ReflectionType;
            }
            set
            {
                buttonCloseTab.ReflectionType = value;
            }
        }
        #endregion

        #region public IDisplayer CloseDisplayer

        ///<summary>
        /// ���� ����������
        ///</summary>
        public IDisplayer CloseDisplayer
        {
            get
            {
                return buttonCloseTab.Displayer;
            }
            set
            {
                buttonCloseTab.Displayer = value;
            }
        }
        #endregion

        #region public string CloseDisplayerText

        ///<summary>
        /// ��������� ��������� ����
        ///</summary>
        public string CloseDisplayerText
        {
            get
            {
                return buttonCloseTab.DisplayerText;
            }
            set
            {
                buttonCloseTab.DisplayerText = value;
            }
        }
        #endregion

        #region public IDisplayingEntity CloseEntity

        ///<summary>
        /// ������������ ��������
        ///</summary>
        public IDisplayingEntity CloseEntity
        {
            get
            {
                return buttonCloseTab.Entity;
            }
            set
            {
                buttonCloseTab.Entity = value;
            }
        }
        #endregion

        #region public bool ShowPrintButton

        /// <summary>
        /// ���������� ��� ������������� ��������, ������������ ����� �� ���������� ������ Print
        /// </summary>
        [Description("����� �� ���������� ������ Print")]
        public bool ShowPrintButton
        {
            get
            {
                return showPrintButton;
            }
            set
            {
                showPrintButton = value;
                UpdateControl();
            }
        }

        #endregion

        #region public RichReferenceButton ButtonPrint

        /// <summary>
        /// ���������� ������ Print
        /// </summary>
        public RichReferenceButton ButtonPrint
        {
            get
            {
                return buttonPrint;
            }
        }

        #endregion

        #region public RichReferenceButton ButtonClose

        /// <summary>
        /// ���������� ������ Close
        /// </summary>
        public RichReferenceButton ButtonClose
        {
            get
            {
                return buttonCloseTab;
            }
        }

        #endregion

        #region public HelpRequestingButtonT ButtonHelp

        /// <summary>
        /// ���������� ������ Help
        /// </summary>
        public HelpRequestingButtonT ButtonHelp
        {
            get
            {
                return buttonHelp;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region private void referenceButtonClosetab_DisplayerRequested(object sender, ReferenceEventArgs e)

        private void referenceButtonClosetab_DisplayerRequested(object sender, ReferenceEventArgs e)
        {
            OnEditDisplayerRequested(e);
        }

        #endregion

        #region private void OnEditDisplayerRequested(ReferenceEventArgs e)

        private void OnEditDisplayerRequested(ReferenceEventArgs e)
        {
            if (EditDisplayerRequested != null)
                EditDisplayerRequested(this, e);
        }

        #endregion

        #region private void UpdateControl()

        /// <summary>
        /// ��������� ������� ����������
        /// </summary>
        private void UpdateControl()
        {
            if (showPrintButton)
            {
                splitViewer1.ControlsAmount = 3;
                splitViewer1[0] = buttonPrint;
                splitViewer1[1] = buttonHelp;
                splitViewer1[2] = buttonCloseTab;
            }
            else
            {
                splitViewer1.ControlsAmount = 2;
                splitViewer1[0] = buttonHelp;
                splitViewer1[1] = buttonCloseTab;
            }
        }

        #endregion
        
        #endregion
        
        #region Events

        /// <summary>
        /// ������� ������� ��������������
        /// </summary>
        public event EventHandler<ReferenceEventArgs> EditDisplayerRequested;

        #endregion
        
    }
}
