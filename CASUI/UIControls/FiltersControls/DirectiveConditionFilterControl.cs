using CAS.Core.Types.ReportFilters;
using CAS.UI.UIControls.FiltersControls;

namespace CAS.UI.UIControls.FiltersControls
{
    ///<summary>
    /// �����, ����������� ������� ����������� ������� ���������
    ///</summary>
    public class DirectiveConditionFilterControl:ConditionStatusControl
    {
        ///<summary>
        /// �������� ������� �� ��������� ���������
        ///</summary>
        ///<returns>��������� ������</returns>
        public override IFilter GetFilter()
        {
            return CreateDirectiveFilter();
        }

        ///<summary>
        ///
        /// �������� ��������� �������
        /// 
        ///</summary>
        ///
        ///<param name="filter">�������� ���������� �������</param>
        public override void SetFilterParameters(IFilter filter)
        {
            if (filter is DirectiveConditionFilter)
            {
                DirectiveConditionFilter directiveConditionFilter = (DirectiveConditionFilter) filter;
                SatisfactoryAppliance = directiveConditionFilter.SatisfactoryAcceptance;
                NotificationAppliance = directiveConditionFilter.NotificationAcceptance;
                UnsatisfactoryAppliance = directiveConditionFilter.NotSatisfactoryAcceptance;
            }
        }

        ///<summary>
        /// �������� ������� �� ��������� ���������
        ///</summary>
        ///<returns>��������� ������</returns>
        public DirectiveConditionFilter CreateDirectiveFilter()
        {
            DirectiveConditionFilter filter = new DirectiveConditionFilter(SatisfactoryAppliance, UnsatisfactoryAppliance, NotificationAppliance);
            return filter;
        }

        
    }
}