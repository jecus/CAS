using System;
using System.Drawing;
using System.Windows.Forms;
using CAS.UI.Interfaces;
using SmartCore.Entities.Dictionaries;

namespace CAS.UI.UIControls.ReferenceControls
{
    ///<summary>
    /// ����������� ������� 
    ///</summary>
    public partial class ReferenceControlCollectionContainer : UserControl
    {
        #region Fields

        #endregion

        #region Constructor
        ///<summary>
        /// ��������� ����� ������ ������� ������
        ///</summary>
        public ReferenceControlCollectionContainer()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        #region public Image UpperLeftIcon
        ///<summary>
        /// ������ ������ �������� ����
        ///</summary>
        public Image UpperLeftIcon
        {
            get { return extendableRichContainer.UpperLeftIcon; }
            set { extendableRichContainer.UpperLeftIcon = value; }
        }
        #endregion

        #region public Color DescriptionTextColor
        ///<summary>
        /// ���� ������ ��������
        ///</summary>
        public Color DescriptionTextColor
        {
            get
            {
                return extendableRichContainer.DescriptionTextColor;
            }
            set
            {
                extendableRichContainer.DescriptionTextColor = value;
            }
        }
        #endregion

        #region public string Caption
        ///<summary>
        /// ��������� �������
        ///</summary>
        public string Caption
        {
            get
            {
                return extendableRichContainer.Caption;
            }
            set
            {
                extendableRichContainer.Caption = value;
            }
        }
        #endregion

        #region public ConditionState Condition
        ///<summary>
        /// ������ ������� ������ �������� ������������� �����, � ����������� �� ����������� ���������
        ///</summary>
        public ConditionState Condition
        {
            get { return extendableRichContainer.Condition;  }
            set
            {
                if (InvokeRequired)
                    Invoke(new Action<ConditionState>(s => extendableRichContainer.Condition = s), value);
                else extendableRichContainer.Condition = value;
            }
        }
        #endregion

        #region public bool Extended
        /// <summary>
        /// ��������� �� �������
        /// </summary>
        public bool Extended
        {
            get
            {
                return extendableRichContainer.Extended;
            }
            set
            {
                extendableRichContainer.Extended = value;
            }
        }
        #endregion

        #region public bool EnableExtendedControl
        ///<summary>
        /// ���������� ��� ������ �������� ����� �� ������ ����������
        ///</summary>
        public bool EnableExtendedControl
        {
            get { return extendableRichContainer.Visible; }
            set
            {
                extendableRichContainer.Visible = value;
                if (value == false)
                {
                    flowLayoutPanelContainer.Visible = true;
                }
            }
        }
        #endregion

        #endregion

        #region Methods

        #region public void Reset()
        ///<summary>
        /// ������� ������� ������ �� ���������
        ///</summary>
        public void Reset()
        {
            flowLayoutPanelContainer.Controls.Clear();
            extendableRichContainer.Condition = ConditionState.NotEstimated;
        }
        #endregion

        #region public void AddButton(IReference linkLabel)
        ///<summary>
        ///</summary>
        ///<param name="button"></param>
        public void AddButton(IReference button)
        {
            if(button != null && button is Control)
                flowLayoutPanelContainer.Controls.Add((Control)button);
        }
        #endregion

        #region private void ExtendableRichContainerExtending(object sender, System.EventArgs e)
        private void ExtendableRichContainerExtending(object sender, EventArgs e)
        {
            flowLayoutPanelContainer.Visible = extendableRichContainer.Extended;
        }
        #endregion

        #endregion
    }
}