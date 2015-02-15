/*
 * ========================================================================
 * Copyright(c) 2006-2010 PWMIS, All Rights Reserved.
 * Welcom use the PDF.NET (PWMIS Data Process Framework).
 * See more information,Please goto http://www.pwmis.com/sqlmap 
 * ========================================================================
 * ���������
 * 
 * ���ߣ���̫��     ʱ�䣺2008-10-12
 * �汾��V5.1
 * 
 * �޸��ߣ�         ʱ�䣺2013-3-5                
 * �޸�˵����Oracle��ҳ��һ�����⣬��һ����¼�����Ѵ�󱦷��֣�
 * 
 * �޸��ߣ�         ʱ�䣺2014-5-6                
 * �޸�˵����Oracle��ҳ��ʱ��ʹ��strWhereUpper ���жϣ������д���⣬
 * Oracle �ԵĴ�д���ֶ��в�ͬ�����壨���Ѵ�󱦷��֣�
 * 
 * ========================================================================
*/
//**************************************************************************
//	�� �� ����  BFService
//	��	  ;��  SQL SERVER ��ҳ�������
//	�� �� �ˣ�  ��̫��
//  �������ڣ�  2006.7.26
//	�� �� �ţ�	V4.6.2.0411
//	�޸ļ�¼��  ��̫�� 2008.3.30 �޸��˶����ѯ(��@@Where������֧��)���������Bug
//              ��̫�� 2013.3.26 �޸�ʹ��SqlServer��Access ��SQL�����ʹ��
//                               Distinct ��ҳ������
//              ����[����] 2013.4.11 �޸�MySQL��ҳ����1ҳ֮�������
//**************************************************************************
/*�򵥲�ѯ��ҳ���� �㷨˵��
 * SQL SERVER ��
		 * ����������򵥵Ĳ�ѯ��䣺
		 * SELECT SelectFields FROM TABLE @@Where ORDER BY OrderFields DESC
		 * ��ô��ҳ�������Բ�������ķ�ʽ��
		 * 
		 * ��һҳ��
		 * SELECT TOP @@PageSize SelectFields FROM TABLE @@Where ORDER BY OrderFields DESC;
		 * �м�ҳ��
		 * SELECT Top @@PageSize * FROM
			             (SELECT Top @@PageSize * FROM
				           (
                             SELECT Top @@Page_Size_Number
							 SelectFields FROM TABLE @@Where ORDER BY OrderFields DESC
						   ) P_T0 ORDER BY OrderFields ASC
						 ) P_T1 ORDER BY OrderFields DESC
		 * ���ҳ��
		 * SELECT * FROM (	 
	                      Select Top @@LeftSize 
						  SelectFields FROM TABLE @@Where ORDER BY OrderFields ASC
						 ) ORDER BY OrderFields DESC
						 
						  
		 * ���� MakePageSQLStringByDBNAME �ڴ˻�����ʵ���˸�Ϊ���ӵķ�ҳ��������ĸ���ʱ˵��ѯ
		 * �����������Ӳ�ѯ�������Ӳ�ѯ��������۲�ѯ��������������ı�׼��
		 * 
		 * ֻ����һ�� SELECT ν�ʣ�
		 * û�� INNER JOIN��RIGHT JOIN��LEFT JOIN �ȱ�����ν�ʣ�
		 * ν�� FROM ��ֻ����һ��������
		 * 
		 * ������Ϊ�ò�ѯΪһ�����Ӳ�ѯ�����ø��Ӳ�ѯ��ҳ������
Oracle ��
�����ķ�ҳԭ������Oracle��ָ�� rownum α�У�����һ���������У���������Order by ֮ǰ���ɣ�ͨ��
��������ķ�ҳ��䣺
select * from
 (select rownum r_n,temptable.* from  
   ( @@SourceSQL ) temptable
 ) temptable2 where r_n between @@RecStart  and @@RecEnd
���У�
@@SourceSQL :��ǰ���⸴�ӵ�SQL���
@@RecStart:��¼��ʼ�ĵ㣬���� ((tCurPage -1) * tPageSize +1) 
@@RecEnd  :��¼�����ĵ㣬���� (tCurPage * tPageSize) 
		 

** Լ����
ʹ�ø÷�ҳ����Ҫ�� SQL��䱾�������������������
1�������Ĳ�ѯ���ܺ��� TOP ν��(��ò�Ҫʹ��TOP�����Ա���Oracle �����ݵ�����)��
2��������ѯ���뺬�� ORDER BY ��䣨Oracle���⣩��
3�����ܺ��������滻����(���ִ�Сд)��@@PageSize,@@Page_Size_Number,@@LeftSize,@@Where
4��SQL������� SQL-92 ���ϱ�׼���� �����ORDER BY ���֮������������䣬
5�����ʹ��SQLSERVER ��������ݿ�ϵͳ������Web.config���ý�����ע�� EngineType ��ֵ��
Group by �ȷ���Order by ֮ǰ��
**
 *
 * =====================������Ч��ҳ��ʽ=========================================
 * �������ݿ�Ĳ�ѯ�Ż�����ҳ�㷨���� http://edu.codepub.com/2009/0522/4437_3.php
 * 
 * ����֪���������κ��ֶΣ����Ƕ�����ͨ��max(�ֶ�)��min(�ֶ�)����ȡĳ���ֶ��е�������Сֵ�������������ֶβ��ظ�����ô�Ϳ���������Щ���ظ����ֶε�max��min��Ϊ��ˮ�룬ʹ���Ϊ��ҳ�㷨�зֿ�ÿҳ�Ĳ������������ǿ����ò�������>����<������������ʹ����ʹ��ѯ������SARG��ʽ���磺
Select top 10 * from table1 where id>200
�������Ǿ��������·�ҳ������
select top ҳ��С *
from table1
where id>
      (select max (id) from
      (select top ((ҳ��-1)*ҳ��С) id from table1 order by id) as T
       )    
  order by id

		 */
