using System;
using System.Windows.Forms;
using CAS.Core.Types.Dictionaries;
using CAS.Core.Types.ReportFilters;

namespace CAS.UI.UIControls.FiltersControls
{
    ///<summary>
    /// �����, ����������� ����������� ������� �� ������������ ������������
    ///</summary>
    public partial class MaintenanceFilterControl : UserControl, IFilterControl
    {
        private CheckBox[] checkBoxes = new CheckBox[4];
        ///<summary>
        ///</summary>
        public MaintenanceFilterControl()
        {
            InitializeComponent();
            checkBoxes[0] = checkBoxConditionMonitoring;
            checkBoxes[1] = checkBoxHardTime;
            checkBoxes[2] = checkBoxOnCondition;
            checkBoxes[3] = checkBoxUnknown;
        }

        /// <summary>
        /// ��������� ������� ����������� ������� �� ���� ������������ ������������
        /// </summary>
        /// <param name="maintenanceTypes"></param>
        public MaintenanceFilterControl(MaintenanceTypeCollection maintenanceTypes):this()
        {
            checkBoxConditionMonitoring.Text = maintenanceTypes[0].ShortName;
            checkBoxHardTime.Text = maintenanceTypes[1].ShortName;
            checkBoxOnCondition.Text = maintenanceTypes[2].ShortName;
            checkBoxUnknown.Text = maintenanceTypes[3].ShortName;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AcceptUnknown
        {
            get { return checkBoxUnknown.Checked; }
            set { checkBoxUnknown.Checked = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool AcceptHardTime
        {
            get { return checkBoxHardTime.Checked; }
            set { checkBoxHardTime.Checked = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool AcceptOnCondition
        {
            get { return checkBoxOnCondition.Checked; }
            set { checkBoxOnCondition.Checked = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool AcceptConditionMonitoring
        {
            get { return checkBoxConditionMonitoring.Checked; }
            set { checkBoxConditionMonitoring.Checked = value; }
        }

        ///<summary>
        /// �������� ������� �� ��������� ���������
        ///</summary>
        ///<returns>��������� ������</returns>
        public IFilter GetFilter()
        {
            return new MaintananceFilter(AcceptConditionMonitoring, AcceptUnknown, AcceptOnCondition, AcceptHardTime);
        }

        public void SetFilterParameters(IFilter filter)
        {
            
        }

        ///<summary>
        /// ������������ �������
        ///</summary>
        public bool FilterAppliance
        {
            get
            {
                for (int i = 0; i < checkBoxes.Length; i++)
                {
                    if (!checkBoxes[i].Checked) return false;
                }
                return true;
            }
            set 
            {
            }
        }

        private void MaintananceFilterControl_Load(object sender, EventArgs e)
        {

        }
    }
}