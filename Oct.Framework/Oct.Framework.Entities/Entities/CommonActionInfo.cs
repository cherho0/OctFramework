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
	public partial class CommonActionInfo : BaseEntity<CommonActionInfo>
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
		
		private string _categoryName;

		/// <summary>
		/// 分类名称
		/// </summary>
		public string CategoryName
		{
			get
			{
				return this._categoryName;
			}
			set
			{
				this.PropChanged("CategoryName", this._categoryName, value);

				this._categoryName = value;
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
		
		private string _url;

		/// <summary>
		/// 
		/// </summary>
		public string Url
		{
			get
			{
				return this._url;
			}
			set
			{
				this.PropChanged("Url", this._url, value);

				this._url = value;
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
		
		private bool _isVisible;

		/// <summary>
		/// 
		/// </summary>
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				this.PropChanged("IsVisible", this._isVisible, value);

				this._isVisible = value;
			}
		}
		
		private bool _isLog;

		/// <summary>
		/// 
		/// </summary>
		public bool IsLog
		{
			get
			{
				return this._isLog;
			}
			set
			{
				this.PropChanged("IsLog", this._isLog, value);

				this._isLog = value;
			}
		}
		
		private int _operation;

		/// <summary>
		/// //浏览        View =1,        //更新        Update =2,        //删除        Delete =3,        //增加        Add =4,        //导出        Export =5,        //导入        Import = 6,
		/// </summary>
		public int Operation
		{
			get
			{
				return this._operation;
			}
			set
			{
				this.PropChanged("Operation", this._operation, value);

				this._operation = value;
			}
		}
		
		private int _sort;

		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			get
			{
				return this._sort;
			}
			set
			{
				this.PropChanged("Sort", this._sort, value);

				this._sort = value;
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
		
		private Guid _menuId;

		/// <summary>
		/// 
		/// </summary>
		public Guid MenuId
		{
			get
			{
				return this._menuId;
			}
			set
			{
				this.PropChanged("MenuId", this._menuId, value);

				this._menuId = value;
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

		public override CommonActionInfo GetEntityFromDataRow(DataRow row)
		{
			if (row["Id"] != null && row["Id"].ToString() != "")
			{
				this.Id = new Guid(row["Id"].ToString());
			}
			if (row["CategoryName"] != null)
			{
				this.CategoryName = row["CategoryName"].ToString();
			}
			if (row["Name"] != null)
			{
				this.Name = row["Name"].ToString();
			}
			if (row["Url"] != null)
			{
				this.Url = row["Url"].ToString();
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
			if (row["IsVisible"] != null && row["IsVisible"].ToString() != "")
			{
				if ((row["IsVisible"].ToString() == "1") || (row["IsVisible"].ToString().ToLower() == "true"))
				{
					this.IsVisible = true;
				}
				else
				{
					this.IsVisible = false;
				}
			}
			if (row["IsLog"] != null && row["IsLog"].ToString() != "")
			{
				if ((row["IsLog"].ToString() == "1") || (row["IsLog"].ToString().ToLower() == "true"))
				{
					this.IsLog = true;
				}
				else
				{
					this.IsLog = false;
				}
			}
			if (row["Operation"] != null && row["Operation"].ToString() != "")
			{
				this.Operation = int.Parse(row["Operation"].ToString());
			}
			if (row["Sort"] != null && row["Sort"].ToString() != "")
			{
				this.Sort = int.Parse(row["Sort"].ToString());
			}
			if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
			{
				this.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
			}
			if (row["ModifyDate"] != null && row["ModifyDate"].ToString() != "")
			{
				this.ModifyDate = DateTime.Parse(row["ModifyDate"].ToString());
			}
			if (row["MenuId"] != null && row["MenuId"].ToString() != "")
			{
				this.MenuId = new Guid(row["MenuId"].ToString());
			}

			return this;
		}

		public override string TableName
		{
			get
			{
				return "Common_ActionInfo";
			}
		}

		public override IOctDbCommand GetInsertCmd()
		{
			var sql = @"
				INSERT INTO Common_ActionInfo (
					Id,
					CategoryName,
					Name,
					Url,
					IsEnable,
					IsVisible,
					IsLog,
					Operation,
					Sort,
					CreateDate,
					ModifyDate,
					MenuId)
				VALUES (
					@Id,
					@CategoryName,
					@Name,
					@Url,
					@IsEnable,
					@IsVisible,
					@IsLog,
					@Operation,
					@Sort,
					@CreateDate,
					@ModifyDate,
					@MenuId)";
			
			DbCommand cmd = new SqlCommand();
			var parameters = new Dictionary<string, object> {
				{"@Id", this.Id},
				{"@CategoryName", this.CategoryName},
				{"@Name", this.Name},
				{"@Url", this.Url},
				{"@IsEnable", this.IsEnable},
				{"@IsVisible", this.IsVisible},
				{"@IsLog", this.IsLog},
				{"@Operation", this.Operation},
				{"@Sort", this.Sort},
				{"@CreateDate", this.CreateDate},
				{"@ModifyDate", this.ModifyDate},
				{"@MenuId", this.MenuId}};

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