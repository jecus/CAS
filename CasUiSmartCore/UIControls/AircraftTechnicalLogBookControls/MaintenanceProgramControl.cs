using SmartCore.Entities.General.Atlbs;


namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{
    /// <summary>
    /// ������� ������� ���������� � ����� ���������� �����
    /// </summary>
    public partial class MaintenanceProgramControl : Interfaces.EditObjectControl
    {

        #region public AircraftFlight Flight
        /// <summary>
        /// �����, � ������� ������ �������
        /// </summary>
        public AircraftFlight Flight
        {
            get { return AttachedObject as AircraftFlight; }
        }
        #endregion

        /*
         * �����������
         */

        #region public MaintenanceProgramControl()
        /// <summary>
        /// ������ �����������
        /// </summary>
        public MaintenanceProgramControl()
        {
            InitializeComponent();
        }
        #endregion

        /*
         * ������������� ������
         */

        #region public override void FillControls()
        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        public override void FillControls()
        {
            BeginUpdate();
            if (Flight != null && Flight.AircraftId > 0)
            {
                //if (Flight.Aircraft.MaintenanceDirective.Limitations[0].CheckType == MaintenanceCheckTypesCollection.Instance.
            }
            EndUpdate();
        }
        #endregion

        
    }
}

