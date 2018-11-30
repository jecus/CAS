using System.Windows.Forms;
using CASReports.Builders;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.Reports
{
    /// <summary>
    /// �����, ����������� ����������� ������ Release To Service
    /// </summary>
    public partial class DispatcheredWorkPackageReleaseToServiceReport : UserControl, IDisplayingEntity
    {
        private readonly WorkPackageReleaseToServiceBuilder builder;

        /// <summary>
        /// ��������� ����������� ������ Release To Service
        /// </summary>
        protected DispatcheredWorkPackageReleaseToServiceReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��������� ����������� ������ Release To Service
        /// </summary>
        public DispatcheredWorkPackageReleaseToServiceReport(WorkPackageReleaseToServiceBuilder builder) : this()
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
            if (!(obj is DispatcheredWorkPackageReleaseToServiceReport))
            {
                return false;
            }
            return ((obj as DispatcheredWorkPackageReleaseToServiceReport).builder.WorkPackage == builder.WorkPackage);
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