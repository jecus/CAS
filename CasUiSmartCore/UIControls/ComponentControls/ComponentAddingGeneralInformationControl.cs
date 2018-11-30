using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CAS.UI.UIControls.Auxiliary.Events;
using CAS.UI.UIControls.DetailsControls;
using CASTerms;
using SmartCore.Calculations;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Attributes;
using SmartCore.Entities.General.Store;
using Component = SmartCore.Entities.General.Accessory.Component;

namespace CAS.UI.UIControls.ComponentControls
{
    ///<summary>
    /// ��������� ���� ��� �������� ������� ���������� � ���������� 
    ///</summary>
    [Designer(typeof(ComponentAddingGeneralInformationDesigner))]
    public partial class ComponentAddingGeneralInformationControl : UserControl
    {

        #region Fields

        private Component _addedComponent;
        private object _currentAircraft;
        private bool _isStore;

        #endregion

        #region Constructor

        #region public DetailAddingGeneralInformationControl(object parent)
        /// <summary>
        /// ������� ������� ����������, �������������� ��� ������� ����� ���������� ��� ���������� ��������
        /// </summary>
        public ComponentAddingGeneralInformationControl(object parent, Component addedComponent)
        {
            if (addedComponent == null)
                throw new ArgumentNullException("addedComponent");

            _currentAircraft = parent;
            _addedComponent = addedComponent;

            _isStore = parent is Store || parent is Operator;
            InitializeComponent();
            UpdateControl();
        }
        #endregion

        #region public DetailAddingGeneralInformationControl()
        ///<summary>
        ///</summary>
        public ComponentAddingGeneralInformationControl()
        {
            InitializeComponent();
        }
        
        #endregion

        #endregion

        #region Propeties

        #region public Detail AddedDetail

        ///<summary>
        /// ������ ����������� ������
        ///</summary>
        public Component AddedComponent
        {
            set { _addedComponent = value; }
        }
        #endregion

        #region public Detail ParentObject

        ///<summary>
        /// ������ ������������ ������ ��� ����������� ������
        ///</summary>
        public object ParentObject
        {
            set
            {
                if (value == null)
                    _currentAircraft = null;
                else if (value is BaseComponent)
                    _currentAircraft = GlobalObjects.AircraftsCore.GetAircraftById(((BaseComponent) value).ParentAircraftId);
				else
                    _currentAircraft = value;

                _isStore = _currentAircraft is Store || _currentAircraft is Operator;
                UpdateControl();
            }
        }
        #endregion

        #region public string MPDItem

        /// <summary>
        /// MPD Item ������������ ��������
        /// </summary>
        public string MPDItem
        {
            get { return textBoxMPDItem.Text; }
            set
            {
                textBoxMPDItem.Text = value;
            }
        }

        #endregion

        #region public ATAChapter ATAChapter

        /// <summary>
        /// ATA ����� ������������ ��������
        /// </summary>
        public AtaChapter ATAChapter
        {
            get
            {
                return comboBoxAtaChapter.ATAChapter;
            }
            set
            {
                comboBoxAtaChapter.ATAChapter = value;
            }
        }

        #endregion

        #region public MaintenanceControlProcess MaintenanceControlProcess

        /// <summary>
        /// ��� ������������ ��������
        /// </summary>
        public MaintenanceControlProcess MaintenanceControlProcess
        {
            get
            {
                return comboBoxMaintenenceType.SelectedItem as MaintenanceControlProcess;
            }
            set
            {
                comboBoxMaintenenceType.SelectedItem = value;
            }
        }

        #endregion

        #region public string PartNumber

        /// <summary>
        /// ��������� ����� ������������ ��������
        /// </summary>
        public string PartNumber
        {
            get { return textBoxPartNo.Text; }
            set
            {
                textBoxPartNo.Text = value;
            }
        }

        #endregion

        #region public string Code
        /// <summary>
        /// ��� ������������ ��������
        /// </summary>
        public string Code
        {
            get { return textBoxProductCode.Text; }
            set
            {
                textBoxProductCode.Text = value;
            }
        }

		#endregion

		#region public ComponentStorePosition State

		public ComponentStorePosition State
	    {
		    get
		    {
			    return comboBoxStorePosition.SelectedItem as ComponentStorePosition;
		    }
		    set
		    {
			    comboBoxStorePosition.SelectedItem = value;
		    }
	    }

	    #endregion

		#region public string Position

		/// <summary>
		/// ������������ ��������� 
		/// </summary>
		public string Position
		{
			get
			{
				return textBoxPosition.Text;
			}
			set
			{
				textBoxPosition.Text = value;
			}
		}

		#endregion

		#region  private Locations ComponentLocation

		/// <summary>
		/// ����� �� ������
		/// </summary>
		private Locations ComponentLocation
		{
			get
			{
				var location = dictionaryComboBoxLocation.SelectedItem as Locations;
				return location ?? Locations.Unknown;
			}
			set
			{
				dictionaryComboBoxLocation.SelectedItem = value;
			}
		}

		#endregion

		#region public string SerialNumber

		/// <summary>
		/// �������� ����� ������������ ��������
		/// </summary>
		public string SerialNumber
        {
            get { return textBoxSerialNo.Text; }
            set
            {
                textBoxSerialNo.Text = value;
            }
        }

        #endregion

        #region public string BatchNumber

