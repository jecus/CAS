using System.Windows.Forms;
using CASReports.Builders;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.Reports
{
    /// <summary>
    /// �����, ����������� ����������� ������ Release To Service
    /// </summary>
    public partial class DispatcheredATLBReport : UserControl, IDisplayingEntity
    {

        #region Fields

        private readonly ATLBBuilder builder;

        #endregion

        #region Constructors

        /// <summary>
        /// ��������� ����������� ������ �� ��������� ������� ��
        /// </summary>
        protected DispatcheredATLBReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��������� ����������� ������ �� ��������� ������� ��
        /// </summary>
        public DispatcheredATLBReport(ATLBBuilder builder) : this()
        {
            this.builder = builder;
            crystalReportViewer1.ReportSource = builder.GenerateReport();
            Dock = DockStyle.Fill;
        }

        #endregion

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
            if (!(obj is DispatcheredATLBReport))
            {
                return false;
            }
            return ((obj as DispatcheredATLBReport).builder.ATLB == builder.ATLB);
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