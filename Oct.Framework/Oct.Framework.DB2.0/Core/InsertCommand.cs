using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.DB.Emit;
using Oct.Framework.DB.Emit.MappingConfiguration;
using Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations;
using Oct.Framework.DB.Emit.Utils;
using Oct.Framework.DB.Linq;
using Oct.Framework.DB.Utils;

namespace Oct.Framework.DB.Core
{
    public static class InsertCommand
    {

        public static DbCommand AddParam(this DbCommand cmd, string paramName, object paramValue)
        {
            if (paramValue is Guid)
            {
                paramValue = ((Guid)paramValue).ToString().ToUpper();
            }

            if (paramValue == null)
            {
                paramValue = DBNull.Value;
            }

            var par = cmd.CreateParameter();
            par.ParameterName = paramName;
            par.Value = paramValue;
            cmd.Parameters.Add(par);
            return cmd;
        }

        public static DbCommand BuildInsertCommand(
            this DbCommand cmd,
            object obj,
            string tableName
        )
        {
            return BuildInsertCommand(cmd, obj, tableName, null, null);
        }

        public static DbCommand BuildInsertCommand(
            this DbCommand cmd,
            object obj,
            string tableName,
            string[] includeFields,
            string[] excludeFields
        )
        {
            IMappingConfigurator config = new AddDbCommandsMappingConfig(
                    includeFields,
                    excludeFields,
                    "insertop_inc_" + includeFields.ToCSV("_") + "_exc_" + excludeFields.ToCSV("_")
            );

            var mapper = ObjectMapperManager.DefaultInstance.GetMapperImpl(
                obj.GetType(),
                typeof(DbCommand),
                config
            );

            string[] fields = mapper.StroredObjects.OfType<SrcReadOperation>().Select(m => m.Source.MemberInfo.Name).ToArray();

            var cmdStr =
                "INSERT INTO "
                + tableName +
                "("
                + fields
                    .Select(f => f)
                    .ToCSV(",")
                + ") VALUES ("
                + fields
                    .Select(f => Constants.ParameterPrefix + f)
                    .ToCSV(",")
                + ")"
                ;
            cmd.CommandText = cmdStr;
            cmd.CommandType = System.Data.CommandType.Text;

            mapper.Map(obj, cmd, null);
            return cmd;
        }
    }
}

