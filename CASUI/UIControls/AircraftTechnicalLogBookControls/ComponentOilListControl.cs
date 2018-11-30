using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CAS.Core.Types.ATLBs;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ������ ������ ��������� ��� ����������� ���������� �� �����
    /// </summary>
    public partial class ComponentOilListControl : CAS.UI.Interfaces.EditObjectControl
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

        #region public ComponentOilListControl()
        /// <summary>
        /// ������ ������ ��������� ��� ����������� ���������� �� �����
        /// </summary>
        public ComponentOilListControl()
        {
            InitializeComponent();
            FillControls();
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
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                ComponentOilControl c = flowLayoutPanel1.Controls[i] as ComponentOilControl;
                if (c == null) continue;
                c.ApplyChanges();
                if (Flight != null && Flight.OilConditionCollection != null && !ConditionExists(c.OilCondition))
                    Flight.OilConditionCollection.Add(c.OilCondition);
            }


            /*
             * ��� ��������� ��������� � ��������� 
             * 
             * ����� ���������� ��������� ��������� ������ 
             * ��������� �������� StringConvertibleCollection � �� ����� ��������� ������� � ���� ������, 
             * � �������� � �������� ���� ������� AircraftFlights
             */

            //
            base.ApplyChanges();
        }
        #endregion

        #region public override void FillControls()
        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        public override void FillControls()
        {
            BuildControls();
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

            // ��������� ��������� ������
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                ComponentOilControl c = flowLayoutPanel1.Controls[i] as ComponentOilControl;
                if (c != null)
                    if (!c.CheckData()) return false;
            }

            // ��� ������ ������� ���������
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private void BuildControls()
        /// <summary>
        /// ������ ������ ��������
        /// </summary>
        private void BuildControls()
        {
            // ����������� ������ ��������
            flowLayoutPanel1.Controls.Clear();


            int count = 5;
            if (Flight != null && Flight.OilConditionCollection != null && Flight.OilConditionCollection.Count > count)
                count = Flight.OilConditionCollection.Count;

            for (int i = 0; i < count; i++)
            {
                // ��������� ������� ��� ����� ������ �� �����
                ComponentOilControl c = new ComponentOilControl();
                if (Flight != null && Flight.OilConditionCollection != null && i < Flight.OilConditionCollection.Count)
                {
                    c.OilCondition = Flight.OilConditionCollection[i];
                }
                else if (Flight != null) 
                {
                    ComponentOilCondition condition = new ComponentOilCondition();
                    c.OilCondition = condition;
                }
                // 
                this.flowLayoutPanel1.Controls.Add(c);
            }
        }
        #endregion

        #region private bool ConditionExists(ComponentOilCondition con)
        /// <summary>
        /// ���������� �� ���������� �� ������ ����� ��� ��������� ��������
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private bool ConditionExists(ComponentOilCondition con)
        {
            //
            if (Flight == null || Flight.OilConditionCollection == null) return false;

            //
            for (int i = 0; i < Flight.OilConditionCollection.Count; i++)
                if (Flight.OilConditionCollection[i] == con)
                    return true;

            //
            return false;
        }
        #endregion



    }
}

