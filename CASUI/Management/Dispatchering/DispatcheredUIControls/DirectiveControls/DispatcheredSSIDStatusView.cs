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
using CASReports.Builders;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.DirectiveControls
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
        public DispatcheredSSIDStatusView(Aircraft aircraft):base(aircraft, null)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return DirectiveTypeCollection.Instance.SSIDDirectiveType.CommonName; }
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
            get { return DirectiveTypeCollection.Instance.SSIDDirectiveType; }
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