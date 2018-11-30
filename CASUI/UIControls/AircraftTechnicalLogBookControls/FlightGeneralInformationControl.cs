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
    /// ������� ���������� ����� ���������� � ������
    /// </summary>
    public partial class FlightGeneralInformationControl : CAS.UI.Interfaces.EditObjectControl
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

        #region public FlightGeneralInformationControl()
        /// <summary>
        /// ������� ���������� ����� ���������� � ������
        /// </summary>
        public FlightGeneralInformationControl()
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
                Flight.FlightNo = textFlightNo.Text;
                Flight.FlightDate = UsefulMethods.StringToDate(textDate.Text).Value;
                Flight.StationFrom = textFrom.Text;
                Flight.StationTo = textTo.Text;
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
                textFlightNo.Text = Flight.FlightNo;
                textDate.Text = UsefulMethods.NormalizeDate(Flight.FlightDate);
                textFrom.Text = Flight.StationFrom;
                textTo.Text = Flight.StationTo;
            }
            else
            {
                textFlightNo.Text = textDate.Text = textFrom.Text = textTo.Text = "";
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
            if (!ValidateFlightDate()) return false;

            //
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private bool ValidateFlightDate()
        /// <summary>
        /// ��������� ��������� ����
        /// </summary>
        /// <returns></returns>
        private bool ValidateFlightDate()
        {
            if (UsefulMethods.StringToDate(textDate.Text) == null)
            {
                SimpleBalloon.Show(textDate, ToolTipIcon.Warning, "Incorrect date format",
                                   "Please enter the date in the following format: DD.MM.YYYY");

                return false;
            }

            //
            return true;
        }
        #endregion


    }
}

