/*using CASReports.Builders;
using CASReports.ReportTemplates;
using CAS.Core.Types.ReportFilters;

namespace CASReports.Builders
{
    /// <summary>
    /// ���������� ������ AD Status
    /// </summary>
    public class SSIDReportBuilder : DirectiveListReportBuilder
    {

        #region Fields

        private readonly SSIDStatusFilter defaultFilter = new SSIDStatusFilter();

        #endregion

        #region Constructor

        /// <summary>
        /// ��������� ������ ��� �������� �������. ���������� ������
        /// </summary>
        public SSIDReportBuilder()
        {
            ReportTitle = "SSID status";
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// ������ �� ���������
        /// </summary>
        public override DirectiveFilter DefaultFilter
        {
            get
            {
                return defaultFilter;
            }
        }
        #endregion

        #region Methods

        #region public override object GenerateReport()

        /// <summary>
        /// �������������� ����� �� ������, ����������� � ������� ������
        /// </summary>
        /// <returns>����������� �����</returns>
        public override object GenerateReport()
        {
            ADReport report = new ADReport();
            report.SetDataSource(GenerateDataSet());
            return report;
        }

        #endregion

        #endregion
    }
}*/