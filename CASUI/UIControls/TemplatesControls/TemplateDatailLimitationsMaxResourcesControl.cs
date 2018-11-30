using System;
using System.Drawing;
using System.Windows.Forms;
using CAS.Core.Core.Interfaces;
using CAS.Core.Core.Management;
using CAS.Core.Types.Aircrafts.Parts;
using CAS.Core.Types.Aircrafts.Parts.Templates;
using CAS.UI.Appearance;
using CAS.UI.UIControls.DetailsControls;

namespace CAS.UI.UIControls.TemplatesControls
{
    /// <summary>
    /// ������� ���������� ��� ����������� ����������� ���������� ��������
    /// </summary>
    public class TemplateDatailLimitationsMaxResourcesControl : UserControl
    {

        #region Fields

        private TemplateAbstractDetail currentDetail;
        private readonly InfoViewer maxResourcesInfo;
        private readonly InfoViewer notifyWhenRemainsInfo;
        private readonly Label labelRemark;

        #endregion

        #region Constructors

        #region public DatailLimitationsMaxResourcesControl()

        /// <summary>
        /// ������� ������� ���������� ��� ����������� ����������� ��������� ��������
        /// </summary>
        public TemplateDatailLimitationsMaxResourcesControl()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            maxResourcesInfo = new InfoViewer(5);
            notifyWhenRemainsInfo = new InfoViewer(5);
            labelRemark = new Label();
            //
            // maxResourcesInfo
            //
            maxResourcesInfo.LifeLengthViewers[0].ShowHeaders = true;
            maxResourcesInfo.Location = new Point(0, 0);
            maxResourcesInfo.LabelTitle.Text = "Max resources";
            maxResourcesInfo.LeftHeaderWidth = 150;
            maxResourcesInfo.LifeLengthViewers[0].LeftHeader = "Since new";
            maxResourcesInfo.LifeLengthViewers[1].LeftHeader = "Since overhaul";
            maxResourcesInfo.LifeLengthViewers[2].LeftHeader = "Since inspection";
            maxResourcesInfo.LifeLengthViewers[3].LeftHeader = "Since shop visit";
            maxResourcesInfo.LifeLengthViewers[4].LeftHeader = "Since hot section " + "\r\n" + " inspection";
            maxResourcesInfo.LifeLengthViewers[4].Visible = false;
            maxResourcesInfo.FirstLifelengthViewerLostFocusByShiftTabClicked = true;
            maxResourcesInfo.ShowMinutes = false;
            maxResourcesInfo.ReadOnly = false;
            maxResourcesInfo.LastLifelengthViewerTabClicked += maxResourcesInfo_LastLifelengthViewerTabClicked;
            maxResourcesInfo.FirstLifelengthViewerShiftTabClicked += maxResourcesInfo_FirstLifelengthViewerShiftTabClicked;
            maxResourcesInfo.LifelengthViewerLostFocus += maxResourcesInfo_LifelengthViewerLostFocus;
            //
            // notifyWhenRemainsInfo
            //
            notifyWhenRemainsInfo.LifeLengthViewers[0].ShowHeaders = true;
            notifyWhenRemainsInfo.Location = new Point(maxResourcesInfo.Right + 100, 0);
            notifyWhenRemainsInfo.LabelTitle.Text = "Notify when remains";
            notifyWhenRemainsInfo.LeftHeaderWidth = 150;
            notifyWhenRemainsInfo.LifeLengthViewers[0].LeftHeader = "Since new";
            notifyWhenRemainsInfo.LifeLengthViewers[1].LeftHeader = "Since overhaul";
            notifyWhenRemainsInfo.LifeLengthViewers[2].LeftHeader = "Since inspection";
            notifyWhenRemainsInfo.LifeLengthViewers[3].LeftHeader = "Since shop visit";
            notifyWhenRemainsInfo.LifeLengthViewers[4].LeftHeader = "Since hot section " + "\r\n" + " inspection";
            notifyWhenRemainsInfo.LifeLengthViewers[4].Visible = false;
            notifyWhenRemainsInfo.LastLifelengthViewerLostFocusByTabClicked = true;
            notifyWhenRemainsInfo.ShowMinutes = false;
            notifyWhenRemainsInfo.ReadOnly = false;
            notifyWhenRemainsInfo.LastLifelengthViewerTabClicked += notifyWhenRemainsInfo_LastLifelengthViewerTabClicked;
            notifyWhenRemainsInfo.FirstLifelengthViewerShiftTabClicked += notifyWhenRemainsInfo_FirstLifelengthViewerShiftTabClicked;
            //
            // labelRemark
            //
            labelRemark.AutoSize = true;
            labelRemark.Font = Css.OrdinaryText.Fonts.RegularFont;
            labelRemark.ForeColor = Css.OrdinaryText.Colors.ForeColor;
            labelRemark.Text = "Use double click to set applicability of the field";

