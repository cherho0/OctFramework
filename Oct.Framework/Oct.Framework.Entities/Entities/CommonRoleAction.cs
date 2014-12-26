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
	public partial class CommonRoleAction : BaseEntity<CommonRoleAction>
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
		
		private Guid _actionId;

		/// <summary>
		/// ActionId
		/// </summary>
		public Guid ActionId
		{
			get
			{
				return this._actionId;
			}
			set
			{
				this.PropChanged("ActionId", this._actionId, value);

				this._actionId = value;
			}
		}
		
		private Guid _roleId;

		/// <summary>
		/// RoleId
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
		/// CreateDate
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
		/// ModifyDate
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

		
		private Dictionary<string, string> _props;

		public override Dictionary<string, string> Props
	    {
	        get {
				if(_props == null)
				{
					_props = new Dictionary<string, string>();
										_props.Add( "Id","Id");
										_props.Add( "ActionId","ActionId");
										_props.Add( "RoleId","RoleId");
										_props.Add( "CreateDate","CreateDate");
										_props.Add( "ModifyDate","ModifyDate");
									}
				return _props;			 
			 }
	    }

		public override CommonRoleAction GetEntityFromDataRow(DataRow row)
		{
			if (row.Table.Columns.Contains("Id") && row["Id"] != null && row["Id"].ToString() != "")
			{
				this._id = new Guid(row["Id"].ToString());
			}
			if (row.Table.Columns.Contains("ActionId") && row["ActionId"] != null && row["ActionId"].ToString() != "")
			{
				this._actionId = new Guid(row["ActionId"].ToString());
			}
			if (row.Table.Columns.Contains("RoleId") && row["RoleId"] != null && row["RoleId"].ToString() != "")
			{
				this._roleId = new Guid(row["RoleId"].ToString());
			}
			if (row.Table.Columns.Contains("CreateDate") && row["CreateDate"] != null && row["CreateDate"].ToString() != "")
			{
				this._createDate = DateTime.Parse(row["CreateDate"].ToString());
			}
			if (row.Table.Columns.Contains("ModifyDate") && row["ModifyDate"] != null && row["ModifyDate"].ToString() != "")
			{
				this._modifyDate = DateTime.Parse(row["ModifyDate"].ToString());
			}

			return this;
		}

		public override CommonRoleAction GetEntityFromDataReader(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
				if (name.ToLower() == "id" && !reader.IsDBNull(i))
{
_id = reader.GetGuid(i);
 continue;
}
if (name.ToLower() == "actionid" && !reader.IsDBNull(i))
{
_actionId = reader.GetGuid(i);
 continue;
}
if (name.ToLower() == "roleid" && !reader.IsDBNull(i))
{
_roleId = reader.GetGuid(i);
 continue;
}
if (name.ToLower() == "createdate" && !reader.IsDBNull(i))
{
_createDate = reader.GetDateTime(i);
 continue;
}
if (name.ToLower() == "modifydate" && !reader.IsDBNull(i))
{
_modifyDate = reader.GetDateTime(i);
 continue;
}
               
}
            return this;
        }

		public override string TableName
		{
			get
			{
				return "Common_RoleAction";
			}
		}

		public override IOctDbCommand GetInsertCmd()
		{
			var sql = @"
				INSERT INTO Common_RoleAction (
					Id,
					ActionId,
					RoleId,
					CreateDate,
					ModifyDate)
				VALUES (
					@Id,
					@ActionId,
					@RoleId,
					@CreateDate,
					@ModifyDate)";
			
			DbCommand cmd = new SqlCommand();
			var parameters = new Dictionary<string, object> {
				{"@Id", this.Id},
				{"@ActionId", this.ActionId},
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