using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Auxiliary;
using CAS.UI.UIControls.Auxiliary;
using CAS.UI.UIControls.Auxiliary.Events;
using SmartCore.Entities.Collections;
using SmartCore.Entities.General.Atlbs;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ������ ��������� �������� ���������� � ������� 
    /// </summary>
    public partial class FuelControl : Interfaces.EditObjectControl
    {

        #region public AircraftFlight Flight
        /// <summary>
        /// �����, � ������� ������ �������
        /// </summary>
        public AircraftFlight Flight
        {
            get { return AttachedObject as AircraftFlight; }
        }
        #endregion

        /*
         * �����������
         */

        #region public FuelControl()
        /// <summary>
        /// ������� ��������� �������� ���������� � �������
        /// </summary>
        public FuelControl()
        {
            InitializeComponent();
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
            // ��������� ��������� ��������� ��������
            for (int i = 0; i < flowLayoutPanelItems.Controls.Count; i++)
            {
                FuelControlItem c = flowLayoutPanelItems.Controls[i] as FuelControlItem;
                if (c == null) continue;
                c.ApplyChanges();
                if (Flight != null && Flight.FuelTankCollection != null && !ConditionExists(c.FuelCondition))
                    Flight.FuelTankCollection.Add(c.FuelCondition);
            }


            // ��������� ����� ���������
            if (Flight != null && Flight.FuelTankCollection != null && Flight.FuelTankCollection.Count >= 1)
            {
                FuelTankCondition c = Flight.FuelTankCollection[0];
                c.CalculateUplift = UsefulMethods.StringToDouble(textCalculateUplift.Text);
                c.ActualUpliftLit = UsefulMethods.StringToDouble(textActualUplift.Text);
                c.Discrepancy = UsefulMethods.StringToDouble(textDiscrepancy.Text);
                c.Density = UsefulMethods.StringToDouble(textDensity.Text);

                // ��������� ��� �������� �� ������ ������ �� ��� ��������� ������ ���������
                for (int i = 1; i < Flight.FuelTankCollection.Count; i++)
                {
                    Flight.FuelTankCollection[i].CalculateUplift = c.CalculateUplift;
                    Flight.FuelTankCollection[i].ActualUpliftLit = c.ActualUpliftLit;
                    Flight.FuelTankCollection[i].Discrepancy = c.Discrepancy;
                    Flight.FuelTankCollection[i].Density = c.Density;
                }
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

            flowLayoutPanelItems.Controls.Clear();

            if (Flight != null && Flight.FuelTankCollection != null)
            {
                for (int i = 0; i < Flight.FuelTankCollection.Count; i++)
                {
                    FuelControlItem item = new FuelControlItem(Flight.FuelTankCollection[i]);
                    item.AfterRemainChanget += ItemAfterRemainChanget;
                    item.RefuelChanget += ItemRefuelChanget;
                    item.OnBoardChanget += ItemOnBoardChanget;
                    item.SpentChanget += ItemSpentChanget;
                    item.BeforeRemainChanget += ItemBeforeRemainChanget;
                    if (i > 0) item.ShowHeaders = false;
                    flowLayoutPanelItems.Controls.Add(item);
                }

                GetTotalBefore();
                GetTotalRefuel();
                GetTotalOnBoard();
                GetTotalSpent();
                GetTotalAfter();
            }
            //�������������� ���� ����� ����� �� ������ ������
            if (Flight != null && Flight.FuelTankCollection != null && Flight.FuelTankCollection.Count >= 1)
            {
                textCalculateUplift.Text = Flight.FuelTankCollection[0].CalculateUplift.ToString();
                textActualUplift.Text = Flight.FuelTankCollection[0].ActualUpliftLit.ToString();
                textDiscrepancy.Text = Flight.FuelTankCollection[0].Discrepancy.ToString();
                textDensity.Text = Flight.FuelTankCollection[0].Density.ToString();
            }
            else
            {
                textCalculateUplift.Text = textActualUplift.Text = textDiscrepancy.Text = textDensity.Text = "";
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
            // ������ ������
            if (!CheckDoubleTextBox(textCalculateUplift)) return false;
            if (!CheckDoubleTextBox(textActualUplift)) return false;
            if (!CheckDoubleTextBox(textDiscrepancy)) return false;
            if (!CheckDoubleTextBox(textDensity)) return false;

            //
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private bool CheckDoubleTextBox(TextBox textBox)
        /// <summary>
        /// ��������� ������������ ����� �������� �����
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        private bool CheckDoubleTextBox(TextBox textBox)
        {
            double d;
            if (!UsefulMethods.StringToDouble(textBox.Text, out d))
            {

                //
                SimpleBalloon.Show(textBox, ToolTipIcon.Warning, "Incorrect numeric format", "Enter valid number"); 

                return false;
            }

            //
            return true;
        }
        #endregion

        #region private void GetTotalBefore()
        private void GetTotalBefore()
        {
            List<FuelControlItem> fcs = flowLayoutPanelItems.Controls.OfType<FuelControlItem>().ToList();

            double t = fcs.Sum(cr => cr.RemainBefore);

            textRemainTotal.Text = t.ToString("F");
        }
        #endregion

        #region private void GetTotalRefuel()
        private void GetTotalRefuel()
        {
            List<FuelControlItem> fcs = flowLayoutPanelItems.Controls.OfType<FuelControlItem>().ToList();

            double t = fcs.Sum(cr => cr.Refuel);

            textCorrectionTotal.Text = t.ToString("F");
        }
        #endregion

        #region private void GetTotalOnBoard()
        private void GetTotalOnBoard()
        {
            List<FuelControlItem> fcs = flowLayoutPanelItems.Controls.OfType<FuelControlItem>().ToList();

            double t = fcs.Sum(cr => cr.OnBoard);

            textOnBoardTotal.Text = t.ToString("F");

            InvokeOnBoardChanget(t);
        }
        #endregion

        #region private void GetTotalSpent()
        private void GetTotalSpent()
        {
            List<FuelControlItem> fcs = flowLayoutPanelItems.Controls.OfType<FuelControlItem>().ToList();

            double t = fcs.Sum(cr => cr.Spent);

            textBoxTotalSpent.Text = t.ToString("F");
        }
        #endregion

        #region private void GetTotalAfter()
        private void GetTotalAfter()
        {
            List<FuelControlItem> fcs = flowLayoutPanelItems.Controls.OfType<FuelControlItem>().ToList();

            double t = fcs.Sum(cr => cr.RemainAfter);

            textBoxRemainAfter.Text = t.ToString("F");

            InvokeRemainAfterChanget(t);
        }
        #endregion

        /*
         * ������� ����� 
         */

        #region private bool ConditionExists(FuelTankCondition con)
        /// <summary>
        /// ���������� �� ���������� �� ������ ����� ��� ��������� ��������
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        private bool ConditionExists(FuelTankCondition con)
        {
            //
            if (Flight == null || Flight.FuelTankCollection == null) return false;

            //
            for (int i = 0; i < Flight.FuelTankCollection.Count; i++)
                if (Flight.FuelTankCollection[i] == con)
                    return true;

            //
            return false;
        }
        #endregion

        #region private void ItemBeforeRemainChanget(object sender, EventArgs e)
        private void ItemBeforeRemainChanget(object sender, EventArgs e)
        {
            GetTotalBefore();
        }
        #endregion

        #region private void ItemSpentChanget(object sender, EventArgs e)
        private void ItemSpentChanget(object sender, EventArgs e)
        {
            GetTotalSpent();
        }
        #endregion

        #region private void ItemOnBoardChanget(object sender, EventArgs e)
        private void ItemOnBoardChanget(object sender, EventArgs e)
        {
            GetTotalOnBoard();
        }
        #endregion

        #region private void ItemRefuelChanget(object sender, EventArgs e)
        private void ItemRefuelChanget(object sender, EventArgs e)
        {
            GetTotalRefuel();
        }
        #endregion

        #region private void ItemAfterRemainChanget(object sender, EventArgs e)
        private void ItemAfterRemainChanget(object sender, EventArgs e)
        {
            GetTotalAfter();
        }
        #endregion

        #region Events

        ///<summary>
        /// ��������� ��� ��������� ������� �� �����
        ///</summary>
        [Category("Fuel data")]
        [Description("��������� ��� ��������� ������� �� �����")]
        public event ValueChangedEventHandler OnBoardChanget;

        ///<summary>
        /// ��������� ��� ��������� ������� �� ����� ������
        ///</summary>
        [Category("Fuel data")]
        [Description("��������� ��� ��������� ������� ����� ������")]
        public event ValueChangedEventHandler RemainAfterChanget;

        ///<summary>
        /// ������������� �� ��������� ������� �� �����
        ///</summary>
        ///<param name="e"></param>
        private void InvokeOnBoardChanget(double e)
        {
            ValueChangedEventHandler handler = OnBoardChanget;
            if (handler != null) handler(new ValueChangedEventArgs(e));
        }

        ///<summary>
        /// ������������� �� ��������� ������� ����� ������
        ///</summary>
        ///<param name="e"></param>
        private void InvokeRemainAfterChanget(double e)
        {
            ValueChangedEventHandler handler = RemainAfterChanget;
            if (handler != null) handler(new ValueChangedEventArgs(e));
        }

        #endregion
    }
}

