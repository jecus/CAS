using System;
using System.ComponentModel;
using SmartCore.Entities.General.Atlbs;


namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ��������� ����������� ������ � ������� ����� ��� ������ ��������
    /// </summary>
    public partial class HydraulicControl : Interfaces.EditObjectControl
    {

        /*
         * ��������
         */

        #region public HydraulicCondition HydraulicCondition

        /// <summary>
        /// ������� � ������� ������ �������
        /// </summary>
        public HydraulicCondition HydraulicCondition
        {
            get { return AttachedObject as HydraulicCondition; }
            set {AttachedObject = value;}
        }
        #endregion

        #region public double RemainBefore

        /// <summary>
        /// 
        /// </summary>
        public double RemainBefore
        {
            get { return (double)numericUpDownRemain.Value; }
        }
        #endregion

        #region public double Refuel

        /// <summary>
        /// 
        /// </summary>
        public double Refuel
        {
            get { return (double)numericUpDownCorrenction.Value; }
        }
        #endregion

        #region double OnBoard

        /// <summary>
        /// 
        /// </summary>
        public double OnBoard
        {
            get { return (double)numericUpDownOnBoard.Value; }
        }
        #endregion

        #region public double Spent

        /// <summary>
        /// 
        /// </summary>
        public double Spent
        {
            get { return (double)numericUpDownSpent.Value; }
        }
        #endregion

        #region public double RemainAfter

        /// <summary>
        /// 
        /// </summary>
        public double RemainAfter
        {
            get { return (double)numericUpDownRemainAfter.Value; }
        }
        #endregion

        /*
         * �����������
         */

        #region public HydraulicControl()
        /// <summary>
        /// ������� ����������� ������ � ������� ����� ��� ������ ��������
        /// </summary>
        public HydraulicControl()
        {
            InitializeComponent();
        }
        #endregion

        #region public HydraulicControl(HydraulicCondition condition) : this()
        /// <summary>
        /// ������� ����������� ������ � ������� ����� ��� ������ ��������
        /// </summary>
        public HydraulicControl(HydraulicCondition condition): this()
        {
            AttachedObject = condition;
        }
        #endregion

        /*
         * ������������� ������
         */

        #region public override void ApplyChanges()
        /// <summary>
        /// ��������� � ������� ��������� ��������� �� ��������. 
        /// ���� �� ��� ������ ������������� ������� ����� (�������� ��� ����� �����), �������� ������� �� ����������, ������������ false
        /// ����� base.ApplyChanges() ����������
        /// </summary>
        /// <returns></returns>
        public override void ApplyChanges()
        {
            if (HydraulicCondition != null)
            {
                HydraulicCondition.HydraulicSystem = textBoxHydraulicSystem.Text;
                HydraulicCondition.Remain = (double)numericUpDownRemain.Value;
                HydraulicCondition.OilAdded = (double)numericUpDownCorrenction.Value;
                HydraulicCondition.OnBoard = (double)numericUpDownOnBoard.Value;
                HydraulicCondition.Spent = (double)numericUpDownSpent.Value;
                HydraulicCondition.RemainAfter = (double)numericUpDownRemainAfter.Value;
            }
            
            base.ApplyChanges();
        }
        #endregion

        #region public override void FillControls()
        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        public override void FillControls()
        {

            BeginUpdate();
            if (HydraulicCondition != null)
            {
                textBoxHydraulicSystem.Text = HydraulicCondition.HydraulicSystem;
                numericUpDownRemain.Value = (decimal)HydraulicCondition.Remain;
                numericUpDownCorrenction.Value = (decimal)HydraulicCondition.OilAdded;
                numericUpDownOnBoard.Value = (decimal)HydraulicCondition.OnBoard;
                numericUpDownSpent.Value = (decimal)HydraulicCondition.Spent;
                numericUpDownRemainAfter.Value = (decimal)HydraulicCondition.RemainAfter;
            }
            else
            {
                numericUpDownRemain.Value = numericUpDownCorrenction.Value = 
                numericUpDownOnBoard.Value = numericUpDownSpent.Value = numericUpDownRemainAfter.Value = 0;
            }
            EndUpdate();
        }
        #endregion

        #region public override bool CheckData()
        /// <summary>
        /// ��������� ��������� ������.
        /// ���� �����-���� ���� �� �������� �� �������, ������� ����� ������ MessageBox, ������� ������ � ����������� ���� � ���������� false � �������� ���������� ������
        /// </summary>
        /// <returns></returns>
        public override bool CheckData()
        {
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region public bool ShowHeaders

        /// <summary>
        /// ���������� �� ���������
        /// </summary>
        public bool ShowHeaders
        {
            get { return labelDetail.Visible; }
            set
            {
                labelDetail.Visible = value;
                labelRemain.Visible = value;
                labelOnAdded.Visible = value;
                labelSpent.Visible = value;
                labelRemainAfter.Visible = value;
                label1.Visible = value;
            }
        }

        #endregion

        #region private void SetNumericValue(System.Windows.Forms.NumericUpDown nud, decimal value, bool valueChangetEventEnabled = true, EventHandler valueChangedEvent = null)
        private void SetNumericValue(System.Windows.Forms.NumericUpDown nud, decimal value,
                                     bool valueChangetEventEnabled = true,
                                     EventHandler valueChangedEvent = null)
        {
            if (nud == null) return;
            if (!valueChangetEventEnabled && valueChangedEvent == null) return;

            if (!valueChangetEventEnabled)
            {
                nud.ValueChanged -= valueChangedEvent;
            }

            if (value < nud.Minimum) value = nud.Minimum;
            if (value > nud.Maximum) value = nud.Maximum;

            nud.Value = value;

            if (!valueChangetEventEnabled)
            {
                nud.ValueChanged += valueChangedEvent;
            }
        }
        #endregion

        #region private void ButtonDeleteClick(object sender, EventArgs e)
        private void ButtonDeleteClick(object sender, EventArgs e)
        {
            if (Deleted != null)
                Deleted(this, EventArgs.Empty);
        }
        #endregion

        #region private void NumericUpDownRemainValueChanged(object sender, System.EventArgs e)
        private void NumericUpDownRemainValueChanged(object sender, EventArgs e)
        {
            InvokeBeforRemainChanget();

            decimal value = (decimal)(RemainBefore + Refuel);
            SetNumericValue(numericUpDownOnBoard, value);
        }
        #endregion

        #region private void NumericUpDownCorrenctionValueChanged(object sender, System.EventArgs e)
        private void NumericUpDownCorrenctionValueChanged(object sender, EventArgs e)
        {
            InvokeRefuelChanget();

            decimal value = (decimal)(RemainBefore + Refuel);
            SetNumericValue(numericUpDownOnBoard, value);
        }
        #endregion

        #region private void NumericUpDownOnBoardValueChanged(object sender, System.EventArgs e)
        private void NumericUpDownOnBoardValueChanged(object sender, EventArgs e)
        {
            InvokeOnBoardChanget();

            decimal value = (decimal)(OnBoard - Spent);
            SetNumericValue(numericUpDownRemainAfter, value, true, NumericUpDownRemainAfterValueChanged);
        }
        #endregion

        #region private void NumericUpDownSpentValueChanged(object sender, System.EventArgs e)
        private void NumericUpDownSpentValueChanged(object sender, EventArgs e)
        {
            InvokeSpentChanget();

            decimal value = (decimal)(OnBoard - Spent);
            SetNumericValue(numericUpDownRemainAfter, value, true, NumericUpDownRemainAfterValueChanged);
        }
        #endregion

        #region private void NumericUpDownRemainAfterValueChanged(object sender, System.EventArgs e)
        private void NumericUpDownRemainAfterValueChanged(object sender, EventArgs e)
        {
            InvokeAfterRemainChanget();

            decimal value = (decimal)(OnBoard - RemainAfter);
            SetNumericValue(numericUpDownSpent, value, true, NumericUpDownSpentValueChanged);
        }
        #endregion

        #region Events
        /// <summary>
        /// </summary>
        public event EventHandler Deleted;

        ///<summary>
        /// ��������� ��� ��������� ������� ����� ����� �������
        ///</summary>
        [Category("Oil data")]
        [Description("��������� ��� ��������� ������� ����� ����� �������")]
        public event EventHandler BeforeRemainChanget;

        ///<summary>
        /// ��������� ��� ��������� �������� �������� �����
        ///</summary>
        [Category("Oil data")]
        [Description("��������� ��� ��������� �������� �������� �����")]
        public event EventHandler RefuelChanget;

        ///<summary>
        /// ��������� ��� ��������� �������� ����� �� �����
        ///</summary>
        [Category("Oil data")]
        [Description("��������� ��� ��������� �������� ����� �� �����")]
        public event EventHandler OnBoardChanget;

        ///<summary>
        /// ��������� ��� ��������� �������� ���������������� �����
        ///</summary>
        [Category("Oil data")]
        [Description("��������� ��� ��������� �������� ���������������� �����")]
        public event EventHandler SpentChanget;

        ///<summary>
        /// ��������� ��� ��������� ������� ����� �������
        ///</summary>
        [Category("Oil data")]
        [Description("��������� ��� ��������� ������� ����� �������")]
        public event EventHandler AfterRemainChanget;

        ///<summary>
        /// ������������� �� ��������� ������� ����� ����� �������
        ///</summary>
        private void InvokeBeforRemainChanget()
        {
            EventHandler handler = BeforeRemainChanget;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        ///<summary>
        /// ������������� �� ��������� �������� �������� �����
        ///</summary>
        private void InvokeRefuelChanget()
        {
            EventHandler handler = RefuelChanget;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        ///<summary>
        /// ������������� �� ��������� ����� �� �����
        ///</summary>
        private void InvokeOnBoardChanget()
        {
            EventHandler handler = OnBoardChanget;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        ///<summary>
        /// ������������� �� ��������� �������� ���������������� �����
        ///</summary>
        private void InvokeSpentChanget()
        {
            EventHandler handler = SpentChanget;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        ///<summary>
        /// ������������� �� ��������� ������� ����� �������
        ///</summary>
        private void InvokeAfterRemainChanget()
        {
            EventHandler handler = AfterRemainChanget;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        #endregion
    }
}

