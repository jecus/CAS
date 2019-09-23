using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using CAS.UI.Helpers;
using EntityCore.DTO.General;
using EntityCore.DTO.Dictionaries;
using EntityCore.Filter;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Hangar;
using SmartCore.Entities.General.Store;
using SmartCore.Entities.General.WorkShop;
using SmartCore.Management;
using SmartCore.Aircrafts;
using SmartCore.AuditMongo.Repository;
using SmartCore.Entities.General;
using SmartCore.Entities.Collections;
using SmartCore.Calculations;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities;
using SmartCore.Entities.General.Interfaces;
using SmartCore.Entities.NewLoader;
using SmartCore.ObjectCache;
using SmartCore.Queries;

namespace SmartCore
{
	/// <summary>
    /// ��������� ������� CAS. ������ CAS
    /// </summary>
    public class CasEnvironment:ICasEnvironment
    {
        // ������ ������ � ���������� CAS

        #region public CasEnvironment()
        /// <summary>
        /// ������� ��������� ������� CAS, ����������� ��� �� ������
        /// </summary>
        public CasEnvironment()
        {
	        _dictionaries = new CommonDictionariesCache();
        }
        #endregion

        #region class FileProcess

        class FileProcess
        {
            private readonly AttachedFile _attachedFile;
            private readonly Process _process;
            private readonly string _fileNameToRemove;

            #region public AttachedFile AttachedFile

            public AttachedFile AttachedFile
            {
                get { return _attachedFile; }
            }
            #endregion

            #region public string FileNameToRemove

            public string FileNameToRemove
            {
                get { return _fileNameToRemove; }
            }
            #endregion

            #region public Process Process

            public Process Process
            {
                get { return _process; }
            }
            #endregion

            #region public FileProcess(AttachedFile attachedFile, string fileNameToRemove, Process process)

            public FileProcess(AttachedFile attachedFile, string fileNameToRemove, Process process)
            {
                _attachedFile = attachedFile;
                _fileNameToRemove = fileNameToRemove;
                _process = process;
            }
            #endregion
        }

        #endregion

        private ApiProvider _apiProvider;
		public ApiProvider ApiProvider
		{
			get { return _apiProvider; }
			set { _apiProvider = value; }
		}


		#region public IAuditRepository AuditRepository { get; set; }

		public IAuditRepository AuditRepository
		{
			get => _auditRepository ?? (_auditRepository = new AuditRepository(null));
			set => _auditRepository = value;
		}

		#endregion

        #region public void Disconnect()

