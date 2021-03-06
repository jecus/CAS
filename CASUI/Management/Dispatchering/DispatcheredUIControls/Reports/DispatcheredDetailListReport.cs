using System.Windows.Forms;
using CAS.UI.Interfaces;
using CASReports.Builders;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.Reports
{
    /// <summary>
    /// ������� ���������� ��� ����������� ������ ��� ���������
    /// </summary>
    public partial class DispatcheredDetailListReport : UserControl ,IDisplayingEntity
    {
        #region Fields

        DetailListReportBuilder reportBuilder;

        #endregion
        
        #region Constructors

        /// <summary>
        /// ��������� ������� ���������� ��� ����������� ������ ��� ���������
        /// </summary>
        protected DispatcheredDetailListReport()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        #region public DispatcheredBaseDetailLogBookReport(LifelengthIncrement[] lifelengthIncrements, BaseDetailLogBookReportBuilder builder) : this()

        /// <summary>
        /// ��������� ����� �� ��
        /// </summary>
        /// <param name="builder"></param>
        public DispatcheredDetailListReport(DetailListReportBuilder builder)
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
        public DetailListReportBuilder ReportBuilder
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
            set {  }
        }

        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        public bool ContainedDataEquals(IDisplayingEntity obj)
        {
            if (!(obj is DispatcheredDetailListReport))
                return false;
            return ((obj as DispatcheredDetailListReport).reportBuilder == reportBuilder);
                
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
