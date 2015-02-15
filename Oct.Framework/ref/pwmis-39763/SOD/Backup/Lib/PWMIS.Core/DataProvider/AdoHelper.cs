/*
 * ========================================================================
 * Copyright(c) 2006-2010 PWMIS, All Rights Reserved.
 * Welcom use the PDF.NET (PWMIS Data Process Framework).
 * See more information,Please goto http://www.pwmis.com/sqlmap 
 * ========================================================================
 * ���������
 * 
 * ���ߣ���̫��     ʱ�䣺2008-10-12
 * �汾��V3.0
 * 
 * �޸��ߣ�         ʱ�䣺2013-1-13                
 * �޸�˵����֧�ֶ�д���룬��ϸ������˵��
 * 
 *  * �޸��ߣ�         ʱ�䣺2013-5-19                
 * �޸�˵����֧��SQL ��ʽ���ƴ���֧��ֱ�ӻ�ý�������б�
 * ע�ⲻ�ܽ�Format��ͷ�ļ����������������������صķ�������лGIV-˳�¡����ִ����⡣
 * ========================================================================
*/
using System;
using System.Data;
using System.Collections.Generic;
using PWMIS.Core;
using PWMIS.DataProvider.Adapter;
namespace PWMIS.DataProvider.Data
{
	/// <summary>
    /// �������ݷ��ʳ����� ���� AdoHelper �� ,ʵ��ʹ�÷����μ� PWMIS.CommonDataProvider.Adapter.MyDB
	/// </summary>
	public abstract class AdoHelper:CommonDB
	{
        public delegate TResult Func<T,TResult>(T arg);

		/// <summary>
		/// Ĭ�Ϲ��캯��
		/// </summary>
		public AdoHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        /// <summary>
        /// ����Ӧ�ó��������ļ���connectionStrings �����е�name���������ݷ��ʶ���
        /// </summary>
        /// <param name="connectionName">�����ַ��������������</param>
        /// <returns>���ݷ��ʶ���</returns>
        public static AdoHelper CreateHelper(string connectionName)
        {
            return MyDB.GetDBHelperByConnectionName(connectionName);
        }


		/// <summary>
		/// �����������ݷ������ʵ��
		/// </summary>
		/// <param name="providerAssembly">�ṩ���������</param>
		/// <param name="providerType">�ṩ������</param>
		/// <returns></returns>
		public static AdoHelper  CreateHelper(string providerAssembly, string providerType)
		{
			return (AdoHelper)CommonDB.CreateInstance(providerAssembly,providerType);
		}

		/// <summary>
		/// ִ�в�����ֵ�Ĳ�ѯ
		/// </summary>
		/// <param name="connectionString">�����ַ���</param>
		/// <param name="commandType">��������</param>
		/// <param name="SQL">SQL</param>
		/// <returns>��Ӱ�������</returns>
		public virtual int ExecuteNonQuery(string connectionString,CommandType commandType,string SQL)
		{
			base.ConnectionString=connectionString;
			return base.ExecuteNonQuery(SQL,commandType,null);
		}

		/// <summary>
		/// ִ�в�����ֵ�Ĳ�ѯ
		/// </summary>
		/// <param name="connectionString">�����ַ���</param>
		/// <param name="commandType">��������</param>
		/// <param name="SQL">SQL</param>
		/// <param name="parameters">��������</param>
		/// <returns>��Ӱ�������</returns>
        public virtual int ExecuteNonQuery(string connectionString, CommandType commandType, string SQL, IDataParameter[] parameters)
		{
			base.DataWriteConnectionString=connectionString;
			return base.ExecuteNonQuery(SQL,commandType,parameters);
		}

		/// <summary>
		/// ִ�������Ķ�����ѯ
		/// </summary>
		/// <param name="connectionString">�����ַ���</param>
		/// <param name="commandType">��������</param>
		/// <param name="SQL">SQL</param>
		/// <param name="parameters">��������</param>
		/// <returns>�����Ķ���</returns>
		public IDataReader ExecuteReader(string connectionString,CommandType commandType,string SQL,IDataParameter[] parameters)
		{
			base.ConnectionString=connectionString;
			return base.ExecuteDataReader(SQL,commandType,parameters);
		}

