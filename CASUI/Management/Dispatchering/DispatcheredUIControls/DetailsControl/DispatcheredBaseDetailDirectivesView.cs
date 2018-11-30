using System.ComponentModel;
using LTR.Core;
using LTR.Core.Types.Aircrafts.Parts;
using LTR.Core.Types.Dictionaries;
using LTR.Core.Types.ReportFilters;
using LTR.UI.Interfaces;
using LTRReports.Builders;

namespace LTR.UI.Management.Dispatchering.DispatcheredUIControls.DetailsControl
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
            get { return "Directives list"; }
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
