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
    public partial class UcUserMsg : BaseEntity<UcUserMsg>
    {
        #region 字段

        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set
            {
                PropChanged("Id", _id, value);
                _id = value;
            }
        }

        private string _userId;

        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                PropChanged("UserId", _userId, value);
                _userId = value;
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                PropChanged("Title", _title, value);
                _title = value;
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                PropChanged("Message", _message, value);
                _message = value;
            }
        }

        private DateTime _createDate;

        public DateTime CreateDate
        {
            get { return _createDate; }
            set
            {
                PropChanged("CreateDate", _createDate, value);
                _createDate = value;
            }
        }

        private bool _isRead;

        public bool IsRead
        {
            get { return _isRead; }
            set
            {
                PropChanged("IsRead", _isRead, value);
                _isRead = value;
            }
        }

        private string _fromUser;

        public string FromUser
        {
            get { return _fromUser; }
            set
            {
                PropChanged("FromUser", _fromUser, value);
                _fromUser = value;
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
            get { return false; }
        }

        public override List<string> Props
        {
            get { return new List<string>(); }
        }


        public override UcUserMsg GetEntityFromDataRow(DataRow row)
        {
            if (row["Id"].ToString() != "")
            {
                _id = new Guid(row["Id"].ToString());
            }
            if (row["UserId"].ToString() != "")
            {
                _userId = row["UserId"].ToString();
            }
            if (row["Title"].ToString() != "")
            {
                _title = row["Title"].ToString();
            }
            if (row["Message"].ToString() != "")
            {
                _message = row["Message"].ToString();
            }

            if (row["CreateDate"].ToString() != "")
            {
                _createDate = DateTime.Parse(row["CreateDate"].ToString());
            }
            if (row["IsRead"].ToString() != "")
            {
                _isRead = row["IsRead"].ToString() == "1";
            }
            _fromUser = row["FromUser"].ToString();
            return this;
        }

        public override string TableName
        {
            get { return "UC_UserMsg"; }
        }

        public override IOctDbCommand GetInsertCmd()
        {
            var sql = @"INSERT INTO UC_UserMsg
           (Id
           ,UserId
           ,Title
           ,Message
           ,CreateDate
           ,IsRead
           ,FromUser)
     VALUES
           (@Id
           ,@UserId
           ,@Title
           ,@Message
           ,@CreateDate
           ,@IsRead
           ,@FromUser)";


            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
            {"@Id", Id},
            {"@UserId",UserId},
            {"@Title",Title},
            {"@Message",Message},
            {"@CreateDate",CreateDate},
            {"@IsRead",IsRead},
            {"@FromUser",FromUser}
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