        /// <summary>
        /// �������� ����� ������������ ��������
        /// </summary>
        public string BatchNumber
        {
            get { return textBoxBatchNumber.Text; }
            set
            {
                textBoxBatchNumber.Text = value;
            }
        }

        #endregion

        #region public string IdNumber

        /// <summary>
        /// ����������������� ����� ������������ ��������
        /// </summary>
        public string IdNumber
        {
            get { return textBoxIdNumber.Text; }
            set
            {
                textBoxIdNumber.Text = value;
            }
        }

        #endregion

        #region public string Description

        /// <summary>
        /// �������� ������������ ��������
        /// </summary>
        public string Description
        {
            get { return textBoxDescription.Text; }
            set
            {
                textBoxDescription.Text = value;
            }
        }

        #endregion

        #region private GoodsClass GoodsClass
        /// <summary>
        /// DetailType
        /// </summary>
        private GoodsClass GoodsClass
        {
            get
            {
                GoodsClass type =
                    comboBoxComponentType.SelectedItem as GoodsClass;
                return type ?? GoodsClass.Unknown;
            }
            set { comboBoxComponentType.SelectedItem = value ?? GoodsClass.Unknown; }
        }

        #endregion

        #region public DateTime InstallationDate
        /// <summary>
        /// ���� ��������� �������� �� ��
        /// </summary>
        public DateTime InstallationDate
        {
            get { return dateTimePickerInstallationDate.Value; }
            set
            {
                dateTimePickerInstallationDate.Value = value;
            }
        }

        #endregion

        #region public DateTime ManufactureDate
        /// <summary>
        /// ���� ������������ ����������
        /// </summary>
        public DateTime ManufactureDate
        {
            get { return dateTimePickerManufactureDate.Value; }
            set
            {
                dateTimePickerManufactureDate.Value = value;
            }
        }

        #endregion

        #region public Lifelength ComponentTSNCSN

        /// <summary>
        /// ��������� �������� �� ������ ���������
        /// </summary>
        public Lifelength ComponentTCSNOnInstall
        {
            get { return lifelengthViewerComponentTCSNOnInstall.Lifelength; }
            set
            {
                lifelengthViewerComponentTCSNOnInstall.Lifelength = value;
            }
        }

        #endregion

        #region public Lifelength AircraftTSNCSN

        /// <summary>
        /// ��������� �� �� ������ ��������� ��������
        /// </summary>
        public Lifelength AircraftTCSNOnInstall
        {
            get { return lifelengthViewerAircraftTCSNOnInstall.Lifelength; }
            set
            {
                lifelengthViewerAircraftTCSNOnInstall.Lifelength = value;
            }
        }

        #endregion

        #region public Lifelength ComponentCurrentTSNCSN

        /// <summary>
        /// ������� ��������� ��������
        /// </summary>
        public Lifelength ComponentCurrentTSNCSN
        {
            get { return lifelengthViewerComponentCurrentTSNCSN.Lifelength; }
            set
            {
                lifelengthViewerComponentCurrentTSNCSN.Lifelength = value;
            }
        }

        #endregion

        #region public Lifelength ComponentTCSI

        /// <summary>
        /// ��������� �������� � ������� ���������
        /// </summary>
        public Lifelength ComponentTCSI
        {
            get { return lifelengthViewerComponentTCSI.Lifelength; }
            set
            {
                lifelengthViewerComponentTCSI.Lifelength = value;
            }
        }

        #endregion

        #region public bool SetCurrentComponentTSNCSN

        /// <summary>
        /// ���������� ��������, ������������ ����� �� ��������� ������ ActualData �� ������� �����
        /// </summary>
        public bool SetCurrentComponentTSNCSN
        {
            get
            {
                return (lifelengthViewerComponentCurrentTSNCSN.Enabled && !lifelengthViewerComponentCurrentTSNCSN.Lifelength.IsNullOrZero());
            }
        }

        #endregion

        #region public bool SetActualDataToAircraft

        /// <summary>
        /// ���������� ��������, ������������ ����� �� ��������� ������ ActualData � ��
        /// </summary>
        public bool SetActualDataToAircraft
        {
            get
            {
                return lifelengthViewerAircraftTCSNOnInstall.Modified;
            }
        }

        #endregion

        #region public bool LLPMark
        /// <summary>
        /// ���� ������������ ����������
        /// </summary>
        public bool LLPMark
        {
            get { return checkBoxLLPMark.Checked; }
            set { checkBoxLLPMark.Checked = value;}
        }

		#endregion

		#region public bool IsPool

		public bool IsPool
	    {
		    get { return checkBoxPOOL.Checked; }
		    set { checkBoxPOOL.Checked = value; }
	    }

		#endregion

		#region public bool IsDangerous

		public bool IsDangerous
	    {
		    get { return checkBoxDangerous.Checked; }
		    set { checkBoxDangerous.Checked = value; }
	    }

	    #endregion

		#region public DateTime DateAsOf

		/// <summary>
		/// ������� ���� 
		/// </summary>
		public DateTime DateAsOf
        {
            get { return dateTimePickerDateAsOf.Value; }
            set
            {
                dateTimePickerDateAsOf.Value = value;
            }
        }

		#endregion

		#endregion

		#region Methods

