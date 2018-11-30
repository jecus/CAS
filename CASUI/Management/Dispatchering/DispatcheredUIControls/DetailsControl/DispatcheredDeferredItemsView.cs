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
    /// �����, ����������� ����������� DeferredItems �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredDeferredItemsView : DispatcheredDirectivesView
    {
        /// <summary>
        /// ��������� ������� - ����������� DeferredItems �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������������ ������</param>
        public DispatcheredDeferredItemsView(BaseDetail currentItem)
            : base(currentItem)
        {
        }

        /// <summary>
        /// ����������� �������� ��� ����� ��
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredDeferredItemsView(Aircraft currentItem)
            : base(currentItem)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return "Deffered items status"; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override DirectiveListReportBuilder CreateReportBuilder()
        {
            return new DefferedItemsReportBuilder();
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.GetByID((int)DirectiveTypeID.DeferredItemsDirectiveTypeID); }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override DirectiveFilter[] GetCollectionFilters()
        {
            return new DirectiveFilter[1] { new DeferredItemsFilter() };
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredDeferredItemsView);
        }
    }
}