using System;

namespace PWMIS.Common
{
    /// <summary>
    /// SQL SERVER ��ҳ�����Զ�ʶ���׼SQL��䲢�����ʺϷ�ҳ��SQL���
    /// </summary>
    public class SQLPage
    {
        //public static string DBType=System.Configuration.ConfigurationSettings .AppSettings ["EngineType"];
        public static DBMSType DbmsType = DBMSType.SqlServer;

        public SQLPage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// ����SQL��ҳ��䣬��¼����Ϊ0��ʾ����ͳ�����
        /// </summary>
        /// <param name="dbmsType">���ݿ�����</param>
        /// <param name="strSQLInfo">ԭʼSQL���</param>
        /// <param name="strWhere">�ڷ�ҳǰҪ�滻���ַ��������ڷ�ҳǰ��ɸѡ</param>
        /// <param name="PageSize">ҳ��С</param>
        /// <param name="PageNumber">ҳ��</param>
        /// <param name="AllCount">��¼�����������0������ͳ�Ƽ�¼�����Ĳ�ѯ</param>
        /// <returns>����SQL��ҳ���</returns>
        public static string MakeSQLStringByPage(DBMSType dbmsType, string strSQLInfo, string strWhere, int PageSize, int PageNumber, int AllCount)
        {
            //���ݲ�ͬ�����ݿ�������ò�ͬ������
            string SQL = string.Empty;
            switch (dbmsType)
            {
                case DBMSType.Access:
                case DBMSType.SqlServer:
                    SQL = MakePageSQLStringByMSSQL(strSQLInfo, strWhere, PageSize, PageNumber, AllCount);
                    break;
                case DBMSType.Oracle:
                    SQL = MakePageSQLStringByOracle(strSQLInfo, strWhere, PageSize, PageNumber, AllCount);
                    break;
                case DBMSType.MySql:
                case DBMSType.SQLite:
                    SQL = MakePageSQLStringByMySQL(strSQLInfo, strWhere, PageSize, PageNumber, AllCount);
                    break;
                case DBMSType.PostgreSQL:
                    SQL = MakePageSQLStringByPostgreSQL(strSQLInfo, strWhere, PageSize, PageNumber, AllCount);
                    break;
                default:
                    //SQL = MakePageSQLStringByMSSQL(strSQLInfo, strWhere, PageSize, PageNumber, AllCount);
                    //SQL = strSQLInfo;
                    //break;
                    throw new Exception("��ҳ���󣺲�֧�ִ������͵����ݿ��ҳ��");
            }
            return SQL;
        }

