using System.Windows.Forms;
using CAS.Core.Types.ReportFilters;

namespace CAS.UI.UIControls.FiltersControls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LandingGearFilterControl : UserControl, IFilterControl
    {
        /// <summary>
        /// 
        /// </summary>
        public LandingGearFilterControl()
        {
            InitializeComponent();
        }

        #region IFilterControl Members

        ///<summary>
        /// �������� ������� �� ��������� ���������
        ///</summary>
        ///<returns>��������� ������</returns>
        public IFilter GetFilter()
        {
            return new LandingGearsFilter(checkBoxGeneral.Checked, checkBoxLeft.Checked, checkBoxRigth.Checked, true);
        }

        ///<summary>
        /// ������������ �������
        ///</summary>
        public bool FilterAppliance
        {
            get
            {
                return true;
            }
            set { }
        }

        #endregion
    }
}