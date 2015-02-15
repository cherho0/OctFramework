/*
 * ========================================================================
 * Copyright(c) 2006-2010 PWMIS, All Rights Reserved.
 * Welcom use the PDF.NET (PWMIS Data Process Framework).
 * See more information,Please goto http://www.pwmis.com/sqlmap 
 * ========================================================================
 * ���������
 * 
 * ���ߣ���̫��     ʱ�䣺2008-10-12
 * �汾��V4.3
 * 
 * �޸��ߣ�         ʱ�䣺2012-4-11                
 * �޸�˵�����ڻ�ȡ�������ݵ�ʱ��,ʹ�� SCOPE_IDENTITY ����Ĭ�ϵķ�ʽ
 * ========================================================================
*/
using System;
using System.Data ;
using System.Data.SqlClient ;

namespace PWMIS.DataProvider.Data
{
	/// <summary>
	/// SqlServer ���ݴ���
	/// </summary>
	public sealed class SqlServer:AdoHelper
	{
		/// <summary>
		/// Ĭ�Ϲ��캯��
		/// </summary>
		public SqlServer()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        /// <summary>
        /// ��ȡ��ǰ���ݿ����͵�ö��
        /// </summary>
        public override PWMIS.Common.DBMSType CurrentDBMSType
        {
            get { return PWMIS.Common.DBMSType.SqlServer ; }
        }

		/// <summary>
		/// �������Ҵ����ݿ�����
		/// </summary>
		/// <returns>���ݿ�����</returns>
		protected override IDbConnection GetConnection()
		{
			IDbConnection conn=base.GetConnection ();
			if(conn==null)
			{
				conn=new SqlConnection (base.ConnectionString );
				//conn.Open ();
			}
			return conn;
		}

		/// <summary>
		/// ��ȡ����������ʵ��
		/// </summary>
		/// <returns>����������</returns>
		protected override IDbDataAdapter  GetDataAdapter(IDbCommand command)
		{
			IDbDataAdapter ada=new SqlDataAdapter ((SqlCommand )command);
			return ada;
		}

		/// <summary>
		/// ��ȡһ���²�������
		/// </summary>
		/// <returns>�ض�������Դ�Ĳ�������</returns>
		public override IDataParameter GetParameter()
		{
			return new SqlParameter ();
		}

		/// <summary>
		///  ��ȡһ���²�������
		/// </summary>
		/// <param name="paraName">������</param>
		/// <param name="dbType">������������</param>
		/// <param name="size">������С</param>
		/// <returns>�ض�������Դ�Ĳ�������</returns>
		public override IDataParameter GetParameter(string paraName,System.Data.DbType dbType,int size)
		{
			SqlParameter para=new SqlParameter();
			para.ParameterName=paraName;
			para.DbType=dbType;
			para.Size=size;
			return para;
		}

        public override string GetNativeDbTypeName(IDataParameter para)
        {
            return ((SqlParameter)para).SqlDbType.ToString();
        }