        /// <summary>
        /// ����SQL��ҳ��䣬��¼����Ϊ0��ʾ����ͳ�����
        /// </summary>
        /// <param name="strSQLInfo">ԭʼSQL���</param>
        /// <param name="strWhere">�ڷ�ҳǰҪ�滻���ַ��������ڷ�ҳǰ��ɸѡ</param>
        /// <param name="PageSize">ҳ��С</param>
        /// <param name="PageNumber">ҳ��</param>
        /// <param name="AllCount">��¼�����������0������ͳ�Ƽ�¼�����Ĳ�ѯ</param>
        /// <returns>����SQL��ҳ���</returns>
        public static string MakeSQLStringByPage(string strSQLInfo, string strWhere, int PageSize, int PageNumber, int AllCount)
        {
            return MakeSQLStringByPage(SQLPage.DbmsType, strSQLInfo, strWhere, PageSize, PageNumber, AllCount);
        }

        /// <summary>
        /// MS SQLSERVER ��ҳSQL�����������ͬ��������ACCESS���ݿ�
        /// </summary>
        /// <param name="strSQLInfo">ԭʼSQL���</param>
        /// <param name="strWhere">�ڷ�ҳǰҪ�滻���ַ��������ڷ�ҳǰ��ɸѡ</param>
        /// <param name="PageSize">ҳ��С</param>
        /// <param name="PageNumber">ҳ��</param>
        /// <param name="AllCount">��¼����</param>
        /// <returns>����SQL��ҳ���</returns>
        private static string MakePageSQLStringByMSSQL(string strSQLInfo, string strWhere, int PageSize, int PageNumber, int AllCount)
        {
            #region ��ҳλ�÷���
            string strSQLType = string.Empty;
            if (AllCount > 0)
            {
                if (PageNumber == 1) //��ҳ
                {
                    strSQLType = "First";
                }
                else if (PageSize * PageNumber > AllCount) //����ҳ @@LeftSize
                {
                    PageSize = AllCount - PageSize * (PageNumber - 1);
                    strSQLType = "Last";
                }
                else //�м�ҳ
                {
                    strSQLType = "Mid";
                }
            }
            else if (AllCount < 0) //���⴦�� dth,2006.10.19
            {
                strSQLType = "First";
            }
            else
            {
                strSQLType = "Count";
            }

            #endregion

            #region SQL ���Ӷȷ���
            //SQL ���Ӷȷ��� ��ʼ
            bool SqlFlag = true;//��SQL���
            string TestSQL = strSQLInfo.ToUpper();
            int n = TestSQL.IndexOf("SELECT ", 0);
            n = TestSQL.IndexOf("SELECT ", n + 7);
            if (n == -1)
            {
                //�����Ǽ򵥵Ĳ�ѯ���ٴδ���
                n = TestSQL.IndexOf(" JOIN ", n + 7);
                if (n != -1) SqlFlag = false;
                else
                {
                    //�ж�From ν�����
                    n = TestSQL.IndexOf("FROM ", 9);
                    if (n == -1) return "";
                    //���� WHERE ν�ʵ�λ��
                    int m = TestSQL.IndexOf("WHERE ", n + 5);
                    // ���û��WHERE ν��
                    if (m == -1) m = TestSQL.IndexOf("ORDER BY ", n + 5);
                    //���û��ORDER BY ν�ʣ���ô�޷������˳���
                    if (m == -1)
                        throw new Exception("��ѯ����������ǰû��Ϊ��ҳ��ѯָ�������ֶΣ����ʵ��޸�SQL��䡣\n" + strSQLInfo);
                    string strTableName = TestSQL.Substring(n, m - n);
                    //�������� , �ű�ʾ�Ƕ���ѯ
                    if (strTableName.IndexOf(",") != -1)
                        SqlFlag = false;
                }
            }
            else
            {
                //���Ӳ�ѯ��
                SqlFlag = false;
            }
            //SQL ���Ӷȷ��� ����
            #endregion

            #region �����﷨����
            //�����﷨���� ��ʼ
            int iOrderAt = strSQLInfo.ToLower().LastIndexOf("order by ");
            //���û��ORDER BY ν�ʣ���ô�޷������ҳ���˳���
            if (iOrderAt == -1)
            {
                if (PageNumber == 1)
                {
                    string sqlTemp = "Select Top @@PageSize * FROM ( " + strSQLInfo +
                           " ) P_T0 @@Where ";
                    return sqlTemp.Replace("@@PageSize", PageSize.ToString()).Replace("@@Where", strWhere);
                }
                else
                {
                    throw new Exception("��ѯ����������ǰû��Ϊ��ҳ��ѯָ�������ֶΣ����ʵ��޸�SQL��䡣\n" + strSQLInfo);
                }
            }

            string strOrder = strSQLInfo.Substring(iOrderAt + 9);
            strSQLInfo = strSQLInfo.Substring(0, iOrderAt);
            string[] strArrOrder = strOrder.Split(new char[] { ',' });
            for (int i = 0; i < strArrOrder.Length; i++)
            {
                string[] strArrTemp = (strArrOrder[i].Trim() + " ").Split(new char[] { ' ' });
                //ѹ������ո�
                for (int j = 1; j < strArrTemp.Length; j++)
                {
                    if (strArrTemp[j].Trim() == "")
                    {
                        continue;
                    }
                    else
                    {
                        strArrTemp[1] = strArrTemp[j];
                        if (j > 1) strArrTemp[j] = "";
                        break;
                    }
                }
                //�ж��ֶε���������
                switch (strArrTemp[1].Trim().ToUpper())
                {
                    case "DESC":
                        strArrTemp[1] = "ASC";
                        break;
                    case "ASC":
                        strArrTemp[1] = "DESC";
                        break;
                    default:
                        //δָ���������ͣ�Ĭ��Ϊ����
                        strArrTemp[1] = "DESC";
                        break;
                }
                //���������ֶζ����޶���
                if (strArrTemp[0].IndexOf(".") != -1)
                    strArrTemp[0] = strArrTemp[0].Substring(strArrTemp[0].IndexOf(".") + 1);
                strArrOrder[i] = string.Join(" ", strArrTemp);

            }
            //���ɷ����������
            string strNewOrder = string.Join(",", strArrOrder).Trim();
            strOrder = strNewOrder.Replace("ASC", "ASC0").Replace("DESC", "ASC").Replace("ASC0", "DESC");
            //�����﷨��������
            #endregion

            #region �����ҳ��ѯ
            string SQL = string.Empty;
            if (!SqlFlag)
            {
                //���Ӳ�ѯ����
                switch (strSQLType.ToUpper())
                {
                    case "FIRST":
                        SQL = "Select Top @@PageSize * FROM ( " + strSQLInfo +
                            " ) P_T0 @@Where ORDER BY " + strOrder;
                        break;
                    case "MID":
                        SQL = @"SELECT Top @@PageSize * FROM
                         (SELECT Top @@PageSize * FROM
                           (
                             SELECT Top @@Page_Size_Number * FROM (";
                        SQL += " " + strSQLInfo + " ) P_T0 @@Where ORDER BY " + strOrder + " ";
                        SQL += @") P_T1
            ORDER BY " + strNewOrder + ") P_T2  " +
                            "ORDER BY " + strOrder;
                        break;
                    case "LAST":
                        SQL = @"SELECT * FROM (     
                          Select Top @@LeftSize * FROM (" + " " + strSQLInfo + " ";
                        SQL += " ) P_T0 @@Where ORDER BY " + strNewOrder + " " +
                            " ) P_T1 ORDER BY " + strOrder;
                        break;
                    case "COUNT":
                        SQL = "Select COUNT(*) FROM ( " + strSQLInfo + " ) P_Count @@Where";
                        break;
                    default:
                        SQL = strSQLInfo + strOrder;//��ԭ
                        break;
                }

            }
            else
            {
                //�򵥲�ѯ����
                string strUpperSQLInfo = strSQLInfo.ToUpper();
                bool isDistinct = strUpperSQLInfo.IndexOf("DISTINCT") >= 7;
                if (isDistinct)
                    strUpperSQLInfo = strUpperSQLInfo.Replace("DISTINCT", "");
                switch (strSQLType.ToUpper())
                {
                    case "FIRST":
                        if(isDistinct)
                            SQL = strUpperSQLInfo.Replace("DISTINCT", "DISTINCT TOP @@PageSize");
                        else
                            SQL = strUpperSQLInfo.Replace("SELECT ", "SELECT TOP @@PageSize ");
                        SQL += "  @@Where ORDER BY " + strOrder;
                        break;
                    case "MID":
                        string strRep = @"SELECT Top @@PageSize * FROM
                         (SELECT Top @@PageSize * FROM
                           (
                             SELECT Top @@Page_Size_Number  ";
                        if (isDistinct)
                            strRep = strRep.Replace("SELECT Top @@Page_Size_Number ", "SELECT DISTINCT Top @@Page_Size_Number");

                        SQL = strUpperSQLInfo.Replace("SELECT ", strRep);
                        SQL += "  @@Where ORDER BY " + strOrder;
                        SQL += "  ) P_T0 ORDER BY " + strNewOrder + " " +
                            " ) P_T1 ORDER BY " + strOrder;
                        break;
                    case "LAST":
                        string strRep2 = @"SELECT * FROM (     
                          Select Top @@LeftSize ";
                        if (isDistinct)
                            strRep2 = strRep2.Replace("Select Top @@LeftSize", "Select DISTINCT Top @@LeftSize");
                        SQL = strUpperSQLInfo.Replace("SELECT ", strRep2);
                        SQL += " @@Where ORDER BY " + strNewOrder + " " +
                            " ) P_T1 ORDER BY " + strOrder;
                        break;
                    case "COUNT":
                        SQL = "Select COUNT(*) FROM ( " + strSQLInfo + " @@Where) P_Count ";//edit 2008.3.29
                        break;
                    default:
                        SQL = strSQLInfo + strOrder;//��ԭ
                        break;
                }
            }

            //ִ�з�ҳ�����滻
            if (PageSize < 0) PageSize = 0;
            SQL = SQL.Replace("@@PageSize", PageSize.ToString())
                .Replace("@@Page_Size_Number", Convert.ToString(PageSize * PageNumber))
                .Replace("@@LeftSize", PageSize.ToString());//
            //.Replace ("@@Where",strWhere);
            //����û��Ķ�����������
            if (strWhere == null) strWhere = "";
            if (strWhere != "" && strWhere.ToUpper().Trim().StartsWith("WHERE "))
            {
                throw new Exception("��ҳ�����ѯ�������ܴ�Whereν�ʣ�");
            }
            if (!SqlFlag)
            {
                if (strWhere != "") strWhere = " Where " + strWhere;
                SQL = SQL.Replace("@@Where", strWhere);
            }
            else
            {
                if (strWhere != "") strWhere = " And (" + strWhere + ")";
                SQL = SQL.Replace("@@Where", strWhere);
            }
            return SQL;
            #endregion

        }


        /// <summary>
        /// Oracle ��ҳSQL���������
        /// </summary>
        /// <param name="strSQLInfo">ԭʼSQL���</param>
        /// <param name="strWhere">�ڷ�ҳǰҪ�滻���ַ��������ڷ�ҳǰ��ɸѡ</param>
        /// <param name="PageSize">ҳ��С</param>
        /// <param name="PageNumber">ҳ��</param>
        /// <param name="AllCount">��¼����</param>
        /// <returns>����SQL��ҳ���</returns>
        private static string MakePageSQLStringByOracle(string strSQLInfo, string strWhere, int PageSize, int PageNumber, int AllCount)
        {
            if (strWhere != null && strWhere != "")
            {
                //ʹ��strWhereUpper ���жϣ������д���⣬Oracle �ԵĴ�д���ֶ��в�ͬ�����塣
                //��л���� ��� ���ִ����� 2014.5.6
                string  strWhereUpper = strWhere.Trim().ToUpper();
                if (strWhereUpper.StartsWith("WHERE "))
                    throw new Exception("���Ӳ�ѯ�������ܴ� where ν��");
                if (strWhereUpper.IndexOf(" ORDER BY ") > 0)
                    throw new Exception("���Ӳ�ѯ�������ܴ� ORDER BY ν��");
                strSQLInfo = "SELECT * FROM (" + strSQLInfo + ") temptable0 WHERE " + strWhere;
            }
            if (AllCount == 0)
            {
                //����ͳ����䡡
                return "select count(*) from (" + strSQLInfo + ") P_Count  " 
                    + (string.IsNullOrEmpty( strWhere)?"":"WHERE "+strWhere);
            }
            //��ҳ�������

            string SqlTemplate = @"SELECT * FROM
 (SELECT rownum r_n,temptable.* FROM  
   ( @@SourceSQL ) temptable Where rownum <= @@RecEnd
 ) temptable2 WHERE r_n >= @@RecStart ";

            int iRecStart = (PageNumber - 1) * PageSize + 1;
            int iRecEnd = PageNumber * PageSize;

            //ִ�в����滻
            string SQL = SqlTemplate.Replace("@@SourceSQL", strSQLInfo)
                .Replace("@@RecStart", iRecStart.ToString())
                .Replace("@@RecEnd", iRecEnd.ToString());
            return SQL;
        }

        private static string MakePageSQLStringByMySQL_PgSQL(string strSQLInfo, string strWhere, int PageSize, int PageNumber, int AllCount, string offsetString)
        {
            strSQLInfo = strSQLInfo.Trim();
            //ȥ��ĩβ�ķֺ�
            if (strSQLInfo.EndsWith(";"))
                strSQLInfo = strSQLInfo.TrimEnd(';');
            if (strWhere != null && strWhere != "")
            {
                strWhere = strWhere.Trim().ToUpper();
                if (strWhere.StartsWith("WHERE "))
                    throw new Exception("���Ӳ�ѯ�������ܴ� where ν��");
                if (strWhere.IndexOf(" ORDER BY ") > 0)
                    throw new Exception("���Ӳ�ѯ�������ܴ� ORDER BY ν��");
                strSQLInfo = "SELECT * FROM (" + strSQLInfo + ") temptable0 WHERE " + strWhere;
            }
            if (AllCount == 0)
            {
                //����ͳ����䣬��л����[���]����strWhere���ܵ�����
                return "select count(*) from (" + strSQLInfo + ") P_Count  " 
                    + (string.IsNullOrEmpty(strWhere) ? "" : "WHERE " + strWhere);
            }

            if (PageNumber == 1)
                return strSQLInfo + " LIMIT " + PageSize;
            //offset ������0��ʼ����л���Ѷž�����ң�Σ� ���ִ�Bug
            //http://www.cnblogs.com/duwolfnet/articles/3293280.html
            int offset = PageSize * (PageNumber - 1);
           
            if (offsetString == ",")//MySQL,��л����[����]���ִ�Bug
                return strSQLInfo + " LIMIT " + offset + offsetString + PageSize;
            else //PostgreSQL
                return strSQLInfo + " LIMIT " + PageSize + offsetString + offset;
        }


        public static string MakePageSQLStringByMySQL(string strSQLInfo, string strWhere, int PageSize, int PageNumber, int AllCount)
        {
            return MakePageSQLStringByMySQL_PgSQL(strSQLInfo, strWhere, PageSize, PageNumber, AllCount,",");
        }

        public static string MakePageSQLStringByPostgreSQL(string strSQLInfo, string strWhere, int PageSize, int PageNumber, int AllCount)
        {
            return MakePageSQLStringByMySQL_PgSQL(strSQLInfo, strWhere, PageSize, PageNumber, AllCount, " offset ");
        }

        /// <summary>
        /// ���������ĸ�Ч���ٷ�ҳ֮�����ҳ
        /// </summary>
        /// <param name="pageNum">ҳ�룬��1��ʼ</param>
        /// <param name="pageSize">ҳ��С������1</param>
        /// <param name="filedList">�ֶ��б�</param>
        /// <param name="tableName">������</param>
        /// <param name="PKName">��������</param>
        /// <param name="conditon">��ѯ����</param>
        /// <returns>����ָ��ҳ��Ŀ��ٷ�ҳSQL���</returns>
        public static string GetDescPageSQLbyPrimaryKey(int pageNum, int pageSize, string filedList, string tableName, string PKName, string conditon)
        {
            if (conditon == null || conditon == "")
                conditon = "1=1";
            if (pageNum == 1)
            {
                string sqlTemplage = "Select top @pageSize @filedList from @table1 where  @conditon order by @PKName desc ";
                return sqlTemplage
                    .Replace("@pageSize", pageSize.ToString())
                    .Replace("@filedList", filedList)
                    .Replace("@table1", tableName)
                    .Replace("@conditon", conditon)
                    .Replace("@PKName", PKName);
            }
            else
            {
                //@topNum= ((ҳ��-1)*ҳ��С)
                string sqlTemplage = @"
select top @pageSize @filedList
from @table1
where @conditon And @PKName<
      (select min (@PKName) from
      (select top @topNum @PKName from @table1 where @conditon order by @PKName desc) as T
       )    
  order by @PKName desc
";
                int topNum = (pageNum - 1) * pageSize;

                return sqlTemplage.Replace("@topNum", topNum.ToString())
                   .Replace("@pageSize", pageSize.ToString())
                   .Replace("@filedList", filedList)
                   .Replace("@table1", tableName)
                   .Replace("@conditon", conditon)
                   .Replace("@PKName", PKName);

            }
        }

        /// <summary>
        /// ���������ĸ�Ч���ٷ�ҳ֮ �����ҳ
        /// </summary>
        /// <param name="pageNum">ҳ�룬��1��ʼ</param>
        /// <param name="pageSize">ҳ��С������1</param>
        /// <param name="filedList">�ֶ��б�</param>
        /// <param name="tableName">������</param>
        /// <param name="PKName">��������</param>
        /// <param name="conditon">��ѯ����</param>
        /// <returns>����ָ��ҳ��Ŀ��ٷ�ҳSQL���</returns>
        public static string GetAscPageSQLbyPrimaryKey(int pageNum, int pageSize, string filedList, string tableName, string PKName, string conditon)
        {
            if (conditon == null || conditon == "")
                conditon = "1=1";
            if (pageNum == 1)
            {
                string sqlTemplage = "Select top @pageSize @filedList from @table1 where  @conditon order by @PKName desc ";
                return sqlTemplage
                    .Replace("@pageSize", pageSize.ToString())
                    .Replace("@filedList", filedList)
                    .Replace("@table1", tableName)
                    .Replace("@conditon", conditon)
                    .Replace("@PKName", PKName);
            }
            else
            {
                //@topNum= ((ҳ��-1)*ҳ��С)
                string sqlTemplage = @"
select top @pageSize @filedList
from @table1
where @conditon And @PKName>
      (select max (@PKName) from
      (select top @topNum @PKName from @table1 where @conditon order by @PKName asc) as T
       )    
  order by @PKName asc
";
                int topNum = (pageNum - 1) * pageSize;

                return sqlTemplage.Replace("@topNum", topNum.ToString())
                   .Replace("@pageSize", pageSize.ToString())
                   .Replace("@filedList", filedList)
                   .Replace("@table1", tableName)
                   .Replace("@conditon", conditon)
                   .Replace("@PKName", PKName);

            }
        }
    }







}
