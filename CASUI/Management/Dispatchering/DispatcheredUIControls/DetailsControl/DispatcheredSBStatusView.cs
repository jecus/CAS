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
    /// �����, ����������� ����������� SBStatus �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredSBStatusView : DispatcheredDirectivesView
    {
        /// <summary>
        /// ��������� ������� - ����������� SBStatus �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������������ ������</param>
        public DispatcheredSBStatusView(BaseDetail currentItem)
            : base(currentItem)
        {
        }

        /// <summary>
        /// ����������� �������� ��� ����� ��
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredSBStatusView(Aircraft currentItem)
            : base(currentItem.AircraftFrame)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return "SB status"; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override DirectiveListReportBuilder CreateReportBuilder()
        {
            return new SBReportBuilder();
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.GetByID((int)DirectiveTypeID.SBDirectiveTypeID); }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override DirectiveFilter[] GetCollectionFilters()
        {
            return new DirectiveFilter[1] { new SBStatusFilter() };
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredSBStatusView);
        }
    }
}
