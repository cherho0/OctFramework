/*
 * ========================================================================
 * Copyright(c) 2006-2010 PWMIS, All Rights Reserved.
 * Welcom use the PDF.NET (PWMIS Data Process Framework).
 * See more information,Please goto http://www.pwmis.com/sqlmap 
 * ========================================================================
 * MyDB.cs
 * ��������ã��ṩ�����ķ�ʽ�������ݿ�ʵ���Ͳ������ݼ�
 * 
 * ���ߣ���̫��     ʱ�䣺2008-10-12
 * �汾��V3.0
 * 
 * �޸��ߣ�         ʱ�䣺2011.11.16                
 * �޸�˵���������Զ��������ݼ��Ĺ���
 * ========================================================================
*/
using System;
using System.Data;
using System.Configuration;
using PWMIS.DataProvider.Data;
using PWMIS.Common;

namespace PWMIS.DataProvider.Adapter
{
    /// <summary>
    /// Ӧ�ó������ݷ���ʵ�����ṩ����ģʽ�͹���ģʽ����ʵ�����󣬸���Ӧ�ó��������ļ��Զ������ض������ݷ��ʶ���
    /// 2008.5.23 ���Ӷ�̬���ݼ����¹���,7.24�����̰߳�ȫ�ľ�̬ʵ����
    /// 2009.4.1  ����SQLite ���ݿ�֧�֡�
    /// 2010.1.6  ���� connectionStrings ����֧��
    /// </summary>
    public class MyDB
    {
        private static AdoHelper _instance = null;
        private string _msg = string.Empty;
        private static object lockObj = new object();

        #region ��ȡ��̬ʵ��

        /// <summary>
        /// ���ݷ��ʾ�̬ʵ������������������п��ܴ��ڲ������ʣ�����ʹ�ø����ԣ����Ǵ�������Ķ�̬ʵ������
        /// </summary>
        public static AdoHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = MyDB.GetDBHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region ��ȡ��̬ʵ������
        /// <summary>
        /// ͨ�������ļ�������ݷ��ʶ���ʵ����
        /// ����Ӧ�ó��������ļ��д��� EngineType ����ֵΪ[DB]��ͬʱ��Ҫ���� [DB]HelperAssembly��[DB]HelperType ��[DB]ConnectionString ����[DB]ֵΪSQLSERVER/OLEDB/ODBC/ORACLE ֮һ
        /// ���δָ�� EngineType ������ʹ�� connectionStrings ���ýڵĵ�һ������������Ϣ������ָ�� providerName������ʹ���������ʽ��
        /// providerName="PWMIS.DataProvider.Data.SqlServer,PWMIS.Core"
        /// Ҳ����ֱ��ʹ�� �������ʽ��
        /// providerName="SqlServer" ����Ȼ��������ʽ���ṩ�������Ĭ�Ͼ��� PWMIS.CommonDataProvider.Data ��
        /// ����ж����Ĭ��ȡ���һ�� providerName
        /// </summary>
        /// <returns>���ݷ��ʶ���ʵ��</returns>
        public static AdoHelper GetDBHelper()
        {
            string engineType = ConfigurationSettings.AppSettings["EngineType"];
            AdoHelper helper = null;
            if (string.IsNullOrEmpty(engineType))
            {
                //�� connectionStrings ��ȡ
                if (ConfigurationManager.ConnectionStrings.Count == 0)
                    throw new Exception("appSettings δָ�� EngineType ���ü���Ҳδ�� connectionStrings ���ý�����������Ϣ");

                helper = GetDBHelperByConnectionSetting(ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings.Count - 1]);
            }
            else
            {
                helper = GetDBHelper(engineType);
                helper.ConnectionString = GetConnectionString();
            }
            return helper;
        }

        /// <summary>
        /// �� connectionStrings ���ýڻ�ȡָ�� �����������Ƶ����ݷ��ʶ���ʵ��
        /// </summary>
        /// <param name="name">������������</param>
        /// <returns></returns>
        public static AdoHelper GetDBHelperByConnectionName(string name)
        {
            ConnectionStringSettings connSetting = ConfigurationManager.ConnectionStrings[name];
            if (connSetting == null)
                throw new Exception("δ�� connectionStrings ���ý��ҵ�ָ���� �������ƣ�" + name);

            return GetDBHelperByConnectionSetting(connSetting);
        }

