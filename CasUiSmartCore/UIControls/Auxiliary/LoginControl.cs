using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CAS.UI.Interfaces;
using CAS.UI.Management;
using CAS.UI.Properties;
using CAS.UI.Events;
using CAS.UI.Management.Dispatchering;
using CAS.UI.UIControls.AnimatedBackgroundWorker;
using CASTerms;
using Microsoft.SqlServer.Management.Common;
using Newtonsoft.Json;
using SmartCore.Entities;
using SmartCore.Management;
using SmartCore.Management.Settings;
using HelpEventHandler = CAS.UI.Events.HelpEventHandler;

namespace CAS.UI.UIControls.Auxiliary
{
    /// <summary>
    /// Элемент управления, предназначенный для подключения к SQL серверу
    /// </summary>
    internal partial class LoginControl : UserControl, IHelpRequester
    {

        #region Fields

        private BackgroundWorker backgroundWorker;
        private LoadingState _loadingState = new LoadingState();
        private LoadingForm _loadForm = new LoadingForm { ShowButtonsPanel = false };

        protected Label labelTitle;
        protected Panel panelLoginPasswordContainer;
        protected Panel panelConnectionSettingsContainer;
        protected LinkLabel linkLabelShowConnectionSettings;
        private PictureBox pictureBoxPasswordBorder;
        private PictureBox pictureBoxLoginBorder;
        private PictureBox pictureBoxServerNameBorder;
        private PictureBox pictureBoxAuthenticationBorder;
        private PictureBox pictureBoxConnectionStatus;

        private Label labelServerName;
        private TextBox textBoxLogin;
        private Label labelAuthentication;
        private Label labelLogin;
        private Label labelPassword;
        private ComboBox comboBoxServerName;
        private ComboBox comboBoxAuthentication;
        private Button buttonConnect;
        private Button buttonExit;
        private HelpRequestingLink linkHelp;
        private TextBox textBoxPassword;
        private CheckBox checkBoxRememberLogin;

        private readonly Font labelFont = new Font("Verdana", 12F, FontStyle.Regular);
        private readonly Font textBoxFont = new Font("Verdana", 22F, FontStyle.Regular);
        private readonly Font buttonFont = new Font("Verdana", 12F);

        private const int PADDING = 20;
        private readonly Size labelSize = new Size(150, 20);
        private readonly Size textBoxSize = new Size(270, 20);
        private readonly Size comboBoxSize = new Size(214, 20);
        private readonly Size buttonSize = new Size(103, 33);

        private ConnectionState _connectionStatus = ConnectionState.Closed;
        //private Thread connectionThread;
        private bool _isSimple = true;
        private string _topicId = "system_openning_system_login_types_of_authorization";
        private bool _casServerFound;
        private JsonSettings _settings;

        #endregion

        #region Constructor
        public LoginControl()
        {
            InitializeComponent();

            Initialize();
            //connectionThread = new Thread(Connect);
            Load += LoginControlLoad;
        }

        #endregion

        #region Delegates
        private delegate void eventInvoker();

        private delegate void failureEventInvoker(string reason);
        #endregion

        #region Events

        #region public event EventHandler Connected
        /// <summary>
        /// Событие, возникающее когда подключение прошло успешно
        /// </summary>
        public event EventHandler Connected;
        #endregion

        #region private void OnConnected()
        private void OnConnected()
        {
            //LoadingDisplayer displayer = new LoadingDisplayer();
            //displayer.Start();

            _connectionStatus = ConnectionState.Open;
            buttonExit.Text = "Exit";
            
            ConnectingFinished();
            //SaveSettings();

            if (Connected != null)
                Connected(this, new EventArgs());
        }
        #endregion

        #region public event EventHandler Connecting
        /// <summary>
        /// Событие, возникающее при начале подключения
        /// </summary>
        public event EventHandler Connecting;
        #endregion

        #region private void OnConnecting()
        private void OnConnecting()
        {
            pictureBoxConnectionStatus.Image = Resources.runner;
            SetEnabled(false, _isSimple);
            buttonExit.Text = "Cancel";
            _connectionStatus = ConnectionState.Connecting;
            if (Connecting != null)
                Connecting(this, new EventArgs());
        }
        #endregion

