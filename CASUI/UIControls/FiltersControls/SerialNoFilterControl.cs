using System.Windows.Forms;
using CAS.Core.Types.ReportFilters;

namespace CAS.UI.UIControls.FiltersControls
{
    /// <summary>
    /// �����, ����������� ������� ����������� ������� �� Serial number 
    /// </summary>
    public partial class SerialNoFilterControl : UserControl, IFilterControl
    {
        #region Fields
        #endregion

        #region Constructors

        #region public SerialNoFilterControl()
        /// <summary>
        /// ��������� ������� ����������� ������� �� Serial number 
        /// </summary>
        public SerialNoFilterControl()
        {
            InitializeComponent();
        }
        #endregion

        #endregion

        #region Properties
        ///<summary>
        /// ������������ �������
        ///</summary>
        public bool FilterAppliance
        {
            get { return checkBoxSerialFilterAppliance.Checked; }
            set 
            {
                checkBoxSerialFilterAppliance.Checked = value;
            }
        }

        /// <summary>
        /// �������� ������������ �������
        /// </summary>
        public CheckBox CheckBoxSerialFilterAppliance
        {
            get { return checkBoxSerialFilterAppliance; }
        }

        /// <summary>
        /// ���� ����� ����� 
        /// </summary>
        public TextBox TextBoxSerialMask
        {
            get { return textBoxSerialMask; }
        }
        #endregion

        #region Methods

        ///<summary>
        /// �������� ������� �� ��������� ���������
        ///</summary>
        ///<returns>��������� ������</returns>
        public IFilter GetFilter()
        {
            return new Core.Types.ReportFilters.SerialNumberFilter(textBoxSerialMask.Text);
        }

        public void SetFilterParameters(IFilter filter)
        {
            
        }

        private void checkBoxSerialFilterAppliance_CheckedChanged(object sender, System.EventArgs e)
        {
            if (FilterAppliance) textBoxSerialMask.Focus();
        }
        #endregion

        private void textBoxSerialMask_TextChanged(object sender, System.EventArgs e)
        {
            checkBoxSerialFilterAppliance.Checked = textBoxSerialMask.Text != "";
        }
    }
}