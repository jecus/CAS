using System.ComponentModel;
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
    /// �����, ����������� ����������� ���� �������� ��������� �������� ��������
    /// </summary>
    [ToolboxItem(false)]
    public class DispatcheredBaseDetailDirectivesView : DispatcheredDirectivesView
    {
        #region public DispatcheredBaseDetailDirectivesView(BaseDetail currentItem):base(currentItem)

        /// <summary>
        /// ��������� ������� - ����������� ���� �������� ��������� �������� ��������
        /// </summary>
        public DispatcheredBaseDetailDirectivesView(BaseDetail currentItem):base(currentItem)
        {
        }

        #endregion

        /// <summary>
        /// ����������� �������� ��� ����� ��
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredBaseDetailDirectivesView(Aircraft currentItem)
            : base(currentItem)
        {
        }

        #region protected override bool IsSameType(IDisplayingEntity obj)

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredBaseDetailDirectivesView);
        }

        #endregion

        #region protected override DirectiveFilter[] GetCollectionFilters()

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return "ContainedDirectives list"; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override DirectiveListReportBuilder CreateReportBuilder()
        {
            return new DirectiveListReportBuilder();//throw new System.NotImplementedException();
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.GetByID((int)DirectiveTypeID.UsualBaseDetailDirectiveTypeID); }
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
            return new DirectiveFilter[1] { new AllDirectiveFilter() };
        }

        #endregion

    }
}