        private static AdoHelper GetDBHelperByConnectionSetting(ConnectionStringSettings connSetting)
        {
            return GetDBHelperByProviderString(connSetting.ProviderName, connSetting.ConnectionString);
        }

        /// <summary>
        /// �����ṩ���������ַ����������ַ����������ṩ����ʵ��
        /// </summary>
        /// <param name="providerName">�����������ַ�������ʽΪ���ṩ����ȫ����,��������</param>
        /// <param name="connectionString">�����ַ���</param>
        /// <returns></returns>
        public static AdoHelper GetDBHelperByProviderString(string providerName, string connectionString)
        {
            string[] providerInfo = providerName.Split(',');
            string helperAssembly;
            string helperType;

            if (providerInfo.Length == 1)
            {
                helperAssembly = "PWMIS.Core";
                helperType = "PWMIS.DataProvider.Data." + providerName;
            }
            else
            {
                helperAssembly = providerInfo[1].Trim();
                helperType = providerInfo[0].Trim();
            }
            return GetDBHelper(helperAssembly, helperType, connectionString);
        }
        /// <summary>
        /// ͨ��ָ�������ݿ����ͣ�ֵΪSQLSERVER/OLEDB/ODBC/ORACLE ֮һ���������ַ�������һ���µ����ݷ��ʶ���
        /// ��Ҫ����[DB]HelperAssembly��[DB]HelperType ����[DB]ֵΪSQLSERVER/OLEDB/ODBC/ORACLE ֮һ
        /// </summary>
        /// <param name="EngineType">���ݿ����ͣ�ֵΪSQLSERVER/OLEDB/ODBC/ORACLE ֮һ��</param>
        /// <param name="ConnectionString">�����ַ���</param>
        /// <returns>���ݷ��ʶ���</returns>
        public static AdoHelper GetDBHelper(string EngineType, string ConnectionString)
        {
            AdoHelper helper = GetDBHelper(EngineType);
            helper.ConnectionString = ConnectionString;
            return helper;
        }

        /// <summary>
        /// �������ݿ����ϵͳö�����ͺ������ַ�������һ���µ����ݷ��ʶ���ʵ��
        /// </summary>
        /// <param name="DbmsType">���ݿ�����ý�飬��ACCESS/MYSQL/ORACLE/SQLSERVER/SYSBASE/UNKNOWN </param>
        /// <param name="ConnectionString">�����ַ���</param>
        /// <returns>���ݷ��ʶ���</returns>
        public static AdoHelper GetDBHelper(DBMSType DbmsType, string ConnectionString)
        {
            string EngineType = "";
            switch (DbmsType)
            {
                case DBMSType.Access:
                    EngineType = "OleDb"; break;
                case DBMSType.MySql:
                    EngineType = "Odbc"; break;
                case DBMSType.Oracle:
                    EngineType = "Oracle"; break;
                case DBMSType.SqlServer:
                    EngineType = "SqlServer"; break;
                case DBMSType.SqlServerCe:
                    EngineType = "SqlServerCe"; break;
                case DBMSType.Sysbase:
                    EngineType = "OleDb"; break;
                case DBMSType.SQLite:
                    EngineType = "SQLite"; break;
                case DBMSType.UNKNOWN:
                    EngineType = "Odbc"; break;
            }
            AdoHelper helper = GetDBHelper(EngineType);
            helper.ConnectionString = ConnectionString;
            return helper;
        }