        #region public event EventHandler Disconnected
        /// <summary>
        /// Событие, возникающее при отключении
        /// </summary>
        public event EventHandler Disconnected;
        #endregion

        #region private void OnDisconnected()
        private void OnDisconnected()
        {
            _connectionStatus = ConnectionState.Closed;
            if (Disconnected != null)
                Disconnected(this, new EventArgs());
        }
        #endregion

        #region public event EventHandler ExitClicked
        /// <summary>
        /// Событие, возникающее при выходе
        /// </summary>
        public event EventHandler ExitClicked;
        #endregion

        #region private void OnExitClicked()
        private void OnExitClicked()
        {
            Disconnect();
            if (ExitClicked != null)
                ExitClicked(this, new EventArgs());
        }
        #endregion

        #region public event EventHandler ConnectionFailed
        /// <summary>
        /// Событие, возникающее при потере соединения с сервером
        /// </summary>
        public event EventHandler ConnectionFailed;
        #endregion

        #region private void OnFailed(string reason)
        private void OnFailed(string reason)
        {
            _connectionStatus = ConnectionState.Closed;
            buttonExit.Text = "Exit";
            Program.Provider.Logger.Log("Failed to connect", new Exception(reason));
            ConnectingFinished();
            if (ConnectionFailed != null)
                ConnectionFailed(this, new EventArgs());
        }
        #endregion

        #endregion

        #region Properties

        #region public bool IsConnecting
        /// <summary>
        /// Идет ли попытка подключения
        /// </summary>
        public bool IsConnecting
        {
            get { return _connectionStatus == ConnectionState.Connecting; }
        }
        #endregion

        #region public Button ButtonConnect
        /// <summary>
        /// Возвращает кнопку подключения к серверу
        /// </summary>
        public Button ButtonConnect
        {
            get { return buttonConnect; }
        }
        #endregion

        #endregion

        #region Methods

