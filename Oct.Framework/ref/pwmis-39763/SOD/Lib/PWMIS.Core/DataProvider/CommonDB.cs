/*
 * ========================================================================
 * Copyright(c) 2006-2010 PWMIS, All Rights Reserved.
 * Welcom use the PDF.NET (PWMIS Data Process Framework).
 * See more information,Please goto http://www.pwmis.com/sqlmap 
 * ========================================================================
 * ���������
 * 
 * ʹ������ķ����������ݷ���ʵ��,������App.config�����������ã�
 <add key="SqlServerConnectionString" value="Data Source=localhost;Initial catalog=DAABAF;user id=daab;password=daab" />
       <add key="SqlServerHelperAssembly" value="CommonDataProvider.Data"></add>
       <add key="SqlServerHelperType" value="CommonDataProvider.Data.SqlServer"></add>
       <add key="OleDbConnectionString" value="Provider=SQLOLEDB;Data Source=localhost;Initial catalog=DAABAF;user id=daab;password=daab" />
       <add key="OleDbHelperAssembly" value="CommonDataProvider.Data"></add>
       <add key="OleDbHelperType" value="CommonDataProvider.Data.OleDb"></add>
       <add key="OdbcConnectionString" value="DRIVER={SQL Server};SERVER=localhost;DATABASE=DAABAF;UID=daab;PWD=daab;" />
       <add key="OdbcHelperAssembly" value="CommonDataProvider.Data"></add>
       <add key="OdbcHelperType" value="CommonDataProvider.Data.Odbc"></add>
       <add key="OracleConnectionString" value="User ID=DAAB;Password=DAAB;Data Source=spinvis_flash;" />
       <add key="OracleHelperAssembly" value="CommonDataProvider.Data"></add>
       <add key="OracleHelperType" value="CommonDataProvider.Data.Oracle"></add>
        * 
       <add key="SQLiteConnectionString" value="Data Source=spinvis_flash;" />
       <add key="SQLiteHelperAssembly" value="CommonDataProvider.Data"></add>
       <add key="SQLiteHelperType" value="CommonDataProvider.Data.SQLite"></add>
 * 
 * ���ߣ���̫��     ʱ�䣺2008-10-12
 * �汾��V4.5.12.1101
 * 
 * �޸��ߣ�         ʱ�䣺2010-3-24                
 * �޸�˵�����ڲ������õ�ʱ�������nullֵ�Ĳ������������ݿ�����NULLֵ��
 * 
 *  * �޸��ߣ�         ʱ�䣺2012-4-11                
 * �޸�˵��������SqlServer�������������,���SqlServer.cs��
 * 
 * * �޸��ߣ�         ʱ�䣺2012-5-11                
 * �޸�˵������������ִ�еĳ�ʱʱ���趨��
 * 
 * �޸��ߣ�         ʱ�䣺2012-10-12                
 * �޸�˵�����������ӻỰ���ܣ��Ա���һ��������ִ�ж�β�ѯ����ͬ�����񣩡�
 * 
 * �޸��ߣ�         ʱ�䣺2012-10-30                
 * �޸�˵����ʹ��MySQL��PDF.NET�ⲿ���ݷ����ṩ�����ʱ�򣬸Ľ�ʵ������Ĵ���Ч�ʡ�
 * 
 * �޸��ߣ�         ʱ�䣺2012-11-01                
 * �޸�˵����Ϊ֧����չSQLite�������Ľ��˱����ĳЩ��Ա�ķ��ʼ���
 * 
 * �޸��ߣ�         ʱ�䣺2012-11-06
 * Ϊ���Ч�ʣ����ټ����ڲ����в�����¡���������SQL��䲻Ҫʹ��ͬ���Ĳ�������
 * 
 * �޸��ߣ�         ʱ�䣺2013-1-13
 * ���ӻ�ȡ�������ݿ����͵Ĳ������Ƶĳ��󷽷������ӡ���д���롱���ܣ�ֻ��Ҫ����
 *   DataWriteConnectionString
 * ���Լ��ɣ�ע�����ø����Բ��ı䵱ǰʹ�õ����ݿ����͡�
 * 
 *  �޸��ߣ�         ʱ�䣺2013-2-24
 * �޸Ļ�ȡ�����ķ���Ϊ����д���Խ��Access ���������д����
 * 
 *  �޸��ߣ�         ʱ�䣺2013-3-8
 * ִ�в�ѯ������������ϣ���������ظ�ռ�õ����⡣
 * 
 * �޸��ߣ�         ʱ�䣺2013-3-25
 * Ϊ֧�ֱ���Ķ�д���빦�ܣ��޸�����������ִ��ExecuteNoneQuery������һ��Bug����л���ѡ�����û��ò�����ִ�Bug��
 * 
 *  �޸��ߣ�         ʱ�䣺2013-4-16
 * �޸�SQL��־�У�û�м�¼��������ѯ�Ĳ���ֵ���⣬��л���ѡ�GIV-˳�¡����ִ�Bug��
 * 
 * *  �޸��ߣ�         ʱ�䣺2013-��-��
 * ������ع������еݼ�����������������޸������Ķ��������ж����������ķ�ʽ
 * 
 * * �޸��ߣ�         ʱ�䣺2015-1-29
 * ����OpenSession �����ӻỰ֮��DataReader�ر����ӵ�����
 * ========================================================================
*/


using System;
using System.Data;
using System.IO;
using System.Reflection;
using PWMIS.Common;
using PWMIS.Core;
using System.Data.Common;
using System.Collections.Generic;