        /// <summary>
        /// ���ݳ������ƺ����ݷ��ʶ������ʹ���һ���µ����ݷ��ʶ���ʵ����
        /// </summary>
        /// <param name="HelperAssembly">��������</param>
        /// <param name="HelperType">���ݷ��ʶ�������</param>
        /// <param name="ConnectionString">�����ַ���</param>
        /// <returns>���ݷ��ʶ���</returns>
        public static AdoHelper GetDBHelper(string HelperAssembly, string HelperType, string ConnectionString)
        {
            AdoHelper helper = null;// CommonDB.CreateInstance(HelperAssembly, HelperType);
            if (HelperAssembly == "PWMIS.Core")
            {
                switch (HelperType)
                {
                    case "PWMIS.DataProvider.Data.SqlServer": helper = new SqlServer(); break;
                    case "PWMIS.DataProvider.Data.Oracle": helper = new Oracle(); break;
                    case "PWMIS.DataProvider.Data.OleDb": helper = new OleDb(); break;
                    case "PWMIS.DataProvider.Data.Odbc": helper = new Odbc(); break;
                    case "PWMIS.DataProvider.Data.Access": helper = new Access(); break;
                    case "PWMIS.DataProvider.Data.SqlServerCe": helper = new SqlServerCe(); break;
                    default: helper = new SqlServer(); break;
                }
            }
            else
            {
                helper = CommonDB.CreateInstance(HelperAssembly, HelperType);
            }
            helper.ConnectionString = ConnectionString;
            return helper;
        }


        /// <summary>
        /// ������ݷ��ʶ���ʵ����EngineTypeֵΪSQLSERVER/OLEDB/ODBC/ORACLE ֮һ��Ĭ��ʹ�� PWMIS.CommonDataProvider.Data.SqlServer
        /// </summary>
        /// <param name="EngineType">���ݿ���������</param>
        /// <returns>���ݷ��ʶ���ʵ��</returns>
        private static AdoHelper GetDBHelper(string EngineType)
        {
            string assembly = null;
            string type = null;

            switch (EngineType.ToUpper())
            {
                case "SQLSERVER":
                    assembly = ConfigurationSettings.AppSettings["SqlServerHelperAssembly"];
                    type = ConfigurationSettings.AppSettings["SqlServerHelperType"];
                    if (string.IsNullOrEmpty(assembly))
                    {
                        assembly = "PWMIS.Core";
                        type = "PWMIS.DataProvider.Data.SqlServer";
                    }
                    break;
                case "SQLSERVERCE":
                    assembly = ConfigurationSettings.AppSettings["SqlServerCeHelperAssembly"];
                    type = ConfigurationSettings.AppSettings["SqlServerCeHelperType"];
                    if (string.IsNullOrEmpty(assembly))
                    {
                        assembly = "PWMIS.Core";
                        type = "PWMIS.DataProvider.Data.SqlServerCe";
                    }
                    break;
                case "OLEDB":
                    assembly = ConfigurationSettings.AppSettings["OleDbHelperAssembly"];
                    type = ConfigurationSettings.AppSettings["OleDbHelperType"];
                    if (string.IsNullOrEmpty(assembly))
                    {
                        assembly = "PWMIS.Core";
                        type = "PWMIS.DataProvider.Data.OleDb";
                    }
                    break;
                case "ACCESS":
                    assembly = ConfigurationSettings.AppSettings["OleDbHelperAssembly"];
                    type = ConfigurationSettings.AppSettings["OleDbHelperType"];
                    if (string.IsNullOrEmpty(assembly))
                    {
                        assembly = "PWMIS.Core";
                        type = "PWMIS.DataProvider.Data.Access";
                    }
                    break;
                case "ODBC":
                    assembly = ConfigurationSettings.AppSettings["OdbcHelperAssembly"];
                    type = ConfigurationSettings.AppSettings["OdbcHelperType"];
                    if (string.IsNullOrEmpty(assembly))
                    {
                        assembly = "PWMIS.Core";
                        type = "PWMIS.DataProvider.Data.Odbc";
                    }
                    break;
                case "ORACLE":
                    assembly = ConfigurationSettings.AppSettings["OracleHelperAssembly"];
                    type = ConfigurationSettings.AppSettings["OracleHelperType"];
                    if (string.IsNullOrEmpty(assembly))
                    {
                        assembly = "PWMIS.Core";
                        type = "PWMIS.DataProvider.Data.Oracle";
                    }
                    break;
                //����֧�������SQLite���ͣ���Ϊ����64λ��32λ��windows,linux �����в�ͬ�����������޷�������ȷ��
                //case "SQLITE":
                //    assembly = ConfigurationSettings.AppSettings["SQLiteHelperAssembly"];
                //    type = ConfigurationSettings.AppSettings["SQLiteHelperType"];
                //    if (string.IsNullOrEmpty(assembly))
                //    {
                //        assembly = "PWMIS.DataProvider.Data.SQLite";
                //        type = "PWMIS.DataProvider.Data.SQLite";
                //    }
                //    break;
                default:
                    assembly = "PWMIS.DataProvider.Data";
                    type = "PWMIS.DataProvider.Data.SqlServer";
                    break;
            }

            return GetDBHelper(assembly, type, null);
        }

