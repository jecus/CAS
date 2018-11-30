using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Auxiliary;
using CAS.UI.UIControls.Auxiliary.Events;
using CASTerms;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General.Atlbs;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls.AircraftFlightLight
{

    /// <summary>
    /// ������� ���������� ����� ���������� � ������
    /// </summary>
    public partial class FlightDateRouteControlLight : Interfaces.EditObjectControl
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
        public FlightDateRouteControlLight()
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
				Flight.FlightNumber = lookupComboboxFlightNumber.SelectedItem as FlightNum ?? FlightNum.Unknown;
                Flight.PageNo = textBoxPageNum.Text;
                Flight.FlightDate = dateTimePickerFlightDate.Value.Date;
				if (Flight.AtlbRecordType == AtlbRecordType.Flight)
					Flight.StationFromId = (AirportsCodes)lookupComboboxFrom.SelectedItem;
				else Flight.StationFromId = AirportsCodes.Unknown;
				Flight.StationToId = (AirportsCodes)lookupComboboxTo.SelectedItem;
	            Flight.TakeOffTime = (Int32)dateTimePickerTakeOff.Value.TimeOfDay.TotalMinutes;
	            Flight.LDGTime = (Int32)dateTimePickerLDG.Value.TimeOfDay.TotalMinutes;
				if (comboBoxRecordType.SelectedItem != null)
                    Flight.AtlbRecordType = (AtlbRecordType)comboBoxRecordType.SelectedItem;

	            if (Flight.CertificateOfReleaseToService != null)
	            {
					if (checkPFC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " PFC ";
		            if (checkTC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " TC ";
		            if (checkDY.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " DY ";
		            if (checkBoxRC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " RC ";
		            if (checkBoxSC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " SC ";
		            if (checkBoxA.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " A ";
		            if (checkBoxC.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " C ";
		            if (checkADD.Checked) Flight.CertificateOfReleaseToService.CheckPerformed += " ADD ";
				}

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

            comboBoxRecordType.Items.Clear();
            foreach (object o in Enum.GetValues(typeof(AtlbRecordType)))
                comboBoxRecordType.Items.Add(o);

            if (Flight != null)
            {

                dateTimePickerFlightDate.Value = Flight.FlightDate.Date;

	            lookupComboboxFrom.Type = typeof(AirportsCodes);
	            lookupComboboxTo.Type = typeof(AirportsCodes);
	            lookupComboboxFlightNumber.Type = typeof(FlightNum);

				lookupComboboxFlightNumber.SelectedItem = Flight.FlightNumber;
	            if (Flight.AtlbRecordType == AtlbRecordType.Flight)
					lookupComboboxFrom.SelectedItem = Flight.StationFromId;
	            lookupComboboxTo.SelectedItem = Flight.StationToId;
                textBoxPageNum.Text = Flight.PageNo;
	            dateTimePickerTakeOff.Value = Flight.FlightDate.Date.AddMinutes(Flight.TakeOffTime);
	            dateTimePickerLDG.Value = Flight.FlightDate.Date.AddMinutes(Flight.LDGTime);
				comboBoxRecordType.SelectedItem = Flight.AtlbRecordType > 0 ? Flight.AtlbRecordType : AtlbRecordType.Flight;

				if(Flight.CertificateOfReleaseToService != null &&
				   !string.IsNullOrEmpty(Flight.CertificateOfReleaseToService.CheckPerformed))
	            {
					checkPFC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("PFC");
		            checkTC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("TC");
		            checkDY.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("DY");
		            checkBoxRC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("RC");
		            checkBoxSC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("SC");
		            checkBoxA.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("A");
		            checkBoxC.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("C");
		            checkADD.Checked = Flight.CertificateOfReleaseToService.CheckPerformed.Contains("ADD");
				}
				else checkPFC.Checked = checkTC.Checked = checkDY.Checked = checkBoxRC.Checked = checkBoxSC.Checked = checkBoxA.Checked = checkBoxC.Checked = false;

			}
            else
            {
	            dateTimePickerTakeOff.Value = DateTime.Today;
	            dateTimePickerLDG.Value = DateTime.Today;
	            checkPFC.Checked = checkTC.Checked = checkDY.Checked = checkBoxRC.Checked = checkBoxSC.Checked = checkBoxA.Checked = checkBoxC.Checked =  false;
				textBoxPageNum.Text = "";
                comboBoxRecordType.SelectedItem = AtlbRecordType.Flight;
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
            if (Flight.ItemId <= 0 && !ValidatePageNo()) return false;

            if (dateTimePickerFlightDate.Value > DateTime.Now)
            {
                MessageBox.Show("Flight date must be between 1950.1.1 and today", (string)new GlobalTermsProvider()["SystemName"],
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

	        if (lookupComboboxTo.SelectedItem == null)
	        {
				MessageBox.Show("Please select Station To", (string)new GlobalTermsProvider()["SystemName"],
			        MessageBoxButtons.OK, MessageBoxIcon.Error);

		        return false;
			}

	        if (Flight.AtlbRecordType == AtlbRecordType.Flight && lookupComboboxFrom.SelectedItem == null)
	        {
		        MessageBox.Show("Please select Station From", (string)new GlobalTermsProvider()["SystemName"],
			        MessageBoxButtons.OK, MessageBoxIcon.Error);

		        return false;
	        }

	        var type = comboBoxRecordType.SelectedItem as AtlbRecordType? ?? 0;


			if (type == AtlbRecordType.Flight && lookupComboboxFlightNumber.SelectedItem == null)
	        {
		        MessageBox.Show("Please select Flight Number", (string)new GlobalTermsProvider()["SystemName"],
			        MessageBoxButtons.OK, MessageBoxIcon.Error);

		        return false;
	        }

			return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private bool ValidatePageNo()
        /// <summary>
        /// ��������� ����� ��������
        /// </summary>
        /// <returns></returns>
        private bool ValidatePageNo()
        {
            #region//�������� �� ������� ������ � ����� �� ������� ��������

            var temp = GlobalObjects.AircraftFlightsCore.GetFlightWithPageNum(Flight.AircraftId, textBoxPageNum.Text, Flight.ATLBId); 

            if (Flight.ParentATLB != null && temp != null && 
                temp.Count >= Flight.ParentATLB.PageFlightCount && 
                temp.Where(t => t.PageNo.Equals(textBoxPageNum.Text)).Any())
            {
                MessageBox.Show(@"The number of flights on a given page number" + 
                                "\nexceeds the possible quantitative flight " + 
                                "\non the same page of the logbook." +
                                "\nPlease enter another page num",
                                new GlobalTermsProvider()["SystemName"].ToString(),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return false;
            }
            #endregion
           
            return true;
        }
        #endregion

        #region private void DateTimePickerFlightDateValueChanged(object sender, EventArgs e)
        private void DateTimePickerFlightDateValueChanged(object sender, EventArgs e)
        {
            InvokeFlightDateChanget(dateTimePickerFlightDate.Value);
        }
        #endregion

        #region private void comboBoxRecordType_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBoxRecordType_SelectedIndexChanged(object sender, EventArgs e)
        {
	        var selectedItem = comboBoxRecordType.SelectedItem as AtlbRecordType? ?? 0;

			lookupComboboxFrom.Enabled = dateTimePickerTakeOff.Enabled = 
			dateTimePickerLDG.Enabled = textFlight.Enabled = lookupComboboxFlightNumber.Enabled = selectedItem != AtlbRecordType.Maintenance;
        }
        #endregion

        #region Events

        ///<summary>
        /// ��������� �� ����� ��������� ���� ������
        ///</summary>
        [Category("Flight data")]
        [Description("��������� �� ����� ��������� ���� ������")]
        public event DateChangedEventHandler FlightDateChanget;

        ///<summary>
        /// ��������� �� ����� ��������� ������� ������� ������
        ///</summary>
        [Category("Flight data")]
        [Description("��������� �� ����� ��������� ������� ������� ������")]
        public event ValueChangedEventHandler FlightStationFromChanget;

        ///<summary>
        /// ������������� �� �������� ���� ������
        ///</summary>
        ///<param name="e"></param>
        private void InvokeFlightDateChanget(DateTime e)
        {
            DateChangedEventHandler handler = FlightDateChanget;
            if (handler != null) handler(new DateChangedEventArgs(e));
        }

        ///<summary>
        /// ������������� �� �������� ���� ������
        ///</summary>
        ///<param name="e"></param>
        private void InvokeFlightStationFromChanget(string e)
        {
            ValueChangedEventHandler handler = FlightStationFromChanget;
            if (handler != null) handler(new ValueChangedEventArgs(e));
        }

	    ///<summary>
	    /// ��������� ��� ��������� ������� ������
	    ///</summary>
	    [Category("Flight data")]
	    [Description("��������� ��� ��������� ������� ������")]
	    public event DateChangedEventHandler TakeOffTimeChanget;

	    ///<summary>
	    /// ��������� ��� ��������� ������� �������
	    ///</summary>
	    [Category("Flight data")]
	    [Description("��������� ��� ��������� ������� �������")]
	    public event DateChangedEventHandler LDGTimeChanget;

	    ///<summary>
	    /// ������������� �� ��������� ������� ����� � �����
	    ///</summary>
	    ///<param name="e"></param>
	    private void InvokeTakeOffTimeChanget(DateTime e)
	    {
		    DateChangedEventHandler handler = TakeOffTimeChanget;
		    if (handler != null) handler(new DateChangedEventArgs(e));
	    }

	    ///<summary>
	    /// ������������� �� ��������� ������� �������
	    ///</summary>
	    ///<param name="e"></param>
	    private void InvokeLDGTimeChanget(DateTime e)
	    {
		    DateChangedEventHandler handler = LDGTimeChanget;
		    if (handler != null) handler(new DateChangedEventArgs(e));
	    }

		#endregion

		#region private void dateTimePickerTakeOff_ValueChanged(object sender, EventArgs e)

		private void dateTimePickerTakeOff_ValueChanged(object sender, EventArgs e)
	    {
		    if (sender != dateTimePickerTakeOff && sender != dateTimePickerLDG)
			    return;

		    TimeSpan flightDifference =
			    UsefulMethods.GetDifference(dateTimePickerLDG.Value.TimeOfDay,
				    dateTimePickerTakeOff.Value.TimeOfDay);
		    textFlight.Text =
			    UsefulMethods.TimeToString(flightDifference);

		    if (sender == dateTimePickerTakeOff) InvokeTakeOffTimeChanget(dateTimePickerTakeOff.Value);
		    if (sender == dateTimePickerLDG) InvokeLDGTimeChanget(dateTimePickerLDG.Value);
	    }

		    #endregion

	}
}

