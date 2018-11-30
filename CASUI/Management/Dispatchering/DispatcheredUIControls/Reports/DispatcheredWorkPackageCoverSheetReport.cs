using System.Windows.Forms;
using CAS.UI.Interfaces;
using CASReports.Builders;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.Reports
{
    /// <summary>
    /// ������� ���������� ��� ����������� ������ ��� ���������
    /// </summary>
    public partial class DispatcheredWorkPackageCoverSheetReport : UserControl, IDisplayingEntity
    {
        #region Fields

        WorkPackageCoverSheetBuilder reportBuilder;

        #endregion

        #region Constructors

        /// <summary>
        /// ��������� ������� ���������� ��� ����������� ������ ��� ���������
        /// </summary>
        protected DispatcheredWorkPackageCoverSheetReport()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        #region public DispatcheredBaseDetailLogBookReport(LifelengthIncrement[] lifelengthIncrements, BaseDetailLogBookReportBuilder builder) : this()

        /// <summary>
        /// ��������� ����� �� ��
        /// </summary>
        /// <param name="builder"></param>
        public DispatcheredWorkPackageCoverSheetReport(WorkPackageCoverSheetBuilder builder)
            : this()
        {
            reportBuilder = builder;
            
            crystalReportViewer1.ReportSource = reportBuilder.GenerateReport();
            }

        #endregion


        #endregion

        #region Properties

        #region public BaseDetailLogBookReportBuilder ReportBuilder

        /// <summary>
        /// ����������� ������� ��� ����������� ���������� �����
        /// </summary>
        public WorkPackageCoverSheetBuilder ReportBuilder
        {
            get
            {
                return reportBuilder;
            }
            set
            {
                reportBuilder = value;
            }
        }

        #endregion

        #endregion

        #region IDisplayingEntity Members

        /// <summary>
        /// Represents data being displayed
        /// </summary>
        public object ContainedData
        {
            get { return reportBuilder; }
            set { }
        }

        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        public bool ContainedDataEquals(IDisplayingEntity obj)
        {
            if (!(obj is DispatcheredWorkPackageCoverSheetReport))
                return false;
            return ((obj as DispatcheredWorkPackageCoverSheetReport).reportBuilder == reportBuilder);

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