        #endregion

        #region ������̬����

        /// <summary>
        /// ������ݷ��������ַ���������Ӧ�ó��������ļ��д��� EngineType��[DB]HelperAssembly��[DB]HelperType ,[DB]ConnectionString����ֵΪSQLSERVER/OLEDB/ODBC/ORACLE ֮һ
        /// ���û���ҵ� [DB]ConnectionString ����Ҳ����ֱ��ʹ�� ConnectionString ��
        /// </summary>
        /// <returns>���ݷ��������ַ���</returns>
        public static string GetConnectionString()
        {

            string connectionString = null;

            switch (ConfigurationSettings.AppSettings["EngineType"].ToUpper())
            {
                case "SQLSERVER":
                    connectionString = ConfigurationSettings.AppSettings["SqlServerConnectionString"];
                    break;
                case "OLEDB":
                    connectionString = ConfigurationSettings.AppSettings["OleDbConnectionString"];
                    break;
                case "ODBC":
                    connectionString = ConfigurationSettings.AppSettings["OdbcConnectionString"];
                    break;
                case "ORACLE":
                    connectionString = ConfigurationSettings.AppSettings["OracleConnectionString"];
                    break;
                case "SQLITE":
                    connectionString = ConfigurationSettings.AppSettings["SQLiteConnectionString"];
                    break;
            }
            if (string.IsNullOrEmpty(connectionString))
                connectionString = ConfigurationSettings.AppSettings["ConnectionString"];

            return connectionString;
        }

