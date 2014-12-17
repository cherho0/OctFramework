using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.Entities.Entities
{
	[Serializable]
	public partial class CommonRoleInfo : BaseEntity<CommonRoleInfo>
	{ 
		#region	属性
		
		private Guid _id;

		/// <summary>
		/// 
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
		
		private string _name;

		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this.PropChanged("Name", this._name, value);

				this._name = value;
			}
		}
		
		private string _code;

		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			get
			{
				return this._code;
			}
			set
			{
				this.PropChanged("Code", this._code, value);

				this._code = value;
			}
		}
		
		private bool _isEnable;

		/// <summary>
		/// 
		/// </summary>
		public bool IsEnable
		{
			get
			{
				return this._isEnable;
			}
			set
			{
				this.PropChanged("IsEnable", this._isEnable, value);

				this._isEnable = value;
			}
		}
		
		private bool _isSysDefault;

		/// <summary>
		/// 是否系统默认角色
		/// </summary>
		public bool IsSysDefault
		{
			get
			{
				return this._isSysDefault;
			}
			set
			{
				this.PropChanged("IsSysDefault", this._isSysDefault, value);

				this._isSysDefault = value;
			}
		}
		
		private DateTime _createDate;

		/// <summary>
		/// 
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
		
		private DateTime? _modifyDate;

		/// <summary>
		/// 
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

		
		public override List<string> Props
		{
			get
			{
				return new List<string>();
			}
		}

		public override CommonRoleInfo GetEntityFromDataRow(DataRow row)
		{
			if (row["Id"] != null && row["Id"].ToString() != "")
			{
				this.Id = new Guid(row["Id"].ToString());
			}
			if (row["Name"] != null)
			{
				this.Name = row["Name"].ToString();
			}
			if (row["Code"] != null)
			{
				this.Code = row["Code"].ToString();
			}
			if (row["IsEnable"] != null && row["IsEnable"].ToString() != "")
			{
				if ((row["IsEnable"].ToString() == "1") || (row["IsEnable"].ToString().ToLower() == "true"))
				{
					this.IsEnable = true;
				}
				else
				{
					this.IsEnable = false;
				}
			}
			if (row["IsSysDefault"] != null && row["IsSysDefault"].ToString() != "")
			{
				if ((row["IsSysDefault"].ToString() == "1") || (row["IsSysDefault"].ToString().ToLower() == "true"))
				{
					this.IsSysDefault = true;
				}
				else
				{
					this.IsSysDefault = false;
				}
			}
			if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
			{
				this.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
			}
			if (row["ModifyDate"] != null && row["ModifyDate"].ToString() != "")
			{
				this.ModifyDate = DateTime.Parse(row["ModifyDate"].ToString());
			}

			return this;
		}

		public override string TableName
		{
			get
			{
				return "Common_RoleInfo";
			}
		}

		public override IOctDbCommand GetInsertCmd()
		{
			var sql = @"
				INSERT INTO Common_RoleInfo (
					Id,
					Name,
					Code,
					IsEnable,
					IsSysDefault,
					CreateDate,
					ModifyDate)
				VALUES (
					@Id,
					@Name,
					@Code,
					@IsEnable,
					@IsSysDefault,
					@CreateDate,
					@ModifyDate)";
			
			DbCommand cmd = new SqlCommand();
			var parameters = new Dictionary<string, object> {
				{"@Id", this.Id},
				{"@Name", this.Name},
				{"@Code", this.Code},
				{"@IsEnable", this.IsEnable},
				{"@IsSysDefault", this.IsSysDefault},
				{"@CreateDate", this.CreateDate},
				{"@ModifyDate", this.ModifyDate}};

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