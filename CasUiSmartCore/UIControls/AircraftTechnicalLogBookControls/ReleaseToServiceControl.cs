using System;
using SmartCore.Entities.General.Atlbs;


namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ������� ��������� ������ ������ ����������� �� ���������� ������������ ��
    /// </summary>
    public partial class ReleaseToServiceControl : Interfaces.EditObjectControl
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
         * ��������
         */

        #region public DateTime ReleaseDate
        ///<summary>
        /// ���������� ��� ������ ���� ������� � ���. �����������
        ///</summary>
        public DateTime ReleaseDate
        {
            get { return dateTimePickerDate.Value; }
            set { dateTimePickerDate.Value = value; }
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
                Flight.CertificateOfReleaseToService.CheckPerformed = "";
                if (checkPFC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " PFC ";
                if (checkTC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " TC ";
                if (checkDY.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " DY ";
	            if (checkBoxRC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " RC ";
	            if (checkBoxSC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " SC ";
	            if (checkBoxA.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " A ";
	            if (checkBoxC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " C ";
	            if (checkAdd.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " ADD ";
				Flight.CertificateOfReleaseToService.RecordDate = dateTimePickerDate.Value.Date;
                //Flight.CertificateOfReleaseToService.AuthorizationNo = textAuth.Text;
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
            if (Flight != null && Flight.CertificateOfReleaseToService != null && 
                !string.IsNullOrEmpty(Flight.CertificateOfReleaseToService.CheckPerformed))
            {
                checkPFC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("PFC");
                checkTC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("TC");
                checkDY.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("DY");
                dateTimePickerDate.Value = Flight.CertificateOfReleaseToService.RecordDate.Date;
	            checkBoxRC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("RC");
	            checkBoxSC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("SC");
	            checkBoxA.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("A");
	            checkBoxC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("C");
	            checkAdd.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("ADD");
				//textAuth.Text = Flight.CertificateOfReleaseToService.AuthorizationNo;
			}
            else
            {
                checkPFC.Checked = checkTC.Checked = checkDY.Checked = checkBoxRC.Checked = checkBoxSC.Checked = checkBoxA.Checked = checkBoxC.Checked = checkAdd.Checked = false;
                textAuth.Text = "";
                dateTimePickerDate.Value = DateTime.Today;
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
            //if (UsefulMethods.StringToDate(textDate.Text) == null)
            //{

            //    SimpleBalloon.Show(textDate, ToolTipIcon.Warning, "Incorrect date format", "Please enter the date in the following format: DD.MM.YYYY"); 

            //    return false;
            //}

            //
            return true;
        }
        #endregion

        /*
         * ������� �����
         */

        #region private void CheckPfcCheckedChanged(object sender, EventArgs e)
        /// <summary>
        /// � ���� ������ ������� ����� ���� �������� ������ ���� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckPfcCheckedChanged(object sender, EventArgs e)
        {
            //if (checkPFC.Checked) 
            //    checkTC.Checked = checkDY.Checked = false;
        }
        #endregion

        #region private void CheckTcCheckedChanged(object sender, EventArgs e)
        /// <summary>
        /// � ���� ������ ����� ���� �������� ������ ���� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTcCheckedChanged(object sender, EventArgs e)
        {
            //if (checkTC.Checked) checkPFC.Checked = checkDY.Checked = false;
        }
        #endregion

        #region private void checkDY_CheckedChanged(object sender, EventArgs e)
        /// <summary>
        /// � ���� ������ ����� ���� �������� ������ ���� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDyCheckedChanged(object sender, EventArgs e)
        {
            //if (checkDY.Checked) checkPFC.Checked = checkTC.Checked = false;
        }
        #endregion

    }
}

