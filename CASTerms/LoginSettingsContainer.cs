namespace CASTerms
{
    /// <summary>
    /// ������ ��������� ��������� �����������
    /// </summary>
    public class LoginSettingsContainer
    {
        private string _password;
        private string _username;
        private string _lastConnectedServer;
        private string[] _servers;
        private bool _isSimpleAuthentication;
        private bool _saveUsernamePassword;

        /// <summary>
        /// ��������� ������ ���������� ��������� �����������
        /// </summary>
        internal LoginSettingsContainer()
        {
            _password = "";
            _username = "";
            _lastConnectedServer = "";
            _servers = new string[0];
            _isSimpleAuthentication = false;
            _saveUsernamePassword = false;
        }

        /// <summary>
        /// ��������� ������ ���������� ��������� �����������
        /// </summary>
        /// <param name="servers">������ �������� � ������� ����������� �����������</param>
        /// <param name="lastConnectedServer">������ � �������� ���� ��������� �������� �����������</param>
        /// <param name="isSimpleAuthentication">���������� ��� ��������������</param>
        /// <param name="saveUsernamePassword">��������� �� ����� � ������</param>
        /// <param name="password">������</param>
        /// <param name="username">�����</param>
        public LoginSettingsContainer(string[] servers, string lastConnectedServer, bool isSimpleAuthentication, bool saveUsernamePassword, string username, string password)
        {
            _servers = servers;
            _lastConnectedServer = lastConnectedServer;
            _isSimpleAuthentication = isSimpleAuthentication;
            _saveUsernamePassword = saveUsernamePassword;
            _password = password;
            _username = username;
        }

        /// <summary>
        /// ������
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// Login 
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// ������ � �������� ���� ��������� �������� �����������
        /// </summary>
        public string LastConnectedServer
        {
            get { return _lastConnectedServer; }
            set { _lastConnectedServer = value; }
        }

        /// <summary>
        /// ������ �������� � ������� ����������� �����������
        /// </summary>
        public string[] Servers
        {
            get { return _servers; }
            set { _servers = value; }
        }

        /// <summary>
        /// �������� �� ����������� Simple(SqlServer)
        /// </summary>
        public bool IsSimpleAuthentication
        {
            get { return _isSimpleAuthentication; }
            set { _isSimpleAuthentication = value; }
        }

        /// <summary>
        /// ��������� �� ������ � Login
        /// </summary>
        public bool SaveUsernamePassword
        {
            get { return _saveUsernamePassword; }
            set { _saveUsernamePassword = value; }
        }
    }
}