using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.DB.Attrbuites;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.DynamicObj;
using Oct.Framework.DB.Emit.Utils;
using Oct.Framework.DB.Implementation;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.GenTable
{

    public class GenDb
    {
        /// <summary>
        /// 请先创建好数据库，然后依据程序集生成所有表
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static void Gen(Assembly assembly, ISQLContext context)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var b = type.BaseType;
                if (b != null && b.FullName.Contains("BaseEntity"))
                {
                    GenTbl.Gen(context, type);
                }
            }
        }
    }

    /// <summary>
    /// 生成数据表
    /// </summary>
    public class GenTbl
    {
        internal static string GetGenTblSql(Type t)
        {
            var props = ReflectionUtils.GetPublicFieldsAndProperties(t);
            var tblname = EntitiesProxyHelper.GetProxyInfo(t).TableName;

            string script = @"
                            CREATE TABLE {0} (
                            {1}
                            ) ";
            string fieldsText = "";
            var idx = 0;
            foreach (var field in props)
            {
                if (field is PropertyInfo)
                {
                    var col = " [{0}] {1} {2} {3} ";
                    var prop = (PropertyInfo)field;

                    var name = prop.Name;

                    object defaultValue = null;
                    if (prop.PropertyType == typeof(string))
                        defaultValue = "";
                    else
                        defaultValue = Activator.CreateInstance(prop.PropertyType);
                    var para = new SqlParameter(name, defaultValue);
                    var dbtype = para.SqlDbType;
                    string dbtypeStr = "";
                    if (dbtype == SqlDbType.NVarChar)
                    {
                        dbtypeStr = "[" + dbtype.ToString() + "](200)";
                    }
                    else
                    {
                        dbtypeStr = "[" + dbtype.ToString() + "]";
                    }

                    var isnull = para.IsNullable;
                    string nullable = " NOT NULL";
                    if (isnull)
                    {
                        nullable = " NULL ";
                    }

                    string keyTrue = "";
                    if (prop.IsPrimaryKey())
                    {
                        keyTrue += "  PRIMARY KEY ";
                    }

                    if (prop.IsAutoIncrease())
                    {
                        keyTrue += " IDENTITY(1,1) ";
                    }

                    if (idx == props.Length - 1)
                    {
                        fieldsText += string.Format(col, name, dbtypeStr, nullable, keyTrue);
                    }
                    else
                    {
                        fieldsText += string.Format(col, name, dbtypeStr, nullable, keyTrue) + ",";
                    }

                    idx++;
                }
            }

            var sql = string.Format(script, tblname, fieldsText);
            return sql;
        }

        public static string Gen(ISQLContext sqlContext, Type t)
        {
            var sql = GetGenTblSql(t);
            sqlContext.ExecuteSQL(sql);

            return sql;
        }

        public static string Gen<T>(ISQLContext sqlContext) where T : BaseEntity<T>, new()
        {
            return Gen(sqlContext, typeof(T));
        }


    }
}
