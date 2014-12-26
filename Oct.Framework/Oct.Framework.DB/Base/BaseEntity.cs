using Oct.Framework.Core.Log;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace Oct.Framework.DB.Base
{
    [Serializable]
    public abstract class BaseEntity<T> : IEntity where T : class, new()
    {
        /// <summary>
        ///     属性变更记录
        /// </summary>
        protected Dictionary<string, object> ChangedProps;

        protected BaseEntity()
        {
            ChangedProps = new Dictionary<string, object>();
        }

        private bool _recordlog = false;

        private string _currentUser = string.Empty;

        public void ActiveLogDataChange(string currentUser)
        {
            _recordlog = true;
            _currentUser = currentUser;
        }

        public virtual object PkValue { get; private set; }

        public virtual string PkName { get; private set; }

        public abstract bool IsIdentityPk { get; }

        public abstract Dictionary<string,string> Props { get; }

        /// <summary>
        ///     获取实体名称
        /// </summary>
        /// <returns></returns>
        public abstract String TableName { get; }

        /// <summary>
        ///     通过datarow获取一个实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public abstract T GetEntityFromDataRow(DataRow row);

        /// <summary>
        ///     通过datarow获取一个实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public abstract T GetEntityFromDataReader(IDataReader reader);

        /// <summary>
        ///     获取查询sql
        /// </summary>
        /// <returns></returns>
        public virtual string GetQuerySQL(string where = "")
        {
            return "";
        }

        public virtual void SetIdentity(object v)
        {

        }

        /// <summary>
        ///     获取单个实体SQl
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public virtual string GetModelSQL(object v)
        {
            return "";
        }

        /// <summary>
        ///     新增SQL
        /// </summary>
        /// <returns></returns>
        public virtual IOctDbCommand GetInsertCmd()
        {
            return null;
        }

        /// <summary>
        ///     更新sql
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IOctDbCommand GetUpdateCmd(string where = "", IDictionary<string, object> paras = null)
        {
            return null;
        }

        /// <summary>
        ///     删除sql
        /// </summary>
        /// <returns></returns>
        public virtual string GetDelSQL()
        {
            return "";
        }

        /// <summary>
        ///     条件删除
        /// </summary>
        /// <param name="v"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual string GetDelSQL(object v, string where = "")
        {
            return "";
        }

        /// <summary>
        ///     属性变更记录
        /// </summary>
        /// <param name="name"></param>
        protected void PropChanged(string name, object oldval, object newval)
        {
            if (!ChangedProps.ContainsKey(name))
            {
                if (!name.Equals(PkName))
                {
                    ChangedProps.Add(name, newval);
                }

            }
            else
            {
                ChangedProps[name] = newval;
            }

            if (_recordlog)
            {
                if (oldval == null && newval == null)
                {
                    return;
                }
                if (oldval != null && oldval.Equals(newval))
                {
                    return;
                }
                Add(name, oldval, newval);
            }

        }

        /// <summary>
        /// 日志实体
        /// </summary>
        private readonly DataChangeLog _oLog = new DataChangeLog();

        private void Add(string name, object oldval, object newval)
        {
            var log = new ValueLog()
            {
                Name = name,
                OldValue = oldval,
                NewValue = newval
            };
            if (log.OldValue is DateTime)
            {
                log.OldValue = ((DateTime)log.OldValue).ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (log.NewValue is DateTime)
            {
                log.NewValue = ((DateTime)log.NewValue).ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (log.OldValue is bool)
            {
                log.OldValue = (bool)log.OldValue ? "是" : "否";
            }

            if (log.NewValue is bool)
            {
                log.NewValue = (bool)log.NewValue ? "是" : "否";
            }
            _oLog.Add(log);
        }

        public DataChangeLog GetLog()
        {
            if (!_recordlog)
            {
                return null;
            }
            var name = _currentUser;
            if (string.IsNullOrEmpty(name))
            {
                name = "系统自动生成";
            }
            if (_oLog != null && _oLog.ValueLogs != null && _oLog.ValueLogs.Count > 0)
            {
                _oLog.OperID = PkValue.ToString();
                _oLog.OperObjName = TableName;
                _oLog.OperDate = DateTime.Now;
                _oLog.OperUser = name;
            }
            return _oLog;
        }

        /// <summary>
        /// 业务逻辑验证
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<BusinessRule> Validate()
        {
            return new BusinessRule[] { };
        }
    }
}