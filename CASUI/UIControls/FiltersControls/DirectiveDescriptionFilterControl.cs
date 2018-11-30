using System;
using System.Collections.Generic;
using System.Text;
using CAS.Core.Types.ReportFilters;
using CAS.UI.UIControls.FiltersControls;

namespace CAS.UI.UIControls.FiltersControls
{
    public class DirectiveDescriptionFilterControl : AbstractDirectiveTextFilterControl
    {
        public DirectiveDescriptionFilterControl()
        {
            FilterTitle = "Requirment filter";
        }

        #region Methods

        #region public override IFilter GetFilter()

        ///<summary>
        /// �������� ������� �� ��������� ���������
        ///</summary>
        ///<returns>��������� ������</returns>
        public override IFilter GetFilter()
        {
            return new DirectiveDescriptionFilter(Mask);
        }

        #endregion

        #region public override void SetFilterParameters(IFilter filter)

        /// <summary>
        /// �������� ��������� �������
        /// </summary>
        /// <param name="filter">�������� ���������� �������</param>
        public override void SetFilterParameters(IFilter filter)
        {
            if (filter is DirectiveDescriptionFilter)
            {
                DirectiveDescriptionFilter titleFilter = (DirectiveDescriptionFilter)filter;
                Mask = titleFilter.Mask;
                SerialFilterAppliance = true;
            }

        }

        #endregion
        #endregion
    }
}