            Controls.Add(maxResourcesInfo);
            Controls.Add(notifyWhenRemainsInfo);
            Controls.Add(labelRemark);

            labelRemark.Location = new Point(0, maxResourcesInfo.Bottom + 20);
        }

        #endregion

        #region public DatailLimitationsMaxResourcesControl(TemplateAbstractDetail detail) : this()

        /// <summary>
        /// ������� ������� ���������� ��� ����������� ����������� ��������� ��������
        /// </summary>
        /// <param name="detail">������� ��������� �������</param>
        public TemplateDatailLimitationsMaxResourcesControl(TemplateAbstractDetail detail) : this()
        {
            if (detail == null)
                throw new ArgumentNullException("detail", "Argument cannot be null");
            CurrentDetail = detail;
        }

        #endregion

        #endregion

        #region Properties

        #region public TemplateAbstractDetail CurrentDetail

        /// <summary>
        /// ���������� ��� ������������� ��������� �������, � ������� �������� ������ ������� ����������
        /// </summary>
        public TemplateAbstractDetail CurrentDetail
        {
            get
            {
                return currentDetail;
            }
            set
            {
                currentDetail = value;
                UpdateInformation();
            }
        }

        #endregion

        #region public ActualState SinceNewLimitation

        /// <summary>
        /// ����������� � ������ ������������
        /// </summary>
        public Lifelength SinceNewLimitation
        {
            get
            {
                return maxResourcesInfo.LifeLengthViewers[0].Lifelength;
            }
            set
            {
                maxResourcesInfo.LifeLengthViewers[0].Lifelength = value;
            }
        }

        #endregion

        #region public ActualState SinceLastOverhaulLimitation

        /// <summary>
        /// ����������� � ���������� �������
        /// </summary>
        public Lifelength SinceLastOverhaulLimitation
        {
            get
            {
                return maxResourcesInfo.LifeLengthViewers[1].Lifelength;
            }
            set
            {
                maxResourcesInfo.LifeLengthViewers[1].Lifelength = value;
            }
        }

        #endregion

        #region public ActualState SinceLastInspectionLimitation

        /// <summary>
        /// ����������� � ���������� ������� ������������
        /// </summary>
        public Lifelength SinceLastInspectionLimitation
        {
            get
            {
                return maxResourcesInfo.LifeLengthViewers[2].Lifelength;
            }
            set
            {
                maxResourcesInfo.LifeLengthViewers[2].Lifelength = value;
            }
        }

        #endregion

        #region public ActualState SinceLastShopVisitLimitation

        /// <summary>
        /// ����������� � ���������� ������� �������
        /// </summary>
        public Lifelength SinceLastShopVisitLimitation
        {
            get
            {
                return maxResourcesInfo.LifeLengthViewers[3].Lifelength;
            }
            set
            {
                maxResourcesInfo.LifeLengthViewers[3].Lifelength = value;
            }
        }

        #endregion

        #region public ActualState SinceLastHotInspectionLimitation

