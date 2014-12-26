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
	public partial class TestTs : BaseEntity<TestTs>
	{ 
		#region	属性
		
		private int _id;

		/// <summary>
		/// Id
		/// </summary>
		public int Id
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
		
		private string _dD;

		/// <summary>
		/// DD
		/// </summary>
		public string DD
		{
			get
			{
				return this._dD;
			}
			set
			{
				this.PropChanged("DD", this._dD, value);

				this._dD = value;
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
				return true; 
			}
		}

				public override void SetIdentity(object v)
		{
			this.Id = int.Parse(v.ToString());
		}
		
		private Dictionary<string, string> _props;

		public override Dictionary<string, string> Props
	    {
	        get {
				if(_props == null)
				{
					_props = new Dictionary<string, string>();
										_props.Add( "Id","Id");
										_props.Add( "DD","DD");
									}
				return _props;			 
			 }
	    }

		public override TestTs GetEntityFromDataRow(DataRow row)
		{
			if (row.Table.Columns.Contains("Id") && row["Id"] != null && row["Id"].ToString() != "")
			{
				this._id = int.Parse(row["Id"].ToString());
			}
			if (row.Table.Columns.Contains("DD") && row["DD"] != null)
			{
				this._dD = row["DD"].ToString();
			}

			return this;
		}

		public override TestTs GetEntityFromDataReader(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
				if (name.ToLower() == "id" && !reader.IsDBNull(i))
{
_id = reader.GetInt32(i);
 continue;
}
if (name.ToLower() == "dd" && !reader.IsDBNull(i))
{
_dD = reader.GetString(i);
 continue;
}
               
}
            return this;
        }

		public override string TableName
		{
			get
			{
				return "TestTs";
			}
		}

		public override IOctDbCommand GetInsertCmd()
		{
			var sql = @"
				INSERT INTO TestTs (
					DD)
				VALUES (
					@DD)";
			
			DbCommand cmd = new SqlCommand();
			var parameters = new Dictionary<string, object> {
				{"@DD", this.DD}};

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