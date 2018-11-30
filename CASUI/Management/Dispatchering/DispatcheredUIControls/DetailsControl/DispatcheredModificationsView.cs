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
            get { return "Modifications status"; }
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
            get { return DirectiveTypeCollection.Instance.GetByID((int)DirectiveTypeID.ModificationDirectiveTypeID); }
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
