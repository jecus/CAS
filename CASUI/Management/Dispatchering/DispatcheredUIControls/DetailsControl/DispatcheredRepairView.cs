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
    /// �����, ����������� ����������� Repair �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredRepairView : DispatcheredDirectivesView
    {
        /// <summary>
        /// ��������� ������� - ����������� Repair �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������������ ������</param>
        public DispatcheredRepairView(BaseDetail currentItem)
            : base(currentItem)
        {
        }
        
        /// <summary>
        /// ����������� �������� ��� ����� ��
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredRepairView(Aircraft currentItem)
            : base(currentItem.AircraftFrame)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return "Repair status"; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override DirectiveListReportBuilder CreateReportBuilder()
        {
            return new RepairStatusReportBuilder();
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.GetByID((int)DirectiveTypeID.RepairDirectiveTypeID); }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override DirectiveFilter[] GetCollectionFilters()
        {
            return new DirectiveFilter[1] { new RepairFilter() };
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredRepairView);
        }
    }
}