	    public void UpdateComponentClass(BaseComponent component)
	    {
			if(component.BaseComponentType == BaseComponentType.Engine)
				comboBoxComponentType.SelectedItem = GoodsClass.AircraftBaseComponentsEngine;
			else comboBoxComponentType.SelectedItem = GoodsClass.AircraftComponents;
		}

		#region public void UpdateControl()
		/// <summary>
		/// ��������� �������
		/// </summary>
		public void UpdateControl()
        {
            if (_isStore)
            {
                labelLocation.Visible = true;
                dictionaryComboBoxLocation.Visible = true;
				dictionaryComboBoxLocation.Type = typeof(Locations);
				comboBoxStorePosition.Location = textBoxPosition.Location;//new Point(103, 344);
                comboBoxStorePosition.Visible = true;
                comboBoxStorePosition.Items.Clear();
				comboBoxStorePosition.Items.AddRange(ComponentStorePosition.Items.ToArray());
				comboBoxStorePosition.SelectedIndex = 3;
				Controls.Remove(labelAircraftTSNCSN);
                Controls.Remove(lifelengthViewerAircraftTCSNOnInstall);
                Controls.Remove(labelTCSI);
                Controls.Remove(lifelengthViewerComponentTCSI);

                FormControlAttribute fca = (FormControlAttribute)
                typeof(Component)
                    .GetProperty("GoodsClass")
                    .GetCustomAttributes(typeof(FormControlAttribute), false)
                    .FirstOrDefault();
                if (fca != null)
                    comboBoxComponentType.RootNodesNames = fca.TreeDictRootNodes;
                comboBoxComponentType.Type = typeof(GoodsClass);
                comboBoxComponentType.SelectedItem = GoodsClass.AircraftComponents;
            }
            else
            {
                comboBoxComponentType.RootNodesNames = new []{ "ComponentsAndParts", "ProductionAuxiliaryEquipment" };
                comboBoxComponentType.Type = typeof(GoodsClass);
                comboBoxComponentType.SelectedItem = GoodsClass.AircraftComponents;    
            }
            comboBoxModel.Type = typeof(ComponentModel);
            comboBoxModel.SelectedItem = null;
            comboBoxModel.Focus();
            Program.MainDispatcher.ProcessControl(comboBoxModel);

            comboBoxAtaChapter.UpdateInformation();
            comboBoxMaintenenceType.Items.Clear();
            comboBoxMaintenenceType.Items.AddRange(MaintenanceControlProcess.Items.ToArray());
            comboBoxMaintenenceType.SelectedItem = MaintenanceControlProcess.OC;

            comboBoxStatus.Items.Clear();
            foreach (object o in Enum.GetValues(typeof(ComponentStatus)))
                comboBoxStatus.Items.Add(o);
            comboBoxStatus.SelectedItem = ComponentStatus.New;

            ManufactureDate = DateTime.Today;
            dateTimePickerDeliveryDate.Value = DateTime.Today;
            InstallationDate = DateTime.Today;
            DateAsOf = DateTime.Today;
		}

        #endregion

        #region public void CalculateLifeLength()

        public void CalculateLifeLength()
        {
            //��������� �������� ��������� �������� �� ������ ���������
            if (ComponentTCSNOnInstall.IsNullOrZero()) return;//��������� ���� 

            //if (ComponentCurrentTSNCSN != Lifelength.Null)
            //{
            //    Lifelength tempLifelength = new Lifelength(ComponentCurrentTSNCSN);
            //    tempLifelength.Substract(ComponentTCSNOnInstall);
            //    ComponentTCSI = tempLifelength;
            //}

            Lifelength aircraftCurrentTSN =
                GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength((Aircraft)_currentAircraft);
            Lifelength tempLifelength = null;

            //������������ ������� ��������� ��������
            if (!ComponentTCSI.IsNullOrZero())
            {
                tempLifelength = new Lifelength(ComponentTCSI);
            }
            else if (!AircraftTCSNOnInstall.IsNullOrZero() && _currentAircraft is Aircraft)
            {
                tempLifelength = new Lifelength(aircraftCurrentTSN);
                tempLifelength.Substract(AircraftTCSNOnInstall);
            }

            if (tempLifelength != null && !tempLifelength.IsNullOrZero())
            {
                tempLifelength.Add(ComponentTCSNOnInstall);
                ComponentCurrentTSNCSN = tempLifelength;
            }
            else ComponentCurrentTSNCSN = Lifelength.Null;

            //������ ��������� � ������� ���������
            if (!ComponentTCSNOnInstall.IsNullOrZero() && !ComponentCurrentTSNCSN.IsNullOrZero())
            {
                tempLifelength = new Lifelength(ComponentCurrentTSNCSN);
                tempLifelength.Substract(ComponentTCSNOnInstall);
                ComponentTCSI = tempLifelength;
            }
            else if (!AircraftTCSNOnInstall.IsNullOrZero() && !aircraftCurrentTSN.IsNullOrZero())
            {
                tempLifelength = new Lifelength(aircraftCurrentTSN);
                tempLifelength.Substract(AircraftTCSNOnInstall);
                ComponentTCSI = tempLifelength;
            }
            else ComponentTCSI = Lifelength.Null;

            //������ ��������� �������� �� ������ ���������
            if (!ComponentTCSI.IsNullOrZero())
            {
                tempLifelength = new Lifelength(aircraftCurrentTSN);
                tempLifelength.Substract(ComponentTCSI);
                AircraftTCSNOnInstall = tempLifelength;
            }
            else if (!ComponentCurrentTSNCSN.IsNullOrZero() && !ComponentTCSNOnInstall.IsNullOrZero())
            {
                Lifelength temp2 = new Lifelength(ComponentCurrentTSNCSN);
                temp2.Substract(ComponentTCSNOnInstall);
                tempLifelength = new Lifelength(aircraftCurrentTSN);
                tempLifelength.Substract(temp2);
                AircraftTCSNOnInstall = tempLifelength;
            }
            else AircraftTCSNOnInstall = Lifelength.Null;

        }

