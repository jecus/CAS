using System;
using System.Xml;

namespace CASTerms
{
    /// <summary>
    /// Класс отвечающий за доступ к настройкам подключений к серверу
    /// </summary>
    public static class LoginSettingsProvider
    {
        private static char[] separators = new char[1]{'▒'};

        #region Properties
        private static char[] Separators
        {
            get
            {
                return separators;
            }
        }

        private static string ServerNames
        {
            get
            {
                return "ServerNames";
            }
        }

        private static string LastConnectedServer
        {
            get
            {
                return "LastConnectedServer";
            }
        }

        private static string AuthenticationType
        {
            get
            {
                return "AuthenticationType";
            }
        }

        private static string LoginSettings
        {
            get
            {
                return "LoginSettings";
            }
        }

        private static string Login
        {
            get
            {
                return "Login";
            }
        }

        private static string Password
        {
            get
            {
                return "Password";
            }
        }

        private static string SaveLoginPassword
        {
            get
            {
                return "SaveLoginPassword";
            }
        }
        
        private static string FilePath
        {
            get
            {
                GlobalTermsProvider instance= new GlobalTermsProvider();
                return instance.GetLoginSettingsPath();
            }
        }

        #endregion

        #region public static LoginSettingsContainer ReadSettings()
        /// <summary>
        /// Считать настройки подключения
        /// </summary>
        /// <returns>Объект, содержащий настройки подключения</returns>
        public static LoginSettingsContainer ReadSettings()
        {
            LoginSettingsContainer settings = new LoginSettingsContainer();
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
            xmlReaderSettings.IgnoreWhitespace = true;
            xmlReaderSettings.IgnoreComments = true;
            XmlReader reader = null;
            try
            {
                reader = XmlReader.Create(FilePath, xmlReaderSettings);
                reader.ReadStartElement(LoginSettings);
                settings.LastConnectedServer = reader.ReadElementString(LastConnectedServer);
                settings.Servers = reader.ReadElementString(ServerNames).Split(Separators);
                settings.IsSimpleAuthentication = bool.Parse(reader.ReadElementString(AuthenticationType));
                if (settings.IsSimpleAuthentication)
                {
                    settings.SaveUsernamePassword = bool.Parse(reader.ReadElementString(SaveLoginPassword));
                    settings.Username = reader.ReadElementString(Login);
                    if (settings.SaveUsernamePassword)
                    {
                        settings.Password = reader.ReadElementString(Password);
                    }
                }
                reader.ReadEndElement();
                reader.Close();
            }
            catch
            {}
            if (reader != null)
                reader.Close();
            return settings;
        }
        #endregion

        #region public static void SaveSettings(LoginSettingsContainer settings)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public static void SaveSettings(LoginSettingsContainer settings)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
            xmlWriterSettings.Indent = true;
            try
            {
                XmlWriter writer = XmlWriter.Create(FilePath, xmlWriterSettings);
                writer.WriteStartElement(LoginSettings);
                writer.WriteElementString(LastConnectedServer, settings.LastConnectedServer);
                string servers = "";
                foreach (string server in settings.Servers)
                {
                    if (servers.Length > 0)
                        servers = servers + separators[0] + server;
                    else
                        servers = server;
                }
                if (!servers.Contains(settings.LastConnectedServer))
                {
                    if (servers.Length == 0)
                        servers = settings.LastConnectedServer;
                    else
                        servers = servers + separators[0] + settings.LastConnectedServer;
                }
                writer.WriteElementString(ServerNames, servers);
                writer.WriteElementString(AuthenticationType, settings.IsSimpleAuthentication.ToString());
                if (settings.IsSimpleAuthentication)
                {
                    writer.WriteElementString(SaveLoginPassword, settings.SaveUsernamePassword.ToString());
                    writer.WriteElementString(Login, settings.Username);
                    if (settings.SaveUsernamePassword)
                    {
                        writer.WriteElementString(Password, settings.Password);
                    }
                }
                writer.WriteEndElement();
                writer.Close();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
        #endregion
    }

    #region public static class LoginSettingsProviderRegistry
    /*public static class LoginSettingsProviderRegistry
    {
        #region Properties
        private static string ServerNames
        {
            get
            {
                return "ServerNames";
            }
        }

        private static string ConnectedServer
        {
            get
            {
                return "ConnectedServer";
            }
        }

        private static string AuthenticationType
        {
            get
            {
                return "AuthenticationType";
            }
        }
        #endregion

        #region public static string[] GetServerNames()
        /// <summary>
        /// Считать из реестра список серверов к которым были установлены подключения ранее
        /// </summary>
        /// <returns></returns>
        public static string[] GetServerNames()
        {
            RegistryKey key = RegistrySettings.OpenRegistry(RegistrySettings.LoginSettingsPath, false);
            if (key != null)
            {
                string[] servers = (string[])key.GetValue(ServerNames);
                key.Close();
                return servers;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region public static void SetConnectedServerName(string server)
        /// <summary>
        /// Добавить имя сервера в список серверов и сделать его как подключением по умолчанию
        /// </summary>
        /// <param name="server">Имя сервера</param>
        public static void SetConnectedServerName(string server)
        {
            RegistryKey key = RegistrySettings.OpenRegistry(RegistrySettings.LoginSettingsPath, true);
            AddToServerList(server, key);
            if (key == null)
            {
                key = RegistrySettings.CreateRegistry(RegistrySettings.LoginSettingsPath);
            }
            key.SetValue(ConnectedServer, server);
            key.Close();
        }
        #endregion

        #region private static void AddToServerList(string server, RegistryKey key)

        private static void AddToServerList(string server, RegistryKey key)
        {
            List<string> servers = new List<string>();
            string[] readServers = (string[])key.GetValue(ServerNames);
            if (readServers != null)
                servers.AddRange(readServers);
            bool serverExist = false;
            foreach (string name in servers)
            {
                serverExist |= (name == server);
            }
            if (!serverExist)
            {
                servers.Add(server);
                key.SetValue(ServerNames, servers.ToArray());
            }
        }

        #endregion

        #region public static string GetConnectedServerName()

        /// <summary>
        /// Считать из реестра имя сервера к которому последний раз было установлено подключение
        /// </summary>
        /// <returns></returns>
        public static string GetConnectedServerName()
        {
            RegistryKey key = RegistrySettings.OpenRegistry(RegistrySettings.LoginSettingsPath, true);
            string server = null;
            if (key != null)
            {
                server = (string)key.GetValue(ConnectedServer);
                key.Close();
            }
            return server;
        }

        #endregion

        #region public static bool IsSimpleAuthenitacion()

        /// <summary>
        /// Считать данные о типе аутентификация
        /// </summary>
        /// <returns>Является ли тип аутентикации Simple</returns>
        public static bool IsSimpleAuthenitacion()
        {
            RegistryKey key = RegistrySettings.OpenRegistry(RegistrySettings.LoginSettingsPath, false);
            if (key != null)
            {
                bool result;
                bool.TryParse((string)key.GetValue(AuthenticationType), out result);
                key.Close();
                return result;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region public static void SetAuthentication(bool isSimple)

        /// <summary>
        /// Установить тип аутентификации
        /// </summary>
        /// <param name="isSimple">Является ли он Simple</param>
        public static void SetAuthentication(bool isSimple)
        {
            RegistryKey key = RegistrySettings.OpenRegistry(RegistrySettings.LoginSettingsPath, true);
            if (key != null)
            {
                key.SetValue(AuthenticationType, isSimple.ToString());
                key.Close();
            }
        }

        #endregion

    }*/
    #endregion
}