namespace PWMIS.DataProvider.Data
{
    /// <summary>
    /// �������ݷ��ʻ�����
    /// </summary>
    public abstract class CommonDB : IDisposable
    {
        private string _connString = string.Empty;
        private string _writeConnString =null;
        private string _errorMessage = string.Empty;
        private bool _onErrorRollback = true;
        private bool _onErrorThrow = true;
        private IDbConnection _connection = null;
        private IDbTransaction _transation = null;
        protected long _elapsedMilliseconds = 0;

        private string appRootPath = "";

        private int transCount;//���������
        private IDbConnection sessionConnection = null;//�Ựʹ�õ�����
        private bool disposed;

        //		//��־���
        //		private string DataLogFile ;
        //		private bool SaveCommandLog;
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CommonDB()
        {
            //			DataLogFile=System.Configuration .ConfigurationSettings .AppSettings ["DataLogFile"];
            //			string temp=System.Configuration .ConfigurationSettings .AppSettings ["SaveCommandLog"];
            //			if(temp!=null && DataLogFile!=null && DataLogFile!="")
            //			{
            //				if(temp.ToUpper() =="TRUE") SaveCommandLog=true ;else SaveCommandLog=false;
            //			}
        }

        /// <summary>
        /// �������ݿ�ʵ����ȡ���ݿ�����ö��
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static DBMSType GetDBMSType(CommonDB db)
        {
            if (db != null)
            {
                if (db is Access)
                    return DBMSType.Access;
                if (db is SqlServer)
                    return DBMSType.SqlServer;
                if (db is Oracle)
                    return DBMSType.Oracle;
                if (db is OleDb)
                    return DBMSType.UNKNOWN;
                if (db is Odbc)
                    return DBMSType.UNKNOWN;
            }
            return DBMSType.UNKNOWN;
        }

        private static Dictionary<string, Type> cacheHelper = null;
        /// <summary>
        /// �����������ݷ������ʵ��
        /// </summary>
        /// <param name="providerAssembly">�ṩ���������</param>
        /// <param name="providerType">�ṩ������</param>
        /// <returns></returns>
        public static AdoHelper CreateInstance(string providerAssembly, string providerType)
        {
            //ʹ��Activator.CreateInstance Ч��Զ����assembly.CreateInstance
            //�������ȼ�黺�������Ƿ����ݷ���ʵ�����������
            //��ϸ������ο� http://www.cnblogs.com/leven/archive/2009/12/08/instanse_create_comparison.html
            //
            if (cacheHelper == null)
                cacheHelper = new Dictionary<string, Type>();
            string key = string.Format("{0}_{1}", providerAssembly, providerType);
            if (cacheHelper.ContainsKey(key))
            {
                return (AdoHelper)Activator.CreateInstance(cacheHelper[key]);
            }

            Assembly assembly = Assembly.Load(providerAssembly);
            object provider = assembly.CreateInstance(providerType);

            if (provider is AdoHelper)
            {
                AdoHelper result = provider as AdoHelper;
                cacheHelper[key] = result.GetType();//���뻺��
                return result;
            }
            else
            {
                throw new InvalidOperationException("��ǰָ���ĵ��ṩ������ AdoHelper ������ľ���ʵ���࣬��ȷ��Ӧ�ó����������ȷ�����ã���connectionStrings ���ýڵ� providerName ���ԣ���");
            }
        }

        /// <summary>
        /// ִ��SQL��ѯ�ĳ�ʱʱ�䣬��λ�롣��������ȡĬ��ʱ�䣬���MSDN��
        /// </summary>
        public int CommandTimeOut { get; set; }

        /// <summary>
        /// ��ǰ���ݿ������ö��
        /// </summary>
        public abstract DBMSType CurrentDBMSType { get; }
        /// <summary>
        /// ��ȡ���һ��ִ�в�ѯ�����ķѵ�ʱ�䣬��λ������
        /// </summary>
        public long ElapsedMilliseconds
        {
            get { return _elapsedMilliseconds; }
            protected set { _elapsedMilliseconds = value; }
        }

        private string _insertKey;
        /// <summary>
        /// �ڲ�����������е����ݺ󣬻�ȡ�ղ������е����ݵķ�ʽ��Ĭ��ʹ�� @@IDENTITY���������������ݿ�ʵ���������Ҫ��д�����Ի�������ʱ��ָ̬����
        /// ��SqlServer��Ĭ��ʹ��SCOPE_IDENTITY()���ɸ���������á�
        /// </summary>
        public virtual string InsertKey
        {
            get
            {
                if (string.IsNullOrEmpty(_insertKey))
                    return "SELECT @@IDENTITY";
                else
                    return _insertKey;
            }
            set
            {
                _insertKey = value;
            }
        }