        /// <summary>
        /// �������ݼ�(���ò�����ʽ)�����ݱ����ָ����������ôִ�и��²���������ִ�в��������
        /// </summary>
        /// <param name="ds">���ݼ�</param>
        /// <returns>��ѯ�����Ӱ�������</returns>
        public static int UpdateDataSet(DataSet ds)
        {
            int count = 0;
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.PrimaryKey.Length > 0)
                {
                    count += UpdateDataTable(dt, GetSqlUpdate(dt));
                }
                else
                {
                    count += UpdateDataTable(dt, GetSqlInsert(dt));
                }// end if
            }//end for
            return count;
        }//end function

        /// <summary>
        /// �Զ������ݼ��е����ݸ��»��߲��뵽���ݿ�
        /// <remarks>����ʱ�䣺2011.11.16</remarks>
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="DB"></param>
        /// <returns></returns>
        public static int SaveDataSet(DataSet ds, CommonDB DB)
        {
            int count = 0;
            foreach (DataTable dt in ds.Tables)
            {
                string insertSql = GetSqlInsert(dt);
                string updateSql = GetSqlUpdate(dt);
                count += SaveDataTable(dt, insertSql, updateSql, DB);
            }//end for
            return count;
        }
        /// <summary>
        /// �������ݼ�����ָ���ı��У����ݱ��е�ָ���е�ֵ������Դ��ɾ������
        /// </summary>
        /// <param name="ds">���ݼ�</param>
        /// <param name="tableName">������</param>
        /// <param name="columnName">����</param>
        /// <returns>��ѯ��Ӱ�������</returns>
        public static int DeleteDataSet(DataSet ds, string tableName, string columnName)
        {
            DataTable dt = ds.Tables[tableName];

            CommonDB DB = MyDB.GetDBHelper();
            string ParaChar = GetDBParaChar(DB);
            int count = 0;

            string sqlDelete = "DELETE FROM " + tableName + " WHERE " + columnName + "=" + ParaChar + columnName;

            DB.BeginTransaction();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    IDataParameter[] paras = { DB.GetParameter(ParaChar + columnName, dr[columnName]) };
                    count += DB.ExecuteNonQuery(sqlDelete, CommandType.Text, paras);
                    if (DB.ErrorMessage != "")
                        throw new Exception(DB.ErrorMessage);
                    if (count >= dt.Rows.Count) break;
                }
                DB.Commit();
            }
            catch (Exception ex)
            {
                DB.Rollback();
                throw ex;
            }
            return count;

        }

        #endregion

        #region ������̬ʵ������
        /// <summary>
        /// ��ȡ��ǰ������Ϣ
        /// </summary>
        public string Message
        {
            get { return _msg; }
        }
        /// <summary>
        /// �������ݼ��������ݷ��ʶ���
        /// </summary>
        /// <param name="ds">���ݼ�</param>
        /// <param name="DB">���ݷ��ʶ���</param>
        /// <returns></returns>
        public int UpdateDataSet(DataSet ds, CommonDB DB)
        {
            int count = 0;
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.PrimaryKey.Length > 0)
                {
                    count += UpdateDataTable(dt, GetSqlUpdate(dt), DB);
                    _msg = "�Ѿ����¼�¼" + count + "��";
                }
                else
                {
                    count += UpdateDataTable(dt, GetSqlInsert(dt), DB);
                    _msg = "�Ѿ������¼" + count + "��";
                }// end if
            }//end for
            return count;
        }//end function

        /// <summary>
        /// �������ݼ�����ָ���ı��У����ݱ��е�ָ���е�ֵ������Դ��ɾ������,�����ݷ��ʶ���
        /// </summary>
        /// <param name="ds">���ݼ�</param>
        /// <param name="tableName">������</param>
        /// <param name="columnName">����</param>
        /// <param name="DB">���ݷ��ʶ���</param>
        /// <returns></returns>
        public int DeleteDataSet(DataSet ds, string tableName, string columnName, CommonDB DB)
        {
            DataTable dt = ds.Tables[tableName];
            string ParaChar = GetDBParaChar(DB);
            int count = 0;
            string sqlDelete = "DELETE FROM " + tableName + " WHERE " + columnName + "=" + ParaChar + columnName;
            foreach (DataRow dr in dt.Rows)
            {
                IDataParameter[] paras = { DB.GetParameter(ParaChar + columnName, dr[columnName]) };
                count += DB.ExecuteNonQuery(sqlDelete, CommandType.Text, paras);
                if (DB.ErrorMessage != "")
                    throw new Exception(DB.ErrorMessage);
                if (count >= dt.Rows.Count) break;
            }
            return count;
        }

        /// <summary>
        /// ����������Ϣ������Դ��ѯ���ݱ����ݼ���
        /// </summary>
        /// <param name="tableName">����Դ�еı�����</param>
        /// <param name="pkNames">������������</param>
        /// <param name="pkValues">����ֵ���飬�������������Ӧ</param>
        /// <returns>���ݼ�</returns>
        public DataSet SelectDataSet(string tableName, string[] pkNames, object[] pkValues)
        {
            return SelectDataSet("*", tableName, pkNames, pkValues);
        }

        /// <summary>
        /// ����������Ϣ������Դ��ѯ���ݱ����ݼ���
        /// </summary>
        /// <param name="fields">�ֶ��б�</param>
        /// <param name="tableName">����Դ�еı�����</param>
        /// <param name="pkNames">������������</param>
        /// <param name="pkValues">����ֵ���飬�������������Ӧ</param>
        /// <param name="DB">���ݷ��ʶ���</param>
        /// <returns></returns>
        public DataSet SelectDataSet(string fields, string tableName, string[] pkNames, object[] pkValues, CommonDB DB)
        {
            string ParaChar = GetDBParaChar(DB);
            string sqlSelect = "SELECT " + fields + " FROM " + tableName + " WHERE 1=1 ";
            IDataParameter[] paras = new IDataParameter[pkNames.Length];
            for (int i = 0; i < pkNames.Length; i++)
            {
                sqlSelect += " And " + pkNames[i] + "=" + ParaChar + pkNames[i];
                paras[i] = DB.GetParameter(ParaChar + pkNames[i], pkValues[i]);
            }
            DataSet ds = DB.ExecuteDataSet(sqlSelect, CommandType.Text, paras);
            ds.Tables[0].TableName = tableName;
            return ds;
        }

        /// <summary>
        /// ����������Ϣ������Դ��ѯ���ݱ����ݼ���
        /// </summary>
        /// <param name="fields">�ֶ��б�</param>
        /// <param name="tableName">����Դ�еı�����</param>
        /// <param name="pkNames">������������</param>
        /// <param name="pkValues">����ֵ���飬�������������Ӧ</param>
        /// <returns>���ݼ�</returns>
        public DataSet SelectDataSet(string fields, string tableName, string[] pkNames, object[] pkValues)
        {
            CommonDB DB = MyDB.GetDBHelper();
            string ParaChar = GetDBParaChar(DB);
            string sqlSelect = "SELECT " + fields + " FROM " + tableName + " WHERE 1=1 ";
            IDataParameter[] paras = new IDataParameter[pkNames.Length];
            for (int i = 0; i < pkNames.Length; i++)
            {
                sqlSelect += " And " + pkNames[i] + "=" + ParaChar + pkNames[i];
                paras[i] = DB.GetParameter(ParaChar + pkNames[i], pkValues[i]);
            }
            DataSet ds = DB.ExecuteDataSet(sqlSelect, CommandType.Text, paras);
            ds.Tables[0].TableName = tableName;
            return ds;
        }

        /// <summary>
        /// �������ݼ��е��ֶε�����Դ��
        /// </summary>
        /// <param name="sDs">Դ���ݼ�</param>
        /// <param name="tableName">Ҫ���µı�</param>
        /// <param name="fieldName">Ҫ���µ��ֶ�</param>
        /// <param name="fieldValue">�ֶε�ֵ</param>
        /// <param name="pkName">��������</param>
        /// <param name="DB">���ݷ��ʶ���</param>
        /// <returns></returns>
        public int UpdateField(DataSet sDs, string tableName, string fieldName, object fieldValue, string pkName, CommonDB DB)
        {
            DataSet ds = sDs.Copy();
            DataTable dt = ds.Tables[tableName];
            fieldName = fieldName.ToUpper();
            pkName = pkName.ToUpper();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string colName = dt.Columns[i].ColumnName.ToUpper();
                if (colName == fieldName || colName == pkName)
                    continue;
                dt.Columns.Remove(colName);
                i = 0;//����Ԫ��λ�ÿ����Ѿ�Ǩ�ƣ�������Ҫ���´�ͷ��ʼ����
            }
            dt.PrimaryKey = new DataColumn[] { dt.Columns[pkName] };
            foreach (DataRow dr in dt.Rows)
            {
                dr[fieldName] = fieldValue;
            }

            int updCount = UpdateDataSet(ds, DB);
            return updCount;
        }

        #endregion

        #region �ڲ�����
        /// <summary>
        /// ��ȡ�ض����ݿ�����ַ�
        /// </summary>
        /// <param name="DB">���ݿ�����</param>
        /// <returns></returns>
        private static string GetDBParaChar(CommonDB DB)
        {
            return DB is Oracle ? ":" : "@";
        }

        /// <summary>
        /// �������ݱ�����Դ��
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="SQL"></param>
        /// <returns></returns>
        private static int UpdateDataTable(DataTable dt, string SQL)
        {
            CommonDB DB = MyDB.GetDBHelper();
            string ParaChar = GetDBParaChar(DB);
            SQL = SQL.Replace("@@", ParaChar);
            int count = 0;
            DB.BeginTransaction();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    IDataParameter[] paras = new IDataParameter[dt.Columns.Count];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        paras[i] = DB.GetParameter(ParaChar + dt.Columns[i].ColumnName, dr[i]);
                    }
                    count += DB.ExecuteNonQuery(SQL, CommandType.Text, paras);
                    if (DB.ErrorMessage != "")
                        throw new Exception(DB.ErrorMessage);
                }
                DB.Commit();
            }
            catch (Exception ex)
            {
                DB.Rollback();
                throw ex;
            }
            return count;

        }

        /// <summary>
        /// �Զ��������ݱ��е����ݵ����ݿ�
        /// <remarks>����ʱ�䣺2011.11.16</remarks>
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="insertSQL"></param>
        /// <param name="updateSQL"></param>
        /// <param name="DB"></param>
        /// <returns></returns>
        private static int SaveDataTable(DataTable dt, string insertSQL, string updateSQL, CommonDB DB)
        {
            //CommonDB DB = MyDB.GetDBHelper();
            string ParaChar = GetDBParaChar(DB);
            insertSQL = insertSQL.Replace("@@", ParaChar);
            updateSQL = updateSQL.Replace("@@", ParaChar);
            int count = 0;
            DB.BeginTransaction();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    IDataParameter[] paras = new IDataParameter[dt.Columns.Count];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        paras[i] = DB.GetParameter(ParaChar + dt.Columns[i].ColumnName, dr[i]);
                    }
                    //�ȸ��£����û�м�¼��Ӱ���ٴγ���ִ�в���
                    int tempCount = DB.ExecuteNonQuery(updateSQL, CommandType.Text, paras);
                    if (tempCount <= 0)
                        tempCount = DB.ExecuteNonQuery(insertSQL, CommandType.Text, paras);

                    count += tempCount;

                    if (DB.ErrorMessage != "")
                        throw new Exception(DB.ErrorMessage);
                }
                DB.Commit();
            }
            catch (Exception ex)
            {
                DB.Rollback();
                throw ex;
            }
            return count;
        }

        /// <summary>
        /// �������ݱ������ݷ��ʶ���
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="SQL"></param>
        /// <param name="DB"></param>
        /// <returns></returns>
        private int UpdateDataTable(DataTable dt, string SQL, CommonDB DB)
        {
            string ParaChar = GetDBParaChar(DB);
            SQL = SQL.Replace("@@", ParaChar);
            int count = 0;

            foreach (DataRow dr in dt.Rows)
            {
                IDataParameter[] paras = new IDataParameter[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    paras[i] = DB.GetParameter(ParaChar + dt.Columns[i].ColumnName, dr[i]);
                }
                count += DB.ExecuteNonQuery(SQL, CommandType.Text, paras);
                if (DB.ErrorMessage != "")
                    throw new Exception(DB.ErrorMessage);
            }

            return count;

        }


        /// <summary>
        /// Ϊ���ݱ����ɸ���SQL��䣬��������@@ǰ׺[����������]
        /// </summary>
        /// <param name="dt">���ݱ�</param>
        /// <returns></returns>
        private static string GetSqlUpdate(DataTable dt)
        {
            string sqlUpdate = "UPDATE " + dt.TableName + " SET ";
            if (dt.PrimaryKey.Length > 0)
            {
                DataColumn[] pks = dt.PrimaryKey;
                foreach (DataColumn dc in dt.Columns)
                {
                    bool isPk = false;
                    for (int i = 0; i < pks.Length; i++)
                        if (dc == pks[i])
                        {
                            isPk = true;
                            break;
                        }
                    //����������
                    if (!isPk)
                        sqlUpdate += dc.ColumnName + "=@@" + dc.ColumnName + ",";
                }
                sqlUpdate = sqlUpdate.TrimEnd(',') + " WHERE 1=1 ";
                foreach (DataColumn dc in dt.PrimaryKey)
                {
                    sqlUpdate += "And " + dc.ColumnName + "=@@" + dc.ColumnName + ",";
                }
                sqlUpdate = sqlUpdate.TrimEnd(',');
                return sqlUpdate;

            }
            else
            {
                throw new Exception("��" + dt.TableName + "û��ָ���������޷�����Update��䣡");
            }
        }

        /// <summary>
        /// Ϊ���ݱ����ɲ���SQL��䣬��������@@ǰ׺
        /// </summary>
        /// <param name="dt">���ݱ�</param>
        /// <returns></returns>
        private static string GetSqlInsert(DataTable dt)
        {
            string Items = "";
            string ItemValues = "";
            string sqlInsert = "INSERT INTO " + dt.TableName;

            foreach (DataColumn dc in dt.Columns)
            {
                Items += dc.ColumnName + ",";
                ItemValues += "@@" + dc.ColumnName + ",";
            }
            sqlInsert += "(" + Items.TrimEnd(',') + ") Values(" + ItemValues.TrimEnd(',') + ")";
            return sqlInsert;
        }

        #endregion

        public DBMSType DBMSType
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public SQLPage SQLPage
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

    }
}

