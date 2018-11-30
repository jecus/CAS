namespace SmartCore.Management.Settings

{
    /// <summary>
    /// �����, ���������� ��������� �����������
    /// </summary>
    public class ConnectionSettingsContainer
    {
        private string password;
        private string serverName;
        private string userName;
        private bool isSimple;

        #region Constructors
        /// <summary>
        /// ��������� ����� ������ ��� �������� �������� ����������� � �������(SQLServer) ���������������
        /// </summary>
        /// <param name="serverName">������ �����������</param>
        public ConnectionSettingsContainer(string serverName)
        {
            this.serverName = serverName;
            isSimple = false;
            password = "";
            userName = "";
        }

        /// <summary>
        /// ��������� ����� ������ ��� �������� �������� ����������� � �������(SQLServer) ���������������
        /// </summary>
        /// <param name="serverName">������ �����������</param>
        /// <param name="userName">��� ������������</param>
        /// <param name="password">������</param>
        public ConnectionSettingsContainer(string serverName, string userName, string password)
        {
            this.serverName = serverName;
            isSimple = true;
            this.userName = userName;
            this.password = password;
        }

        /// <summary>
        /// ��������� ����� ������ ��� �������� �������� ����������� � �������(SQLServer) ���������������
        /// </summary>
        /// <param name="serverName">������ �����������</param>
        /// <param name="userName">��� ������������</param>
        /// <param name="password">������</param>
        /// <param name="isSimple">��� �������������� - SQLServer, ���� true</param>
        public ConnectionSettingsContainer(string serverName, string userName, string password, bool isSimple)
        {
            this.serverName = serverName;
            this.userName = userName;
            this.password = password;
            this.isSimple = isSimple;
        }
        #endregion

        #region Properties
        /// <summary>
        /// ������ �����������
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// ��� ������������
        /// </summary>
        public string Username
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// ������ ��� �����������
        /// </summary>
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        /// <summary>
        /// �������� �� �������������� �������(SQLServer)
        /// </summary>
        public bool IsSimple
        {
            get { return isSimple; }
            set { isSimple = value; }
        }
        #endregion
    }
}
