using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.Directives;
using CAS.UI.UIControls.Auxiliary;
using CAS.UI.UIControls.DirectivesControls;
using CASReports.Builders;
using CAS.Core.Types.Aircrafts.Parts;
using CAS.Core.Types.Dictionaries;
using CAS.Core.Types.ReportFilters;
using CAS.UI.Interfaces;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.DirectiveControls
{
    /// <summary>
    /// �����, ����������� ����������� Modifications �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredModificationsView : DispatcheredDirectivesView
    {
        /// <summary>
        /// ��������� ������� - ����������� Modifications �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������������ ������</param>
        public DispatcheredModificationsView(BaseDetail currentItem)
            : base(currentItem)
        {
        }

        /// <summary>
        /// ����������� �������� ��� ����� ��
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredModificationsView(Aircraft currentItem)
            : base(currentItem)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return DirectiveTypeCollection.Instance.ModificationDirectiveType.CommonName; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override DirectiveListReportBuilder CreateReportBuilder()
        {
            return new ModificationStatusReportBuilder();
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.ModificationDirectiveType; }
        }

        /// <summary>
        /// SDList ��� ��������
        /// </summary>
        public override SDList<BaseDetailDirective> DirectiveListView
        {
            get { return new DirectiveListView(); }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override DirectiveFilter[] GetCollectionFilters()
        {
            return new DirectiveFilter[1] { new ModificationFilter() };
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredModificationsView);
        }
    }
}