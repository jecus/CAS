using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LTR.UI.UIControls
{
    /// <summary>
    /// ������� ����������, ����������� ��� ����������� ����������� ���������
    /// </summary>
    [ToolboxItem(false)]
    internal partial class UIDisplayerControl : Panel
    {
        
        #region Constructor

        /// <summary>
        /// ������� ������� ����������, ����������� ��� ����������� ����������� ���������
        /// </summary>
        /// <param name="minWidth">����������� ����� �������� ����������</param>
        /// <param name="minHeight">����������� ������ �������� ����������</param>
        public UIDisplayerControl(int minWidth, int minHeight)
        {
            InitializeComponent();

            this.minWidth = minWidth;
            this.minHeight = minHeight;

            displayerPanel = new FlowLayoutPanel();
            displayerPanel.Dock = DockStyle.Left;
            displayerPanel.Width = Width - minWidth/2;
            displayerPanel.Height = Height - minHeight/2;
            displayerPanel.BackColor = Color.Transparent;
            displayerPanel.Padding = new Padding(0);
            displayerPanel.AutoScroll = true;
            
            Controls.Add(displayerPanel);
        }

        #endregion

        #region Fields

        private readonly FlowLayoutPanel displayerPanel;
        private readonly int minWidth;
        private readonly int minHeight;
        private int oldWidth = 0;
        private int oldHeight = 0;
        
        #endregion

        #region Properties

        #region public FlowLayoutPanel DisplayerPanel

        /// <summary>
        /// ���������� ������, ���� ������� ��������� ���������������� �������� ����������
        /// </summary>
        public FlowLayoutPanel DisplayerPanel
        {
            get
            {
                return displayerPanel;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region protected override void OnSizeChanged(EventArgs e)

        /// <summary>
        /// �����, ����������� ��� ����������� ����������� ������� �������� ����������, ��� ��������� ��� ��������
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (Width < minWidth)
            {
                Width = minWidth;
                oldWidth = Width;
            }
            if (Width != oldWidth)
            {
                displayerPanel.Width = Width - minWidth/2;
                oldWidth = Width;
            }
            if (Height < minHeight)
            {
                Height = minHeight;
                oldHeight = Height;
            }
            if (Height != oldHeight)
            {
                displayerPanel.Height = Height;
                oldHeight = Height;
            }
        }

        #endregion

        #endregion

    }
}