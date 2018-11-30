using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Auxiliary;
using CAS.UI.UIControls.Auxiliary;
using CAS.Core.Types.ATLBs;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ��������� ����������� ������ � ������� ����� ��� ������ ��������
    /// </summary>
    public partial class ComponentOilControl : CAS.UI.Interfaces.EditObjectControl
    {

        #region public ComponentOilCondition OilCondition
        /// <summary>
        /// ������� � ������� ������ �������
        /// </summary>
        public ComponentOilCondition OilCondition
        {
            get { return AttachedObject as ComponentOilCondition; }
            set { AttachedObject = value; }
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
                OilCondition.Detail = textDetail.Text;
                OilCondition.OilAdded = UsefulMethods.StringToDouble(textAdded.Text);
                OilCondition.OnBoard = UsefulMethods.StringToDouble(textOnBoard.Text);
            }
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

            BeginUpdate();
            if (OilCondition != null)
            {
                textDetail.Text = OilCondition.Detail;
                textAdded.Text = OilCondition.OilAdded.ToString();
                textOnBoard.Text = OilCondition.OnBoard.ToString();
            }
            else
            {
                textDetail.Text = textAdded.Text = textOnBoard.Text = "";
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
            if (!ValidateDoubleTextBox(textAdded)) return false;
            if (!ValidateDoubleTextBox(textOnBoard)) return false;

            //
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private bool ValidateDoubleTextBox(TextBox textBox)
        /// <summary>
        /// ��������� ������������ ����� �������� �����
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        private bool ValidateDoubleTextBox(TextBox textBox)
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


    }
}