        #endregion

        #region public void ChangeAircraftTCSNOnInstall()
        /// <summary>
        /// ��������� �������� ��������� �������� �� ������ ���������
        /// </summary>
        public void ChangeAircraftTCSNOnInstall()
        {
            //���� ����������� ������� ��������� �������� ���������
            //�� ���������� ��������� ������
            Lifelength aircraftCurrent =
                    GlobalObjects.CasEnvironment.Calculator.GetFlightLifelengthOnEndOfDay((Aircraft)_currentAircraft, dateTimePickerDateAsOf.Value);

            if (aircraftCurrent.IsNullOrZero())
            {
                if (lifelengthViewerAircraftTCSNOnInstall.SystemCalculated)
                {
                    //���� ������ � ��������� � ������� ��������� ������� �������,
                    //� �� ������������, �� �� ����� ��������
                    AircraftTCSNOnInstall = Lifelength.Null;
                }
                return;
            }

            if (AircraftTCSNOnInstall.IsNullOrZero() ||
                lifelengthViewerAircraftTCSNOnInstall.SystemCalculated)
            {
                //���� ��������� �������� � ������� ��������� �� ������� ���
                //������� ��������, �� � ���� ���������
                Lifelength temp;
                if (!ComponentCurrentTSNCSN.IsNullOrZero() &&
                    !ComponentTCSNOnInstall.IsNullOrZero())
                {
                    //���� �������� ������� ��������� �������� �
                    //��������� �������� �� ������ ���������
                    temp = new Lifelength(ComponentCurrentTSNCSN);
                    temp.Substract(ComponentTCSNOnInstall);
                    aircraftCurrent.Substract(temp);
                    AircraftTCSNOnInstall = aircraftCurrent;
                    lifelengthViewerAircraftTCSNOnInstall.SystemCalculated = true;
                    return;
                }
                
                if (!ComponentTCSI.IsNullOrZero())
                {
                    //���� �������� ��������� �������� � ������� ���������
                    temp = new Lifelength(aircraftCurrent);
                    temp.Substract(ComponentTCSI);
                    AircraftTCSNOnInstall = temp;
                    lifelengthViewerAircraftTCSNOnInstall.SystemCalculated = true;
                    return;
                }

                //�����, ��������� ��������� ������
                AircraftTCSNOnInstall = Lifelength.Null;
                lifelengthViewerAircraftTCSNOnInstall.SystemCalculated = true;
            }
        }

        #endregion

        #region public void ChangeComponentTCSNOnInstall()
        /// <summary>
        /// ��������� �������� ��������� �������� �� ������ ���������
        /// </summary>
        public void ChangeComponentTCSNOnInstall()
        {
            //���� ����������� ������� ��������� ��������
            //�� ���������� ��������� ������
            if (ComponentCurrentTSNCSN.IsNullOrZero())
            {
                if (lifelengthViewerComponentTCSNOnInstall.SystemCalculated)
                {
                    //���� ������ � ��������� �� ������ ��������� ������� �������,
                    //� �� ������������, �� �� ����� ��������
                    ComponentTCSNOnInstall = Lifelength.Null;
                }
                return;
            }

            if (ComponentTCSNOnInstall.IsNullOrZero() ||
                lifelengthViewerComponentTCSNOnInstall.SystemCalculated)
            {
                //���� ��������� �������� �� ������ ��������� �� ������� ���
                //������� ��������, �� � ���� ���������
                Lifelength temp = new Lifelength(ComponentCurrentTSNCSN);
                if (!ComponentTCSI.IsNullOrZero())
                {
                    //���� �������� ��������� �������� � ������� ���������
                    temp.Add(ComponentTCSI);
                    ComponentTCSNOnInstall = temp;
                    lifelengthViewerComponentTCSNOnInstall.SystemCalculated = true;
                    return;
                }

                Lifelength aircraftCurrent =
                    GlobalObjects.CasEnvironment.Calculator.GetFlightLifelengthOnEndOfDay((Aircraft)_currentAircraft, dateTimePickerDateAsOf.Value);

                if (!AircraftTCSNOnInstall.IsNullOrZero() &&
                    !aircraftCurrent.IsNullOrZero())
                {
                    //���� �������� ������� ��������� �������� �
                    //��������� �������� �� ������ ���������
                    aircraftCurrent.Substract(AircraftTCSNOnInstall);
                    temp.Substract(aircraftCurrent);
                    ComponentTCSNOnInstall = temp;
                    lifelengthViewerComponentTCSNOnInstall.SystemCalculated = true;
                    return;
                }

                //�����, ��������� ��������� ������
                ComponentTCSNOnInstall = Lifelength.Null;
                lifelengthViewerComponentTCSNOnInstall.SystemCalculated = true;
            }

        }