        /// <summary>
        /// ���������� ���������� �� ���� ������
        /// </summary>
        public void Disconnect()
        {
            _exit = true;

            if(_tempFileMonitoringThread != null)
            {
                _tempFileMonitoringThread.Abort();

                foreach (FileProcess fileProcess in _openFileProcesses.Where(p => File.Exists(p.FileNameToRemove)))
                {
                    try
                    {
                        File.Delete(fileProcess.FileNameToRemove);
                    }
                    catch
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }

        #endregion

        #region public void Connect(String serverName, String userName, String pass, String database)
        /// <summary>
        /// ������������ � ���� ������ ��� ��������� ������� ������� � �������� ���� ������
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <param name="database"></param>
        public void Connect(string serverName, string userName, string pass, string database)
        {
	        _ipServer = serverName;

	        //_apiProvider = new ApiProvider(serverName);

	        _apiProvider.CheckAPIConnection();

	        var user = _apiProvider.GetUserAsync(userName, pass);
	        if(user == null)
		        throw new Exception($"Invalid combination of login and password");

	        IdentityUser = user;
	        AuditRepository.WriteAsync(new Entities.User(user), AuditOperation.SignIn, user);

	        _newLoader = new NewLoader(this);
		}
		#endregion

		public void UpdateUser(string password)
		{
			_apiProvider.UpdatePassword(IdentityUser.ItemId, password);
		}

		#region public void CheckTablesFor(Type type)
        public void CheckTablesFor(Type type)
        {
           
        }
        #endregion

        #region public void CreateTablesFor(Type type)
        public void CreateTablesFor(Type type)
        {
            
        }
        #endregion

        #region public override string ToString()
        /// <summary>
        /// ���� �������� ���������� � ����������� ���� ������ � ���������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //return "Orenair, CasDemo at Dev\\Dev2005, 15 aircrafts";
            return "";
        }
		#endregion

		#region public UserDTO GetCorrector(int id)

		public string GetCorrector(int id)
		{
			return Users.ContainsKey(id) ? Users[id] : "Unknown";
		}

		public string GetCorrector(IBaseEntityObject entity)
		{
			return Users.ContainsKey(entity.CorrectorId) ?
				$"{Users[entity.CorrectorId]} ({Auxiliary.Convert.GetDateFormat(entity.Updated)} {entity.Updated.TimeOfDay.Hours}:{entity.Updated.TimeOfDay.Minutes}:{entity.Updated.TimeOfDay.Seconds})"
				: "Unknown";
		}

		#endregion

		/*
		* ���������� ��������
		*/

		public DataSet Execute(string sql)
	    {
		    return _apiProvider.Execute(sql);
	    }

	    public DataSet Execute(IEnumerable<DbQuery> dbQueries, out List<ExecutionResultArgs> results)
	    {
		    results = new List<ExecutionResultArgs>();

			int counter = 0;

			var dataset = new DataSet();

			foreach (var query in dbQueries)
			{
				var dt = _apiProvider.Execute(query.QueryString).Tables[0];

				var casTable = new CasDataTable(query.ElementType, query.Branch, query.Branch);

				foreach (DataColumn row in dt.Columns)
					casTable.Columns.Add(new DataColumn(row.ColumnName, row.DataType));

				foreach (DataRow row in dt.Rows)
					casTable.ImportRow(row);

				dataset.Tables.Add(casTable);

				counter++;
			}

			return dataset;
	    }

	    public DataSet Execute(string query, SqlParameter[] parameters)
	    {
		    return _apiProvider.Execute(query, parameters);
	    }

	    /*
         * ��������, ���������� �������
         */

		#region public OperatorCollection Operators { get; internal set; }

		/// <summary>
		/// ��������� ���������
		/// </summary>
		private OperatorCollection _operators;
        /// <summary>
        /// ��������� ���������
        /// </summary>
        public OperatorCollection Operators
        {
            get
            {
                if (_operators.Count == 0 || _stores == null || _baseComponents == null)
                {
	                NewLoader.FirstLoad();
                }
                //
                return _operators;
            }
            internal set { _operators = value; }
        }
        #endregion

        #region public CommonCollection<Vehicle> Vehicles { get; internal set; }
        /// <summary>
        /// ��������� ������������ ��������
        /// </summary>
        private CommonCollection<Vehicle> _vehicles;
        /// <summary>
        /// ��������� ������������ ��������
        /// </summary>
        public CommonCollection<Vehicle> Vehicles
        {
            get
            {
                if (_operators.Count == 0 || _stores == null || _baseComponents == null)
                {
	                NewLoader.FirstLoad();
                }
                //
                return _vehicles ?? (_vehicles = new CommonCollection<Vehicle>());
            }
            internal set
            {
                _vehicles = value;
            }
        }
        #endregion

        #region public CommonCollection<Store> Stores { get; internal set; }
        /// <summary>
        /// ��������� ������ ��������
        /// </summary>
        private CommonCollection<Store> _stores;
        /// <summary>
        /// ��������� ������ ��������
        /// </summary>
        public CommonCollection<Store> Stores
        {
            get
            {
                if (_operators.Count == 0 || _stores == null || _baseComponents == null)
                {
	                NewLoader.FirstLoad();
                }
                //
                return _stores;
            }
            internal set
            {
                _stores = value;
            }
        }
		#endregion

		#region public CommonCollection<Store> User { get; internal set; }

		public Dictionary<int, string> Users { get; set; }
		#endregion

		#region public CommonCollection<Hangar> Hangars { get; internal set; }
		/// <summary>
		/// ��������� ������ ��������
		/// </summary>
		private CommonCollection<Hangar> _hangars;
        /// <summary>
        /// ��������� ������ ��������
        /// </summary>
        public CommonCollection<Hangar> Hangars
        {
            get
            {
                if (_operators.Count == 0 || _stores == null || _baseComponents == null)
                {
	                NewLoader.FirstLoad();
                }
                //
                return _hangars ?? (_hangars = new CommonCollection<Hangar>());
            }
            internal set
            {
                _hangars = value;
            }
        }
        #endregion

        #region public CommonCollection<WorkShop> WorkShops { get; internal set; }
        /// <summary>
        /// ��������� ����������� ��������
        /// </summary>
        private CommonCollection<WorkShop> _workShops;
        /// <summary>
        /// ��������� ����������� ��������
        /// </summary>
        public CommonCollection<WorkShop> WorkShops
        {
            get
            {
                if (_operators.Count == 0 || _stores == null || _baseComponents == null)
                {
	                NewLoader.FirstLoad();
                }
                //
                return _workShops ?? (_workShops = new CommonCollection<WorkShop>());
            }
            internal set
            {
                _workShops = value;
            }
        }
		#endregion

		#region public BaseComponentCollection BaseComponents { get; internal set; }
		/// <summary>
		/// ��������� ������� ��������
		/// </summary>
		private BaseComponentCollection _baseComponents;
        /// <summary>
        /// ��������� ������� ��������
        /// </summary>
        public BaseComponentCollection BaseComponents
        {
            get
            {
                if (_operators.Count == 0 || _stores == null || _baseComponents == null)
                {
	                NewLoader.FirstLoad();
                }
                //
                return _baseComponents;
            }
            internal set
            {
                _baseComponents = value;
            }
        }
        #endregion

        #region public CommonDictionariesCache Dictionaries

        private readonly CommonDictionariesCache _dictionaries;

        public IDictionaryCollection GetDictionary<T>() where T : IDictionaryItem
        {
            return _dictionaries.GetDictionary<T>();
        }

        public IDictionaryCollection GetDictionary(Type type)
        {
            return _dictionaries.GetDictionary(type);
        }

        public void ClearDictionaries()
        {
            _dictionaries.Clear();
        }

        public void AddDictionary(Type t, IDictionaryCollection dictionary)
        {
            _dictionaries.Add(t, dictionary);    
        }

        #endregion

        #region public Dictionary<string, ICommonCollection> TempCollections
        private Dictionary<string, ICommonCollection> _tempCollections;
        /// <summary>
        /// ��������� ��������� (���������, ������������ ����� ��� � ����� ��������)
        /// </summary>
        public Dictionary<string, ICommonCollection> TempCollections
        {
            get { return _tempCollections; }
            internal set { _tempCollections = value; }
        }
        #endregion

        #region public ReasonCollection Reasons { get; }
        /// <summary>
        /// ������� ����
        /// </summary>
        private ReasonCollection _reasons;
        /// <summary>
        /// ���������� ��� ������� ����
        /// </summary>
        public ReasonCollection Reasons
        {
            get
            {
                if (_reasons == null)
                {
                    //�������� ������ ����
                    _reasons = new ReasonCollection(_newLoader.GetObjectList<ReasonDTO, Reason>().ToArray());
                }
                //
                return _reasons;
            }
        }
		#endregion

		#region public IIdentityUser IdentityUser { get; }
		/// <summary>
		/// ������������, ������������ � ���� ������
		/// </summary>
		private IIdentityUser _currentUser;
        /// <summary>
        /// ���������� ������������, ������������� � ���� ������
        /// </summary>
        public IIdentityUser IdentityUser
        {
            get
            {
                // ���������� �������� ������������ 
                if (_currentUser == null)
                    _currentUser = new UserDTO(){Name = "Error"};
               
                return _currentUser;
            }
	        set { _currentUser = value; }
        }
        #endregion

        /*
         * ��������, ������������ ����
         */

        #region public void Reset()
        /// <summary>
        /// ���������� ��� ��������� � ��������� �� ������ - ��� ������ ������ �� ������� ��������, ��������� ���� � ��������� ���������� �� ����������� 
        /// </summary>
        public void Reset()
        {
            //��������� ���������
            if(_tempCollections != null)
            {
                foreach (KeyValuePair<string, ICommonCollection> pair in _tempCollections)
                {
                    if(pair.Value != null)pair.Value.Clear();
                }
                _tempCollections.Clear();
            }
            _tempCollections = null;

            //�������
            _dictionaries.Clear();

            //���������
            if(_operators != null)
                _operators.Clear();
            _operators = null;
            

            //������
            if (_stores != null)
                _stores.Clear();
            _stores = null;

            //������� ��������
            if (_baseComponents != null)
                _baseComponents.Clear();
            _baseComponents = null;

            //������� �����
            if (_reasons != null)
                _reasons.Clear();
            _reasons = null;

            //�����������
            if (_calculator != null) _calculator.Reset();
        }
        #endregion



		#region public void InitAsync(BackgroundWorker backgroundWorker, LoadingState loadingState)
		/// <summary>
		/// ��������� ��� ������ �� ����, ����������� ��� ���������������� ����
		/// </summary>
		public void InitAsync(BackgroundWorker backgroundWorker, LoadingState loadingState)
        {
			if ( backgroundWorker == null )return;
            
            if ( loadingState == null )
                 loadingState = new LoadingState();
            loadingState.MaxPersentage = 10;

            //�������� ���� �������������
			loadingState.CurrentPersentage = 0;
            loadingState.CurrentPersentageDescription = "Loading Users";
            backgroundWorker.ReportProgress(1, loadingState);

            var users =  ApiProvider.GetAllUsersAsync();
            Users = new Dictionary<int, string>();
			foreach (var user in users)
				Users.Add(user.ItemId, user.ToString());

            if (backgroundWorker.CancellationPending)
            {
	            return;
            }

			//��������� ���������
			loadingState.CurrentPersentage = 1;
            loadingState.CurrentPersentageDescription = "Loading Temp Collection";
            backgroundWorker.ReportProgress(1, loadingState);

            TempCollections = new Dictionary<string, ICommonCollection>();

            if (backgroundWorker.CancellationPending)
            {
                return;
            }

            //�������� ���� ��������
            loadingState.CurrentPersentage = 2;
            loadingState.CurrentPersentageDescription = "Loading Dictionaries";
            backgroundWorker.ReportProgress(1, loadingState);

            NewLoader.GetDictionaries();
            
            if (backgroundWorker.CancellationPending)
            {
                return;
            }

            // �������� ���� ����������
            loadingState.CurrentPersentage = 3;
            loadingState.CurrentPersentageDescription = "Loading Operators";
            backgroundWorker.ReportProgress(1, loadingState);

            Operators = new OperatorCollection(_newLoader.GetObjectList<OperatorDTO,Operator>().ToArray());
            
            if (backgroundWorker.CancellationPending)
            {
                return;
            }

            // �������� ���� ��������� ����� 
            loadingState.CurrentPersentage = 4;
            loadingState.CurrentPersentageDescription = "Loading Aircrafts";
            backgroundWorker.ReportProgress(1, loadingState);

			_aircraftsCore.LoadAllAircrafts();

			if (backgroundWorker.CancellationPending)
            {
                return;
            }

#if !KAC
            //�������� ���� ��
            loadingState.CurrentPersentage = 5;
            loadingState.CurrentPersentageDescription = "Loading Vehicles";
            backgroundWorker.ReportProgress(1, loadingState);

	        Vehicles = new CommonCollection<Vehicle>(_newLoader.GetObjectList<VehicleDTO, Vehicle>());

            if (backgroundWorker.CancellationPending)
            {
                return;
            }

            //�������� ���� �������
            loadingState.CurrentPersentage = 6;
            loadingState.CurrentPersentageDescription = "Loading Hangars";
            backgroundWorker.ReportProgress(1, loadingState);

			Hangars = new CommonCollection<Hangar>(_newLoader.GetObjectList<HangarDTO, Hangar>());

            if (backgroundWorker.CancellationPending)
            {
                return;
            }
#endif

            // �������� ���� �������
            loadingState.CurrentPersentage = 7;
            loadingState.CurrentPersentageDescription = "Loading Stores";
            backgroundWorker.ReportProgress(1, loadingState);

			Stores = new CommonCollection<Store>(_newLoader.GetObjectList<StoreDTO, Store>());

            if (backgroundWorker.CancellationPending)
            {
                return;
            }

#if !KAC
            // �������� ���� �����������
            loadingState.CurrentPersentage = 8;
            loadingState.CurrentPersentageDescription = "Loading Work Shops";
            backgroundWorker.ReportProgress(1, loadingState);

	        WorkShops = new CommonCollection<WorkShop>(_newLoader.GetObjectList<WorkShopDTO, WorkShop>());

            if (backgroundWorker.CancellationPending)
            {
                return;
            }
#endif

            // �������� ���� ������� ���������
            loadingState.CurrentPersentage = 9;
            loadingState.CurrentPersentageDescription = "Loading Base Details";
            backgroundWorker.ReportProgress(1, loadingState);

            BaseComponents = new BaseComponentCollection(NewLoader.GetObjectListAll<BaseComponentDTO, BaseComponent>(loadChild:true));
            
            if (backgroundWorker.CancellationPending)
            {
                return;
            }

            // ���������� ������ ����� ���������
            loadingState.CurrentPersentage = 10;
            loadingState.CurrentPersentageDescription = "Set Links";
            backgroundWorker.ReportProgress(1, loadingState);

			_newLoader.SetParentsToStores();
            _newLoader.SetParentsToBaseComponents();

            //foreach (Vehicle vehicle in Vehicles)
            //    vehicle.Operator = Operators.GetOperatorById(vehicle.OperatorId);

            foreach (Hangar store in Hangars)
                store.Operator = Operators.GetOperatorById(store.OperatorId);

            foreach (WorkShop store in WorkShops)
                store.Operator = Operators.GetOperatorById(store.OperatorId);

            _tempFileMonitoringThread = new Thread(TempFileDeleting) {IsBackground = true};
            _tempFileMonitoringThread.Start();
        }
        #endregion

        /*
         * ��������������� 
         */

        /*
         * ������� ��������
         */
        /*
         * ������������ ������ ��� ������� �������
         */

        /*
         * ������ � �������: ��������, ���������� �������
         */

        private bool _exit;
        private Thread _tempFileMonitoringThread;
        private List<FileProcess> _openFileProcesses = new List<FileProcess>();

        #region public void OpenFile(AttachedFile attachedFile, out string message)

        /// <summary>
        /// �������� ���� �� ��������� �����. � ����������� �� ���������� ����� ��������� ���������� �������
        /// </summary>
        /// <param name="attachedFile">���� ��� ��������</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public void OpenFile(AttachedFile attachedFile, out string message)
        {   
            message = "";

	        if (attachedFile.FileData == null)
            {
                attachedFile = _newLoader.GetObjectById<AttachedFileDTO, AttachedFile>(attachedFile.ItemId, true);

                if (attachedFile.FileData == null)
                    return;
            }

            var fileNameToRemove = Path.GetTempPath() + attachedFile.FileName;
            if (!File.Exists(fileNameToRemove))
            {
                try
                {
                    FileStream fileStreamBack = new FileStream(fileNameToRemove, FileMode.Create, FileAccess.Write);
                    fileStreamBack.Write(attachedFile.FileData, 0, attachedFile.FileData.Length);
                    fileStreamBack.Close();
                }
                catch (IOException ioException)
                {
                    message = ioException.Message;
                }
            }

            if (File.Exists(fileNameToRemove))
            {
                Process tempProcess = new Process();
                //����������� ���������� �����
                string pattern = @"\.\w+$";
                Match m = Regex.Match(attachedFile.FileName, pattern);
                string fileExtension = m.Groups[0].Value;
                //�����������,��� ������� ����, � ����������� �� ����������
                if (Regex.IsMatch(fileExtension, @"(\.[pP][dD][fF])"))
                    tempProcess.StartInfo.FileName = fileNameToRemove;
                if (Regex.IsMatch(fileExtension, @"(\.[pP][nN][gG])") ||
                    Regex.IsMatch(fileExtension, @"(\.[jJ][pP][gG])"))
                {
                    //tempProcess.StartInfo.FileName = "mspaint.exe";
                    tempProcess.StartInfo.FileName = fileNameToRemove;
                    tempProcess.StartInfo.Arguments = fileNameToRemove;
                }

                tempProcess.Start();
                FileProcess fileProcess = new FileProcess(attachedFile, fileNameToRemove, tempProcess);

                lock (_openFileProcesses)
                {
                    _openFileProcesses.Add(fileProcess);    
                }
            }
            else
            {
                message = "File : " + fileNameToRemove + " deleted or moved in another place." +
                            "\nPlease check file location or bing another file.";
            }
        }

		#endregion

		#region public void SaveAsFile(AttachedFile attachedFile, string filePath,out string message)

		public void SaveAsFile(AttachedFile attachedFile, string filePath,out string message)
	    {
		    message = "";

		    if (attachedFile.FileData == null)
		    {
				attachedFile = _newLoader.GetObjectById<AttachedFileDTO, AttachedFile>(attachedFile.ItemId, true);

				if (attachedFile.FileData == null)
				    return;
		    }

		    try
		    {
			    var fileStreamBack = new FileStream(filePath, FileMode.Create, FileAccess.Write);
			    fileStreamBack.Write(attachedFile.FileData, 0, attachedFile.FileData.Length);
			    fileStreamBack.Close();
		    }
		    catch (IOException ioException)
		    {
			    message = ioException.Message;
		    }
	    }

	    #endregion

        #region private void TempFileDeleting()

        /// <summary>
        /// �������� ���� �� ��������� �����. � ����������� �� ���������� ����� ��������� ��� � ���������� ��������
        /// </summary>
        /// <returns></returns>
        private void TempFileDeleting()
        {
            while (!_exit)
            {
                lock (_openFileProcesses)
                {
                    List<FileProcess> removedFileProcesses = new List<FileProcess>();
                    foreach (FileProcess fileProcess in _openFileProcesses.Where(p => File.Exists(p.FileNameToRemove)))
                    {
                        try
                        { 
                            File.Delete(fileProcess.FileNameToRemove);
                            removedFileProcesses.Add(fileProcess);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    if (removedFileProcesses.Count > 0)
                        foreach (FileProcess removedFileProcess in removedFileProcesses)
                            _openFileProcesses.Remove(removedFileProcess);
                }
                Thread.Sleep(10000);
            }

            if (!_exit) 
                return;
            foreach (FileProcess fileProcess in _openFileProcesses.Where(p => File.Exists(p.FileNameToRemove)))
            {
                while (File.Exists(fileProcess.FileNameToRemove))
                {
                    try
                    {
                        File.Delete(fileProcess.FileNameToRemove);
                    }
                    catch
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }

        #endregion

        /*
         * �������� ������ �� ���� 
         */

        #region public Loader Loader { get; }
        /// <summary>
        /// ��������� ������
        /// </summary>
        private ILoader _loader;
        /// <summary>
        /// ��������� ������ - �� �������� �������� �������� ��������� �����
        /// </summary>
        public ILoader Loader
        {
            get
            {
                if (_loader == null) _loader = new Loader(this);
                return _loader;
            }
        }
		#endregion


		/*
         * ��������������� ������� 
         */

		#region public Calculator Calculator { get; }
		/// <summary>
		/// ���������� �������������� ������� ������� Cas
		/// </summary>
		private Calculator _calculator;
        /// <summary>
        /// ���������� �������������� ������� ������� Cas
        /// </summary>
        public Calculator Calculator
        {
            get
            {
                return _calculator;
            }
	        set { _calculator = value; }
        }
        #endregion

        /*
         * ���������� ��������
         */

        #region public Keeper Keeper { get; }
        /// <summary>
        /// ���������� ���� ���������
        /// </summary>
        private Keeper _keeper;
        /// <summary>
        /// ���������� ���� ���������
        /// </summary>
        public Keeper Keeper
        {
            get
            {
                if (_keeper == null) _keeper = new Keeper(this, AuditRepository);
                //
                return _keeper;
            }
        }
        #endregion

        /*
         * ���������� ���������
         */

        #region public Manipulator Manipulator { get; }
        /// <summary>
        /// ���������� ���������
        /// </summary>
        private Manipulator _manipulator;
	    /// <summary>
        /// ���������� ���������
        /// </summary>
        public Manipulator Manipulator
        {
            get
            {
                if (_manipulator == null) _manipulator = new Manipulator(this, AuditRepository);
                //
                return _manipulator;
            }
        }
		#endregion


		#region public INewLoader NewLoader { get; }

		private INewLoader _newLoader;

	    public INewLoader NewLoader
	    {
		    get { return _newLoader ?? (_newLoader = new NewLoader(this)); }
	    }

		#endregion


		#region public NewKeeper NewKeeper { get; }
	    /// <summary>
	    /// ��������� ������
	    /// </summary>
	    private INewKeeper _newKeeper;
	    /// <summary>
	    /// ��������� ������ - �� �������� �������� �������� ��������� �����
	    /// </summary>
	    public INewKeeper NewKeeper
	    {
		    get
		    {
			    if (_newKeeper == null) _newKeeper = new NewKeeper(this, AuditRepository);
			    return _newKeeper;
		    }
	    }
	    #endregion

		private IAircraftsCore _aircraftsCore;

	    private string _ipServer;
	    private IAuditRepository _auditRepository;

	    //TODO: �������� �����. IAircraftsCore ������ ������������ ����� �����������
		public void SetAircraftCore(IAircraftsCore aircraftsCore)
	    {
		    _aircraftsCore = aircraftsCore;
	    }

		/*
		 * ����������
		*/

	    #region public  T CloneObject<T>(T source)

		public T CloneObject<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        #endregion
    }
}
