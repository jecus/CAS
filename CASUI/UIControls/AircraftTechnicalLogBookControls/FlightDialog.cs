using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.ATLBs;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering;
using CAS.UI.Management.Dispatchering.DispatcheredUIControls.Reports;
using CASReports.Builders;
using Microsoft.VisualBasic.Devices;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{
    /// <summary>
    /// ������ ��������� ������������� � ������������� ���������� � ������ ���������� �����
    /// </summary>
    public partial class FlightDialog : CAS.UI.Interfaces.EditObjectDialog
    {


        /*
         * �������� �������
         */

        #region public AircraftFlight Flight
        /// <summary>
        /// �����, ������� ������������� ��������
        /// </summary>
        public AircraftFlight Flight
        {
            get { return AttachedObject as AircraftFlight; }
        }
        #endregion

        /*
         * ������������
         */

        #region protected FlightDialog()
        /// <summary>
        /// �� ��������� � ������� ������
        /// </summary>
        protected FlightDialog()
        {
            InitializeComponent();
            this.AutoScroll = false;
            this.AutoScroll = true;
        }
        #endregion

        #region protected FlightDialog(AircraftFlight Flight) : this ()
        /// <summary>
        /// ������ ����������� ���������� �� ������
        /// </summary>
        /// <param name="Flight"></param>
        protected FlightDialog(AircraftFlight Flight) : this ()
        {
            AttachedObject = Flight;
        }
        #endregion

        /*
         * ������������� ������
         */

        #region public static void Show(AircraftFlight Flight)
        /// <summary>
        /// �������� ������ �������������� �������.
        /// ������ ����� �������� ������� ����������.
        /// </summary>
        /// <param name="Flight"></param>
        public static void Show(AircraftFlight Flight)
        {
            FlightDialog dlg = GetDialogByObject(Flight) as FlightDialog;
            if (dlg == null)
            {
                dlg = new FlightDialog(Flight);
                RegisterDialog(dlg);
            }
            dlg.Show();
            try
            {
                dlg.Activate();
            }
            catch
            {
            }
        }
        #endregion

        #region protected override void ObjectChanged()
        /// <summary>
        /// ������ ��� �������
        /// </summary>
        protected override void ObjectChanged()
        {
            UpdateDialogText();
            base.ObjectChanged();
        }
        #endregion

        #region protected override bool SaveObjectCalled()
        /// <summary>
        /// ���� ������� ���������� �������
        /// </summary>
        protected override bool SaveObjectCalled()
        {

            //if (Flight.ExistAtDataBase) Flight.Save(); else Flight.Aircraft.RegisterFlight(Flight); return false; // ��� ��������

            try
            {
                if (Flight != null)
                {
                    if (Flight.ExistAtDataBase) Flight.Save();
                    else
                    {
                        Flight.Aircraft.RegisterFlight(Flight);
                        // ��. �������� ������ ������ DiscrepanciesListControl.cs - ��������� ������ � ������ ApplyChanges()
                        discrepanciesListControl1.ApplyChanges();
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception while saving Aircraft Flight information.\nDetails:\n" + ex.Message);
                return false;
            }

            // �������� ������ �������
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private void UpdateDialogText()
        /// <summary>
        /// ��������� ��������� �����
        /// </summary>
        private void UpdateDialogText()
        {
            string s1 = (Flight != null && Flight.Aircraft != null) ? "Aircraft " + Flight.Aircraft.RegistrationNumber + ", " : "";
            string s2 = (Flight == null) ? "New flight" : "Flight No " + Flight.FlightNo;
            this.Text = s1 + s2;
        }
        #endregion

        /*
         * ��������� ������� ������ 
         */

        #region private void buttonSave_Click(object sender, EventArgs e)
        /// <summary>
        /// ������ ������ ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        #endregion

        private void discrepanciesListControl1_Load(object sender, EventArgs e)
        {

        }

        private void FlightDialog_Load(object sender, EventArgs e)
        {

        }
    }
}

