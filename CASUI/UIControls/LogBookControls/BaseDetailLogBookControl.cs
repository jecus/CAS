using System;
using System.Drawing;
using System.Windows.Forms;
using CAS.Core.Types.Aircrafts.Parts;
using CAS.UI.Appearance;

namespace CAS.UI.UIControls.LogBookControls
{
/*    /// <summary>
    /// ������� ���������� ��� ����������� ���������� � ���������� ������� ���������
    /// </summary>
    public partial class BaseDetailLogBookControl : UserControl
    {

        #region Fields

        private readonly BaseDetailLogBookDataInterval baseDetailLogBookDataInterval;
        private readonly BaseDetailLogBookTableControl baseDetailLogBookTableControl;
        private readonly BaseDetailLogBookItem baseDetailLogBookItem;
        private readonly Panel panel = new Panel();
        private const int MARGIN = 20;
        private bool readOnly = true;
        private BaseDetail currentDetail;
        private int oldWidth = 0;
        private int oldHeight = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// ������� ������� ���������� ��� ����������� ��������� ������� ���������
        /// </summary>
        /// <param name="currentDetail">������� �������</param>
        /// <param name="startDate">����, � ������� ������� ���������� ���������</param>
        /// <param name="endDate">����, �� ������� ������� ���������� ���������</param>
        public BaseDetailLogBookControl(BaseDetail currentDetail, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            baseDetailLogBookDataInterval = new BaseDetailLogBookDataInterval(currentDetail, startDate, endDate);
            baseDetailLogBookTableControl = new BaseDetailLogBookTableControl(currentDetail);
            BackColor = Css.CommonAppearance.Colors.BackColor;
            MinimumSize = new Size(baseDetailLogBookDataInterval.Width + BaseDetailLogBookItem.DefaultItemSize.Width, baseDetailLogBookDataInterval.Height);
            //
            // baseDetailLogBookDataInterval
            //
            baseDetailLogBookDataInterval.AplyClicked += baseDetailLogBookDataInterval_AplyClicked;
            baseDetailLogBookDataInterval.Location = new Point(0, 0);
            baseDetailLogBookDataInterval.TabIndex = 0;
            //
            // baseDetailLogBookItem
            //
            baseDetailLogBookItem = new BaseDetailLogBookItem();
            baseDetailLogBookItem.Location = new Point(baseDetailLogBookDataInterval.Right, 0);
            baseDetailLogBookItem.TabIndex = 1;
            //
            // baseDetailLogBookTableControl 
            //
            baseDetailLogBookTableControl.Margin = new Padding(0, MARGIN, 0, 0);
            baseDetailLogBookTableControl.Location = new Point(0,0);
            baseDetailLogBookTableControl.TabIndex = 2;
            //
            // panel
            //
            panel.AutoScroll = true;
            panel.Location = new Point(baseDetailLogBookDataInterval.Right, baseDetailLogBookItem.Bottom - 1);
            panel.Controls.Add(baseDetailLogBookTableControl);
            panel.TabIndex = 3;

            Controls.Add(baseDetailLogBookDataInterval);
            Controls.Add(baseDetailLogBookItem);
            Controls.Add(panel);
        }

        #endregion

        #region Properties

        #region public bool ReadOnly

        /// <summary>
        /// ���������� ��� ������������� �������� readOnly ������� ����������, ����� �� ������������� ���������
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                readOnly = value;
                baseDetailLogBookTableControl.ReadOnly = value;
            }
        }

        #endregion

        #region public BaseDetail CurrentDetail

        /// <summary>
        /// ���������� ��� ������������� ������� �������
        /// </summary>
        public BaseDetail CurrentDetail
        {
            get
            {
                return currentDetail;
            }
            set
            {
                currentDetail = value;
                UpdateControl();
            }
        }

        #endregion

        #region public BaseDetailLogBookDataInterval DataIntervalControl

        /// <summary>
        /// ���������� ������� ���������� BaseDetailLogBookDataInterval
        /// </summary>
        public BaseDetailLogBookDataInterval DataIntervalControl
        {
            get
            {
                return baseDetailLogBookDataInterval;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region private void baseDetailLogBookDataInterval_AplyClicked(object sender, EventArgs e)

        private void baseDetailLogBookDataInterval_AplyClicked(object sender, EventArgs e)
        {
            baseDetailLogBookTableControl.LifelengthIncrements = baseDetailLogBookDataInterval.LifelengthIncrements;
        }

        #endregion

        #region public void UpdateControl()

        /// <summary>
        /// ��������� ������� ����������
        /// </summary>
        public void UpdateControl()
        {
            baseDetailLogBookDataInterval.GetLifelenghIncrements();
        }

        #endregion

        #region protected override void OnSizeChanged(EventArgs e)

        /// <summary>
        /// �����, ��������������� ��� ����������� ����������� ������� �������� ����������
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if ((Width != oldWidth) && (baseDetailLogBookDataInterval != null))
            {
                oldWidth = Width;
                panel.Width = Width - baseDetailLogBookDataInterval.Width;
            }
            if ((Height != oldHeight) && (baseDetailLogBookItem != null))
            {
                oldHeight = Height;
                panel.Height = Height - baseDetailLogBookItem.Height;
            }
        }

        #endregion


        #endregion

    }*/
}