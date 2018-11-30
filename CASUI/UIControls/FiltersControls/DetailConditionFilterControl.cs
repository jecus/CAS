using CAS.Core.Types.ReportFilters;
using CAS.UI.UIControls.FiltersControls;

namespace CAS.UI.UIControls.FiltersControls
{
    ///<summary>
    /// �����, ����������� ������� ����������� ������� ���������
    ///</summary>
    public class DetailConditionFilterControl:ConditionStatusControl
    {
        ///<summary>
        /// �������� ������� �� ��������� ���������
        ///</summary>
        ///<returns>��������� ������</returns>
        public override IFilter GetFilter()
        {
            return CreateDetailFilter();
        }

        ///<summary>
        /// �������� ������� �� ��������� ���������
        ///</summary>
        ///<returns>��������� ������</returns>
        public DetailConditionFilter CreateDetailFilter()
        {
            DetailConditionFilter filter = new DetailConditionFilter(SatisfactoryAppliance, UnsatisfactoryAppliance, NotificationAppliance);
            return filter;
        }

        public override void SetFilterParameters(IFilter filter)
        {
        }
    }
}