        /// <summary>
        /// ���������ַ���
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connString;
            }
            set
            {
                _connString = value;
                //���� ���·�����ٶ� ~ ·����ʽ����Web��������·��
                //if(!string.IsNullOrEmpty (value) && _connString.IndexOf ('~')>0)
                //{
                //    if (appRootPath == "")
                //    {
                //        string EscapedCodeBase = Assembly.GetExecutingAssembly().EscapedCodeBase;
                //        Uri u = new Uri(EscapedCodeBase);
                //        string path = Path.GetDirectoryName(u.LocalPath);
                //        if (path.Length > 4)
                //            appRootPath = path.Substring(0, path.Length - 3);// ȥ�� \bin����ȡ��Ŀ¼
                //    }
                //    _connString = _connString.Replace("~", appRootPath);
                //}
                CommonUtil.ReplaceWebRootPath(ref _connString);
            }
        }

        /// <summary>
        /// д�����ݵ������ַ�����ExecuteNoneQuery �������Զ�ʹ�ø�����
        /// </summary>
        public string DataWriteConnectionString
        {
            get {
                //if (_writeConnString == null)
                //    return this.ConnectionString;
                //else
                //    return _writeConnString;
                return _writeConnString ?? this.ConnectionString;
            }
            set {
                _writeConnString = value;
            }
        }

        /// <summary>
        /// ��ȡ�����ַ���������
        /// </summary>
        public abstract DbConnectionStringBuilder ConnectionStringBuilder { get; }
        /// <summary>
        /// �������ݿ��û���ID
        /// </summary>
        public abstract string ConnectionUserID { get; }

        /// <summary>
        /// ���ݲ����Ĵ�����Ϣ������ÿ�β�ѯ�������Ϣ��
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (value != null && value != "")
                    _errorMessage += ";" + value;
                else
                    _errorMessage = value;
            }
        }

        /// <summary>
        /// ������ִ���ڼ䣬���¹���������ִ����Ƿ��Զ��ع�����Ĭ��Ϊ�ǡ�
        /// </summary>
        public bool OnErrorRollback
        {
            get { return _onErrorRollback; }
            set { _onErrorRollback = value; }
        }

        /// <summary>
        /// ��ѯ���ִ����Ƿ��ǽ������׳���Ĭ��Ϊ�ǡ�
        /// �������Ϊ�񣬽��򻯵��ó�����쳣������������ÿ�θ��º���Ӱ��Ľ�����ʹ�����Ϣ��������ĳ����߼���
        /// ���������ִ���ڼ䣬�������ִ�������̽������������ñ�����Ϊ �ǡ�
        /// </summary>
        public bool OnErrorThrow
        {
            get { return _onErrorThrow; }
            set { _onErrorThrow = value; }
        }

        /// <summary>
        /// ��ȡ����������������
        /// </summary>
        /// <returns>�����������</returns>
        protected virtual IDbConnection GetConnection() //
        {
            //����ʹ�����������
            if (Transaction != null)
            {
                IDbTransaction trans = Transaction;
                if (trans.Connection != null)
                    return trans.Connection;
            }
            //����������ӻỰ����ʹ�ø�����
            if (sessionConnection != null)
            {
                return sessionConnection;
            }
            return null;
        }

        /// <summary>
        /// ��ȡ���ݿ����Ӷ���ʵ��
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetDbConnection()
        {
            return this.GetConnection();
        }

        /// <summary>
        /// ��ȡ�����������ʵ��
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <returns>�����������</returns>
        public IDbConnection GetConnection(string connectionString)
        {
            this.ConnectionString = connectionString;
            return this.GetConnection();
        }

        /// <summary>
        /// ��ȡ����������ʵ��
        /// </summary>
        /// <returns>����������</returns>
        protected abstract IDbDataAdapter GetDataAdapter(IDbCommand command);

        /// <summary>
        /// ��ȡ���������������
        /// </summary>
        public IDbTransaction Transaction
        {
            get { return _transation; }
            set { _transation = value; }
        }

        /// <summary>
        /// ��ȡ�������ı�ʶ�ַ���Ĭ��ΪSQLSERVER��ʽ������������ݿ��������Ҫ��д������
        /// </summary>
        public virtual string GetParameterChar
        {
            get { return "@"; }
        }

        /// <summary>
        /// ��ȡһ���²�������
        /// </summary>
        /// <returns>�ض�������Դ�Ĳ�������</returns>
        public abstract IDataParameter GetParameter();

        /// <summary>
        /// ��ȡһ���²�������
        /// </summary>
        /// <param name="paraName">��������</param>
        /// <param name="dbType">���ݿ���������</param>
        /// <param name="size">�ֶδ�С</param>
        /// <returns>�ض�������Դ�Ĳ�������</returns>
        public abstract IDataParameter GetParameter(string paraName, System.Data.DbType dbType, int size);

        /// <summary>
        /// ��ȡһ���²�������
        /// </summary>
        /// <param name="paraName">��������</param>
        /// <param name="dbType">>���ݿ���������</param>
        /// <returns>�ض�������Դ�Ĳ�������</returns>
        public virtual IDataParameter GetParameter(string paraName, DbType dbType)
        {
            IDataParameter para = this.GetParameter();
            para.ParameterName = paraName;
            para.DbType = dbType;
            return para;
        }

        /// <summary>
        /// ���ݲ�������ֵ���ز���һ���µĲ�������
        /// </summary>
        /// <param name="paraName">������</param>
        /// <param name="Value">����ֵ</param>
        /// <returns>�ض�������Դ�Ĳ�������</returns>
        public virtual IDataParameter GetParameter(string paraName, object Value)
        {
            IDataParameter para = this.GetParameter();
            para.ParameterName = paraName;
            para.Value = Value;
            return para;
        }

        /// <summary>
        /// ��ȡһ���²�������
        /// </summary>
        /// <param name="paraName">������</param>
        /// <param name="dbType">����ֵ</param>
        /// <param name="size">������С</param>
        /// <param name="paraDirection">�����������</param>
        /// <returns>�ض�������Դ�Ĳ�������</returns>
        public IDataParameter GetParameter(string paraName, System.Data.DbType dbType, int size, System.Data.ParameterDirection paraDirection)
        {
            IDataParameter para = this.GetParameter(paraName, dbType, size);
            para.Direction = paraDirection;
            return para;
        }

        /// <summary>
        /// ��ȡһ���²�������
        /// </summary>
        /// <param name="paraName">������</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">����ֵ�ĳ���</param>
        /// <param name="paraDirection">�����������������</param>
        /// <param name="precision">����ֵ�����ľ���</param>
        /// <param name="scale">������С��λλ��</param>
        /// <returns></returns>
        public IDataParameter GetParameter(string paraName, System.Data.DbType dbType, int size, System.Data.ParameterDirection paraDirection, byte precision, byte scale)
        {
            IDbDataParameter para = (IDbDataParameter)this.GetParameter(paraName, dbType, size);
            para.Direction = paraDirection;
            para.Precision = precision;
            para.Scale = scale;
            return para;
        }

        
        

        /// <summary>
        /// ��ȡ��ǰ���ݿ����͵Ĳ���������������
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public abstract string GetNativeDbTypeName(IDataParameter para);
        /// <summary>
        /// ���ش� SqlConnection ������Դ�ļܹ���Ϣ��
        /// </summary>
        /// <param name="collectionName">�������ƣ�����Ϊ��</param>
        /// <param name="restrictionValues">����ļܹ���һ������ֵ������Ϊ��</param>
        /// <returns>���ݿ�ܹ���Ϣ��</returns>
        public abstract DataTable GetSchema(string collectionName, string[] restrictionValues);

        /// <summary>
        /// ��ȡ�洢���̡������Ķ������ݣ��������֧�֣���Ҫ����������д
        /// </summary>
        /// <param name="spName">�洢��������</param>
        /// <returns></returns>
        public virtual string GetSPDetail(string spName)
        {
            return "";
        }

        /// <summary>
        /// ��ȡ��ͼ���壬�������֧�֣���Ҫ����������д
        /// </summary>
        /// <param name="viewName">��ͼ����</param>
        /// <returns></returns>
        public virtual string GetViweDetail(string viewName)
        {
            return "";
        }

        /// <summary>
        /// �����Ӳ���������
        /// </summary>
        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// ��������ָ��������뼶��
        /// </summary>
        /// <param name="ilevel"></param>
        public void BeginTransaction(IsolationLevel ilevel)
        {
            transCount++;
            this.ErrorMessage = "";
            _connection = GetConnection();//�������н����ȡ���Ӷ���ʵ��
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            if (transCount == 1)
                _transation = _connection.BeginTransaction(ilevel);
            CommandLog.Instance.WriteLog("�����Ӳ���������", "AdoHelper");
        }

        /// <summary>
        /// �ύ���񲢹ر�����
        /// </summary>
        public void Commit()
        {
            transCount--;
            if (_transation != null && _transation.Connection != null && transCount == 0)
                _transation.Commit();

            if (transCount <= 0)
            {
                CloseGlobalConnection();
                transCount = 0;            
            }
            CommandLog.Instance.WriteLog("�ύ���񲢹ر�����", "AdoHelper");
        }

        /// <summary>
        /// �ع����񲢹ر�����
        /// </summary>
        public void Rollback()
        {
            transCount--;
            if (_transation != null && _transation.Connection != null)
                _transation.Rollback();
            CloseGlobalConnection();
            CommandLog.Instance.WriteLog("�ع����񲢹ر�����", "AdoHelper");
        }

        /// <summary>
        /// ��һ�����ݿ����ӻỰ�������������ִ��һϵ��AdoHelper��ѯ
        /// </summary>
        /// <returns>���ӻỰ����</returns>
        public ConnectionSession OpenSession()
        {
            this.ErrorMessage = "";
            sessionConnection = GetConnection();//�������н����ȡ���Ӷ���ʵ��
            if (sessionConnection.State != ConnectionState.Open)
                sessionConnection.Open();

            CommandLog.Instance.WriteLog("�򿪻Ự����", "ConnectionSession");
            return new ConnectionSession(sessionConnection);
        }

        /// <summary>
        /// �ر����ӻỰ
        /// </summary>
        public void CloseSession()
        {
            if (sessionConnection != null && sessionConnection.State == ConnectionState.Open)
            {
                sessionConnection.Close();
                sessionConnection.Dispose();
                sessionConnection = null;
            }
        }



        private bool _sqlServerCompatible = true;
        /// <summary>
        /// SQL SERVER ���������ã�Ĭ��Ϊ���ݡ������Կ��Խ�SQLSERVER�������ֲ�������������͵����ݿ⣬�����ֶηָ����ţ����ں����ȡ�
        /// �����ƴ���ַ�����ʽ�Ĳ�ѯ����������ΪFalse��������ƴ�ӣӣѣ̵�ʱ����˵�'@'�������ַ�
        /// </summary>
        public bool SqlServerCompatible
        {
            get { return _sqlServerCompatible; }
            set { _sqlServerCompatible = value; }
        }
        /// <summary>
        /// ��ӦSQL�����������Ĵ������罫SQLSERVER���ֶ�������������滻�����ݿ��ض����ַ����÷�������ִ�в�ѯǰ���ã�Ĭ������²������κδ���
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        protected virtual string PrepareSQL(ref string SQL)
        {
            return SQL;
        }

        /// <summary>
        /// ��ȡ�����������ݿ����ʹ������SQL���
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetPreparedSQL(string sql)
        {
            return this.PrepareSQL(ref sql);
        }

        /// <summary>
        /// �����������,������������������������ӣ����δ���������ｫ����
        /// ע�⣺Ϊ���Ч�ʣ����ټ����ڲ����в�����¡���������SQL��䲻Ҫʹ��ͬ���Ĳ�������
        /// </summary>
        /// <param name="cmd">�������</param>
        /// <param name="SQL">SQL</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��������</param>
        protected void CompleteCommand(IDbCommand cmd, ref string SQL, ref CommandType commandType, ref IDataParameter[] parameters)
        {
            cmd.CommandText = SqlServerCompatible ? PrepareSQL(ref  SQL) : SQL;
            cmd.CommandType = commandType;
            cmd.Transaction = this.Transaction;
            if (this.CommandTimeOut > 0)
                cmd.CommandTimeout = this.CommandTimeOut;

            if (parameters != null)
                for (int i = 0; i < parameters.Length; i++)
                    if (parameters[i] != null)
                    {
                        if (commandType != CommandType.StoredProcedure)
                        {
                            //IDataParameter para = (IDataParameter)((ICloneable)parameters[i]).Clone();
                            IDataParameter para = parameters[i];
                            if (para.Value == null)
                                para.Value = DBNull.Value;
                            cmd.Parameters.Add(para);
                        }
                        else
                        {
                            //Ϊ�洢���̴��ط���ֵ
                            cmd.Parameters.Add(parameters[i]);
                        }
                    }

            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            //������־����
            //dth,2008.4.8
            //
            //			if(SaveCommandLog )
            //				RecordCommandLog(cmd);
            //CommandLog.Instance.WriteLog(cmd,"AdoHelper");
        }

        //		/// <summary>
        //		/// ��¼������Ϣ
        //		/// </summary>
        //		/// <param name="command"></param>
        //		private void RecordCommandLog(IDbCommand command)
        //		{
        //			WriteLog("'"+DateTime.Now.ToString ()+ " @AdoHelper ִ�����\rSQL=\""+command.CommandText+"\"\r'�������ͣ�"+command.CommandType.ToString ());
        //			if(command.Parameters.Count >0)
        //			{
        //				WriteLog("'���С�"+command.Parameters.Count+"�������������");
        //				for(int i=0;i<command.Parameters.Count ;i++)
        //				{
        //					IDataParameter p=(IDataParameter)command.Parameters[i];
        //					WriteLog ("Parameter["+p.ParameterName+"]=\""+p.Value.ToString ()+"\"  'DbType=" +p.DbType.ToString ());
        //				}
        //			}
        //			WriteLog ("\r\n");
        //
        //		}
        //
        //		/// <summary>
        //		/// д����־
        //		/// </summary>
        //		/// <param name="log"></param>
        //		private void WriteLog(string log)
        //		{
        //			StreamWriter sw=File.AppendText (this.DataLogFile );;
        //			sw.WriteLine (log);
        //			sw.Close ();
        //		}

        /// <summary>
        /// ִ�в�����ֵ�Ĳ�ѯ������˲�ѯ�����˴��������� OnErrorThrow ����Ϊ �ǣ����׳����󣻷��򽫷��� -1����ʱ����ErrorMessage���ԣ�
        /// ����˲�ѯ�������в��ҳ����˴��󣬽����� OnErrorRollback ���������Ƿ��Զ��ع�����
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��������</param>
        /// <returns>��Ӱ�������</returns>
        public virtual int ExecuteNonQuery(string SQL, CommandType commandType, IDataParameter[] parameters)
        {
            ErrorMessage = "";
            IDbConnection conn = GetConnection();
            if (conn.State != ConnectionState.Open) //�����Ѿ��򿪣������л������ַ�������л���� ������û��ò�����ִ�Bug 
                conn.ConnectionString = this.DataWriteConnectionString;
            IDbCommand cmd = conn.CreateCommand();
            CompleteCommand(cmd, ref SQL, ref commandType, ref parameters);
            //cmd.Prepare();
            CommandLog cmdLog = new CommandLog(true);

            int result = -1;
            try
            {
                result = cmd.ExecuteNonQuery();
                //����������������ϲ�����߾�����ʱ�ύ����
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                bool inTransaction = cmd.Transaction == null ? false : true;

                //�������������ô�˴�Ӧ�û�������
                if (cmd.Transaction != null && OnErrorRollback)
                    cmd.Transaction.Rollback();

                cmdLog.WriteErrLog(cmd, "AdoHelper:" + ErrorMessage);
                if (OnErrorThrow)
                {
                    throw new QueryException(ex.Message, cmd.CommandText, commandType, parameters, inTransaction, conn.ConnectionString);
                }
            }
            finally
            {
                cmdLog.WriteLog(cmd, "AdoHelper", out _elapsedMilliseconds);
                CloseConnection(conn, cmd);
            }
            return result;
        }

        /// <summary>
        /// ִ�в�����ֵ�Ĳ�ѯ������˲�ѯ�����˴��󣬽����� -1����ʱ����ErrorMessage���ԣ�
        /// ����˲�ѯ�������в��ҳ����˴��󣬽����� OnErrorRollback ���������Ƿ��Զ��ع�����
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <returns>��Ӱ�������</returns>
        public int ExecuteNonQuery(string SQL)
        {
            return ExecuteNonQuery(SQL, CommandType.Text, null);
        }

        /// <summary>
        /// ִ�в������ݵĲ�ѯ��������Access��SqlServer
        /// </summary>
        /// <param name="SQL">�������ݵ�SQL</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��������</param>
        /// <param name="ID">Ҫ�����ı��β������²��������е�����IDֵ</param>
        /// <returns>���β�ѯ��Ӱ�������</returns>
        public virtual int ExecuteInsertQuery(string SQL, CommandType commandType, IDataParameter[] parameters, ref object ID)
        {
            IDbConnection conn = GetConnection();
            if (conn.State != ConnectionState.Open) //�����Ѿ��򿪣������л������ַ�������л���� ������û��ò�����ִ�Bug 
                conn.ConnectionString = this.DataWriteConnectionString;
            IDbCommand cmd = conn.CreateCommand();
            CompleteCommand(cmd, ref SQL, ref commandType, ref parameters);

            CommandLog cmdLog = new CommandLog(true);

            bool inner = false;
            int result = -1;
            ID = 0;
            try
            {
                if (cmd.Transaction == null)
                {
                    inner = true;
                    cmd.Transaction = conn.BeginTransaction();
                }

                result = cmd.ExecuteNonQuery();
                cmd.CommandText = this.InsertKey;// "SELECT @@IDENTITY ";
                ID = cmd.ExecuteScalar();
                //������ڲ��������������ύ���񣬷����ⲿ�����߾�����ʱ�ύ����
                if (inner)
                {
                    cmd.Transaction.Commit();
                    cmd.Transaction = null;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                bool inTransaction = cmd.Transaction == null ? false : true;
                if (cmd.Transaction != null)
                    cmd.Transaction.Rollback();
                if (inner)
                    cmd.Transaction = null;

                cmdLog.WriteErrLog(cmd, "AdoHelper:" + ErrorMessage);
                if (OnErrorThrow)
                {
                    throw new QueryException(ex.Message, cmd.CommandText, commandType, parameters, inTransaction, conn.ConnectionString);
                }

            }
            finally
            {
                cmdLog.WriteLog(cmd, "AdoHelper", out _elapsedMilliseconds);
                CloseConnection(conn, cmd);
            }
            return result;
        }

        /// <summary>
        /// ִ�в������ݵĲ�ѯ
        /// </summary>
        /// <param name="SQL">�������ݵ�SQL</param>
        /// <param name="ID">Ҫ�����ı��β������²��������е�����IDֵ</param>
        /// <returns>���β�ѯ��Ӱ�������</returns>
        public int ExecuteInsertQuery(string SQL, ref object ID)
        {
            return ExecuteInsertQuery(SQL, CommandType.Text, null, ref ID);
        }

        /// <summary>
        /// ִ�з��ص�һֵ�ò�ѯ
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��������</param>
        /// <returns>��ѯ���</returns>
        public virtual object ExecuteScalar(string SQL, CommandType commandType, IDataParameter[] parameters)
        {
            IDbConnection conn = GetConnection();
            IDbCommand cmd = conn.CreateCommand();
            CompleteCommand(cmd, ref SQL, ref commandType, ref parameters);

            CommandLog cmdLog = new CommandLog(true);

            object result = null;
            try
            {
                result = cmd.ExecuteScalar();
                //����������������ϲ�����߾�����ʱ�ύ����
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                //�������������ô�˴�Ӧ�û�������
                //if(cmd.Transaction!=null)
                //    cmd.Transaction.Rollback ();

                bool inTransaction = cmd.Transaction == null ? false : true;
                cmdLog.WriteErrLog(cmd, "AdoHelper:" + ErrorMessage);
                if (OnErrorThrow)
                {
                    throw new QueryException(ex.Message, cmd.CommandText, commandType, parameters, inTransaction, conn.ConnectionString);
                }
            }
            finally
            {
                cmdLog.WriteLog(cmd, "AdoHelper", out _elapsedMilliseconds);
                CloseConnection(conn, cmd);
            }
            return result;
        }

        /// <summary>
        /// ִ�з��ص�һֵ�ò�ѯ
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <returns>��ѯ���</returns>
        public object ExecuteScalar(string SQL)
        {
            return ExecuteScalar(SQL, CommandType.Text, null);
        }

        /// <summary>
        /// ִ�з������ݼ��Ĳ�ѯ
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��������</param>
        /// <returns>���ݼ�</returns>
        public virtual DataSet ExecuteDataSet(string SQL, CommandType commandType, IDataParameter[] parameters)
        {
            //IDbConnection conn=GetConnection();
            //IDbCommand cmd=conn.CreateCommand ();
            //CompleteCommand(cmd,ref SQL,ref commandType,ref parameters);
            //IDataAdapter ada=GetDataAdapter(cmd);

            //CommandLog cmdLog = new CommandLog(true);

            //DataSet ds=new DataSet ();
            //try
            //{
            //    ada.Fill(ds);//FillSchema(ds,SchemaType.Mapped )
            //}
            //catch(Exception ex)
            //{
            //    ErrorMessage=ex.Message ;
            //    bool inTransaction = cmd.Transaction == null ? false : true;
            //    cmdLog.WriteErrLog(cmd, "AdoHelper:" + ErrorMessage);
            //    if (OnErrorThrow)
            //    {
            //        throw new QueryException(ex.Message, cmd.CommandText, commandType, parameters, inTransaction, conn.ConnectionString);
            //    }
            //}
            //finally
            //{
            //    if(cmd.Transaction==null && conn.State ==ConnectionState.Open )
            //        conn.Close ();
            //}

            //cmdLog.WriteLog(cmd, "AdoHelper", out _elapsedMilliseconds);

            //return ds;

            DataSet ds = new DataSet();
            return ExecuteDataSetWithSchema(SQL, commandType, parameters, ds);
        }

        /// <summary>
        /// ִ�з������ݼܹ��Ĳ�ѯ��ע�⣬�������κ���
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��������</param>
        /// <returns>���ݼܹ�</returns>
        public virtual DataSet ExecuteDataSetSchema(string SQL, CommandType commandType, IDataParameter[] parameters)
        {
            IDbConnection conn = GetConnection();
            IDbCommand cmd = conn.CreateCommand();
            CompleteCommand(cmd, ref SQL, ref commandType, ref parameters);
            IDataAdapter ada = GetDataAdapter(cmd);

            DataSet ds = new DataSet();
            try
            {
                ada.FillSchema(ds, SchemaType.Mapped);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                bool inTransaction = cmd.Transaction == null ? false : true;
                CommandLog.Instance.WriteErrLog(cmd, "AdoHelper:" + ErrorMessage);
                if (OnErrorThrow)
                {
                    throw new QueryException(ex.Message, cmd.CommandText, commandType, parameters, inTransaction, conn.ConnectionString);
                }
            }
            finally
            {
                //if (cmd.Transaction == null && conn.State == ConnectionState.Open)
                //    conn.Close();
                CloseConnection(conn, cmd);
            }
            return ds;
        }

        /// <summary>
        /// ִ�в�ѯ,�����ؾ������ݼܹ������ݼ�(�������̽�ʹ��һ������)
        /// </summary>
        /// <param name="SQL">��ѯ���</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��ѯ����</param>
        /// <returns>�������ݼܹ������ݼ�</returns>
        public virtual DataSet ExecuteDataSetWithSchema(string SQL, CommandType commandType, IDataParameter[] parameters)
        {
            IDbConnection conn = GetConnection();
            IDbCommand cmd = conn.CreateCommand();
            CompleteCommand(cmd, ref SQL, ref commandType, ref parameters);
            IDataAdapter ada = GetDataAdapter(cmd);

            CommandLog cmdLog = new CommandLog(true);
            DataSet ds = new DataSet();
            try
            {
                ada.FillSchema(ds, SchemaType.Mapped);
                ada.Fill(ds);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                bool inTransaction = cmd.Transaction == null ? false : true;
                cmdLog.WriteErrLog(cmd, "AdoHelper:" + ErrorMessage);
                if (OnErrorThrow)
                {
                    throw new QueryException(ex.Message, cmd.CommandText, commandType, parameters, inTransaction, conn.ConnectionString);
                }
            }
            finally
            {
                //if (cmd.Transaction == null && conn.State == ConnectionState.Open)
                //    conn.Close();
                CloseConnection(conn, cmd);
            }

            cmdLog.WriteLog(cmd, "AdoHelper", out _elapsedMilliseconds);

            return ds;
        }

        /// <summary>
        /// ִ�в�ѯ,����ָ����(�������ݼܹ���)���ݼ����������
        /// </summary>
        /// <param name="SQL">��ѯ���</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��ѯ����</param>
        /// <param name="schemaDataSet">ָ����(�������ݼܹ���)���ݼ�</param>
        /// <returns>�������ݵ����ݼ�</returns>
        public virtual DataSet ExecuteDataSetWithSchema(string SQL, CommandType commandType, IDataParameter[] parameters, DataSet schemaDataSet)
        {
            IDbConnection conn = GetConnection();
            IDbCommand cmd = conn.CreateCommand();
            CompleteCommand(cmd, ref SQL, ref commandType, ref parameters);
            IDataAdapter ada = GetDataAdapter(cmd);

            CommandLog cmdLog = new CommandLog(true);

            try
            {
                //ʹ��MyDB.Intance ���Ӳ��ܼ�ʱ�رգ�������
                ada.Fill(schemaDataSet);//FillSchema(ds,SchemaType.Mapped )
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                bool inTransaction = cmd.Transaction == null ? false : true;
                cmdLog.WriteErrLog(cmd, "AdoHelper:" + ErrorMessage);
                if (OnErrorThrow)
                {
                    throw new QueryException(ex.Message, cmd.CommandText, commandType, parameters, inTransaction, conn.ConnectionString);
                }
            }
            finally
            {
                cmdLog.WriteLog(cmd, "AdoHelper", out _elapsedMilliseconds);
                CloseConnection(conn, cmd);
            }
            return schemaDataSet;
        }

        /// <summary>
        /// ִ�з������ݼ��Ĳ�ѯ
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <returns>���ݼ�</returns>
        public DataSet ExecuteDataSet(string SQL)
        {
            return ExecuteDataSet(SQL, CommandType.Text, null);
        }


        /// <summary>
        /// ���ص�һ�е������Ķ���
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <returns>�����Ķ���</returns>
        public IDataReader ExecuteDataReaderWithSingleRow(string SQL)
        {
            IDataParameter[] paras = { };
            //���������ʱ���ܹر�����
            return ExecuteDataReaderWithSingleRow(SQL, paras);
        }

        /// <summary>
        /// ���ص�һ�е������Ķ���
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <param name="paras">����</param>
        /// <returns>�����Ķ���</returns>
        public IDataReader ExecuteDataReaderWithSingleRow(string SQL, IDataParameter[] paras)
        {
            //����������������ӻỰ��ʱ���ܹر�����
            if (this.transCount > 0 || this.sessionConnection != null)
                return ExecuteDataReader(ref SQL, CommandType.Text, CommandBehavior.SingleRow, ref paras);
            else
                return ExecuteDataReader(ref SQL, CommandType.Text, CommandBehavior.SingleRow | CommandBehavior.CloseConnection, ref paras);
        }

        /// <summary>
        /// ���ݲ�ѯ���������Ķ��������ڷ���������У��Ķ����Ժ���Զ��ر����ݿ�����
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <returns>�����Ķ���</returns>
        public IDataReader ExecuteDataReader(string SQL)
        {
            IDataParameter[] paras = { };
            //������������лỰ��ʱ���ܹر����� edit at 2012.7.23
            //this.Transaction == null ����ȫ
            CommandBehavior behavior = this.transCount > 0 || this.sessionConnection != null
                ? CommandBehavior.SingleResult
                : CommandBehavior.SingleResult | CommandBehavior.CloseConnection;
            return ExecuteDataReader(ref SQL, CommandType.Text, behavior, ref paras);
        }

        /// <summary>
        /// ���ݲ�ѯ���������Ķ�������
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <param name="cmdBehavior">�Բ�ѯ�ͷ��ؽ����Ӱ���˵��</param>
        /// <returns>�����Ķ���</returns>
        public IDataReader ExecuteDataReader(string SQL, CommandBehavior cmdBehavior)
        {
            IDataParameter[] paras = { };
            return ExecuteDataReader(ref SQL, CommandType.Text, cmdBehavior, ref paras);
        }

        /// <summary>
        /// ���ݲ�ѯ���������Ķ������󣬵����������ȡ��������
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��������</param>
        /// <returns>�����Ķ���</returns>
        public IDataReader ExecuteDataReader(string SQL, CommandType commandType, IDataParameter[] parameters)
        {
            //������������лỰ��ʱ���ܹر����� edit at 2012.7.23
            //this.Transaction == null ����ȫ
            CommandBehavior behavior = this.transCount > 0 || this.sessionConnection != null
                ? CommandBehavior.SingleResult
                : CommandBehavior.SingleResult | CommandBehavior.CloseConnection;
            return ExecuteDataReader(ref SQL, commandType, behavior, ref parameters);
        }

        /// <summary>
        /// ���ݲ�ѯ���������Ķ�������,����˳���ȡ���ڵĴ�������
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��������</param>
        /// <returns>�����Ķ���</returns>
        public IDataReader ExecuteDataReaderSequentialAccess(string SQL, CommandType commandType, IDataParameter[] parameters)
        {
            CommandBehavior behavior = this.transCount == 0 //this.Transaction == null ����ȫ
                ? CommandBehavior.SingleResult | CommandBehavior.CloseConnection | CommandBehavior.SequentialAccess
                : CommandBehavior.SingleResult | CommandBehavior.SequentialAccess;//����SequentialAccess 2013.9.24
            return ExecuteDataReader(ref SQL, commandType, behavior, ref parameters);
        }

        /// <summary>
        /// ���ݲ�ѯ���������Ķ�������
        /// </summary>
        /// <param name="SQL">SQL</param>
        /// <param name="commandType">��������</param>
        /// <param name="cmdBehavior">�Բ�ѯ�ͷ��ؽ����Ӱ���˵��</param>
        /// <param name="parameters">��������</param>
        /// <returns>�����Ķ���</returns>
        protected virtual IDataReader ExecuteDataReader(ref string SQL, CommandType commandType, CommandBehavior cmdBehavior, ref IDataParameter[] parameters)
        {
            IDbConnection conn = GetConnection();
            IDbCommand cmd = conn.CreateCommand();
            CompleteCommand(cmd, ref SQL, ref commandType, ref parameters);

            CommandLog cmdLog = new CommandLog(true);

            IDataReader reader = null;
            try
            {
                //������������������Ϊ�գ���ôǿ���ڶ�ȡ�����ݺ�ر��Ķ��������ݿ����� 2008.3.20
                if (cmd.Transaction == null && cmdBehavior == CommandBehavior.Default)
                    cmdBehavior = CommandBehavior.CloseConnection;
                reader = cmd.ExecuteReader(cmdBehavior);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                //ֻ�г����˴������û�п������񣬿��Թر�����
                //if (cmd.Transaction == null && conn.State == ConnectionState.Open)
                //    conn.Close();
                CloseConnection(conn, cmd);

                bool inTransaction = cmd.Transaction == null ? false : true;
                cmdLog.WriteErrLog(cmd, "AdoHelper:" + ErrorMessage);
                if (OnErrorThrow)
                {
                    throw new QueryException(ex.Message, cmd.CommandText, commandType, parameters, inTransaction, conn.ConnectionString);
                }
            }

            cmdLog.WriteLog(cmd, "AdoHelper", out _elapsedMilliseconds);
            cmd.Parameters.Clear();

            return reader;
        }

        /// <summary>
        /// �ر�����
        /// </summary>
        /// <param name="conn">���Ӷ���</param>
        /// <param name="cmd">�������</param>
        protected void CloseConnection(IDbConnection conn, IDbCommand cmd)
        {
            if (cmd.Transaction == null && conn != sessionConnection && conn.State == ConnectionState.Open)
                conn.Close();
            cmd.Parameters.Clear();
        }

        private void CloseGlobalConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }

        void IDisposable.Dispose()
        {
            if (!disposed)
            {
                CloseGlobalConnection();
                CloseSession();
                disposed = true;
            }
        }
    }
}