        #endregion

        #region public void ChangeCurrentComponentTSN()
        /// <summary>
        /// ��������� �������� ��������� �������� �� ������� ������
        /// </summary>
        public void ChangeCurrentComponentTSN()
        {
            //���� ����������� ��������� �������� �� ������ ���������
            //�� ���������� ��������� ������
            if(ComponentTCSNOnInstall.IsNullOrZero())
            {
                if (lifelengthViewerComponentCurrentTSNCSN.SystemCalculated)
                {
                    //���� ������ � ������� ��������� ������� �������,
                    //� �� ������������, �� �� ����� ��������
                    ComponentCurrentTSNCSN = Lifelength.Null;
                }
                return;
            }
            
            if(ComponentCurrentTSNCSN.IsNullOrZero() || 
                lifelengthViewerComponentCurrentTSNCSN.SystemCalculated)
            {
                //���� ������� ��������� �������� �� ������� ���
                //������� ��������, �� � ���� ���������
                Lifelength temp = new Lifelength(ComponentTCSNOnInstall);
                if(!ComponentTCSI.IsNullOrZero())
                {
                    //���� �������� ��������� �������� � ������� ���������
                    temp.Add(ComponentTCSI);
                    temp.Resemble(ComponentTCSNOnInstall);
                    ComponentCurrentTSNCSN = temp;
                    lifelengthViewerComponentCurrentTSNCSN.SystemCalculated = true;
                    return;
                }

                if (_isStore) return;
                Lifelength aircraftCurrent = 
                    GlobalObjects.CasEnvironment.Calculator.GetFlightLifelengthOnEndOfDay((Aircraft)_currentAircraft,dateTimePickerDateAsOf.Value);
                
                if(!AircraftTCSNOnInstall.IsNullOrZero() &&
                   !aircraftCurrent.IsNullOrZero())
                {
                    //���� �������� ������� ��������� �������� �
                    //��������� �������� �� ������ ���������
                    aircraftCurrent.Substract(AircraftTCSNOnInstall);
                    temp.Substract(aircraftCurrent);
                    temp.Resemble(ComponentTCSNOnInstall);
                    ComponentCurrentTSNCSN = temp;
                    lifelengthViewerComponentCurrentTSNCSN.SystemCalculated = true;
                    return;
                }

                //�����, ��������� ��������� ������
                ComponentCurrentTSNCSN = Lifelength.Null;
                lifelengthViewerComponentCurrentTSNCSN.SystemCalculated = true;
            }

        }

        #endregion

        #region public void ChangeComponentTSNSinseInstall()
        /// <summary>
        /// ��������� �������� ��������� �������� � ������� ���������
        /// </summary>
        public void ChangeComponentTSNSinseInstall()
        {
            if(_isStore) return;
            //���� ����������� ��������� �������� �� ������ ���������
            //�� ���������� ��������� ������
            Lifelength aircraftCurrent =
                    GlobalObjects.CasEnvironment.Calculator.GetFlightLifelengthOnEndOfDay((Aircraft)_currentAircraft, dateTimePickerDateAsOf.Value);

            if ((aircraftCurrent.IsNullOrZero() && AircraftTCSNOnInstall.IsNullOrZero()) ||
                (ComponentCurrentTSNCSN.IsNullOrZero() && ComponentTCSNOnInstall.IsNullOrZero()))
            {
                if (lifelengthViewerComponentTCSI.SystemCalculated)
                {
                    //���� ������ � ��������� � ������� ��������� ������� �������,
                    //� �� ������������, �� �� ����� ��������
                    ComponentTCSI = Lifelength.Null;
                }
                return;
            }

            if (ComponentTCSI.IsNullOrZero() ||
                lifelengthViewerComponentTCSI.SystemCalculated)
            {
                //���� ��������� �������� � ������� ��������� �� ������� ���
                //������� ��������, �� � ���� ���������
                Lifelength temp;
                if (!aircraftCurrent.IsNullOrZero() &&
                    !AircraftTCSNOnInstall.IsNullOrZero())
                {
                    //���� �������� ������� ��������� �������� �
                    //��������� �������� �� ������ ���������
                    temp = new Lifelength(aircraftCurrent);
                    temp.Substract(AircraftTCSNOnInstall);
                    ComponentTCSI = temp;
                    lifelengthViewerComponentCurrentTSNCSN.SystemCalculated = true;
                    return;
                }

                if (!ComponentCurrentTSNCSN.IsNullOrZero() && 
                    !ComponentTCSNOnInstall.IsNullOrZero())
                {
                    //���� �������� ������� ��������� �������� �
                    //��������� �������� �� ������ ���������
                    temp = new Lifelength(ComponentCurrentTSNCSN);
                    temp.Substract(ComponentTCSNOnInstall);
                    ComponentTCSI = temp;
                    lifelengthViewerComponentCurrentTSNCSN.SystemCalculated = true;
                    return;
                }
                //�����, ��������� ��������� ������
                ComponentTCSI = Lifelength.Null;
                lifelengthViewerComponentCurrentTSNCSN.SystemCalculated = true;
            }
        }

        #endregion

        #region public bool GetChangeStatus()