		/// <summary>
		/// ִ�з������ݼ��Ĳ�ѯ
		/// </summary>
		/// <param name="connectionString">�����ַ���</param>
		/// <param name="commandType">��������</param>
		/// <param name="SQL">SQL</param>
		/// <param name="parameters">��������</param>
		/// <returns>���ݼ�</returns>
		public DataSet ExecuteDataSet(string connectionString,CommandType commandType,string SQL,IDataParameter[] parameters)
		{
			base.ConnectionString=connectionString;
			return base.ExecuteDataSet(SQL,commandType,parameters);
		}

        /// <summary>
        /// ִ�з������ݼ��Ĳ�ѯ
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="commandType">��������</param>
        /// <param name="SQL">SQL</param>
        /// <returns>���ݼ�</returns>
        public DataSet ExecuteDataSet(string connectionString, CommandType commandType, string SQL)
        {
            base.ConnectionString = connectionString;
            return base.ExecuteDataSet(SQL, commandType, null);
        }

	
		/// <summary>
		/// ִ�з��ص�һֵ�ò�ѯ
		/// </summary>
		/// <param name="connectionString">�����ַ���</param>
		/// <param name="commandType">��������</param>
		/// <param name="SQL">SQL</param>
		/// <param name="parameters">��������</param>
		/// <returns>���</returns>
		public object ExecuteScalar(string connectionString,CommandType commandType,string SQL,IDataParameter[] parameters)
		{
			base.ConnectionString=connectionString;
			return base.ExecuteScalar (SQL,commandType,parameters);
        }

        #region ֱ�Ӳ�������ѯ��չ
        /// <summary>
        /// ִ�в����ؽ�����Ĳ�ѯ��
        /// �����ַ������͵Ĳ��������ָ�������ĳ��ȣ���ʽ������{0:50}��
        /// ����Decimal���ͣ����ָ�����Ⱥ�С��λ������{0:8.3}����ʾ����Ϊ8��С��λΪ3
        /// </summary>
        /// <param name="sqlFormat">����ʽ�Ĳ�ѯ�����ַ���������SELECT * FROM TABLE1 WHERE CLASSID={0} AND CLASSNAME={1:50} PRICE={2:8.3}</param>
        /// <param name="parameters">Ҫ�滻�Ĳ���</param>
        /// <returns>��ѯ��Ӱ�������</returns>
        public int FormatExecuteNonQuery(string sqlFormat, params object[] parameters)
        {
            DataParameterFormat formater = new DataParameterFormat(this);
            string sql = string.Format(formater,sqlFormat, parameters);
            return base.ExecuteNonQuery(sql,CommandType.Text,formater.DataParameters);
        }
        /// <summary>
        /// ִ�з��������Ķ����Ĳ�ѯ
        /// �����ַ������͵Ĳ��������ָ�������ĳ��ȣ���ʽ������{0:50}��
        /// ����Decimal���ͣ����ָ�����Ⱥ�С��λ������{0:8.3}����ʾ����Ϊ8��С��λΪ3
        /// </summary>
        /// <param name="sqlFormat">����ʽ�Ĳ�ѯ�����ַ���������SELECT * FROM TABLE1 WHERE CLASSID={0} AND CLASSNAME={1:50} PRICE={2:8.3}</param>
        /// <param name="parameters">Ҫ�滻�Ĳ���</param>
        /// <returns>�����Ķ���</returns>
        public IDataReader FormatExecuteDataReader(string sqlFormat, params object[] parameters)
        {
            if (parameters == null)
            {
                return base.ExecuteDataReader(sqlFormat);
            }
            else
            {
                DataParameterFormat formater = new DataParameterFormat(this);
                string sql = string.Format(formater, sqlFormat, parameters);
                return base.ExecuteDataReader(sql, CommandType.Text, formater.DataParameters);
            }
        }
        /// <summary>
        /// ִ�з������ݼ��Ĳ�ѯ
        /// �����ַ������͵Ĳ��������ָ�������ĳ��ȣ���ʽ������{0:50}��
        /// ����Decimal���ͣ����ָ�����Ⱥ�С��λ������{0:8.3}����ʾ����Ϊ8��С��λΪ3
        /// </summary>
        /// <param name="sqlFormat">����ʽ�Ĳ�ѯ�����ַ���������SELECT * FROM TABLE1 WHERE CLASSID={0} AND CLASSNAME={1:50} PRICE={2:8.3}</param>
        /// <param name="parameters">Ҫ�滻�Ĳ���</param>
        /// <returns>���ݼ�</returns>
        public DataSet FormatExecuteDataSet(string sqlFormat, params object[] parameters)
        {
            DataParameterFormat formater = new DataParameterFormat(this);
            string sql = string.Format(formater, sqlFormat, parameters);
            return base.ExecuteDataSet(sql, CommandType.Text, formater.DataParameters);
        }
        /// <summary>
        /// ִ�з��ص�ֵ����Ĳ�ѯ
        /// �����ַ������͵Ĳ��������ָ�������ĳ��ȣ���ʽ������{0:50}��
        /// ����Decimal���ͣ����ָ�����Ⱥ�С��λ������{0:8.3}����ʾ����Ϊ8��С��λΪ3
        /// </summary>
        /// <param name="sqlFormat">����ʽ�Ĳ�ѯ�����ַ���������SELECT * FROM TABLE1 WHERE CLASSID={0} AND CLASSNAME={1:50} PRICE={2:8.3}</param>
        /// <param name="parameters">Ҫ�滻�Ĳ���</param>
        /// <returns>���صĵ�ֵ���</returns>
        public object FormatExecuteScalar(string sqlFormat, params object[] parameters)
        {
            DataParameterFormat formater = new DataParameterFormat(this);
            string sql = string.Format(formater, sqlFormat, parameters);
            return base.ExecuteScalar(sql, CommandType.Text, formater.DataParameters);
        }
        #endregion

