using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Core
{
    public class OctDbCommand : IOctDbCommand
    {
        public OctDbCommand(string sql, IDictionary<string, object> paras)
        {
            SqlText = sql;
            Paras = paras;
        }

        public string SqlText { get; private set; }
        public IDictionary<string, object> Paras { get; private set; }

        public DbCommand GetDbCommand(Func<string, string> createSql, Func<DbCommand> createCmd,
            Func<IDictionary<string, object>, IDbDataParameter[]> createPara)
        {
            string sql = createSql(SqlText);
            DbCommand cmd = createCmd();
            IDbDataParameter[] paras = createPara(Paras);
            return PrepareCommand(cmd, sql, paras);
        }

        /// <summary>
        ///     创建command
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        protected DbCommand PrepareCommand(DbCommand cmd, string cmdText, IDbDataParameter[] cmdParms)
        {
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (IDbDataParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput ||
                         parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }

            return cmd;
        }
    }
}