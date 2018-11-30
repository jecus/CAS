using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CAS.UI.UIControls.Auxiliary;
using Auxiliary;
using CAS.Core.Types.ATLBs;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ������� ��������� ������ ������ ����������� �� ���������� ������������ ��
    /// </summary>
    public partial class ReleaseToServiceControl : CAS.UI.Interfaces.EditObjectControl
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

        #region public ReleaseToServiceControl()
        /// <summary>
        /// ������� ��������� ������ ������ ����������� �� ���������� ������������ ��
        /// </summary>
        public ReleaseToServiceControl()
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
            if (Flight != null && Flight.CertificateOfReleaseToService != null)
            {
                if (checkPFC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed = "PFC";
                else if (checkTC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed = "TC";
                else if (checkDY.Checked) Flight.CertificateOfReleaseToService.CheckPerformed = "DY";
                else Flight.CertificateOfReleaseToService.CheckPerformed = "";
                Flight.CertificateOfReleaseToService.Date = UsefulMethods.StringToDate(textDate.Text).Value;
                Flight.CertificateOfReleaseToService.AuthorizationNo = textAuth.Text;
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
            if (Flight != null && Flight.CertificateOfReleaseToService != null)
            {
                checkPFC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed == "PFC";
                checkTC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed == "TC";
                checkDY.Checked = Flight.CertificateOfReleaseToService.CheckPerformed == "DY";
                textDate.Text = UsefulMethods.NormalizeDate(Flight.CertificateOfReleaseToService.Date);
                textAuth.Text = Flight.CertificateOfReleaseToService.AuthorizationNo;
            }
            else
            {
                checkPFC.Checked = checkTC.Checked = checkDY.Checked = false;
                textDate.Text = textAuth.Text = "";
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
            if (!ValidateDate()) return false;

            //
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private bool ValidateDate()
        /// <summary>
        /// ��������� ��������� ����
        /// </summary>
        /// <returns></returns>
        private bool ValidateDate()
        {
            if (UsefulMethods.StringToDate(textDate.Text) == null)
            {

                SimpleBalloon.Show(textDate, ToolTipIcon.Warning, "Incorrect date format", "Please enter the date in the following format: DD.MM.YYYY"); 

                return false;
            }

            //
            return true;
        }
        #endregion

        /*
         * ������� �����
         */

        #region private void checkPFC_CheckedChanged(object sender, EventArgs e)
        /// <summary>
        /// � ���� ������ ������� ����� ���� �������� ������ ���� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkPFC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPFC.Checked) 
                checkTC.Checked = checkDY.Checked = false;
        }
        #endregion

        #region private void checkTC_CheckedChanged(object sender, EventArgs e)
        /// <summary>
        /// � ���� ������ ����� ���� �������� ������ ���� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTC.Checked) checkPFC.Checked = checkDY.Checked = false;
        }
        #endregion

        #region private void checkDY_CheckedChanged(object sender, EventArgs e)
        /// <summary>
        /// � ���� ������ ����� ���� �������� ������ ���� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkDY_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDY.Checked) checkPFC.Checked = checkTC.Checked = false;
        }
        #endregion

    }
}