        /// <summary>
        /// ����������� � ���������� ������� hot inspection
        /// </summary>
        public Lifelength SinceLastHotInspectionLimitation
        {
            get
            {
                return maxResourcesInfo.LifeLengthViewers[4].Lifelength;
            }
            set
            {
                maxResourcesInfo.LifeLengthViewers[4].Lifelength = value;
            }
        }

        #endregion


        #region public ActualState SinceNewNotification

        /// <summary>
        /// ����������� ����������� � ������ ������������
        /// </summary>
        public Lifelength SinceNewNotification
        {
            get
            {
                return notifyWhenRemainsInfo.LifeLengthViewers[0].Lifelength;
            }
            set
            {
                notifyWhenRemainsInfo.LifeLengthViewers[0].Lifelength = value;
            }
        }

        #endregion

        #region public ActualState SinceLastOverhaulNotification

        /// <summary>
        /// ����������� ����������� � ���������� �������
        /// </summary>
        public Lifelength SinceLastOverhaulNotification
        {
            get
            {
                return notifyWhenRemainsInfo.LifeLengthViewers[1].Lifelength;
            }
            set
            {
                notifyWhenRemainsInfo.LifeLengthViewers[1].Lifelength = value;
            }
        }

        #endregion

        #region public ActualState SinceLastInspectionNotification

        /// <summary>
        /// ����������� ����������� � ���������� ������� ������������
        /// </summary>
        public Lifelength SinceLastInspectionNotification
        {
            get
            {
                return notifyWhenRemainsInfo.LifeLengthViewers[2].Lifelength;
            }
            set
            {
                notifyWhenRemainsInfo.LifeLengthViewers[2].Lifelength = value;
            }
        }

        #endregion

        #region public ActualState SinceLastShopVisitNotification

        /// <summary>
        /// ����������� ����������� � ���������� ������� �������
        /// </summary>
        public Lifelength SinceLastShopVisitNotification
        {
            get
            {
                return notifyWhenRemainsInfo.LifeLengthViewers[3].Lifelength;
            }
            set
            {
                notifyWhenRemainsInfo.LifeLengthViewers[3].Lifelength = value;
            }
        }

        #endregion

        #region public ActualState SinceLastHostInspectionNotification

        /// <summary>
        /// ����������� ����������� � ���������� Hot section inspection
        /// </summary>
        public Lifelength SinceLastHostInspectionNotification
        {
            get
            {
                return notifyWhenRemainsInfo.LifeLengthViewers[4].Lifelength;
            }
            set
            {
                notifyWhenRemainsInfo.LifeLengthViewers[4].Lifelength = value;
            }
        }

        #endregion


        #endregion

        #region Methods

        #region public bool GetChangeStatus()

        /// <summary>
        /// ���������� ��������, ������������ ���� �� ��������� � ������ �������� ����������
        /// </summary>
        /// <returns></returns>
        public bool GetChangeStatus()
        {
            return GetChangeStatus(currentDetail);
        }

        #endregion

        #region public bool GetChangeStatus(TemplateAbstractDetail detail)

        /// <summary>
        /// ���������� ��������, ������������ ���� �� ��������� � ������ �������� ����������
        /// </summary>
        /// <returns></returns>
        public bool GetChangeStatus(TemplateAbstractDetail detail)
        {
            return maxResourcesInfo.Modified || notifyWhenRemainsInfo.Modified; 
        }

        #endregion

        #region public bool GetChangeStatusOfNewDetail()

