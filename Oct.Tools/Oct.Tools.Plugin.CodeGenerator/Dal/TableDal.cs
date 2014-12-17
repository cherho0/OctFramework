using Oct.Tools.Core.Unity;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Oct.Tools.Plugin.CodeGenerator.Dal
{
    public class TableDal
    {
        #region 方法

        /// <summary>
        /// 获取数据库表的名称集合
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<string> GetDBTableNameList(string connectionString)
        {
            var dt = DBHelper.GetDataTable(connectionString, "SELECT Name FROM SysObjects Where XType = 'U' ORDER BY Name");

            return dt.AsEnumerable().Select(d => d.Field<string>("Name")).ToList();
        }

        /// <summary>
        /// 获取数据库表的信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetDBTableInfo(string connectionString, string tableName)
        {
            var dt = DBHelper.GetDataTable(connectionString,
                string.Format(@"
                SELECT
                       序号=a.colorder,
                       列名=a.name,
                       数据类型=b.name,
                       长度=Columnproperty(a.id, a.name, 'PRECISION'),
                       小数=Isnull(Columnproperty(a.id, a.name, 'Scale'), 0),
                       标识=CASE
                                WHEN Columnproperty(a.id, a.name, 'IsIdentity') = 1 THEN 'true'
                                ELSE 'false'
                              END,
                       主键=CASE
                                WHEN EXISTS(SELECT 1
                                            FROM   sysobjects
                                            WHERE  xtype = 'PK'
                                                   AND name IN (SELECT name
                                                                FROM   sysindexes
                                                                WHERE  indid IN(SELECT indid
                                                                                FROM   sysindexkeys
                                                                                WHERE  id = a.id
                                                                                       AND colid = a.colid))) THEN 'true'
                                ELSE 'false'
                              END,
                       允许空=CASE
                                   WHEN a.isnullable = 1 THEN 'true'
                                   ELSE 'false'
                                 END,
                       默认值=Isnull(e.text, ''),
                       说明=Isnull(g.[value], '')
                FROM   syscolumns a
                       LEFT JOIN systypes b
                              ON a.xusertype = b.xusertype
                       INNER JOIN sysobjects d
                               ON a.id = d.id
                                  AND d.xtype = 'U'
                                  AND d.name <> 'dtproperties'
                       LEFT JOIN syscomments e
                              ON a.cdefault = e.id
                       LEFT JOIN sys.extended_properties g
                              ON a.id = g.major_id
                                 AND a.colid = g.minor_id
                       LEFT JOIN sys.extended_properties f
                              ON d.id = f.major_id
                                 AND f.minor_id = 0
                WHERE  d.name = '{0}'
                ORDER  BY a.id,
                          a.colorder", tableName));

            dt.TableName = tableName;

            return dt;
        }

        #endregion
    }
}
