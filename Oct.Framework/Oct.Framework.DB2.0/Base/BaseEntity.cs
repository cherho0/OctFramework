using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Log;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.DynamicObj;
using Oct.Framework.DB.Emit;
using Oct.Framework.DB.Emit.MappingConfiguration;
using Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations;
using Oct.Framework.DB.Emit.Utils;
using Oct.Framework.DB.Interface;
using Oct.Framework.DB.Linq;
using Oct.Framework.DB.Utils;

namespace Oct.Framework.DB.Base
{
    [Serializable]
    public abstract class BaseEntity<T> : IEntity where T : class, new()
    {
        private ObjectsChangeTracker _tracker;
        private Dictionary<string, object> _oldValues;
        protected BaseEntity()
        {
            _tracker = new ObjectsChangeTracker();
            _tracker.RegisterObject(this);
        }

        private string TableName
        {
            get
            {
                var proxy = EntitiesProxyHelper.GetProxyInfo<T>();
                if (proxy.IsCompositeQuery)
                {
                    return string.Format(" ({0}) tab ", proxy.CompositeSql);
                }
                else
                {
                    return proxy.TableName;
                }

            }
        }

        private string PkName
        {
            get { return EntitiesProxyHelper.GetProxyInfo<T>().PrimaryKeys.FirstOrDefault(); }
        }

        private object PkValue
        {
            get
            {
                var curObj = (T)((IEntity)this);
                return EntitiesProxyHelper.GetDynamicMethod<T>().GetValue(curObj, PkName);
            }
        }

        private string Identity
        {
            get
            {
                var idd = EntitiesProxyHelper.GetProxyInfo<T>().IdentitesProp;
                if (idd == null)
                {
                    return "";
                }
                return idd;
            }
        }

        public void EntryUpdateStack()
        {
            var cc = _tracker.GetChanges(this);
            _oldValues = new Dictionary<string, object>();
            foreach (var trackingMember in cc)
            {
                _oldValues.Add(trackingMember.name, trackingMember.value);
            }
            _tracker.RegisterObject(this);
        }

        /// <summary>
        ///     获取查询sql
        /// </summary>
        /// <returns></returns>
        internal virtual string GetQuerySql(string where = "")
        {
            return "select * from " + TableName + (where.IsNullOrEmpty() ? " " : " where " + where);
        }


        /// <summary>
        ///     获取单个实体SQl
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal virtual string GetModelSql(object v)
        {
            return string.Format("select * from {0} where {1} = '{2}'", this.TableName, this.PkName, v);
        }

        /// <summary>
        ///     新增SQL
        /// </summary>
        /// <returns></returns>
        internal SqlCommand GetInsertCmd()
        {
            IMappingConfigurator config = new AddDbCommandsMappingConfig(
                  null,
                  null,
                  "insertop_inc__exc_"
          );

            var mapper = ObjectMapperManager.DefaultInstance.GetMapperImpl(
                this.GetType(),
                typeof(DbCommand),
                config
            );
            SqlCommand cmd = new SqlCommand();
            string[] fields = mapper.StroredObjects.OfType<SrcReadOperation>().Select(m => m.Source.MemberInfo.Name).ToArray();

            var cmdStr =
                "INSERT INTO "
                + TableName +
                "("
                + fields
                    .Select(f => f).Where(f => f != Identity)
                    .ToCSV(",")
                + ") VALUES ("
                + fields
                    .Where(f => f != Identity).Select(f => Constants.ParameterPrefix + f)
                    .ToCSV(",")
                + ")"
                ;
            cmd.CommandText = cmdStr;
            cmd.CommandType = System.Data.CommandType.Text;
            mapper.Map(this, cmd, null);
            return cmd;
            /*  var changes = _tracker.GetChanges(this);
              string insertSQl = "insert into {0} ({1}) values ({2}) ";

              var colBuilder = new StringBuilder();
              var valBuilder = new StringBuilder();
              var parameters = new Dictionary<string, object>();
              int idx = 0;
              foreach (var changedProp in changes)
              {
                  if (idx == changes.Length - 1)
                  {
                      colBuilder.Append(changedProp.name + "");
                      valBuilder.Append("@" + changedProp.name + "");
                      parameters.Add("@" + changedProp.name, changedProp.value);
                  }
                  else
                  {
                      colBuilder.Append(changedProp.name + ",");
                      valBuilder.Append("@" + changedProp.name + ",");
                      parameters.Add("@" + changedProp.name, changedProp.value);
                  }
                  idx++;
              }
              var sql = string.Format(insertSQl, TableName, colBuilder, valBuilder);*/
            //IOctDbCommand retcmd = new OctDbCommand(cmdStr, cmd.Parameters);
            //return retcmd;
        }

        /// <summary>
        ///     更新sql
        /// </summary>
        /// <returns></returns>
        internal IOctDbCommand GetUpdateCmd()
        {
            var changedProps = _tracker.GetChanges(this);
            if (changedProps.Length == 0)
            {
                return new OctDbCommand("", null);
            }
            var sb = new StringBuilder();
            var parameters = new Dictionary<string, object>();

            sb.Append("update " + this.TableName + " set ");

            foreach (var changedProp in changedProps)
            {
                if (changedProp.name.Equals(PkName))
                {
                    continue;
                }
                sb.Append(string.Format("{0} = @{0},", changedProp.name));

                parameters.Add("@" + changedProp.name, changedProp.value);
            }

            var sql = sb.ToString().Remove(sb.Length - 1);
            sql += string.Format(" where {0} = '{1}'", this.PkName, this.PkValue);

            return new OctDbCommand(sql, parameters);
        }

        /// <summary>
        ///     删除sql
        /// </summary>
        /// <returns></returns>
        internal string GetDelSql()
        {
            return string.Format("delete from {0} where {1} = '{2}'", this.TableName, this.PkName, this.PkValue);
        }

        /// <summary>
        ///     删除sql
        /// </summary>
        /// <returns></returns>
        internal string GetDelSql(object pk)
        {
            return string.Format("delete from {0} where {1} = '{2}'", this.TableName, this.PkName, pk);
        }

        /// <summary>
        /// 业务逻辑验证
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<BusinessRule> Validate()
        {
            return new BusinessRule[] { };
        }

        public DataChangeLog GetChangeLogs()
        {
            DataChangeLog dlog = new DataChangeLog();

            var changedProps = _tracker.GetChanges(this);
            foreach (var trackingMember in changedProps)
            {
                var log = new ValueLog();

                log.Name = trackingMember.name;
                log.OldValue = _oldValues.ContainsKey(log.Name) ? _oldValues[log.Name] : null;
                log.NewValue = trackingMember.value;
                dlog.Add(log);
            }
            return dlog;
        }
    }
}