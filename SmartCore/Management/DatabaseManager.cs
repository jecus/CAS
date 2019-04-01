using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data;
using SmartCore.Auxiliary;
using SmartCore.Calculations;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Attributes;
using SmartCore.Entities.General.Interfaces;
using SmartCore.Queries;

namespace SmartCore.Management
{


    // ��������: 
    // ����� ���������� ������ �� ������
    // ����� ���������� ������ �� �������
    // ���������� ����������� �������� 

    /// <summary>
    /// ��������� ������ �������������� � ����� ������
    /// </summary>
    public class DatabaseManager : IDatabaseManager
	{

        private Int32 _queries;

        private const bool Debug = true;

        private bool _transactionOpen;

        #region public ServerConnection ServerConnection { get; }
        /// <summary>
        /// �������� ����������� � �������
        /// </summary>
        private ServerConnection _serverConnection;
        /// <summary>
        /// ���������� �������� ����������� � �������
        /// </summary>
        public ServerConnection ServerConnection { get { return _serverConnection; } }
        #endregion

        #region public Server Server { get; }
        /// <summary>
        /// ������������ ������
        /// </summary>
        private Server _server;
        /// <summary>
        /// ������������ ������
        /// </summary>
        public Server Server { get { return _server; } }
        #endregion

        #region public Database Database { get; set; }
        /// <summary>
        /// ���� ������, ������� ����� ������������ �� ��������� ��� ���������� ��������
        /// </summary>
        public Database Database { get; set; }
        #endregion