        /// <summary>
        /// ���������� ��������, ������������ ���� �� ��������� � ������ �������� ����������
        /// </summary>
        public bool GetChangeStatusOfNewDetail()
        {
            return ((maxResourcesInfo.LifeLengthViewers[0].Lifelength != Lifelength.NullLifelength) ||
                    (maxResourcesInfo.LifeLengthViewers[1].Lifelength != Lifelength.NullLifelength) ||
                    (maxResourcesInfo.LifeLengthViewers[2].Lifelength != Lifelength.NullLifelength) ||
                    (maxResourcesInfo.LifeLengthViewers[3].Lifelength != Lifelength.NullLifelength) ||
                    (maxResourcesInfo.LifeLengthViewers[4].Lifelength != Lifelength.NullLifelength) ||
                    (notifyWhenRemainsInfo.LifeLengthViewers[0].Lifelength != Lifelength.NullLifelength) ||
                    (notifyWhenRemainsInfo.LifeLengthViewers[1].Lifelength != Lifelength.NullLifelength) ||
                    (notifyWhenRemainsInfo.LifeLengthViewers[2].Lifelength != Lifelength.NullLifelength) ||
                    (notifyWhenRemainsInfo.LifeLengthViewers[3].Lifelength != Lifelength.NullLifelength) ||
                    (notifyWhenRemainsInfo.LifeLengthViewers[4].Lifelength != Lifelength.NullLifelength));
        }

        #endregion

        #region public void UpdateInformation()

        /// <summary>
        /// ��������� ������������ ����������
        /// </summary>
        public void UpdateInformation()
        {
            if (currentDetail != null)
                UpdateInformation(currentDetail);
        }

        #endregion

        #region public void UpdateInformation(TemplateAbstractDetail sourceDetail)

        /// <summary>
        /// ��������� ������������ ����������
        /// </summary>
        /// <param name="sourceDetail">������� �� �������� ������������ ����������</param>
        public void UpdateInformation(TemplateAbstractDetail sourceDetail)
        {
            if (sourceDetail == null) throw new ArgumentNullException("sourceDetail");
            SinceNewLimitation = sourceDetail.Limitation.MaxResourceSinceNew;
            SinceLastOverhaulLimitation = sourceDetail.Limitation.MaxResourceSinceOverhaul;
            SinceLastInspectionLimitation = sourceDetail.Limitation.MaxResourceSinceInspection;
            SinceLastShopVisitLimitation = sourceDetail.Limitation.MaxResourceSinceShopVisit;
            
            SinceNewNotification = sourceDetail.Limitation.NotificationSinceNew;
            SinceLastOverhaulNotification = sourceDetail.Limitation.NotificationSinceOverhaul;
            SinceLastInspectionNotification = sourceDetail.Limitation.NotificationSinceInspection;
            SinceLastShopVisitNotification = sourceDetail.Limitation.NotificationSinceShopVisit;
            
            if (sourceDetail is TemplateEngine)
            {
                notifyWhenRemainsInfo.LifeLengthViewers[4].Visible = true;
                maxResourcesInfo.LifeLengthViewers[4].Visible = true;
                SinceLastHotInspectionLimitation = sourceDetail.Limitation.MaxResourceSinceHotSectionInspection;
                SinceLastHostInspectionNotification = sourceDetail.Limitation.NotificationSinceHotSectionInspection;
            }

            bool permission = (currentDetail.HasPermission(Users.CurrentUser, DataEvent.Update));
            maxResourcesInfo.ReadOnly = !permission;
            notifyWhenRemainsInfo.ReadOnly = !permission;
            labelRemark.Visible = permission;

        }

        #endregion

        #region public void SaveData()

        /// <summary>
        /// �������� ������ ��������
        /// </summary>
        public void SaveData()
        {
            if (currentDetail != null)
                SaveData(currentDetail);
        }

        #endregion

        #region public void SaveData(TemplateAbstractDetail destinationDetail)

