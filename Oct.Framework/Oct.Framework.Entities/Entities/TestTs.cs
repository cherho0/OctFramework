using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.Entities.Entities
{
    [Serializable]
    public partial class TestTs : BaseEntity<TestTs>
    {
        #region 字段

        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                PropChanged("Id", _id, value);
                _id = value;
            }
        }

        private string _DD;

        public string DD
        {
            get
            {
                return _DD;
            }
            set
            {
                PropChanged("DD", _DD, value);
                _DD = value;
            }
        }


        #endregion

        #region 重载

        public override object PkValue
        {
            get { return Id; }
        }

        public override string PkName
        {
            get { return "Id"; }
        }

        public override bool IsIdentityPk
        {
            get { return true; }
        }

        public override void SetIdentity(object v)
        {
            _id = int.Parse(v.ToString());
        }

        public override List<string> Props
        {
            get { return new List<string>(); }
        }


        public override TestTs GetEntityFromDataRow(DataRow row)
        {
            if (row["Id"].ToString() != "")
            {
                _id = int.Parse(row["Id"].ToString());
            }
            if (row["DD"].ToString() != "")
            {
                _DD = row["DD"].ToString();
            }

            return this;
        }

        public override string TableName
        {
            get { return "TestTs"; }
        }

        public override IOctDbCommand GetInsertCmd()
        {
            var sql = @"INSERT INTO TestTs
           (DD
          )
     VALUES
           (
           @DD          )";


            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
            {"@DD",DD}
            };
            return new OctDbCommand(sql, parameters);
        }

        public override IOctDbCommand GetUpdateCmd(string @where = "", IDictionary<string, object> paras = null)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            sb.Append("update " + TableName + " set ");
            foreach (var changedProp in ChangedProps)
            {
                sb.Append(string.Format("{0}=@{0},", changedProp.Key));
                parameters.Add("@" + changedProp.Key, changedProp.Value);
            }

            var sql = sb.ToString().Remove(sb.Length - 1);
            sql += string.Format(" where {0}='{1}'", PkName, PkValue);

            if (!string.IsNullOrEmpty(@where))
            {
                sql += " and " + @where;
            }

            if (paras != null)
            {
                foreach (var p in paras)
                {
                    parameters.Add(p.Key, p.Value);
                }
            }

            return new OctDbCommand(sql, parameters);
        }

        public override string GetDelSQL()
        {
            return string.Format("delete from {0} where {1}='{2}'", TableName, PkName, PkValue);
        }

        public override string GetDelSQL(object v, string @where = "")
        {
            string sql = string.Format("delete from {0} where 1=1 ", TableName);
            if (v != null)
            {
                sql += "and " + PkName + "='" + v + "'";
            }
            if (!string.IsNullOrEmpty(@where))
            {
                sql += "and " + @where;
            }
            return sql;
        }

        public override string GetModelSQL(object v)
        {
            return string.Format("select * from {0} where {1}='{2}'", TableName, PkName, v);
        }

        public override string GetQuerySQL(string @where = "")
        {
            var sql = string.Format("select * from {0} where 1=1 ", TableName);
            if (!string.IsNullOrEmpty(@where))
            {
                sql += " and " + @where;
            }
            return sql;
        }

        #endregion
    }
}