        #region private void Initialize()
        private void Initialize()
        {
            Width = 3 * PADDING + labelSize.Width + textBoxSize.Width;
            Height = 6 * PADDING + 4 * labelSize.Height + buttonSize.Height;


            labelServerName = new Label();
            labelAuthentication = new Label();
            labelLogin = new Label();
            labelPassword = new Label();
            comboBoxServerName = new ComboBox();
            comboBoxAuthentication = new ComboBox();
            textBoxLogin = new TextBox();
            textBoxPassword = new TextBox();
            buttonConnect = new Button();
            buttonExit = new Button();
            linkHelp = new HelpRequestingLink();
            checkBoxRememberLogin = new CheckBox();
            panelLoginPasswordContainer = new Panel();
            panelConnectionSettingsContainer = new Panel();
            labelTitle = new Label();
            pictureBoxLoginBorder = new PictureBox();
            pictureBoxPasswordBorder = new PictureBox();
            pictureBoxServerNameBorder = new PictureBox();
            pictureBoxAuthenticationBorder = new PictureBox();
            linkLabelShowConnectionSettings = new LinkLabel();
            //
            // labelTitle
            //
            labelTitle.Location = new Point(PADDING, PADDING - 10);
            labelTitle.AutoSize = true;
            labelTitle.Text = "Login form";
            labelTitle.Font = new Font("Verdana", 18F, FontStyle.Bold);
            labelTitle.BackColor = Color.Transparent;
            labelTitle.ForeColor = Color.White;
            //
            // panelLoginPasswordContainer
            //
            panelLoginPasswordContainer.TabIndex = 1;
            panelLoginPasswordContainer.Location = new Point(PADDING, PADDING + labelTitle.Height + PADDING);
            panelLoginPasswordContainer.Size = new Size(Width - 2 * PADDING, 160);
            panelLoginPasswordContainer.BackColor = Color.Transparent;

            pictureBoxConnectionStatus = new PictureBox();
            pictureBoxConnectionStatus.Dock = DockStyle.Bottom;
            pictureBoxConnectionStatus.Height = 10;
            pictureBoxConnectionStatus.BackColor = Color.Transparent;
            panelLoginPasswordContainer.Controls.Add(pictureBoxConnectionStatus);

            //
            // panelConnectionSettingsContainer
            //
            panelConnectionSettingsContainer.TabIndex = 2;
            panelConnectionSettingsContainer.Location =
                new Point(PADDING, panelLoginPasswordContainer.Top + panelLoginPasswordContainer.Height + 10);
            panelConnectionSettingsContainer.Size = new Size(Width - 2 * PADDING, 200);
            panelConnectionSettingsContainer.BackColor = Color.Transparent;
            panelConnectionSettingsContainer.Visible = false;
            //
            // labelLogin 
            //
            labelLogin.AutoSize = true;
            labelLogin.Location = new Point(0, 20);
            labelLogin.Font = labelFont;
            labelLogin.ForeColor = Color.White;
            labelLogin.TextAlign = ContentAlignment.MiddleLeft;
            labelLogin.Text = "User name:";
            //
            // labelPassword
            //
            labelPassword.Size = labelSize;
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(0, 70);
            labelPassword.Font = labelFont;
            labelPassword.ForeColor = Color.White;
            labelPassword.TextAlign = ContentAlignment.MiddleLeft;
            labelPassword.Text = "Password:";
            //
            // comboBoxServerName
            //
            comboBoxServerName.Size = comboBoxSize;
            comboBoxServerName.Location = new Point(186, 10);
            comboBoxServerName.Font = labelFont;
            comboBoxServerName.ForeColor = Color.White;
            comboBoxServerName.TabIndex = 6;
            comboBoxServerName.FlatStyle = FlatStyle.Flat;
            comboBoxServerName.BackColor = Color.FromArgb(52, 121, 191);
            comboBoxServerName.PreviewKeyDown += EnterPressed;
            //
            // comboBoxAuthentication
            //
            comboBoxAuthentication.Size = comboBoxSize;
            comboBoxAuthentication.Location = new Point(186, 50);
            comboBoxAuthentication.Font = labelFont;
            comboBoxAuthentication.ForeColor = Color.White;
            comboBoxAuthentication.TabIndex = 7;
            comboBoxAuthentication.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAuthentication.Items.Add("Simple");
            comboBoxAuthentication.Items.Add("Windows");
            comboBoxAuthentication.FlatStyle = FlatStyle.Flat;
            comboBoxAuthentication.BackColor = Color.FromArgb(52, 121, 191);
            comboBoxAuthentication.SelectedIndexChanged += comboBoxAuthentication_SelectedIndexChanged;
            comboBoxAuthentication.PreviewKeyDown += EnterPressed;
            //
            // labelServerName 
            //
            labelServerName.AutoSize = true;
            labelServerName.Location = new Point(0, 10);
            labelServerName.Font = labelFont;
            labelServerName.ForeColor = Color.White;
            labelServerName.TextAlign = ContentAlignment.MiddleLeft;
            labelServerName.Text = "Server name:";
            //
            // labelAuthentication
            //
            labelAuthentication.AutoSize = true;
            labelAuthentication.Location = new Point(0, 55);
            labelAuthentication.Font = labelFont;
            labelAuthentication.ForeColor = Color.White;
            labelAuthentication.TextAlign = ContentAlignment.MiddleLeft;
            labelAuthentication.Text = "Authentication:";
            //
            // textBoxLogin
            //
            textBoxLogin.Size = textBoxSize;
            textBoxLogin.Location = new Point(130, 12);
            textBoxLogin.Font = textBoxFont;
            textBoxLogin.ForeColor = Color.White;
            textBoxLogin.TabIndex = 1;
            textBoxLogin.BackColor = Color.FromArgb(52, 121, 191);
            textBoxLogin.BorderStyle = BorderStyle.None;
            textBoxLogin.Text = "username";
            textBoxLogin.PreviewKeyDown += EnterPressed;
            //
            // textBoxPassword
            //
            textBoxPassword.Size = textBoxSize;
            textBoxPassword.Location = new Point(130, 62);
            textBoxPassword.Font = textBoxFont;
            textBoxPassword.ForeColor = Color.White;
            textBoxPassword.TabIndex = 2;
            textBoxPassword.BackColor = Color.FromArgb(52, 121, 191);
            textBoxPassword.PasswordChar = '•';
            textBoxPassword.BorderStyle = BorderStyle.None;
            textBoxPassword.PreviewKeyDown += EnterPressed;
            //
            // pictureBoxPasswordBorder
            //
            pictureBoxPasswordBorder.Location = new Point(textBoxPassword.Left - 2, textBoxPassword.Top - 2);
            pictureBoxPasswordBorder.Size = new Size(textBoxPassword.Width + 4, textBoxPassword.Height - 3);
            pictureBoxPasswordBorder.BackColor = Color.White;
            //
            // pictureBoxLoginBorder
            //
            pictureBoxLoginBorder.Location = new Point(textBoxLogin.Left - 2, textBoxLogin.Top - 2);
            pictureBoxLoginBorder.Size = new Size(textBoxLogin.Width + 4, textBoxLogin.Height - 3);
            pictureBoxLoginBorder.BackColor = Color.White;
            //
            // pictureBoxAuthenticationBorder
            //
            pictureBoxAuthenticationBorder.Location = new Point(185, 49);
            pictureBoxAuthenticationBorder.Size = new Size(comboBoxAuthentication.Width + 2, 28);
            pictureBoxAuthenticationBorder.BackColor = Color.White;
            //
            // pictureBoxServerNameBorder
            //
            pictureBoxServerNameBorder.Location = new Point(185, 9);
            pictureBoxServerNameBorder.Size = new Size(comboBoxServerName.Width + 2, 28);
            pictureBoxServerNameBorder.BackColor = Color.White;
            //
            // buttonConnect
            //
            buttonConnect.Size = buttonSize;
            //buttonConnect.Location = new Point(Width - 3*PADDING - 3*buttonSize.Width,5*PADDING + 4*textBoxSize.Height);
            buttonConnect.FlatStyle = FlatStyle.Flat;
            buttonConnect.FlatAppearance.BorderSize = 0;
            buttonConnect.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 186, 0);
            buttonConnect.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 180, 0);
            buttonConnect.BackColor = Color.FromArgb(255, 138, 0);
            buttonConnect.Location = new Point(186, 110);
            buttonConnect.Font = buttonFont;
            buttonConnect.ForeColor = Color.White;
            buttonConnect.TabIndex = 3;
            buttonConnect.Text = "Connect";
            buttonConnect.Click += ButtonConnectClick;
            buttonConnect.Enabled = false;
            //
            // buttonExit
            //
            buttonExit.Size = buttonSize;
            //buttonExit.Location = new Point(Width - 2*PADDING - 2*buttonSize.Width,5*PADDING + 4*textBoxSize.Height);
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.FlatAppearance.BorderSize = 0;
            buttonExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 186, 0);
            buttonExit.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 180, 0);
            buttonExit.BackColor = Color.FromArgb(255, 138, 0);
            buttonExit.Location = new Point(186 + PADDING / 2 + buttonConnect.Width, 110);
            buttonExit.Font = buttonFont;
            buttonExit.ForeColor = Color.White;
            buttonExit.TabIndex = 4;
            buttonExit.Text = "Exit";
            buttonExit.Click += ButtonExitClick;
            //
            // linkHelp
            //
            linkHelp.Size = buttonSize;
            linkHelp.Location = new Point(376, 17);
            linkHelp.Font = labelFont;
            linkHelp.TopicId = "system-login.html";
            linkHelp.ActiveLinkColor = Color.White;
            linkHelp.VisitedLinkColor = Color.White;
            linkHelp.LinkColor = Color.White;
            linkHelp.ForeColor = Color.Transparent;
            linkHelp.TabIndex = 0;
            linkHelp.Text = "Help";
            linkHelp.Visible = false;
            linkHelp.Click += LinkHelpClick;
            //
            // linkLabelShowConnectionSettings
            //
            linkLabelShowConnectionSettings.Text = "Connection settings";
            linkLabelShowConnectionSettings.ActiveLinkColor = Color.White;
            linkLabelShowConnectionSettings.VisitedLinkColor = Color.White;
            linkLabelShowConnectionSettings.LinkColor = Color.White;
            linkLabelShowConnectionSettings.ForeColor = Color.Transparent;
            linkLabelShowConnectionSettings.Location = new Point(0, 115);
            linkLabelShowConnectionSettings.Font = new Font(labelFont.FontFamily, labelFont.Size - 1);
            linkLabelShowConnectionSettings.AutoSize = true;
            linkLabelShowConnectionSettings.TabStop = true;


	        linkLabelShowConnectionSettings.Visible = false;

            

            linkLabelShowConnectionSettings.PreviewKeyDown += linkLabelShowConnectionSettings_PreviewKeyDown;
            linkLabelShowConnectionSettings.Click += LinkLabelShowConnectionSettingsClick;
            
            panelLoginPasswordContainer.Controls.Add(labelLogin);
            panelLoginPasswordContainer.Controls.Add(labelPassword);
            panelLoginPasswordContainer.Controls.Add(textBoxLogin);
            panelLoginPasswordContainer.Controls.Add(textBoxPassword);
            panelLoginPasswordContainer.Controls.Add(pictureBoxPasswordBorder);
            panelLoginPasswordContainer.Controls.Add(pictureBoxLoginBorder);
            panelLoginPasswordContainer.Controls.Add(buttonConnect);
            panelLoginPasswordContainer.Controls.Add(buttonExit);
            panelLoginPasswordContainer.Controls.Add(linkLabelShowConnectionSettings);

            panelConnectionSettingsContainer.Controls.Add(labelServerName);
            panelConnectionSettingsContainer.Controls.Add(labelAuthentication);
            panelConnectionSettingsContainer.Controls.Add(comboBoxServerName);
            panelConnectionSettingsContainer.Controls.Add(comboBoxAuthentication);
            panelConnectionSettingsContainer.Controls.Add(pictureBoxAuthenticationBorder);
            panelConnectionSettingsContainer.Controls.Add(pictureBoxServerNameBorder);

            Controls.Add(panelLoginPasswordContainer);
            Controls.Add(panelConnectionSettingsContainer);
            Controls.Add(labelTitle);
            Controls.Add(linkHelp);
        }

        void linkLabelShowConnectionSettings_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                panelConnectionSettingsContainer.Visible = !panelConnectionSettingsContainer.Visible;
            }
        }
        #endregion

        #region void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _loadingState.Stages = 2;
                _loadingState.CurrentStage = 1;
                _loadingState.CurrentStageDescription = "Core Initialization";
                _loadingState.CurrentPersentage = 0;
                _loadingState.MaxPersentage = 0;
                _loadingState.CurrentPersentageDescription = "";

                backgroundWorker.ReportProgress(1, _loadingState);

                GlobalObjects.CasEnvironment.Reset();
                GlobalObjects.CasEnvironment.InitAsync(backgroundWorker, _loadingState);

                _loadingState.CurrentStage = 2;
                _loadingState.CurrentStageDescription = "Calculator Initialization";
                _loadingState.CurrentPersentage = 0;
                _loadingState.MaxPersentage = 0;
                _loadingState.CurrentPersentageDescription = "";

                backgroundWorker.ReportProgress(2, _loadingState);

                GlobalObjects.CasEnvironment.Calculator.InitAsync(backgroundWorker, _loadingState);
            }
            catch (Exception ex)
            {
                Program.Provider.Logger.Log("Error while init the core", ex);
            }
        }
        #endregion

        #region void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                _loadForm.LoadingState = _loadingState;
            }
            catch (Exception ex)
            {
                Program.Provider.Logger.Log("Error while init the core", ex);
            }
        }
        #endregion

        #region void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _loadForm.Hide();
            //Разлокировка кнопки Exit
            buttonExit.Enabled = true;

            OnConnectedAction();
            OnConnected();
        }
        #endregion

        #region private void LinkLabelShowConnectionSettingsClick(object sender, EventArgs e)

        private void LinkLabelShowConnectionSettingsClick(object sender, EventArgs e)
        {
            panelConnectionSettingsContainer.Visible = !panelConnectionSettingsContainer.Visible;
        }

        #endregion

        #region private void LoadSettings()
        /// <summary>
        /// Создает новый элемент управления для подключения к базе данных
        /// </summary>
        private void LoadSettings()
        {
            LoginSettingsContainer settings = LoginSettingsProvider.ReadSettings();
            string[] servers = settings.Servers;
            if (servers != null)
            {
                comboBoxServerName.Items.Clear();
                comboBoxServerName.Items.AddRange(servers);
            }
            comboBoxServerName.Text = settings.LastConnectedServer;
            _isSimple = settings.IsSimpleAuthentication;
            if (_isSimple)
            {
                comboBoxAuthentication.SelectedIndex = 0;
                checkBoxRememberLogin.Checked = settings.SaveUsernamePassword;
                textBoxLogin.Text = settings.Username;
            }
            else
            {
                comboBoxAuthentication.SelectedIndex = 1;
            }
            SetEnabled(true, _isSimple);
        }
        #endregion

        #region private void SaveSettings()
        private void SaveSettings()
        {
            string[] servers = new string[comboBoxServerName.Items.Count];
            for (int i = 0; i < comboBoxServerName.Items.Count; i++)
                servers[i] = (string)comboBoxServerName.Items[i];
            LoginSettingsContainer settings =
                new LoginSettingsContainer(servers, comboBoxServerName.Text, _isSimple, checkBoxRememberLogin.Checked,
                                           textBoxLogin.Text, "");
            LoginSettingsProvider.SaveSettings(settings);
        }
        #endregion

        #region private void SetEnabled(bool value, bool authentication)
        /// <summary>
        /// Устанавливает свойство Enabled всем полям по заданному значению
        /// </summary>
        /// <param name="value">Новое значение свойства Enabled</param>
        /// <param name="authentication">true - если Authentication="Simple", false - если Authentication="Windows"</param>
        /// <remarks>Используется, когда необходимо произвести подключение</remarks>
        private void SetEnabled(bool value, bool authentication)
        {
            //labelServerName.Enabled = value;
            //labelAuthentication.Enabled = value;
            //labelLogin.Enabled = value;
            //labelPassword.Enabled = value;
            comboBoxServerName.Enabled = value;
            comboBoxAuthentication.Enabled = value;
            buttonConnect.Enabled = value && _casServerFound;
            //linkHelp.Enabled = value;
            checkBoxRememberLogin.Enabled = value && authentication;
            textBoxLogin.Enabled = value && authentication;
            textBoxPassword.Enabled = value && authentication;
        }
        #endregion

        #region private void EnterPressed(object sender, PreviewKeyDownEventArgs e)

        private void EnterPressed(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                StartConnection();
        }

        #endregion

        #region private void ButtonConnectClick(object sender, EventArgs e)
        private void ButtonConnectClick(object sender, EventArgs e)
        {
            LicenseDispatcher.CheckLicenseInformation();
            if (LicenseDispatcher.IsUSBKeyFound)
            {
                StartConnection();
            }
            else
            {
                if (LicenseDispatcher.IsLicenseFound)
                    StartConnection();
            }
        }
        #endregion

        #region private void StartConnection()

        private void StartConnection()
        {
	        string serverName;
#if DemoDebug
			serverName = "91.213.233.139,1433\\MSSQLSERVER@CASDBTest";
#else
			serverName = comboBoxServerName.Text;
#endif

			var settings = new ConnectionSettingsContainer(_settings.ConnectionStrings[serverName], textBoxLogin.Text, textBoxPassword.Text, _isSimple);
            Connect(settings);
            //connectionThread = new Thread(Connect);
            //connectionThread.Start(settings);
        }

        #endregion

        #region private void comboBoxAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBoxAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool value = (comboBoxAuthentication.SelectedIndex == 0);
            _isSimple = value;
            SetEnabled(true, _isSimple);
        }
        #endregion

        #region private void ButtonExitClick(object sender, EventArgs e)
        private void ButtonExitClick(object sender, EventArgs e)
        {
            if (_connectionStatus == ConnectionState.Connecting)
            {
                CancelConnection();
            }
            else
            {
                OnExitClicked();
            }
        }
        #endregion

        #region private void Connect(object obj)
        private void Connect(object obj)
        {
            //ModeProvider provider = Program.Provider;
            //ILogger currentLogger = provider.Logger;
            //currentLogger.BeginProcess(ConnectionAction, obj);
            ConnectionAction(obj);
        }

        private void ConnectionAction(Object obj)
        {
            ConnectionSettingsContainer settings = obj as ConnectionSettingsContainer;
            if (settings != null)
            {
	            AuthenticationType authentication = AuthenticationType.Windows;
	            if (settings.IsSimple)
		            authentication = AuthenticationType.SqlServer;
	            OnConnecting();
	            string message;
	            //if (settings.ServerName.Split('@').Length >= 2)
	            //{
	            //    string serverName = settings.ServerName.Split('@')[0];
	            //    string baseName = settings.ServerName.Split('@')[1];

	            try
	            {
		            //GlobalObjects.CasEnvironment.Connect(serverName, settings.Username, settings.Password, baseName);
		            GlobalObjects.CasEnvironment.Connect(settings.ServerName, settings.Username, settings.Password, "");
		            SaveJsonSetting(settings.Username);
	            }
	            catch (ConnectionFailureException ex)
	            {
		            if (ex.InnerException != null)
		            {
			            var sqlException = ex.InnerException as SqlException;
			            if (sqlException != null)
				            MessageBox.Show(sqlException.Message, "Failed to connect server", MessageBoxButtons.OK,
					            MessageBoxIcon.Error);
			            else
				            MessageBox.Show("Server was not found or is not available", "Failed to connect server",
					            MessageBoxButtons.OK, MessageBoxIcon.Error);
		            }
		            else
		            {
			            MessageBox.Show("Server was not found or is not available", "Failed to connect server",
				            MessageBoxButtons.OK, MessageBoxIcon.Error);
		            }

		            return;
	            }
	            catch (Exception ex)
	            {
		            MessageBox.Show(ex.Message, "Failed to connect server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetEnabled(true, _isSimple);
                    _connectionStatus = ConnectionState.Closed;
                    buttonExit.Text = "Exit";
		            return;
	            }



	            //нажатие клавиши Enter
	            buttonExit.Enabled = false;

	            _loadForm.Show();
	            backgroundWorker.RunWorkerAsync();

            }
            else
            {
                OnFailed("Wrong connection parameters");
                //Invoke(new failureEventInvoker(OnFailed), new object[] { "Wrong connection parameters" });
            }
        }

        #endregion

        protected virtual void OnConnectedAction()
        {
        }

        #region private void Disconnect()
        private void Disconnect()
        {
            //if (connectionThread.IsAlive)
            //{
            //    try
            //    {
            //        connectionThread.Abort();
            //    }
            //    catch
            //    {

//                }
                GlobalObjects.CasEnvironment.Disconnect();
                OnDisconnected();
  //          }
        }
        #endregion

        #region private void CancelConnection()
        private void CancelConnection()
        {
            try
            {
                //connectionThread.Abort();
            }
            catch
            { }
            ConnectingFinished();
            GlobalObjects.CasEnvironment.Disconnect();
            OnDisconnected();
        }
        #endregion

        #region private void ConnectingFinished()
        private void ConnectingFinished()
        {
            pictureBoxConnectionStatus.Image = null;
            SetEnabled(true, _isSimple);
            buttonExit.Text = "Exit";
        }
        #endregion

        #region private void LinkHelpClick(object sender, EventArgs e)
        private void LinkHelpClick(object sender, EventArgs e)
        {
            //RequestHelp(_topicId);
        }
        #endregion

        #region private void LoginControlLoad(object sender, EventArgs e)

        private void LoginControlLoad(object sender, EventArgs e)
        {
            textBoxPassword.Focus();
            if (textBoxPassword.ReadOnly || !textBoxPassword.Enabled)
                buttonConnect.Focus();

            //CasNetworkObserver.CasNetworkObserver networkObserver = new CasNetworkObserver.CasNetworkObserver();
            //networkObserver.CasServerFound += NetworkObserverCasServerFound;
            //networkObserver.FindServers();

            //LoadSettings();
            LoadJsonSettings();

			_casServerFound = true;
            if (comboBoxServerName.Items.Count == 0)
            {
                panelConnectionSettingsContainer.Visible = true;
                comboBoxAuthentication.SelectedIndex = 0;
            }
            buttonConnect.Enabled = true;
        }

		#endregion

		private void SaveJsonSetting(string login)
		{
			var exePath = Path.GetDirectoryName(Application.ExecutablePath);
			var path = Path.Combine(exePath, "AppSettings.json");
			var json = File.ReadAllText(path);
			_settings = JsonConvert.DeserializeObject<JsonSettings>(json);

			if (_settings.LastInformation.Login.Equals(login))
				return;

			_settings.LastInformation.Login = login;
			var output = JsonConvert.SerializeObject(_settings, Newtonsoft.Json.Formatting.Indented);
			File.WriteAllText(path, output);
		}

		private void LoadJsonSettings()
		{
			string exePath = Path.GetDirectoryName(Application.ExecutablePath);
			var path = Path.Combine(exePath, "AppSettings.json");
			var json = File.ReadAllText(path);
			_settings = JsonConvert.DeserializeObject<JsonSettings>(json);

			if (_settings != null)
			{
				comboBoxServerName.Items.Clear();
				comboBoxServerName.Items.AddRange(_settings.ConnectionStrings.Select(i => i.Key).ToArray());

				if (_settings.LastInformation != null)
				{
					textBoxLogin.Text = _settings.LastInformation.Login;
					comboBoxServerName.SelectedItem = _settings.LastInformation.Server;
				}
			}

			comboBoxAuthentication.SelectedIndex = 0;
			SetEnabled(true, _isSimple);
		}

		#region private void NetworkObserverCasServerFound(string database)

		private void NetworkObserverCasServerFound(string database)
        {
            if (InvokeRequired)
            {
                Invoke(
                    new CasNetworkObserver.CasNetworkObserver.CasServerFoundEventHandler(NetworkObserverCasServerFound),
                    new object[] { database });
            }
            else
            {
                LoadSettings();
                _casServerFound = true;
                if (database != "")
                {
                    if (!comboBoxServerName.Items.Contains(database))
                        comboBoxServerName.Items.Add(database);
                    comboBoxServerName.Text = database;
                    comboBoxAuthentication.SelectedIndex = 0;
                }
                if (comboBoxServerName.Items.Count == 0)
                {
                    panelConnectionSettingsContainer.Visible = true;
                    comboBoxAuthentication.SelectedIndex = 0;
                }
                buttonConnect.Enabled = true;
            }
        }

        #endregion

        #endregion

        #region IHelpRequester Members

        /// <summary>
        /// Occurs when help was requested by object
        /// </summary>
        public new event HelpEventHandler HelpRequested;

        ///<summary>
        /// Описание раздела помощи
        ///</summary>
        public string TopicId
        {
            get { return _topicId; }
            set { _topicId = value; }
        }

        /// <summary>
        /// Вызывается событие HelpRequested
        /// </summary>
        public void RequestHelp()
        {
            RequestHelp(_topicId);
        }

        /// <summary>
        /// Вызывается событие HelpRequested для отображения справки заданного раздела
        /// </summary>
        /// <param name="topicId">Описание раздела справки</param>
        public void RequestHelp(string topicId)
        {
            if (HelpRequested != null)
                HelpRequested.Invoke(this, new HelpEventHandlerArgs(_topicId));
        }

        #endregion
    }
}