        /// <summary>
        /// �������� ������ ��������
        /// </summary>
        /// <param name="destinationDetail">������� � �������� �������� ������</param>
        public void SaveData(TemplateAbstractDetail destinationDetail)
        {
            if (destinationDetail == null) throw new ArgumentNullException("destinationDetail");

            maxResourcesInfo.LifeLengthViewers[0].SaveData(destinationDetail.Limitation.MaxResourceSinceNew);
            maxResourcesInfo.LifeLengthViewers[1].SaveData(destinationDetail.Limitation.MaxResourceSinceOverhaul);
            maxResourcesInfo.LifeLengthViewers[2].SaveData(destinationDetail.Limitation.MaxResourceSinceInspection);
            maxResourcesInfo.LifeLengthViewers[3].SaveData(destinationDetail.Limitation.MaxResourceSinceShopVisit);
            maxResourcesInfo.LifeLengthViewers[4].SaveData(destinationDetail.Limitation.MaxResourceSinceHotSectionInspection);

            notifyWhenRemainsInfo.LifeLengthViewers[0].SaveData(destinationDetail.Limitation.NotificationSinceNew);
            notifyWhenRemainsInfo.LifeLengthViewers[1].SaveData(destinationDetail.Limitation.NotificationSinceOverhaul);
            notifyWhenRemainsInfo.LifeLengthViewers[2].SaveData(destinationDetail.Limitation.NotificationSinceInspection);
            notifyWhenRemainsInfo.LifeLengthViewers[3].SaveData(destinationDetail.Limitation.NotificationSinceShopVisit);
            notifyWhenRemainsInfo.LifeLengthViewers[4].SaveData(destinationDetail.Limitation.NotificationSinceHotSectionInspection);
        }

        #endregion

        #region public void ClearFields()

        /// <summary>
        /// ������� ��� ����
        /// </summary>
        public void ClearAllFields()
        {
            SinceNewLimitation = new Lifelength();
            SinceLastOverhaulLimitation = new Lifelength();
            SinceLastInspectionLimitation = new Lifelength();
            SinceLastShopVisitLimitation = new Lifelength();
            SinceLastHotInspectionLimitation = new Lifelength();

            SinceNewNotification = new Lifelength();
            SinceLastOverhaulNotification = new Lifelength();
            SinceLastInspectionNotification = new Lifelength();
            SinceLastShopVisitNotification = new Lifelength();
            SinceLastHostInspectionNotification = new Lifelength();
        }

        #endregion

        #region private void maxResourcesInfo_LastLifelengthViewerTabClicked(int position)

        private void maxResourcesInfo_LastLifelengthViewerTabClicked(int position)
        {
            notifyWhenRemainsInfo.FocusFirstElement(position);
        }

        #endregion

        #region private void notifyWhenRemainsInfo_LastLifelengthViewerTabClicked(int position)

        private void notifyWhenRemainsInfo_LastLifelengthViewerTabClicked(int position)
        {
            //if ((notifyWhenRemainsInfo.LifeLengthViewers[4].Visible && position < 4) || (!notifyWhenRemainsInfo.LifeLengthViewers[4].Visible && position < 3))
                maxResourcesInfo.FocusFirstElement(++position);
        }

        #endregion

        #region private void maxResourcesInfo_FirstLifelengthViewerShiftTabClicked(int position)

        private void maxResourcesInfo_FirstLifelengthViewerShiftTabClicked(int position)
        {
            if (position > 0)
                notifyWhenRemainsInfo.FocusLastElement(--position);
        }

        #endregion

        #region private void notifyWhenRemainsInfo_FirstLifelengthViewerShiftTabClicked(int position)

        private void notifyWhenRemainsInfo_FirstLifelengthViewerShiftTabClicked(int position)
        {
            maxResourcesInfo.FocusLastElement(position);
        }

        #endregion

        #region private void maxResourcesInfo_LifelengthViewerLostFocus(int position)

        private void maxResourcesInfo_LifelengthViewerLostFocus(int position)
        {
            if (!maxResourcesInfo.LifeLengthViewers[position].HoursApplicable &&
                !maxResourcesInfo.LifeLengthViewers[position].CyclesApplicable &&
                !maxResourcesInfo.LifeLengthViewers[position].CalendarApplicable)
            {
                if (maxResourcesInfo.LifeLengthViewers[position].HoursApplicable)
                    notifyWhenRemainsInfo.LifeLengthViewers[position].Hours = new TimeSpan((int)(0.1 * maxResourcesInfo.LifeLengthViewers[position].Hours.Hours), 0, 0);
            }
        }

        #endregion

        #endregion

    }
}