        /// <summary>
        /// ִ�в�ѯ,����ָ����(�������ݼܹ���)���ݼ����������
        /// </summary>
        /// <param name="SQL">��ѯ���</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��ѯ����</param>
        /// <param name="schemaDataSet">ָ����(�������ݼܹ���)���ݼ�</param>
        /// <returns>�������ݵ����ݼ�</returns>
        public override DataSet ExecuteDataSetWithSchema(string SQL, CommandType commandType, IDataParameter[] parameters, DataSet schemaDataSet)
        {
            IDbConnection conn = GetConnection();
            IDbCommand cmd = conn.CreateCommand();
            CompleteCommand(cmd, ref SQL, ref commandType, ref parameters);
            SqlDataAdapter ada = new SqlDataAdapter((SqlCommand)cmd);

            CommandLog cmdLog = new CommandLog(true);

            try
            {
                if (schemaDataSet.Tables.Count > 0)
                    ada.Fill(schemaDataSet, schemaDataSet.Tables[0].TableName);
                else
                    ada.Fill(schemaDataSet);
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
        /// ִ��ǿ���͵����ݼ���ѯ
        /// </summary>
        /// <param name="SQL">SQL���</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��ѯ����</param>
        /// <param name="schemaDataSet">ǿ���͵����ݼ�</param>
        /// <param name="tableName">Ҫ���ı�����</param>
        /// <returns></returns>
        public DataSet ExecuteTypedDataSet(string SQL, CommandType commandType, IDataParameter[] parameters, DataSet schemaDataSet, string tableName)
        {
            bool flag = false;
            for (int i = 0; i < schemaDataSet.Tables.Count; i++)
            {
                if (schemaDataSet.Tables[i].TableName == tableName)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                throw new ArgumentException("��ǿ���͵����ݼ��У�û���ҵ��ƶ������ݱ����ƣ�");

            IDbConnection conn = GetConnection();
            IDbCommand cmd = conn.CreateCommand();
            CompleteCommand(cmd, ref SQL, ref commandType, ref parameters);
            SqlDataAdapter ada = new SqlDataAdapter((SqlCommand)cmd);

            CommandLog cmdLog = new CommandLog(true);

            try
            {
                ada.Fill(schemaDataSet, tableName);
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
        /// ���ش� SqlConnection ������Դ�ļܹ���Ϣ��
        /// </summary>
        /// <param name="collectionName">��������</param>
        /// <param name="restrictionValues">����ļܹ���һ������ֵ</param>
        /// <returns>���ݿ�ܹ���Ϣ��</returns>
        public override DataTable GetSchema(string collectionName, string[] restrictionValues)
        {
            using (SqlConnection conn = (SqlConnection)this.GetConnection())
            {
                conn.Open();
                if (restrictionValues == null && string.IsNullOrEmpty(collectionName))
                    return conn.GetSchema();
                else if (restrictionValues == null && !string.IsNullOrEmpty(collectionName))
                    return conn.GetSchema(collectionName);
                else
                    return conn.GetSchema(collectionName, restrictionValues);
            }
            
        }

        /// <summary>
        /// ��ȡ�洢���̵Ķ�������
        /// </summary>
        /// <param name="spName">�洢��������</param>
        public override  string GetSPDetail(string spName)
        {
            string value = "";
            DataSet ds = this.ExecuteDataSet("sp_helptext", CommandType.StoredProcedure, 
                new IDataParameter[] { this.GetParameter("@objname", spName) });
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    value += dt.Rows[i][0].ToString();
                }
            }
            else
                value = "nothing";
            return value;
        }

        /// <summary>
        /// ��ȡ��ͼ���壬�������֧�֣���Ҫ����������д
        /// </summary>
        /// <param name="viewName">��ͼ����</param>
        /// <returns></returns>
        public override  string GetViweDetail(string viewName)
        {
            return GetSPDetail(viewName);
        }
		
		

        /// <summary>
        /// SQL��������
        /// </summary>
        /// <param name="sourceReader">����Դ��DataReader</param>
        /// <param name="connectionString">Ŀ�����ݿ�������ַ���</param>
        /// <param name="destinationTableName">Ҫ�����Ŀ�������</param>
        /// <param name="batchSize">ÿ����������Ĵ�С</param>
        public static void BulkCopy(IDataReader sourceReader,string connectionString, string destinationTableName,int batchSize)
        {
            // Ŀ�� 
            using (SqlConnection destinationConnection = new SqlConnection(connectionString))
            {
                // ������ 
                destinationConnection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                {
                    bulkCopy.BatchSize = batchSize;

                    bulkCopy.DestinationTableName = destinationTableName;
                    bulkCopy.WriteToServer(sourceReader);
                }
            }
            sourceReader.Close();
        }

        /// <summary>
        /// SQL��������
        /// </summary>
        /// <param name="sourceTable">����Դ��</param>
        /// <param name="connectionString">Ŀ�����ݿ�������ַ���</param>
        /// <param name="destinationTableName">Ҫ�����Ŀ�������</param>
        /// <param name="batchSize">ÿ����������Ĵ�С</param>
        public static void BulkCopy(DataTable sourceTable, string connectionString, string destinationTableName, int batchSize)
        {
            using (SqlConnection destinationConnection = new SqlConnection(connectionString))
            {
                // ������ 
                destinationConnection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                {
                    
                    bulkCopy.BatchSize = batchSize;
                    
                    bulkCopy.DestinationTableName = destinationTableName;
                    bulkCopy.WriteToServer(sourceTable);
                }
            }
        }


        /// <summary>
        /// SqlServer ִ�в������ݵĲ�ѯ�����ִ�гɹ�����Ӱ�������ֻ�᷵��1
        /// </summary>
        /// <param name="SQL">�������ݵ�SQL</param>
        /// <param name="commandType">��������</param>
        /// <param name="parameters">��������</param>
        /// <param name="ID">Ҫ�����ı��β������²��������е�����IDֵ</param>
        /// <returns>���β�ѯ��Ӱ�������</returns>
        public  override int ExecuteInsertQuery(string SQL, CommandType commandType, IDataParameter[] parameters, ref object ID)
        {
            IDbConnection conn = GetConnection();
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
                cmd.CommandText = SQL + " ;SELECT SCOPE_IDENTITY();";
               
                ID = cmd.ExecuteScalar();
                //������ڲ��������������ύ���񣬷����ⲿ�����߾�����ʱ�ύ����
                result = 1;

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
                if (cmd.Transaction == null && conn.State == ConnectionState.Open)
                    conn.Close();
            }

            long _elapsedMilliseconds;
            cmdLog.WriteLog(cmd, "AdoHelper", out _elapsedMilliseconds);
            base.ElapsedMilliseconds = _elapsedMilliseconds;
            return result;
        }

        public override System.Data.Common.DbConnectionStringBuilder ConnectionStringBuilder
        {
            get { return new SqlConnectionStringBuilder(this.ConnectionString); }
        }

        public override string ConnectionUserID
        {
            get { return ((SqlConnectionStringBuilder)ConnectionStringBuilder).UserID; }
        }
	}
}