        /// <summary>
        /// ���������� ��������, ������������ ���� �� ��������� � ������ �������� ����������
        /// </summary>
        /// <returns></returns>
        public bool GetChangeStatus()
        {
            return (comboBoxModel.SelectedItem != null ||
                    (comboBoxStatus.SelectedItem is ComponentStatus && (ComponentStatus)comboBoxStatus.SelectedItem != ComponentStatus.New) ||
                    textBoxManufacturer.Text != "" ||
                    MPDItem != "" ||
                    ATAChapter != null ||
                    MaintenanceControlProcess != MaintenanceControlProcess.UNK ||
                    Description != "" ||
                    PartNumber != "" ||
                    Code != "" ||
                    Position != "" ||
                    SerialNumber != "" || BatchNumber != "" || IdNumber != "" ||
                    dateTimePickerDeliveryDate.Value != DateTime.Today ||
                    InstallationDate != DateTime.Today ||
                    lifelengthViewerComponentTCSNOnInstall.Modified ||
                    lifelengthViewerAircraftTCSNOnInstall.Modified ||
                    DateAsOf != DateTime.Today ||
                    lifelengthViewerComponentCurrentTSNCSN.Modified ||
                    lifelengthViewerComponentTCSI.Modified);
        }

		#endregion

		#region public void ApplyChanges(Detail detail)

		public void ApplyChanges(Component component)
	    {
		    component.Model = comboBoxModel.SelectedItem as ComponentModel;
		    component.ComponentStatus = comboBoxStatus.SelectedItem is ComponentStatus
			    ? (ComponentStatus) comboBoxStatus.SelectedItem
			    : ComponentStatus.New;
		    component.Manufacturer = textBoxManufacturer.Text;
		    component.MPDItem = MPDItem;
		    component.ATAChapter = ATAChapter;
		    component.Description = Description;
		    component.DeliveryDate = dateTimePickerDeliveryDate.Value;
		    component.PartNumber = PartNumber;
		    component.Code = Code;
		    component.SerialNumber = SerialNumber;
		    component.BatchNumber = BatchNumber;
		    component.IdNumber = IdNumber;
		    component.ManufactureDate = ManufactureDate;
		    component.MaintenanceControlProcess = MaintenanceControlProcess;
		    component.LLPMark = checkBoxLLPMark.Checked;
		    component.Location = ComponentLocation;
		    component.GoodsClass = GoodsClass;
		    component.IsDangerous = IsDangerous;
		    component.IsPOOL = IsPool;
	    }
		#endregion

        #region public void ClearFields()

        /// <summary>
        /// ������� ��� ����
        /// </summary>
        public void ClearFields()
        {
            comboBoxAtaChapter.ClearValue();
            comboBoxModel.SelectedItem = null;
            comboBoxStatus.SelectedItem = ComponentStatus.New;
            textBoxPosition.Text = "";
            MPDItem = "";
            Description = "";
            PartNumber = "";
            SerialNumber = "";
            BatchNumber = "";
            IdNumber = "";
			Code = "";
	        textBoxALTPN.Text = "";
			ManufactureDate = DateTime.Today;
            dateTimePickerDeliveryDate.Value = DateTime.Today;
            InstallationDate = DateTime.Today;
            ComponentTCSNOnInstall = Lifelength.Null;
            lifelengthViewerComponentTCSNOnInstall.Modified = false;
            comboBoxMaintenenceType.SelectedItem = MaintenanceControlProcess.OC;
            if (!_isStore)
            {
                //AircraftTSNCSN = GlobalObjects.CasEnvironment.Calculator.GetLifelength()
                lifelengthViewerAircraftTCSNOnInstall.Modified = false;
            }
            DateAsOf = DateTime.Today;
            // ComponentCurrentTSNCSN = Lifelength.Null;
            //  lifelengthViewerComponentCurrentTSNCSN.Modified = false;
            //   ComponentTCSI = Lifelength.Null;
            // lifelengthViewerComponentTCSI.Modified = false;
        }

        #endregion

        #region private void DateTimePickerInstallationDateValueChanged(object sender, EventArgs e)

        private void DateTimePickerInstallationDateValueChanged(object sender, EventArgs e)
        {
            if (!lifelengthViewerAircraftTCSNOnInstall.Modified)
            {
                if (!_isStore)
                {
                    if (InstallationDate <= DateTime.Today)
                    {
                        AircraftTCSNOnInstall = GlobalObjects.CasEnvironment.Calculator.
                            GetFlightLifelengthOnEndOfDay((Aircraft) _currentAircraft, InstallationDate);
                    }
                    else
                    {
                        AircraftTCSNOnInstall = GlobalObjects.CasEnvironment.Calculator.
                            GetFlightLifelengthOnEndOfDay((Aircraft)_currentAircraft, DateTime.Today);
                    }
                    lifelengthViewerAircraftTCSNOnInstall.Modified = false;
                    lifelengthViewerAircraftTCSNOnInstall.SystemCalculated = true;
                }
            }

            Lifelength onInstall = new Lifelength(ComponentTCSNOnInstall);
            onInstall.Days = (int)(InstallationDate - ManufactureDate).TotalDays;

            ComponentTCSNOnInstall = onInstall;
            lifelengthViewerComponentTCSNOnInstall.SystemCalculated = true;
        }

        #endregion

        #region private void LifelengthViewerComponentCurrentTsncsnLifelengthChanged(object sender, EventArgs e)

