using System.Windows.Forms;
using CASReports.Builders;
using CASReports.ReportTemplates;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.Reports
{
    /// <summary>
    /// �����, ����������� ����������� ������� Maintenance Status
    /// </summary>
    public partial class DispatcheredMaintenanceJobCardReport : UserControl, IDisplayingEntity
    {
        private readonly MaintenanceJobCardBuilder builder;

        /// <summary>
        /// ��������� ����������� ������� Maintenance Status
        /// </summary>
        protected DispatcheredMaintenanceJobCardReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��������� ����������� ������� Maintenance Status
        /// </summary>
        public DispatcheredMaintenanceJobCardReport(MaintenanceJobCardBuilder builder) : this()
        {
            this.builder = builder;
            crystalReportViewer1.ReportSource = builder.GenerateReport();
            Dock = DockStyle.Fill;
        }

        #region IDisplayingEntity Members

        /// <summary>
        /// Represents data being displayed
        /// </summary>
        public object ContainedData
        {
            get { return builder; }
            set { }
        }

        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        public bool ContainedDataEquals(IDisplayingEntity obj)
        {
            if (!(obj is DispatcheredMaintenanceJobCardReport))
            {
                return false;
            }
            return ((obj as DispatcheredMaintenanceJobCardReport).builder.JobCard == builder.JobCard);
        }

        /// <summary>
        /// ���������� ������� �������� ������������� �������
        /// </summary>
        /// <param name="arguments"></param>
        public void OnDisplayerRemoving(DisplayerCancelEventArgs arguments)
        {
            
        }

        /// <summary>
        /// ��������, ������������ ��� ����������� �������, ���������� ������ ��������
        /// </summary>
        /// <param name="arguments"></param>
        public void OnDisplayerDeselecting(DisplayerCancelEventArgs arguments)
        {
            
        }

        public void SetEnabled(bool isEnbaled)
        {
            
        }

        #endregion
    }
}