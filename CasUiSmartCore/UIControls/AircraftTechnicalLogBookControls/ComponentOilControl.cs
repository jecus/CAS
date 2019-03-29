using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using CASTerms;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Atlbs;


namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ��������� ����������� ������ � ������� ����� ��� ������ ��������
    /// </summary>
    public partial class ComponentOilControl : Interfaces.EditObjectControl
    {

        /*
         * ��������
         */
        private BaseComponent _prevSelectionBaseComponent;
        private BaseComponent _currentSelectionBaseComponent;

        private readonly Aircraft _currentAircraft;

        #region public BaseDetail PrevPowerUnit
        /// <summary>
        /// ��������������. �������� ������� ��������� ��� null
        /// </summary>
        public BaseComponent PrevPowerUnit
        {
            get
            {
                return _prevSelectionBaseComponent;
            }
        }
        #endregion

        #region public BaseDetail PowerUnit
        /// <summary>
        /// ���������� �������� ������� ��������� ��� null
        /// </summary>
        public BaseComponent PowerUnit
        {
            get
            {
                return _currentSelectionBaseComponent;
            }
        }
        #endregion

        #region public ComponentOilCondition OilCondition

        /// <summary>
        /// ������� � ������� ������ �������
        /// </summary>
        public ComponentOilCondition OilCondition
        {
            get{return AttachedObject as ComponentOilCondition; }
            set{AttachedObject = value;}
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
        /// ���������� ���-�� ���������������� �������
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

        #region public double Flow
        /// <summary>
        /// ������ ������ ������� (����.���)
        /// </summary>
        public double Flow
        {
            get { return (double) numericUpDownFlow.Value; }
            set {  numericUpDownFlow.Value = (decimal)value; }
        }
        #endregion

        /*
         * �����������
         */

        #region public ComponentOilControl()
        /// <summary>
        /// ������� ����������� ������ � ������� ����� ��� ������ ��������
        /// </summary>
        public ComponentOilControl()
        {
            InitializeComponent();
        }
        #endregion

        #region public ComponentOilControl(Aircraft aircraft, ComponentOilCondition condition) : this()
        /// <summary>
        /// ������� ����������� ������ � ������� ����� ��� ������ ��������
        /// </summary>
        public ComponentOilControl(Aircraft aircraft, ComponentOilCondition condition): this()
        {
            _currentAircraft = aircraft;
            AttachedObject = condition;
        }
        #endregion

        #region public ComponentOilControl(Aircraft aircraft) : this(aircraft, new ComponentOilCondition())
        /// <summary>
        /// ������� ����������� ������ � ������� ����� ��� ������ ��������
        /// </summary>
        public ComponentOilControl(Aircraft aircraft) : this(aircraft, new ComponentOilCondition())
        {
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
            if (OilCondition != null)
            {
                if(comboBoxDetail.SelectedItem is BaseComponent)
                {
                    OilCondition.ComponentId =
                        ((BaseComponent) comboBoxDetail.SelectedItem).ItemId;
                }
                OilCondition.Remain = (double) numericUpDownRemain.Value;
                OilCondition.OilAdded = (double)numericUpDownCorrenction.Value;
                OilCondition.OnBoard = (double)numericUpDownOnBoard.Value;
                OilCondition.Spent = (double)numericUpDownSpent.Value;
                OilCondition.RemainAfter = (double)numericUpDownRemainAfter.Value;
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
            if (OilCondition != null)
            {
                //����� ������� ������
	            var baseDetailTypeIds = new[] {BaseComponentType.Apu.ItemId, BaseComponentType.Engine.ItemId};
				var aircraftBaseDetails = GlobalObjects.ComponentCore.GetAicraftBaseComponents(_currentAircraft.ItemId, baseDetailTypeIds).ToList();

                comboBoxDetail.Items.Clear();

                if (OilCondition.ItemId <= 0)
                {
                    //����� ������ �� ������ �����
                    //����� � ������ ����������� ��� ��������� � ���
                    if (aircraftBaseDetails.Count > 0)
                    {
                        comboBoxDetail.Items.AddRange(aircraftBaseDetails.ToArray());
                        comboBoxDetail.SelectedIndex = 0;
                    }    
                }
                else if (OilCondition.BaseComponent != null)
                {
                    //������ ��� ������� ���������� ������ ����������
                    if (_currentAircraft != null && _currentAircraft.ItemId == OilCondition.BaseComponent.ParentAircraftId)
					{
                        //������ ��� ������� ����������� ������ ��������� �� ���� ��������
                        //����� � ������ ����������� ��� ��������� � ���
                        if (aircraftBaseDetails.Count>0)
                            comboBoxDetail.Items.AddRange(aircraftBaseDetails.ToArray());
                    }
                    else
                    {
                        //������ ��� ������� ����������� ������ ��������� �� ������ �������� � ������
                        //����� � ������ ����������� ������ ������ ��� ������� ����������� ������
                        comboBoxDetail.Items.Add(OilCondition.BaseComponent);
                    }
                    comboBoxDetail.SelectedItem = OilCondition.BaseComponent;
                }
                else
                {
                    //������� ��������� ��� �������� ������� ������ �� ������
                    comboBoxDetail.Items.Add("Error: Can't Find component");
                }
                numericUpDownRemain.Value = (decimal) OilCondition.Remain;
                numericUpDownCorrenction.Value = (decimal)OilCondition.OilAdded;
                numericUpDownOnBoard.Value = (decimal)OilCondition.OnBoard;
                numericUpDownSpent.Value = (decimal)OilCondition.Spent;
                numericUpDownRemainAfter.Value = (decimal)OilCondition.RemainAfter;
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
                labelFlow.Visible = value;
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

        #region private void NumericUpDownFlowValueChanged(object sender, EventArgs e)
        private void NumericUpDownFlowValueChanged(object sender, EventArgs e)
        {
            if (_currentSelectionBaseComponent.ComponentWorkParams.Count > 0)
            {
                double flowMax = double.MinValue;
                double flowMin = double.MaxValue;
                foreach (ComponentWorkInRegimeParams param in _currentSelectionBaseComponent.ComponentWorkParams)
                {
                    if (param.OilFlowMaxEnabled && param.OilFlowMax > flowMax)
                        flowMax = param.OilFlowMax;
                    if (param.OilFlowMinEnabled && param.OilFlowMin < flowMin)
                        flowMin = param.OilFlowMin;
                }

                if(flowMax > double.MinValue && (double)numericUpDownFlow.Value > flowMax || 
                   flowMin < double.MaxValue && (double)numericUpDownFlow.Value < flowMin)
                    numericUpDownFlow.BackColor = Color.Red;
                else numericUpDownFlow.BackColor = Color.White;
            }
            else numericUpDownFlow.BackColor = Color.White;

            InvokeOilFlowChanget();
        }
        #endregion

        #region private void ComboBoxDetailSelectedIndexChanged(object sender, EventArgs e)
        private void ComboBoxDetailSelectedIndexChanged(object sender, EventArgs e)
        {
            _prevSelectionBaseComponent = _currentSelectionBaseComponent;
            _currentSelectionBaseComponent = comboBoxDetail.SelectedItem as BaseComponent;

            InvokeComponentChanget();
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
        /// ��������� ��� ��������� ������� �����
        ///</summary>
        [Category("Oil data")]
        [Description("��������� ��� ��������� ������� �������")]
        public event EventHandler OilFlowChanget;

        /// <summary>
        /// �������, ����������� �� ��������� ������� ������ ������� ���������
        /// </summary>
        [Category("Power unit work time data")]
        [Description("�������, ����������� �� ��������� ������� ���������")]
        public event EventHandler ComponentChanget;

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

        ///<summary>
        /// ������������� �� ��������� ������� �����
        ///</summary>
        private void InvokeOilFlowChanget()
        {
            EventHandler handler = OilFlowChanget;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        ///<summary>
        /// ���������� ������� ����������� �� ��������� ������� ���������
        ///</summary>
        private void InvokeComponentChanget()
        {
            EventHandler handler = ComponentChanget;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }


        #endregion
    }
}