        #region public Logger Logger { get; }
        /// <summary>
        /// ����������� �������� � ���� ������
        /// </summary>
        private Logger _logger;
        /// <summary>
        /// ����������� �������� � ���� ������
        /// </summary>
        public Logger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = new Logger("casdblog.txt");
                    _logger.Clear();
                    _logger.Write(DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                }
                //
                return _logger;
            }
        }
        #endregion

        #region public bool TransactionOpen
        /// <summary>
        /// ������� �� �������� ����������
        /// </summary>
        public bool TransactionOpen
        {
            get { return _transactionOpen; }
        }
        #endregion
        //

        #region public DatabaseManager()
        /// <summary>
        /// ��������� �������������� � ����� ������ CAS
        /// </summary>
        public DatabaseManager()
        {
        }
        #endregion

        // ����������� � ������� 

        #region public void Connect(string serverName, string userName, string pass)
        /// <summary>
        /// ������������ � ������� ��������� ������� ������ � ������
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        public void Connect(string serverName, string userName, string pass)
        {
            string connectionString = GetConnectionString(serverName, "", userName, pass);
            ConnectPrivate(connectionString);
        }
		#endregion

		#region public void Connect(string serverName, string dataBaseName, string userName, string pass)
		/// <summary>
		/// ������������ � ������� ��������� ������� ������ � ������
		/// </summary>
		/// <param name="serverName"></param>
		/// <param name="dataBaseName"></param>
		/// <param name="userName"></param>
		/// <param name="pass"></param>
		public void Connect(string serverName, string dataBaseName, string userName, string pass)
		{
			string connectionString = GetConnectionString(serverName, dataBaseName, userName, pass);
			ConnectPrivate(connectionString);

			for (int i = 0; i < Server.Databases.Count; i++)
				if (Server.Databases[i].Name.ToLower() == dataBaseName.ToLower())
				{
					Database = Server.Databases[i];
					return;
				}
			
			throw new Exception($"Database {Server.Name} was not found on server");
		}
		#endregion

        #region public void Disconnect()
        /// <summary>
        /// ����������� �� �������
        /// </summary>
        public void Disconnect()
        {
            if(_serverConnection == null)return;

            if(_serverConnection.IsOpen)
                _serverConnection.Disconnect();
        }
        #endregion
        
        // ���������� ��������

        #region public DataSet Execute(String query)
        /// <summary> 
        /// ��������� ����������� ������ 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataSet Execute(String query)
        {
            return Execute(Database, query);
        }
        #endregion

        #region public DataSet Execute(Database database, String query)
        /// <summary> 
        /// ��������� ������ � ��������� ���� ������
        /// </summary>
        /// <param name="database"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataSet Execute(Database database, String query)
        {
            lock (database)
            {
                if (Debug)
                {
                    _queries++;
                    Logger.Write(_queries + "\r\n" + query);
                }
                // ��������� ������� � ��������� ��
                //query = String.Format("USE [{0}];\r\n{1}", database.Name, query);
                //SqlCommand com = new SqlCommand(query, ServerConnection.SqlConnectionObject);
                //SqlDataAdapter sda = new SqlDataAdapter(com);
                //DataSet ds = new DataSet();
                //sda.Fill(ds);

                if (ServerConnection.SqlConnectionObject.State == ConnectionState.Closed)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            //������� �������� ������� ����������
                            ServerConnection.SqlConnectionObject.Open();
                            //���� ������� �������, ����� �� �����
                            if (ServerConnection.SqlConnectionObject.State == ConnectionState.Open)
                                break;
                        }
                        catch (Exception)
                        {
                            //� ��������� ������ ���������� �������
                            Thread.Sleep(500*i);
                        }
                    }

                    if (ServerConnection.SqlConnectionObject.State == ConnectionState.Closed)
                        return null;
                }

                DataSet ds;
                try
                {
                    ds = database.ExecuteWithResults(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                // ���������� ���������
                if (Debug)
                    Logger.Write("Results: " + (ds.Tables.Count > 0 ? ds.Tables[0].Rows.Count.ToString() : "no results") +
                                 " rows\r\n\r\n");
                return ds;
            }

        }
        #endregion

        #region public DataSet Execute(IEnumerable<DbQuery> dbQueries, out List<ExecutionResultArgs> results)
        /// <summary> 
        /// ��������� ������ � ��������� ���� ������
        /// </summary>
        /// <param name="dbQueries"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public DataSet Execute(IEnumerable<DbQuery> dbQueries, out List<ExecutionResultArgs> results)
        {
            lock (Database)
            {
                results = new List<ExecutionResultArgs>();
                DataSet ds = new DataSet();

                if (ServerConnection.SqlConnectionObject.State == ConnectionState.Closed)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            //������� �������� ������� ����������
                            ServerConnection.SqlConnectionObject.Open();
                            //���� ������� �������, ����� �� �����
                            if (ServerConnection.SqlConnectionObject.State == ConnectionState.Open)
                                break;
                        }
                        catch (Exception)
                        {
                            //� ��������� ������ ���������� �������
                            Thread.Sleep(500*i);
                        }
                    }

                    if (ServerConnection.SqlConnectionObject.State == ConnectionState.Closed)
                    {
                        //ExecutionResultArgs result = new ExecutionResultArgs
                        //{
                        //    Result = ExecutionResult.Exception,
                        //    Exception = new Exception(""),
                        //    Query = ""
                        //};
                        //results.Add(result);
                        return ds;
                    }
                }

                try
                {
                    foreach (DbQuery query in dbQueries)
                    {
                        if (Debug)
                        {
                            _queries++;
                            Logger.Write(_queries + "\r\n" + query.QueryString);
                        }
                        try
                        {
                            //CasDataTable dt = new CasDataTable(query.ElementType, query.Branch, query.Branch);
							var dt = new DataTable();
                            query.QueryString = String.Format("USE [{0}];\r\n{1}", Database.Name, query.QueryString);
                         //   SqlCommand com = new SqlCommand(query.QueryString, ServerConnection.SqlConnectionObject);
	                        //com.CommandTimeout = 60;
                         //   SqlDataAdapter sda = new SqlDataAdapter(com);
                         //   //���������� ������� �������
                         //   sda.Fill(dt);
                            //��������� ������� � ����� ������

	                        dt = Database.ExecuteWithResults(query.QueryString).Tables[0].Copy();

                            ds.Tables.Add(dt);
                        }
                        catch (Exception ex)
                        {
                            ExecutionResultArgs result = new ExecutionResultArgs
                            {
                                Result = ExecutionResult.Exception,
                                Exception = ex,
                                Query = query.QueryString
                            };
                            results.Add(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExecutionResultArgs result = new ExecutionResultArgs
                    {
                        Result = ExecutionResult.Exception,
                        Exception = ex,
                    };
                    results.Add(result);
                    return ds;
                }
                // ���������� ���������
                if (Debug)
                {
                    string rows = ds.Tables.Cast<DataTable>().Aggregate("", (current, dataTable) => current + (dataTable.TableName + ": " + dataTable.Rows.Count + " rows" + "\n"));
                    Logger.Write("Results: " + (ds.Tables.Count > 0 ? rows : "no results") + "\r\n\r\n");
                }
                return ds;    
            }
        }
        #endregion

        #region public DataSet Execute(String query, SqlParameter[] parameters)
        /// <summary>
        /// ��������� ������ � �����������
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet Execute(String query, SqlParameter[] parameters)
        {
            return Execute(Database, query, parameters);
        }
        #endregion

        #region public DataSet Execute(Database database, String query, SqlParameter[] parameters)
        /// <summary>
        /// ��������� ������ � ����������� � ��������� ���� ������
        /// </summary>
        /// <param name="database"></param>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        public DataSet Execute(Database database, String query, SqlParameter[] parameters)
        {
            lock (database)
            {
                if (Debug)
                {
                    _queries++;
                    Logger.Write(_queries + "\r\n" + query);
                }

                // ��������� ������� � ��������� ��
                query = String.Format("USE [{0}];\r\n{1}", database.Name, query);
                SqlCommand com = new SqlCommand(query, _serverConnection.SqlConnectionObject);
                com.Parameters.AddRange(parameters);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                // ���������� ���������
                if (Debug)
                    Logger.Write(String.Format("Results: {0} \r\n\r\n",
                                               ds.Tables.Count > 0 ? ds.Tables[0].Rows.Count + " rows" : ""));
                return ds;
            }
        }
        #endregion

        /*
         * ���������� ������������
         */
        /*
         * ���� ������ CAS
         */

        #region private DataType GetDataType(PropertyInfo propertyInfo, TableColumnAttribute attr)

        /// <summary>
        /// ���������� ����� ��� �������� ����� ��� ���������� ����������� �������� � ��
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        private DataType GetDataType(PropertyInfo propertyInfo, TableColumnAttribute attr)
        {
            //�������� �� ��, �������� �� ��� �������� �������� BaseSmartCoreObject
            if (propertyInfo.PropertyType.IsSubclassOf(typeof(BaseEntityObject))) return DataType.Int;
            if (propertyInfo.PropertyType.GetInterface(typeof(IBaseEntityObject).Name) != null) return DataType.Int;
            if (propertyInfo.PropertyType.IsEnum) return DataType.SmallInt;
            string typeName = propertyInfo.PropertyType.Name.ToLower();
            switch (typeName)
            {
                case "byte[]":
                    return DataType.VarBinary(-1);
                case "string":
                    return DataType.NVarChar(attr != null && (attr.Size < 0 || attr.Size > 256) ? attr.Size : 256);
                case "int32":
                    return DataType.Int;
                case "int16":
                    return DataType.SmallInt;
                case "datetime":
                    return DataType.DateTime;
                case "bool":
                case "boolean":
                    return  DataType.Bit;
                case "double":
                    return DataType.Float;

                case "detectionphase":
                case "flightregime":
                case "kitcostcondition":
                case "powerloss":
                case "runupcondition":
                case "runuptype":
                case "shutdowntype":
                case "thrustlever":
                case "weathercondition":
                    return DataType.SmallInt;
                case "highlight":
                case "timespan":
                case "workpackagestatus":
                    return DataType.Int;
                case "directivethreshold":
                    return DataType.VarBinary(DirectiveThreshold.SerializedDataLength);
                case "componentdirectivethreshold":
                    return DataType.VarBinary(ComponentDirectiveThreshold.SerializedDataLength);
                case "maintenancedirectivethreshold":
                    return DataType.VarBinary(MaintenanceDirectiveThreshold.SerializedDataLength);
				case "trainingthreshold":
					return DataType.VarBinary(TrainingThreshold.SerializedDataLength);
				case "lifelength":
                    return DataType.VarBinary(Lifelength.SerializedDataLength);
                default:
                    return null;
            }   
        }
        #endregion

        #region public bool CheckType(string tableName)
        /// <summary>
        /// ��������� ������� � ������������ ������� � �� ��� ����������/�������� �������� ����������� ����
        /// </summary>
        /// <returns></returns>
        public bool CheckType<T>() where T : BaseEntityObject, new()
        {
            //����������� ����
            Type type = typeof(T);
            //����������� �������� ����������� �������
            TableAttribute dbTable = (TableAttribute)type.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault();

            if (dbTable == null || dbTable.TableName == "")
            {
                //���� ����������� ������� �������, � ������� ����������� 
                //���������� ������� ���� ��� ��� ������� �� ������, �� 
                //�������� ����������
                throw new DbTableAttributeNullException(new T());
            }

            lock (Database)
            {
                if (!Database.Tables.Contains(dbTable.TableName, dbTable.TableScheme))
                {
                    //���� ���������� �������, ��������������� ��� ���������� �������� 
                    //����������� ���� �� �������� ����������
                    throw new DbTableAttributeException(type, dbTable);
                }
            }

            return true;   
        }
        #endregion

        #region public void CheckTableFor(Type type)
        /// <summary>
        /// ��������� ������� � ������������ ������� � �� ��� ����������/�������� �������� ����������� ����
        /// </summary>
        /// <returns></returns>
        public void CheckTableFor(Type type)
        {
            //����������� �������� ����������� �������
            TableAttribute dbTable = (TableAttribute)type.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault();

            if (!Database.Tables.Contains(dbTable.TableName, dbTable.TableScheme))
            {
                //���� ���������� �������, ��������������� ��� ���������� �������� 
                //����������� ���� �� �������� ����������
                throw new DbTableAttributeException(type, dbTable);
            }

            //��������� �������
            Table table = Database.Tables[dbTable.TableName, dbTable.TableScheme];
            //����������� ������� ����
            List<PropertyInfo> preProrerty = new List<PropertyInfo>(type.GetProperties());
            //����������� �������, ������� ������� "�����������"
            List<PropertyInfo> properties =
                preProrerty.Where(p => p.GetCustomAttributes(typeof(TableColumnAttribute), false).Length != 0).ToList();

            //�������� ������� �� ������� �������������� �������, �� ���� � ���� ��������� ��������
            //List<ColumnError> errors = new  List<ColumnError>();
            string checkResult = "";
            foreach (PropertyInfo p in properties)
            {
                TableColumnAttribute tca =
                    (TableColumnAttribute) p.GetCustomAttributes(typeof (TableColumnAttribute), false).FirstOrDefault();
                //��� ������ ������������, ����� �� ��������� ��� � ��
                DataType storedType = GetDataType(p, tca);
                if (storedType == null) throw new Exception("��� ���� " + p.PropertyType.Name + " �� ������� ���������� �������� ��� � ��");

                //�������� ������� ������� � �������� ������ � �������
                if(!table.Columns.Contains(tca.ColumnName))
                {
                    //���� ������� � �������� ������ ��� � �������
                    //�� ���-� �� ���� ���������� � ��������� ��������
                    //errors.Add(new ColumnError(tca.ColumnName, ColumnErrorType.NoFind));
                    if (checkResult != "") checkResult += ",\n";
                    checkResult += "������� � ������ " + tca.ColumnName + " ����������� � �������";

                    continue;
                }

                //�������� ���� ��������� �������� �������
                Column col = table.Columns[tca.ColumnName];
                if(col.DataType.Name != storedType.Name)
                {
                    //���� ��� ������� � ������� �� ������������� ���� ��� ��������
                    //�� ���-� �� ���� ���������� � ��������� ��������
                    //errors.Add(new ColumnError(tca.ColumnName, ColumnErrorType.InvalidType));
                    if (checkResult != "") checkResult += ",\n";
                    checkResult += "��� ������� " + tca.ColumnName + " �� ������������� ���� �������� ��� " 
                                                  + p.Name + "(" + p.PropertyType.Name + ")";

                    continue;
                }
                if (col.DataType.MaximumLength != storedType.MaximumLength && storedType.MaximumLength != 0)
                {
                    //���� ������ ���� ������ ������� � ������� �� ������������� ������� ��� ��������
                    //�� ���-� �� ���� ���������� � ��������� ��������
                    //errors.Add(new ColumnError(tca.ColumnName, ColumnErrorType.InvalidSize));
                    if (checkResult != "") checkResult += ",\n";
                    checkResult += "������ ������� " + tca.ColumnName + "(" + col.DataType.MaximumLength + ")"
                                    + " �� ������������� ������� �������� ��� " + p.Name + "(" + storedType.MaximumLength + ")";

                    continue;
                }
                //if (tca.ColumnName.ToLower() == "isdeleted")
                //{
                //    // ����������� ������� �����
                //    DefaultConstraint dc =  col.DefaultConstraint;
                //    //newColumn.AddDefaultConstraint( =) "((0))");
                //}
            }
            //if(errors.Count > 0) throw new DbTableColumnsAttributeException(type,errors.ToArray());
            if (checkResult != "") throw new DbTableColumnsAttributeException(type,checkResult);
        }
        #endregion

        #region public void CreateTableFor(Type type)
        /// <summary>
        /// ������� ������� � �� ��� ����������/�������� �������� ����������� ����
        /// </summary>
        /// <returns></returns>
        public void CreateTableFor(Type type)
        {
            //����������� �������� ����������� �������
            TableAttribute dbTable = (TableAttribute)type.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault();

            if (!Database.Tables.Contains(dbTable.TableName, dbTable.TableScheme))
            {
                if (!Database.Schemas.Contains(dbTable.TableScheme))
                {
                    //���� ����� ��� � ��, �� ������������ �� ��������
                    Schema newSchema = new Schema(Database, dbTable.TableScheme);
                    newSchema.Owner = "dbo";

                    //Create the schema on the instance of SQL Server. 
                    newSchema.Create();

                    //Define an ObjectPermissionSet that contains the Update and Select object permissions. 
                    ObjectPermissionSet obperset = new ObjectPermissionSet();
                    obperset.Add(ObjectPermission.Select);
                    obperset.Add(ObjectPermission.Update);
                    obperset.Add(ObjectPermission.Insert);
                    obperset.Add(ObjectPermission.Delete);

                    //Grant the set of permissions on the schema to the guest account. 
                    newSchema.Grant(obperset, "sa");
                }

                Table newTable = new Table(Database, dbTable.TableName, dbTable.TableScheme);
                //����������� ������� ����
                List<PropertyInfo> preProrerty = new List<PropertyInfo>(type.GetProperties());
                //����������� �������, ������� ������� "�����������"
                List<PropertyInfo> properties =
                    preProrerty.Where(p => p.GetCustomAttributes(typeof(TableColumnAttribute), false).Length != 0).ToList();

                foreach (PropertyInfo t in properties)
                {
                    TableColumnAttribute tca =
                        (TableColumnAttribute)t.GetCustomAttributes(typeof(TableColumnAttribute), false).FirstOrDefault();

                    Column newColumn = new Column(newTable, tca.ColumnName);

                    DataType storedType = GetDataType(t, tca);
                    if (storedType != null) newColumn.DataType = storedType;
                    else throw new Exception("��� ���� " + t.PropertyType.Name + " �� ������� ���������� �������� ��� � ��");

                    newColumn.Nullable = true;

                    if (tca.ColumnName == dbTable.PrimaryKey)
                    {
                        // ����������� ������� �����
                        newColumn.Nullable = false;
                        newColumn.Identity = true;
                        newColumn.IdentitySeed = 1;
                        newColumn.IdentityIncrement = 1;
                    }
                    newTable.Columns.Add(newColumn);
                }
                // Create a PK Index for the table
                Index index = new Index(newTable, "PK_" + dbTable.TableName) { IndexKeyType = IndexKeyType.DriPrimaryKey };
                // The PK index will consist of 1 column, "ID"
                index.IndexedColumns.Add(new IndexedColumn(index, dbTable.PrimaryKey));
                // Add the new index to the table.
                newTable.Indexes.Add(index);
                // Physically create the table in the database
                newTable.Create();

                //Database.Tables.Add(newTable);

                if (newTable.Columns.Contains("IsDeleted"))
                {
                    // ����������� ������� �����
                    Column col = newTable.Columns["IsDeleted"];
                    
                    string defName = dbTable.TableName + "_" + col.Name;
                    
                    DefaultConstraint dc = col.AddDefaultConstraint(defName);
                    dc.Text = "((0))";
                    dc.Create();
                    
                    
                    col.Nullable = false;
                    col.Alter();

                    //Default def = new Default(Database, defName, dbTable.TableScheme)
                    //{
                    //    TextHeader = "CREATE DEFAULT " + dbTable.TableScheme + ".[" + defName + "] AS",
                    //    TextBody = "((0))"
                    //};
                    ////Create the default on the instance of SQL Server. 
                    //def.Create();

                    ////Bind the default to a column in a table in AdventureWorks2012
                    //def.BindToColumn(dbTable.TableName, col.Name, dbTable.TableScheme);
                }
            }
            else
            {
                //��������� �������
                Table table = Database.Tables[dbTable.TableName, dbTable.TableScheme];
                //����������� ������� ����
                List<PropertyInfo> preProrerty = new List<PropertyInfo>(type.GetProperties());
                //����������� �������, ������� ������� "�����������"
                List<PropertyInfo> properties =
                    preProrerty.Where(p => p.GetCustomAttributes(typeof(TableColumnAttribute), false).Length != 0).ToList();

                //�������� ������� �� ������� �������������� �������, �� ���� � ���� ��������� ��������
                foreach (PropertyInfo p in properties)
                {
                    TableColumnAttribute tca =
                        (TableColumnAttribute)p.GetCustomAttributes(typeof(TableColumnAttribute), false).FirstOrDefault();
                    //��� ������ ������������, ����� �� ��������� ��� � ��
                    DataType storedType = GetDataType(p, tca);
                    if (storedType == null) throw new Exception("��� ���� " + p.PropertyType.Name + " �� ������� ���������� �������� ��� � ��");

                    //�������� ������� ������� � �������� ������ � �������
                    if (!table.Columns.Contains(tca.ColumnName))
                    {
                        //���� ������� � �������� ������ ��� � �������
                        //�� ������������ �� ��������

                        Column newColumn = new Column(table, tca.ColumnName);
                        newColumn.DataType = storedType;
                        newColumn.Nullable = true;

                        if (tca.ColumnName == dbTable.PrimaryKey)
                        {
                            // ����������� ������� �����
                            newColumn.Nullable = false;
                            newColumn.Identity = true;
                            newColumn.IdentitySeed = 1;
                            newColumn.IdentityIncrement = 1;

                            newColumn.Create();
                            //table.Columns.Add(newColumn);
                            // Create a PK Index for the table
                            Index index = new Index(table, "PK_" + dbTable.TableName) { IndexKeyType = IndexKeyType.DriPrimaryKey };
                            // The PK index will consist of 1 column, "ID"
                            index.IndexedColumns.Add(new IndexedColumn(index, dbTable.PrimaryKey));
                            // Add the new index to the table.
                            table.Indexes.Add(index);

                            continue;
                        }
                        newColumn.Create();

                        continue;
                    }

                    //�������� ���� ��������� �������� �������
                    Column col = table.Columns[tca.ColumnName];
                    if (col.DataType.Name != storedType.Name)
                    {
                        //���� ��� ������� � ������� �� ������������� ���� ��� ��������
                        //�� ������������ ����� ����
                        col.DataType = storedType;
                        col.Alter();

                        continue;
                    }
                    if (col.DataType.MaximumLength != storedType.MaximumLength && storedType.MaximumLength != 0)
                    {
                        //���� ������ ���� ������ ������� � ������� �� ������������� ������� ��� ��������
                        //�� ������������ ��������� �������
                        col.DataType.MaximumLength = storedType.MaximumLength;
                        col.Alter();

                        continue;
                    }
                }
            }
        }
		#endregion

		#region private void SelectDatabase(DatabaseManager databaseManager, String database)
		/// <summary>
		/// �������� ���� ������ 
		/// </summary>
		/// <param name="databaseManager"></param>
		/// <param name="database"></param>
		public void SelectDatabase(DatabaseManager databaseManager, string database)
		{
			for (int i = 0; i < databaseManager.Server.Databases.Count; i++)
				if (databaseManager.Server.Databases[i].Name.ToLower() == database.ToLower())
				{
					Database = databaseManager.Server.Databases[i];
					return;
				}
			//
			throw new Exception($"Database {databaseManager.Server.Name} was not found on server");
		}
		#endregion


		/*
         * ����������
         */

		#region private string GetConnectionString(string serverName, string databaseName, string userName, string userPass)
		/// <summary>
		/// ���������� ������ ����������� � ���������� ������� ��������� ��������� ������� ������, ��� ���� ������ ����� �� ���������
		/// </summary>
		/// <param name="serverName"></param>
		/// <param name="databaseName"></param>
		/// <param name="userName"></param>
		/// <param name="userPass"></param>
		/// <returns></returns>
		private string GetConnectionString(string serverName, string databaseName, string userName, string userPass)
        {
            string cn = "SERVER = " +serverName + ";" +
                        "UID=" + userName + ";" +
                        "PWD=" + userPass + "; ";
            if (databaseName != "") cn += "DATABASE=" + databaseName + "; ";
            cn += "Pooling=false;";

            return cn;
        }
        #endregion

        #region private string GetTrustedConnectionString(string serverName, string databaseName)
        /// <summary>
        /// ���������� ������ ����������� ��������� ���������� ����������� (�������������� Windows) (����� ������� ������ ���� ������)
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        private string GetTrustedConnectionString(string serverName, string databaseName)
        {
            return string.Format("Server={0};{1}Trusted_Connection=True;", serverName, databaseName == "" ? "" : "Database=" + databaseName + ";");
            //return string.Format("Data Source={0};{1}Integrated Security=SSPI;", serverName, databaseName == "" ? "" : "Initial Catalog=" + databaseName + ";");
        }
        #endregion

        #region private void ConnectPrivate(string connectionString)
        /// <summary>
        /// ����������� � ������� ��������� ��������� ������ �����������
        /// </summary>
        /// <param name="connectionString"></param>
        private void ConnectPrivate(string connectionString)
        {
            _serverConnection = new ServerConnection();
            _serverConnection.ConnectionString = connectionString;
            _serverConnection.Connect();
            _server = new Server(_serverConnection);
        }
        #endregion

        /*
         *  �������� ��������
         */

    }


    #region public enum ExecutionResult
    /// <summary>
    /// ��������� ���������� �������
    /// </summary>
    public enum ExecutionResult
    {

        /// <summary>
        /// �������� ���������� �������
        /// </summary>
        Successfull,

        /// <summary>
        /// �� ����� ������� �������� �������������� ��������
        /// </summary>
        Exception,

    }
    #endregion

    #region public class ExecutionResultArgs
    /// <summary>
    /// ����� �������� ��������� ���������� �������
    /// </summary>
    public class ExecutionResultArgs
    {
        /// <summary>
        /// ��������� ���������� ��������
        /// </summary>
        public ExecutionResult Result = ExecutionResult.Successfull;

        /// <summary>
        /// �������������� ��������, ������� �������� �� ����� ���������� �������
        /// </summary>
        public Exception Exception = null;

        /// <summary>
        /// ���������� � �������, ������� ������ ������
        /// </summary>
        public String Query = null;
    }
    #endregion

}
