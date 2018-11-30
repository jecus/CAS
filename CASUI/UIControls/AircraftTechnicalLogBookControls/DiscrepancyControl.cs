using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Auxiliary;
using CAS.Core.Types.ATLBs;
using CAS.Core.Types.Dictionaries;
using CAS.UI.UIControls.Auxiliary;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{


    /// <summary>
    /// ����� ���������� ������ 
    /// </summary>
    public partial class DiscrepancyControl : CAS.UI.Interfaces.EditObjectControl
    {

        /*
         * �������� 
         */

        #region public Discrepancy Discrepancy
        /// <summary>
        /// ����������, � ������� ������ �������
        /// </summary>
        public Discrepancy Discrepancy
        {
            get { return AttachedObject as Discrepancy; }
            set { AttachedObject = value; }
        }
        #endregion





        #region public bool IsNull
        /// <summary>
        /// �������� ����������, ����� �� ��������� ���������� ��� ���. 
        /// ���� �� �������� ������ � ������� ��� ���
        /// </summary>
        public bool IsNull
        {
            get
            {
                return textDescription.Text.Trim() == "";
            }
        }
        #endregion

        #region private int _Index;
        /// <summary>
        /// ����� ��������
        /// </summary>
        private int _Index;
        /// <summary>
        /// ����� ��������
        /// </summary>
        public int Index
        {
            get { return _Index; }
            set { _Index = value; labelDiscrepancy.Text = "Discrepancy #" + _Index.ToString(); }
        }
        #endregion

        /*
         * �����������
         */

        #region public DiscrepancyControl()
        /// <summary>
        /// ������ �����������
        /// </summary>
        public DiscrepancyControl()
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
            if (Discrepancy != null)
            {
                Discrepancy.FilledBy = radioCrew.Checked ? DiscrepancyFilledByEnum.Crew : DiscrepancyFilledByEnum.MaintenanceStaff;
                
                // ���� ATA ����� �� ������ ������ N/A ��� �����
                Discrepancy.ATAChapter = ATAChapterCollection.GetATAChapterByName(textATA.Text);
                if (Discrepancy.ATAChapter == null) Discrepancy.ATAChapter = ATAChapterCollection.Instance.NotApplicableATAChapter;

                Discrepancy.CorrectiveAction.ADDNo = textADDNo.Text;
                Discrepancy.CorrectiveAction.Status = radioClose.Checked ? CorrectiveActionStatus.Close : CorrectiveActionStatus.Open;
                Discrepancy.Description = textDescription.Text;
                
                Discrepancy.CorrectiveAction.Description = textCorrectiveAction.Text;
                Discrepancy.CorrectiveAction.PartNumberOn = textPNOn.Text;
                Discrepancy.CorrectiveAction.PartNumberOff = textPNOff.Text;
                Discrepancy.CorrectiveAction.SerialNumberOn = textSNOn.Text;
                Discrepancy.CorrectiveAction.SerialNumberOff = textSNOff.Text;
                Discrepancy.CertificateOfReleaseToService.Station = textStation.Text;
                Discrepancy.CertificateOfReleaseToService.Date = UsefulMethods.StringToDate(textRTSDate.Text).Value;
                Discrepancy.CertificateOfReleaseToService.AuthorizationNo = textAuthNo.Text;
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
            if (Discrepancy != null)
            {
                radioCrew.Checked = Discrepancy.FilledBy == DiscrepancyFilledByEnum.Crew;
                radioMaintenanceStaff.Checked = Discrepancy.FilledBy == DiscrepancyFilledByEnum.MaintenanceStaff;
                textATA.Text = Discrepancy.ATAChapter != null ? Discrepancy.ATAChapter.ShortName : "";
                radioOpen.Checked = Discrepancy.Status == CorrectiveActionStatus.Open;
                radioClose.Checked = Discrepancy.Status == CorrectiveActionStatus.Close;
                textDescription.Text = Discrepancy.Description;
                //
                if (Discrepancy.CorrectiveAction != null)
                {
                    textCorrectiveAction.Text = Discrepancy.CorrectiveAction.Description;
                    textPNOff.Text = Discrepancy.CorrectiveAction.PartNumberOff;
                    textSNOff.Text = Discrepancy.CorrectiveAction.SerialNumberOff;
                    textPNOn.Text = Discrepancy.CorrectiveAction.PartNumberOn;
                    textSNOn.Text = Discrepancy.CorrectiveAction.SerialNumberOn;
                    textADDNo.Text = Discrepancy.CorrectiveAction.ADDNo;
                }
                else
                {
                    textCorrectiveAction.Text = textPNOff.Text = textSNOff.Text = textPNOn.Text = textSNOn.Text = "";
                }
                //
                if (Discrepancy.CertificateOfReleaseToService != null)
                {
                    textStation.Text = Discrepancy.CertificateOfReleaseToService.Station;
                    textRTSDate.Text = UsefulMethods.NormalizeDate(Discrepancy.CertificateOfReleaseToService.Date);
                    textAuthNo.Text = Discrepancy.CertificateOfReleaseToService.AuthorizationNo;
                }
                else
                {
                    textStation.Text = textRTSDate.Text = textAuthNo.Text = "";
                }
            }
            else
            {
                textATA.Text = textADDNo.Text = textDescription.Text = textCorrectiveAction.Text = textPNOff.Text =
                    textSNOff.Text = textPNOn.Text = textSNOn.Text = textStation.Text = textRTSDate.Text = textAuthNo.Text = "";
                radioOpen.Checked = radioClose.Checked = radioCrew.Checked = radioMaintenanceStaff.Checked = false;
                //
                textDescription.Text = "What Where When Extent";
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

            // ���������� �� ��������� ATA �����
            // ���� ATA ����� �� ������ �� ������� N/A

            // ������� �� ���� Open / Close ��� Crew / Maintenance Staff
            if (!radioCrew.Checked && !radioMaintenanceStaff.Checked)
            {
                MessageBox.Show ("Select one of the Crew or Maintenance Staff radio buttons.");
                return false;
            }

            if (!radioOpen.Checked && !radioClose.Checked)
            {
                MessageBox.Show ("Select one of the Crew or Maintenance Staff radio buttons.");
                return false;
            }

            // ������������ ����� ����
            if (!ValidateRTSDate()) return false;

            //
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private bool ValidateRTSDate()
        /// <summary>
        /// ��������� ��������� ����
        /// </summary>
        /// <returns></returns>
        private bool ValidateRTSDate()
        {
            if (UsefulMethods.StringToDate(textRTSDate.Text) == null)
            {

                SimpleBalloon.Show(textRTSDate, ToolTipIcon.Warning, "Incorrect date format", "Please enter the date in the following format: DD.MM.YYYY"); 
                return false;
            }

            //
            return true;
        }
        #endregion

    }
}

