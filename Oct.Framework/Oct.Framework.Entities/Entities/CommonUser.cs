using Oct.Framework.DB.Base;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Oct.Framework.Entities.Entities
{
	[Serializable]
	public partial class CommonUser : BaseEntity<CommonUser>
	{ 
		#region	属性
		
		private Guid _id;

		/// <summary>
		/// Id
		/// </summary>
		public Guid Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.PropChanged("Id", this._id, value);

				this._id = value;
			}
		}
		
		private string _account;

		/// <summary>
		/// 用户名
		/// </summary>
		public string Account
		{
			get
			{
				return this._account;
			}
			set
			{
				this.PropChanged("Account", this._account, value);

				this._account = value;
			}
		}
		
		private string _userName;

		/// <summary>
		/// 姓名
		/// </summary>
		public string UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				this.PropChanged("UserName", this._userName, value);

				this._userName = value;
			}
		}
		
		private string _password;

		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this.PropChanged("Password", this._password, value);

				this._password = value;
			}
		}
		
		private int _status;

		/// <summary>
		/// 状态
		/// </summary>
		public int Status
		{
			get
			{
				return this._status;
			}
			set
			{
				this.PropChanged("Status", this._status, value);

				this._status = value;
			}
		}
		
		private DateTime _createDate;

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate
		{
			get
			{
				return this._createDate;
			}
			set
			{
				this.PropChanged("CreateDate", this._createDate, value);

				this._createDate = value;
			}
		}
		
		private Guid? _createUserId;

		/// <summary>
		/// 创建人ID
		/// </summary>
		public Guid? CreateUserId
		{
			get
			{
				return this._createUserId;
			}
			set
			{
				this.PropChanged("CreateUserId", this._createUserId, value);

				this._createUserId = value;
			}
		}
		
		private Guid? _modifyUserId;

		/// <summary>
		/// 修改人ID
		/// </summary>
		public Guid? ModifyUserId
		{
			get
			{
				return this._modifyUserId;
			}
			set
			{
				this.PropChanged("ModifyUserId", this._modifyUserId, value);

				this._modifyUserId = value;
			}
		}
		
		private DateTime? _modifyDate;

		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime? ModifyDate
		{
			get
			{
				return this._modifyDate;
			}
			set
			{
				this.PropChanged("ModifyDate", this._modifyDate, value);

				this._modifyDate = value;
			}
		}
		
		private string _phone;

		/// <summary>
		/// 电话
		/// </summary>
		public string Phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				this.PropChanged("Phone", this._phone, value);

				this._phone = value;
			}
		}
		
		private string _email;

		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				this.PropChanged("Email", this._email, value);

				this._email = value;
			}
		}
		
		private string _fax;

		/// <summary>
		/// 传真
		/// </summary>
		public string Fax
		{
			get
			{
				return this._fax;
			}
			set
			{
				this.PropChanged("Fax", this._fax, value);

				this._fax = value;
			}
		}
		
		private string _qQ;

		/// <summary>
		/// QQ
		/// </summary>
		public string QQ
		{
			get
			{
				return this._qQ;
			}
			set
			{
				this.PropChanged("QQ", this._qQ, value);

				this._qQ = value;
			}
		}
		
		private string _address;

		/// <summary>
		/// 地址
		/// </summary>
		public string Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this.PropChanged("Address", this._address, value);

				this._address = value;
			}
		}
		
		private int? _gander;

		/// <summary>
		/// 性别
		/// </summary>
		public int? Gander
		{
			get
			{
				return this._gander;
			}
			set
			{
				this.PropChanged("Gander", this._gander, value);

				this._gander = value;
			}
		}
		
		private string _mobile;

		/// <summary>
		/// 手机
		/// </summary>
		public string Mobile
		{
			get
			{
				return this._mobile;
			}
			set
			{
				this.PropChanged("Mobile", this._mobile, value);

				this._mobile = value;
			}
		}
		
		private string _iDNumber;

		/// <summary>
		/// 身份证号码
		/// </summary>
		public string IDNumber
		{
			get
			{
				return this._iDNumber;
			}
			set
			{
				this.PropChanged("IDNumber", this._iDNumber, value);

				this._iDNumber = value;
			}
		}
		
		private string _avatar;

		/// <summary>
		/// 头像
		/// </summary>
		public string Avatar
		{
			get
			{
				return this._avatar;
			}
			set
			{
				this.PropChanged("Avatar", this._avatar, value);

				this._avatar = value;
			}
		}
		
		private DateTime? _lastLoginDate;

		/// <summary>
		/// 最后登录时间
		/// </summary>
		public DateTime? LastLoginDate
		{
			get
			{
				return this._lastLoginDate;
			}
			set
			{
				this.PropChanged("LastLoginDate", this._lastLoginDate, value);

				this._lastLoginDate = value;
			}
		}
		
		private int? _loginCount;

		/// <summary>
		/// 登录次数
		/// </summary>
		public int? LoginCount
		{
			get
			{
				return this._loginCount;
			}
			set
			{
				this.PropChanged("LoginCount", this._loginCount, value);

				this._loginCount = value;
			}
		}
		
		#endregion

		#region 重载

		public override object PkValue
		{
			get
			{
				return this.Id; 
			}
		}

		public override string PkName
		{
			get
			{
				return "Id"; 
			}
		}

		public override bool IsIdentityPk
		{
			get 
			{
				return false; 
			}
		}

		
		private Dictionary<string, string> _props;

		public override Dictionary<string, string> Props
	    {
	        get {
				if(_props == null)
				{
					_props = new Dictionary<string, string>();
										_props.Add( "Id","Id");
										_props.Add( "Account","Account");
										_props.Add( "UserName","UserName");
										_props.Add( "Password","Password");
										_props.Add( "Status","Status");
										_props.Add( "CreateDate","CreateDate");
										_props.Add( "CreateUserId","CreateUserId");
										_props.Add( "ModifyUserId","ModifyUserId");
										_props.Add( "ModifyDate","ModifyDate");
										_props.Add( "Phone","Phone");
										_props.Add( "Email","Email");
										_props.Add( "Fax","Fax");
										_props.Add( "QQ","QQ");
										_props.Add( "Address","Address");
										_props.Add( "Gander","Gander");
										_props.Add( "Mobile","Mobile");
										_props.Add( "IDNumber","IDNumber");
										_props.Add( "Avatar","Avatar");
										_props.Add( "LastLoginDate","LastLoginDate");
										_props.Add( "LoginCount","LoginCount");
									}
				return _props;			 
			 }
	    }

		public override CommonUser GetEntityFromDataRow(DataRow row)
		{
			if (row.Table.Columns.Contains("Id") && row["Id"] != null && row["Id"].ToString() != "")
			{
				this._id = new Guid(row["Id"].ToString());
			}
			if (row.Table.Columns.Contains("Account") && row["Account"] != null)
			{
				this._account = row["Account"].ToString();
			}
			if (row.Table.Columns.Contains("UserName") && row["UserName"] != null)
			{
				this._userName = row["UserName"].ToString();
			}
			if (row.Table.Columns.Contains("Password") && row["Password"] != null)
			{
				this._password = row["Password"].ToString();
			}
			if (row.Table.Columns.Contains("Status") && row["Status"] != null && row["Status"].ToString() != "")
			{
				this._status = int.Parse(row["Status"].ToString());
			}
			if (row.Table.Columns.Contains("CreateDate") && row["CreateDate"] != null && row["CreateDate"].ToString() != "")
			{
				this._createDate = DateTime.Parse(row["CreateDate"].ToString());
			}
			if (row.Table.Columns.Contains("CreateUserId") && row["CreateUserId"] != null && row["CreateUserId"].ToString() != "")
			{
				this._createUserId = new Guid(row["CreateUserId"].ToString());
			}
			if (row.Table.Columns.Contains("ModifyUserId") && row["ModifyUserId"] != null && row["ModifyUserId"].ToString() != "")
			{
				this._modifyUserId = new Guid(row["ModifyUserId"].ToString());
			}
			if (row.Table.Columns.Contains("ModifyDate") && row["ModifyDate"] != null && row["ModifyDate"].ToString() != "")
			{
				this._modifyDate = DateTime.Parse(row["ModifyDate"].ToString());
			}
			if (row.Table.Columns.Contains("Phone") && row["Phone"] != null)
			{
				this._phone = row["Phone"].ToString();
			}
			if (row.Table.Columns.Contains("Email") && row["Email"] != null)
			{
				this._email = row["Email"].ToString();
			}
			if (row.Table.Columns.Contains("Fax") && row["Fax"] != null)
			{
				this._fax = row["Fax"].ToString();
			}
			if (row.Table.Columns.Contains("QQ") && row["QQ"] != null)
			{
				this._qQ = row["QQ"].ToString();
			}
			if (row.Table.Columns.Contains("Address") && row["Address"] != null)
			{
				this._address = row["Address"].ToString();
			}
			if (row.Table.Columns.Contains("Gander") && row["Gander"] != null && row["Gander"].ToString() != "")
			{
				this._gander = int.Parse(row["Gander"].ToString());
			}
			if (row.Table.Columns.Contains("Mobile") && row["Mobile"] != null)
			{
				this._mobile = row["Mobile"].ToString();
			}
			if (row.Table.Columns.Contains("IDNumber") && row["IDNumber"] != null)
			{
				this._iDNumber = row["IDNumber"].ToString();
			}
			if (row.Table.Columns.Contains("Avatar") && row["Avatar"] != null)
			{
				this._avatar = row["Avatar"].ToString();
			}
			if (row.Table.Columns.Contains("LastLoginDate") && row["LastLoginDate"] != null && row["LastLoginDate"].ToString() != "")
			{
				this._lastLoginDate = DateTime.Parse(row["LastLoginDate"].ToString());
			}
			if (row.Table.Columns.Contains("LoginCount") && row["LoginCount"] != null && row["LoginCount"].ToString() != "")
			{
				this._loginCount = int.Parse(row["LoginCount"].ToString());
			}

			return this;
		}

		public override CommonUser GetEntityFromDataReader(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
				if (name.ToLower() == "id" && !reader.IsDBNull(i))
{
_id = reader.GetGuid(i);
 continue;
}
if (name.ToLower() == "account" && !reader.IsDBNull(i))
{
_account = reader.GetString(i);
 continue;
}
if (name.ToLower() == "username" && !reader.IsDBNull(i))
{
_userName = reader.GetString(i);
 continue;
}
if (name.ToLower() == "password" && !reader.IsDBNull(i))
{
_password = reader.GetString(i);
 continue;
}
if (name.ToLower() == "status" && !reader.IsDBNull(i))
{
_status = reader.GetInt32(i);
 continue;
}
if (name.ToLower() == "createdate" && !reader.IsDBNull(i))
{
_createDate = reader.GetDateTime(i);
 continue;
}
if (name.ToLower() == "createuserid" && !reader.IsDBNull(i))
{
_createUserId = reader.GetGuid(i);
 continue;
}
if (name.ToLower() == "modifyuserid" && !reader.IsDBNull(i))
{
_modifyUserId = reader.GetGuid(i);
 continue;
}
if (name.ToLower() == "modifydate" && !reader.IsDBNull(i))
{
_modifyDate = reader.GetDateTime(i);
 continue;
}
if (name.ToLower() == "phone" && !reader.IsDBNull(i))
{
_phone = reader.GetString(i);
 continue;
}
if (name.ToLower() == "email" && !reader.IsDBNull(i))
{
_email = reader.GetString(i);
 continue;
}
if (name.ToLower() == "fax" && !reader.IsDBNull(i))
{
_fax = reader.GetString(i);
 continue;
}
if (name.ToLower() == "qq" && !reader.IsDBNull(i))
{
_qQ = reader.GetString(i);
 continue;
}
if (name.ToLower() == "address" && !reader.IsDBNull(i))
{
_address = reader.GetString(i);
 continue;
}
if (name.ToLower() == "gander" && !reader.IsDBNull(i))
{
_gander = reader.GetInt32(i);
 continue;
}
if (name.ToLower() == "mobile" && !reader.IsDBNull(i))
{
_mobile = reader.GetString(i);
 continue;
}
if (name.ToLower() == "idnumber" && !reader.IsDBNull(i))
{
_iDNumber = reader.GetString(i);
 continue;
}
if (name.ToLower() == "avatar" && !reader.IsDBNull(i))
{
_avatar = reader.GetString(i);
 continue;
}
if (name.ToLower() == "lastlogindate" && !reader.IsDBNull(i))
{
_lastLoginDate = reader.GetDateTime(i);
 continue;
}
if (name.ToLower() == "logincount" && !reader.IsDBNull(i))
{
_loginCount = reader.GetInt32(i);
 continue;
}
               
}
            return this;
        }

		public override string TableName
		{
			get
			{
				return "Common_User";
			}
		}

		public override IOctDbCommand GetInsertCmd()
		{
			var sql = @"
				INSERT INTO Common_User (
					Id,
					Account,
					UserName,
					Password,
					Status,
					CreateDate,
					CreateUserId,
					ModifyUserId,
					ModifyDate,
					Phone,
					Email,
					Fax,
					QQ,
					Address,
					Gander,
					Mobile,
					IDNumber,
					Avatar,
					LastLoginDate,
					LoginCount)
				VALUES (
					@Id,
					@Account,
					@UserName,
					@Password,
					@Status,
					@CreateDate,
					@CreateUserId,
					@ModifyUserId,
					@ModifyDate,
					@Phone,
					@Email,
					@Fax,
					@QQ,
					@Address,
					@Gander,
					@Mobile,
					@IDNumber,
					@Avatar,
					@LastLoginDate,
					@LoginCount)";
			
			DbCommand cmd = new SqlCommand();
			var parameters = new Dictionary<string, object> {
				{"@Id", this.Id},
				{"@Account", this.Account},
				{"@UserName", this.UserName},
				{"@Password", this.Password},
				{"@Status", this.Status},
				{"@CreateDate", this.CreateDate},
				{"@CreateUserId", this.CreateUserId},
				{"@ModifyUserId", this.ModifyUserId},
				{"@ModifyDate", this.ModifyDate},
				{"@Phone", this.Phone},
				{"@Email", this.Email},
				{"@Fax", this.Fax},
				{"@QQ", this.QQ},
				{"@Address", this.Address},
				{"@Gander", this.Gander},
				{"@Mobile", this.Mobile},
				{"@IDNumber", this.IDNumber},
				{"@Avatar", this.Avatar},
				{"@LastLoginDate", this.LastLoginDate},
				{"@LoginCount", this.LoginCount}};

			return new OctDbCommand(sql, parameters);
		}

		public override IOctDbCommand GetUpdateCmd(string @where = "", IDictionary<string, object> paras = null)
		{
			var sb = new StringBuilder();
			var parameters = new Dictionary<string, object>();
          
			sb.Append("update " + this.TableName + " set ");
         
			foreach (var changedProp in this.ChangedProps)
			{
				sb.Append(string.Format("{0} = @{0},", changedProp.Key));

				parameters.Add("@" + changedProp.Key, changedProp.Value);
			}

			var sql = sb.ToString().Remove(sb.Length - 1);
			sql += string.Format(" where {0} = '{1}'", this.PkName, this.PkValue);

			if (!string.IsNullOrEmpty(@where))
				sql += " and " + @where;

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
			return string.Format("delete from {0} where {1} = '{2}'", this.TableName, this.PkName, this.PkValue);
		}

		public override string GetDelSQL(object v, string @where = "")
		{
			string sql = string.Format("delete from {0} where 1 = 1 ", this.TableName);
         
			if (v != null)
				sql += "and " + this.PkName + " = '" + v + "'";
         
			if (!string.IsNullOrEmpty(@where))
				sql += "and " + @where;

			return sql;
		}

		public override string GetModelSQL(object v)
		{
			return string.Format("select * from {0} where {1} = '{2}'", this.TableName, this.PkName, v);
		}

		public override string GetQuerySQL(string @where = "")
		{
			var sql = string.Format("select * from {0} where 1 = 1 ", this.TableName);
           
			if (!string.IsNullOrEmpty(@where))
				sql += "and " + @where;

			return sql;
		}

		#endregion
	}
}