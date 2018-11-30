using System;
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
        public DispatcheredSBStatusView(Aircraft currentItem) : base(currentItem, null)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return DirectiveTypeCollection.Instance.SBDirectiveType.CommonName; }
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
            get { return DirectiveTypeCollection.Instance.SBDirectiveType; }
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