        private void LifelengthViewerComponentCurrentTsncsnLifelengthChanged(object sender, EventArgs e)
        {
            //��������� �������� ������� ��������� ��������
            lifelengthViewerComponentCurrentTSNCSN.SystemCalculated = false;

            if (ComponentCurrentTSNCSN.IsGreater(ComponentTCSNOnInstall) == false) return;

            if(!lifelengthViewerComponentTCSNOnInstall.SystemCalculated)
            {
                ChangeComponentTSNSinseInstall();
                ChangeAircraftTCSNOnInstall();

                return;
            }

            if(!lifelengthViewerComponentTCSI.SystemCalculated)
            {
                ChangeComponentTCSNOnInstall();
                ChangeAircraftTCSNOnInstall();

                return;
            }

            if(!lifelengthViewerAircraftTCSNOnInstall.SystemCalculated)
            {
                ChangeComponentTSNSinseInstall();
                ChangeAircraftTCSNOnInstall();
            }
        }

        #endregion

        #region private void LifelengthViewerComponentTcsiLifelengthChanged(object sender, EventArgs e)

        private void LifelengthViewerComponentTcsiLifelengthChanged(object sender, EventArgs e)
        {
            //��������� �������� ��������� �������� � ������� ���������
            lifelengthViewerComponentTCSI.SystemCalculated = false;

            if (!lifelengthViewerComponentCurrentTSNCSN.SystemCalculated)
            {
                ChangeComponentTCSNOnInstall();
                ChangeAircraftTCSNOnInstall();

                return;
            }

            if (!lifelengthViewerComponentTCSNOnInstall.SystemCalculated)
            {
                //�������� ��������� ���������� �� ������ ��������� 
                //������� �������������

                ChangeCurrentComponentTSN();//�������� �������� �������� ��������� �������� 
                ChangeAircraftTCSNOnInstall();//�������� �������� ��������� �������� �� ������ ��������� ��������

                return;
            }

            if (!lifelengthViewerAircraftTCSNOnInstall.SystemCalculated)
            {
                //�������� ��������� �������� �� ������ ��������� �������� 
                //������� �������������
                ChangeCurrentComponentTSN();
                ChangeComponentTCSNOnInstall();
            }
        }

        #endregion

        #region private void LifelengthViewerComponentTsncsnLifelengthChanged(object sender, EventArgs e)

        private void LifelengthViewerComponentTsncsnLifelengthChanged(object sender, EventArgs e)
        {
            //��������� �������� ��������� �������� �� ������ ���������
            lifelengthViewerComponentTCSNOnInstall.SystemCalculated = false;

            if (!lifelengthViewerComponentCurrentTSNCSN.SystemCalculated)//������� ���������
            {
                ChangeAircraftTCSNOnInstall();
                ChangeComponentTSNSinseInstall();
                
                return;
            }

            if (!lifelengthViewerComponentTCSI.SystemCalculated)
            {
                //�������� ��������� ���������� � ������� ��������� 
                //������� �������������

                ChangeCurrentComponentTSN();//�������� �������� �������� ��������� �������� 
                ChangeAircraftTCSNOnInstall();//�������� �������� ��������� �������� �� ������ ��������� ��������

                return;
            }

            //if (!lifelengthViewerAircraftTCSNOnInstall.SystemCalculated)
            //{
                //�������� ��������� �������� �� ������ ��������� �������� 
                //������� �������������

                ChangeComponentTSNSinseInstall();
                ChangeCurrentComponentTSN();
            //}

        }

        #endregion

        #region private void DateTimePickerManufactureDateValueChanged(object sender, EventArgs e)

        private void DateTimePickerManufactureDateValueChanged(object sender, EventArgs e)
        {
            Lifelength onInstall = new Lifelength(ComponentTCSNOnInstall);
            onInstall.Days = (int)(InstallationDate - ManufactureDate).TotalDays;

            ComponentTCSNOnInstall = onInstall;
            lifelengthViewerComponentTCSNOnInstall.SystemCalculated = true;

            InvokeManufactureDateChanged(dateTimePickerManufactureDate.Value);
        }

        #endregion

        #region private void LifelengthViewerAircraftTcsnOnInstallLifelengthChanged(object sender, EventArgs e)

        private void LifelengthViewerAircraftTcsnOnInstallLifelengthChanged(object sender, EventArgs e)
        {
            //��������� �������� ��������� �������� �� ������ ��������� ��������
            lifelengthViewerAircraftTCSNOnInstall.SystemCalculated = false;

            if (!lifelengthViewerComponentTCSNOnInstall.SystemCalculated)//��������� �� ������ ���������
            {
                ChangeComponentTSNSinseInstall();
                ChangeCurrentComponentTSN();

                return;
            }

            if (!lifelengthViewerComponentCurrentTSNCSN.SystemCalculated)
            {
                ChangeComponentTCSNOnInstall();
                ChangeComponentTSNSinseInstall();
                
                return;
            }

            if (!lifelengthViewerComponentTCSI.SystemCalculated)
            {
                //�������� ��������� �������� �� ������ ��������� �������� 
                //������� �������������

                ChangeCurrentComponentTSN();
                ChangeComponentTCSNOnInstall();
            }

        }

        #endregion

        #region private void DictionaryComboProductSelectedIndexChanged(object sender, EventArgs e)
        private void DictionaryComboProductSelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBoxStandart.SelectedIndexChanged -= ComboBoxStandartSelectedIndexChanged;

