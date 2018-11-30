using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CAS.Core.Types.ATLBs;
using CAS.Core.Types.Directives;
using CAS.Core.Types.Dictionaries;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{
    /// <summary>
    /// ������� ������� ���������� � ����� ���������� �����
    /// </summary>
    public partial class MaintenanceProgramControl : CAS.UI.Interfaces.EditObjectControl
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
            if (Flight != null && Flight.Aircraft != null)
            {
                //if (Flight.Aircraft.MaintenanceDirective.Limitations[0].CheckType == MaintenanceCheckTypesCollection.Instance.
            }
            else
            {
            }
            EndUpdate();
        }
        #endregion

        
    }
}

