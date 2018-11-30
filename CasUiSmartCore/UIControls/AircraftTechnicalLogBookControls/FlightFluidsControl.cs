using System;
using System.Windows.Forms;

using Auxiliary;
using CAS.UI.UIControls.Auxiliary;
using SmartCore.Entities.General.Atlbs;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ������� ��������� �������� ���������� � ��������� 
    /// </summary>
    public partial class FlightFluidsControl : Interfaces.EditObjectControl
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

        #region public FlightFluidsControl()
        /// <summary>
        /// ������� ��������� ������ ���������� � ��������� 
        /// </summary>
        public FlightFluidsControl()
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
            if (Flight != null && Flight.FluidsCondition != null)
            {

                // Hydraulic Fluid
                //Flight.FluidsCondition.HydraulicFluidAdded = UsefulMethods.StringToDouble(textAdded.Text);
                //Flight.FluidsCondition.HydraulicFluidOnBoard = UsefulMethods.StringToDouble(textOnBoard.Text);

                // De Iceing
                Flight.FluidsCondition.GroundDeIce = checkDeIce.Checked;

                TimeSpan time1, time2;
                UsefulMethods.ParseTimePeriod(textTimePeriod.Text, out time1, out time2);
                Flight.FluidsCondition.AntiIcingStartTime = time1;
                Flight.FluidsCondition.AntiIcingEndTime = time2;
                Flight.FluidsCondition.AntiIcingFluidType = textFluidType.Text;
                Flight.FluidsCondition.AEACode = textAEACode.Text;
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
            if (Flight != null && Flight.FluidsCondition != null)
            {
                //textAdded.Text = Flight.FluidsCondition.HydraulicFluidAdded.ToString();
                //textOnBoard.Text = Flight.FluidsCondition.HydraulicFluidOnBoard.ToString();
                checkDeIce.Checked = Flight.FluidsCondition.GroundDeIce;
                textTimePeriod.Text = UsefulMethods.TimePeriodToString(Flight.FluidsCondition.AntiIcingStartTime, Flight.FluidsCondition.AntiIcingEndTime);
                textFluidType.Text = Flight.FluidsCondition.AntiIcingFluidType;
                textAEACode.Text = Flight.FluidsCondition.AEACode;
            }
            else
            {
                //textAdded.Text = textOnBoard.Text = textTimePeriod.Text = textFluidType.Text = textAEACode.Text = "";
                checkDeIce.Checked = false;
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

            // Hydraulic Fluid
            //if (!ValidateDoubleTextBox(textAdded)) return false;
            //if (!ValidateDoubleTextBox(textOnBoard)) return false;

            // De Iceing
            if (!ValidateTimePeriod(textTimePeriod)) return false;

            //
            return true;
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
                SimpleBalloon.Show(textBox, ToolTipIcon.Warning, "Incorrect numeric format", "Enter valid number"); 
                return false;
            }

            //
            return true;
        }
        #endregion


        /*
         * ������� ��������
         */

        #region private void TextTimePeriodLeave(object sender, EventArgs e)
        /// <summary>
        /// ����� ������������ ���� �������� ������� � ������ � �������� �� ��������� ������ � �������� �� � ������ ������ - �������� ������� ������ �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextTimePeriodLeave(object sender, EventArgs e)
        {
            TimeSpan time1, time2;
            if (UsefulMethods.ParseTimePeriod(textTimePeriod.Text, out time1, out time2))
                textTimePeriod.Text = UsefulMethods.TimePeriodToString(time1, time2);
        }
        #endregion
    }
}