            ComponentModel accessoryDescription;
            if ((accessoryDescription = comboBoxModel.SelectedItem as ComponentModel) != null)
            {
                comboBoxComponentType.SelectedItem = accessoryDescription.GoodsClass;

                comboBoxComponentType.Enabled = false;

                comboBoxAtaChapter.ATAChapter = accessoryDescription.ATAChapter;
                comboBoxAtaChapter.Enabled = false;
                //comboBoxMeasure.Enabled = false;
                //comboBoxStandart.Enabled = false;
                textBoxPartNo.ReadOnly = true;
                textBoxDescription.ReadOnly = true;
                //textBoxProductCode.ReadOnly = true;
                //dataGridViewControlSuppliers.ReadOnly = true;
                //textBoxRemarks.ReadOnly = true;

                //comboBoxMeasure.SelectedItem = accessoryDescription.Measure;
                //comboBoxStandart.SelectedItem = accessoryDescription.Standart;
                textBoxPartNo.Text = accessoryDescription.PartNumber;
                textBoxDescription.Text = accessoryDescription.Description;
                //textBoxProductCode.Text = accessoryDescription.Code;
                textBoxManufacturer.Text = accessoryDescription.Manufacturer;
                //dataGridViewControlSuppliers.SetItemsArray(accessoryDescription.SupplierRelations);
                //textBoxRemarks.Text = accessoryDescription.Remarks;
            }
            else
            {
                comboBoxComponentType.Enabled = true;
                comboBoxAtaChapter.Enabled = true;
                textBoxPartNo.ReadOnly = false;
                textBoxDescription.ReadOnly = false;
                //textBoxProductCode.ReadOnly = false;
                //dataGridViewControlSuppliers.ReadOnly = false;
                //textBoxRemarks.ReadOnly = false;
                //numericCostNew.ReadOnly = false;
                //numericCostServiceable.ReadOnly = false;
                //numericCostOverhaul.ReadOnly = false;
            }

            //comboBoxStandart.SelectedIndexChanged += ComboBoxStandartSelectedIndexChanged;
        }
        #endregion

        #region private void CheckBoxLLPMarkCheckedChanged(object sender, EventArgs e)
        private void CheckBoxLLPMarkCheckedChanged(object sender, EventArgs e)
        {
            _addedComponent.LLPMark = checkBoxLLPMark.Checked;
			//TODO: ����������� � ���� �����
            //if (LLPMarkChecked != null) LLPMarkChecked(this, e);
        }
        #endregion

        #region private void ComboBoxComponentTypeSelectedIndexChanged(object sender, EventArgs e)
        private void ComboBoxComponentTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            GoodsClass dt = comboBoxComponentType.SelectedItem as GoodsClass;
            if (dt == null)
                labelQuantity.Visible = numericUpDownQuantity.Visible = false;
            else if (dt.IsNodeOrSubNodeOf(GoodsClass.AircraftComponentsEmergency))
                labelQuantity.Visible = numericUpDownQuantity.Visible = true;
            else labelQuantity.Visible = numericUpDownQuantity.Visible = false;

            InvokeComponentTypeChanged(dt ?? GoodsClass.Unknown);
        }
        #endregion

        #endregion

        #region  Events
        ///<summary>
        ///</summary>
        public event EventHandler LLPMarkChecked;

        #region public event ValueChangedEventHandler ComponentTypeChanged
        ///<summary>
        /// ������������� � ����� ���� ����������
        ///</summary>
        private void InvokeComponentTypeChanged(GoodsClass value)
        {
            ValueChangedEventHandler handler = ComponentTypeChanged;
            if (handler != null) handler(new ValueChangedEventArgs(value));
        }
        ///<summary>
        /// ������� ����������� � ����� ���� ����������
        ///</summary>
        [Category("Component data")]
        [Description("��������� ��� ��������� ���� ����������")]
        public event ValueChangedEventHandler ComponentTypeChanged;
        #endregion

        #region public event DateChangedEventHandler ManufactureDateChanged
        ///<summary>
        /// ������������� � ����� ���� ������������ ����������
        ///</summary>
        private void InvokeManufactureDateChanged(DateTime value)
        {
            DateChangedEventHandler handler = ManufactureDateChanged;
            if (handler != null) handler(new DateChangedEventArgs(value));
        }
        ///<summary>
        /// ������� ����������� � ����� ���� ������������ ����������
        ///</summary>
        [Category("Component data")]
        [Description("��������� ��� ��������� ���� ������������ ����������")]
        public event DateChangedEventHandler ManufactureDateChanged;
		#endregion

		#region public event DateChangedEventHandler InstallationDateChanged
		/////<summary>
		///// ������������� � ����� ���� ��������� ����������
		/////</summary>
		//private void InvokeInstallationDateChanged(DateTime value)
		//{
		//    DateChangedEventHandler handler = InstallationDateChanged;
		//    if (handler != null) handler(new DateChangedEventArgs(value));
		//}
		/////<summary>
		///// ������� ����������� � ����� ���� ��������� ����������
		/////</summary>
		//[Category("Component data")]
		//[Description("��������� ��� ��������� ���� ������������ ����������")]
		//public event DateChangedEventHandler InstallationDateChanged;
		#endregion

		#endregion
	}
}
