using CAS.Core.Types.ReportFilters;

namespace CAS.UI.UIControls.FiltersControls
{
    ///<summary>
    /// ��������� ����������� ������� ����������� �������
    ///</summary>
    public interface IFilterControl
    {
        ///<summary>
        /// �������� ������� �� ��������� ���������
        ///</summary>
        ///<returns>��������� ������</returns>
        IFilter GetFilter();

        /// <summary>
        /// �������� ��������� �������
        /// </summary>
        /// <param name="filter">�������� ���������� �������</param>
        void SetFilterParameters(IFilter filter);

        #region public bool FilterAppliance
        ///<summary>
        /// ������������ �������
        ///</summary>
        bool FilterAppliance
        {
            get;
            set;
        }
        #endregion
    }
}