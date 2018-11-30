using CAS.Core.Types.Aircrafts;
using CASReports.Builders;
using CAS.Core.Types.Aircrafts.Parts;
using CAS.Core.Types.Dictionaries;
using CAS.Core.Types.ReportFilters;
using CAS.UI.Interfaces;
using CASReports.Builders;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.DirectiveControls
{
    /// <summary>
    /// �����, ����������� ����������� EngeneeringOrders �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredEngeneeringOrdersView : DispatcheredDirectivesView
    {
        /// <summary>
        /// ��������� ������� - ����������� EngeneeringOrders �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������������ ������</param>
        public DispatcheredEngeneeringOrdersView(BaseDetail currentItem)
            : base(currentItem)
        {
        }

        /// <summary>
        /// ����������� �������� ��� ����� ��
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredEngeneeringOrdersView(Aircraft currentItem)
            : base(currentItem)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return DirectiveTypeCollection.Instance.EngineeringOrdersDirectiveType.CommonName; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override DirectiveListReportBuilder CreateReportBuilder()
        {
            return new EngineeringOrdersReportBuilder();
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.EngineeringOrdersDirectiveType; }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override DirectiveFilter[] GetCollectionFilters()
        {
            return new DirectiveFilter[1] { new EngeneeringOrderFilter() };
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredEngeneeringOrdersView);
        }
    }
}