        /// <summary>
        /// ���ݲ�ѯ����ȡ�����б�
        /// <example>
        /// <code>
        /// <![CDATA[
        /// AdoHelper dbLocal = new SqlServer();
        /// dbLocal.ConnectionString = "Data Source=.;Initial Catalog=LocalDB;Integrated Security=True";
        /// var dataList = dbLocal.GetList(reader =>
        /// {
        ///     return new
        ///    {
        ///        UID=reader.GetInt32(0),
        ///        Name=reader.GetString(1)
        ///    };
        /// }, "SELECT UID,Name FROM Table_User WHERE Sex={0} And Height>={1:5.2}",1, 1.60M);
        /// 
        /// ]]>
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="TResult">���صĶ��������</typeparam>
        /// <param name="fun">Ҫ���������Ķ����ľ��巽�����÷���������һ������</param>
        /// <param name="sqlFormat">SQL ��ʽ�������</param>
        /// <param name="parameters">�����滻�Ĳ���</param>
        /// <returns>�����б�</returns>
        public IList<TResult> GetList<TResult>(Func<IDataReader, TResult> fun, string sqlFormat, params object[] parameters) where TResult : class
        {
            List<TResult> resultList = new List<TResult>();
            using (IDataReader reader = FormatExecuteDataReader(sqlFormat, parameters))
            {
                while (reader.Read())
                {
                    TResult t = fun(reader);
                    resultList.Add(t);
                }
            }
            return resultList;
        }

        public static List<T> QueryList<T>(IDataReader reader) where T : class, new()
        {
            List<T> list = new List<T>();
            using (reader)
            {
                if (reader.Read())
                {
                    int fcount = reader.FieldCount;
                    INamedMemberAccessor[] accessors = new INamedMemberAccessor[fcount];
                    DelegatedReflectionMemberAccessor drm = new DelegatedReflectionMemberAccessor();
                    for (int i = 0; i < fcount; i++)
                    {
                        accessors[i] = drm.FindAccessor<T>(reader.GetName(i));
                    }

                    do
                    {
                        T t = new T();
                        for (int i = 0; i < fcount; i++)
                        {
                            if (!reader.IsDBNull(i))
                                accessors[i].SetValue(t, reader.GetValue(i));
                        }
                        list.Add(t);
                    } while (reader.Read());
                }
            }
            return list;
        }
    }
}
