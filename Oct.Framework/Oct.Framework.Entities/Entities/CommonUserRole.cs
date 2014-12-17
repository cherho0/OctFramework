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
	public partial class CommonUserRole : BaseEntity<CommonUserRole>
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
		
		private Guid _userId;

		/// <summary>
		/// 
		/// </summary>
		public Guid UserId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this.PropChanged("UserId", this._userId, value);

				this._userId = value;
			}
		}
		
		private Guid _roleId;

		/// <summary>
		/// 
		/// </summary>
		public Guid RoleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this.PropChanged("RoleId", this._roleId, value);

				this._roleId = value;
			}
		}
		
		private DateTime? _createDate;

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
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

		public override CommonUserRole GetEntityFromDataRow(DataRow row)
		{
			if (row["Id"] != null && row["Id"].ToString() != "")
			{
				this.Id = new Guid(row["Id"].ToString());
			}
			if (row["UserId"] != null && row["UserId"].ToString() != "")
			{
				this.UserId = new Guid(row["UserId"].ToString());
			}
			if (row["RoleId"] != null && row["RoleId"].ToString() != "")
			{
				this.RoleId = new Guid(row["RoleId"].ToString());
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
				return "Common_UserRole";
			}
		}

		public override IOctDbCommand GetInsertCmd()
		{
			var sql = @"
				INSERT INTO Common_UserRole (
					Id,
					UserId,
					RoleId,
					CreateDate,
					ModifyDate)
				VALUES (
					@Id,
					@UserId,
					@RoleId,
					@CreateDate,
					@ModifyDate)";
			
			DbCommand cmd = new SqlCommand();
			var parameters = new Dictionary<string, object> {
				{"@Id", this.Id},
				{"@UserId", this.UserId},
				{"@RoleId", this.RoleId},
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