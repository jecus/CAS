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
    /// �����, ����������� ����������� OutOffPhase �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredOutOffPhaseView : DispatcheredDirectivesView
    {
        /// <summary>
        /// ��������� ������� - ����������� OutOffPhase �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������������ ������</param>
        public DispatcheredOutOffPhaseView(BaseDetail currentItem)
            : base(currentItem)
        {
        }

        /// <summary>
        /// ����������� �������� ��� ����� ��
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredOutOffPhaseView(Aircraft currentItem) : base(currentItem, null)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return DirectiveTypeCollection.Instance.OutOffPhaseDirectiveType.CommonName; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override DirectiveListReportBuilder CreateReportBuilder()
        {
            return new OutOffPhaseReportBuilder();
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.OutOffPhaseDirectiveType; }
        }

        /// <summary>
        /// SDList ��� ��������
        /// </summary>
        public override SDList<BaseDetailDirective> DirectiveListView
        {
            get { return new OutOffPhaseReferencesListView(); }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override DirectiveFilter[] GetCollectionFilters()
        {
            return new DirectiveFilter[1] { new OutOffPhaseFilter() };
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredOutOffPhaseView);
        }
    }
}