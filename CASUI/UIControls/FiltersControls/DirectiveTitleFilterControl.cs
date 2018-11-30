using CAS.Core.Types.ReportFilters;
using CAS.UI.UIControls.FiltersControls;

namespace CAS.UI.UIControls.FiltersControls
{
    /// <summary>
    /// ������ �������� �� ��������
    /// </summary>
    public class DirectiveTitleFilterControl : AbstractDirectiveTextFilterControl
    {

        #region Methods

        #region public override IFilter GetFilter()

        ///<summary>
        /// �������� ������� �� ��������� ���������
        ///</summary>
        ///<returns>��������� ������</returns>
        public override IFilter GetFilter()
        {
            return new DirectiveTitleFilter(Mask);
        }

        #endregion

        #region public override void SetFilterParameters(IFilter filter)

        /// <summary>
        /// �������� ��������� �������
        /// </summary>
        /// <param name="filter">�������� ���������� �������</param>
        public override void SetFilterParameters(IFilter filter)
        {
            if (filter is DirectiveTitleFilter)
            {
                DirectiveTitleFilter titleFilter = (DirectiveTitleFilter)filter;
                Mask = titleFilter.Mask;
                SerialFilterAppliance = true;
            }

        }

        #endregion

        #endregion


    }
}