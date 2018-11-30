using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CAS.Core.Types.ATLBs;
using Auxiliary;
using CAS.UI.UIControls.Auxiliary;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ������� ���������� ���������� � ������� ������
    /// </summary>
    public partial class FlightTimeControl : CAS.UI.Interfaces.EditObjectControl
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

        #region public FlightTimeControl()
        /// <summary>
        /// ������� ���������� ���������� � ������� ������
        /// </summary>
        public FlightTimeControl()
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
            if (Flight != null)
            {
                TimeSpan time1, time2;
                UsefulMethods.ParseTimePeriod(textOutIn.Text, out time1, out time2);
                Flight.OutTime = time1;
                Flight.InTime = time2;
                UsefulMethods.ParseTimePeriod(textTakeOffLDG.Text, out time1, out time2);
                Flight.TakeOffTime = time1;
                Flight.LdgTime = time2;
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
            if (Flight != null)
            {
                textOutIn.Text = UsefulMethods.TimePeriodToString(Flight.OutTime, Flight.InTime);
                textTakeOffLDG.Text = UsefulMethods.TimePeriodToString(Flight.TakeOffTime, Flight.LdgTime);
            }
            else
            {
                textOutIn.Text = textTakeOffLDG.Text = "";
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

            if (!ValidateTimePeriod(textOutIn)) return false;
            if (!ValidateTimePeriod(textTakeOffLDG)) return false;

            //
            return true;
        }
        #endregion

        /*
         * ������� �����
         */

        #region private void textOutIn_TextChanged(object sender, EventArgs e)
        /// <summary>
        /// ������������ ���� ��������� � �������� �������, �������� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textOutIn_TextChanged(object sender, EventArgs e)
        {
            TimeSpan time1, time2;
            if (UsefulMethods.ParseTimePeriod(textOutIn.Text, out time1, out time2))
            {
                textBlock.Text = UsefulMethods.TimeToString(UsefulMethods.GetDifference(time2, time1));
            }
            else
            {
                textBlock.Text = "";
            }
        }
        #endregion

        #region private void textTakeOffLDG_TextChanged(object sender, EventArgs e)
        /// <summary>
        /// ������������ ���� ��������� � �������� �������, �������� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textTakeOffLDG_TextChanged(object sender, EventArgs e)
        {
            TimeSpan time1, time2;
            if (UsefulMethods.ParseTimePeriod(textTakeOffLDG.Text, out time1, out time2))
            {
                textFlight.Text = UsefulMethods.TimeToString(UsefulMethods.GetDifference(time2, time1));
            }
            else
            {
                textFlight.Text = "";
            }
        }
        #endregion

        #region private void textOutIn_Leave(object sender, EventArgs e)
        /// <summary>
        /// ������������ �������� ���� ����� ���������, �������� � ������� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textOutIn_Leave(object sender, EventArgs e)
        {
            TimeSpan time1, time2;
            if (UsefulMethods.ParseTimePeriod(textOutIn.Text, out time1, out time2))
                textOutIn.Text = UsefulMethods.TimePeriodToString(time1, time2);
        }
        #endregion

        #region private void textTakeOffLDG_Leave(object sender, EventArgs e)
        /// <summary>
        /// ������������ �������� ���� ����� ���������, �������� � ������� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textTakeOffLDG_Leave(object sender, EventArgs e)
        {
            TimeSpan time1, time2;
            if (UsefulMethods.ParseTimePeriod(textTakeOffLDG.Text, out time1, out time2))
                textTakeOffLDG.Text = UsefulMethods.TimePeriodToString(time1, time2);
        }
        #endregion

        /*
         * ����������
         */

        #region private bool ValidateTimePeriod(TextBox textBox)
        /// <summary>
        /// ��������� ��������� �������� �������
        /// </summary>
        /// <returns></returns>
        private bool ValidateTimePeriod(TextBox textBox)
        {

            TimeSpan time1, time2;
            if (!UsefulMethods.ParseTimePeriod(textBox.Text, out time1, out time2))
            {
                
                SimpleBalloon.Show(textBox, ToolTipIcon.Warning, "Incorrect time format", "Please enter the time period in the following format:\nHH.MM - HH.MM");

                return false;
            }

            //
            return true;
        }
        #endregion

    }
}

