using LTR.Core;
using LTR.Core.Types.Aircrafts.Parts;
using LTR.Core.Types.Dictionaries;
using LTR.Core.Types.ReportFilters;
using LTR.UI.Interfaces;
using LTRReports;
using LTRReports.Builders;

namespace LTR.UI.Management.Dispatchering.DispatcheredUIControls.DetailsControl
{
    /// <summary>
    /// �����, ����������� ����������� SSIDStatus �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredSSIDStatusView : DispatcheredDirectivesView
    {
        /// <summary>
        /// ��������� ������� - ����������� SSIDStatus �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������������ ������</param>
        public DispatcheredSSIDStatusView(BaseDetail currentItem)
            : base(currentItem)
        {
        }

        /// <summary>
        /// ����������� �������� SSID ��� ����� ��
        /// </summary>
        /// <param name="aircraft"></param>
        public DispatcheredSSIDStatusView(Aircraft aircraft):base(aircraft.AircraftFrame)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return "SSID status"; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override DirectiveListReportBuilder CreateReportBuilder()
        {
            return new SSIDReportBuilder();
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.GetByID((int)DirectiveTypeID.SSIDDirectiveTypeID); }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override DirectiveFilter[] GetCollectionFilters()
        {
            return new DirectiveFilter[1] { new SSIDStatusFilter() };
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredSSIDStatusView);
        }
    }
}
