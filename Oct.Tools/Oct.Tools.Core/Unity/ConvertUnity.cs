using System;

namespace Oct.Tools.Core.Unity
{
    public class ConvertUnity
    {
        #region 方法

        /// <summary>
        /// 转换成C#类型
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public static string MapCsharpType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype))
                return dbtype;

            dbtype = dbtype.ToLower();

            var csharpType = "object";

            switch (dbtype)
            {
                case "bigint": csharpType = "long"; break;
                case "binary": csharpType = "byte[]"; break;
                case "bit": csharpType = "bool"; break;
                case "char": csharpType = "string"; break;
                case "date": csharpType = "DateTime"; break;
                case "datetime": csharpType = "DateTime"; break;
                case "datetime2": csharpType = "DateTime"; break;
                case "datetimeoffset": csharpType = "DateTimeOffset"; break;
                case "dityint": csharpType = "bool"; break;
                case "decimal": csharpType = "decimal"; break;
                case "float": csharpType = "double"; break;
                case "image": csharpType = "byte[]"; break;
                case "int": csharpType = "int"; break;
                case "money": csharpType = "decimal"; break;
                case "nchar": csharpType = "string"; break;
                case "ntext": csharpType = "string"; break;
                case "numeric": csharpType = "decimal"; break;
                case "nvarchar": csharpType = "string"; break;
                case "real": csharpType = "Single"; break;
                case "smalldatetime": csharpType = "DateTime"; break;
                case "smallint": csharpType = "short"; break;
                case "smallmoney": csharpType = "decimal"; break;
                case "sql_variant": csharpType = "object"; break;
                case "sysname": csharpType = "object"; break;
                case "text": csharpType = "string"; break;
                case "longtext": csharpType = "string"; break;
                case "time": csharpType = "TimeSpan"; break;
                case "timestamp": csharpType = "byte[]"; break;
                case "tinyint": csharpType = "byte"; break;
                case "uniqueidentifier": csharpType = "Guid"; break;
                case "varbinary": csharpType = "byte[]"; break;
                case "varchar": csharpType = "string"; break;
                case "xml": csharpType = "string"; break;

                default: csharpType = "object"; break;
            }

            return csharpType;
        }

        /// <summary>
        /// 转换成C#类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string MapCsharpType(Type type)
        {
            var csharpType = "object";

            switch (type.Name.ToLower())
            {
                case "boolean": csharpType = "bool"; break;
                case "byte": csharpType = "byte"; break;
                case "byte[]": csharpType = "byte[]"; break;
                case "datetime": csharpType = "DateTime"; break;
                case "decimal": csharpType = "decimal"; break;
                case "double": csharpType = "double"; break;
                case "guid": csharpType = "Guid"; break;
                case "int32": csharpType = "int"; break;
                case "int64": csharpType = "long"; break;
                case "string": csharpType = "string"; break;

                default: csharpType = "object"; break;
            }

            return csharpType;
        }

        /// <summary>
        /// 转换成通用类型
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public static Type MapCommonType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype))
                return Type.Missing.GetType();

            dbtype = dbtype.ToLower();

            var commonType = typeof(object);

            switch (dbtype)
            {
                case "bigint": commonType = typeof(long); break;
                case "binary": commonType = typeof(byte[]); break;
                case "bit": commonType = typeof(bool); break;
                case "char": commonType = typeof(string); break;
                case "date": commonType = typeof(DateTime); break;
                case "datetime": commonType = typeof(DateTime); break;
                case "datetime2": commonType = typeof(DateTime); break;
                case "datetimeoffset": commonType = typeof(DateTimeOffset); break;
                case "dityint": commonType = typeof(Boolean); break;
                case "decimal": commonType = typeof(decimal); break;
                case "float": commonType = typeof(double); break;
                case "image": commonType = typeof(byte[]); break;
                case "int": commonType = typeof(int); break;
                case "money": commonType = typeof(decimal); break;
                case "nchar": commonType = typeof(string); break;
                case "ntext": commonType = typeof(string); break;
                case "numeric": commonType = typeof(decimal); break;
                case "nvarchar": commonType = typeof(string); break;
                case "real": commonType = typeof(Single); break;
                case "smalldatetime": commonType = typeof(DateTime); break;
                case "smallint": commonType = typeof(short); break;
                case "smallmoney": commonType = typeof(decimal); break;
                case "sql_variant": commonType = typeof(object); break;
                case "sysname": commonType = typeof(object); break;
                case "text": commonType = typeof(string); break;
                case "time": commonType = typeof(TimeSpan); break;
                case "timestamp": commonType = typeof(byte[]); break;
                case "tinyint": commonType = typeof(byte); break;
                case "uniqueidentifier": commonType = typeof(Guid); break;
                case "varbinary": commonType = typeof(byte[]); break;
                case "varchar": commonType = typeof(string); break;
                case "xml": commonType = typeof(string); break;

                default: commonType = typeof(object); break;
            }

            return commonType;
        }

        /// <summary>
        /// 转换成C#类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type MapCommonType(Type type)
        {
            var commonType = typeof(object);

            switch (type.Name.ToLower())
            {
                case "boolean": commonType = typeof(bool); break;
                case "byte": commonType = typeof(byte); break;
                case "byte[]": commonType = typeof(byte[]); break;
                case "datetime": commonType = typeof(DateTime); break;
                case "decimal": commonType = typeof(decimal); break;
                case "double": commonType = typeof(double); break;
                case "guid": commonType = typeof(Guid); break;
                case "int32": commonType = typeof(int); break;
                case "int64": commonType = typeof(long); break;
                case "string": commonType = typeof(string); break;

                default: commonType = typeof(object); break;
            }

            return commonType;
        }

        #endregion
    }
}
