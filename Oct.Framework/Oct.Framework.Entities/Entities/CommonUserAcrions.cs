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
    public partial class CommonUserAcrions : BaseEntity<CommonUserAcrions>
    {
        #region	属性

        public string MenuName
        {
            get;
            set;
        }

        public string CategoryName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public bool IsLog
        {
            get;
            set;
        }

        public bool IsVisible
        {
            get;
            set;
        }

        public int Operation
        {
            get;
            set;
        }

        public int Sort
        {
            get;
            set;
        }

        public int msort
        {
            get;
            set;
        }

        #endregion

        #region 重载

        public override CommonUserAcrions GetEntityFromDataRow(DataRow row)
        {
            if (row.Table.Columns.Contains("MenuName") && row["MenuName"] != null)
            {
                this.MenuName = row["MenuName"].ToString();
            }
            if (row.Table.Columns.Contains("CategoryName") && row["CategoryName"] != null)
            {
                this.CategoryName = row["CategoryName"].ToString();
            }
            if (row.Table.Columns.Contains("Name") && row["Name"] != null)
            {
                this.Name = row["Name"].ToString();
            }
            if (row.Table.Columns.Contains("Url") && row["Url"] != null)
            {
                this.Url = row["Url"].ToString();
            }
            if (row.Table.Columns.Contains("IsLog") && row["IsLog"] != null && row["IsLog"].ToString() != "")
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
            if (row.Table.Columns.Contains("IsVisible") && row["IsVisible"] != null && row["IsVisible"].ToString() != "")
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
            if (row.Table.Columns.Contains("Operation") && row["Operation"] != null && row["Operation"].ToString() != "")
            {
                this.Operation = int.Parse(row["Operation"].ToString());
            }
            if (row.Table.Columns.Contains("Sort") && row["Sort"] != null && row["Sort"].ToString() != "")
            {
                this.Sort = int.Parse(row["Sort"].ToString());
            }
            if (row.Table.Columns.Contains("msort") && row["msort"] != null && row["msort"].ToString() != "")
            {
                this.msort = int.Parse(row["msort"].ToString());
            }

            return this;
        }

        public override CommonUserAcrions GetEntityFromDataReader(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                if (name.ToLower() == "menuname" && !reader.IsDBNull(i))
                {
                    MenuName = reader.GetString(i);
                    continue;
                }
                if (name.ToLower() == "categoryname" && !reader.IsDBNull(i))
                {
                    CategoryName = reader.GetString(i);
                    continue;
                }
                if (name.ToLower() == "name" && !reader.IsDBNull(i))
                {
                    Name = reader.GetString(i);
                    continue;
                }
                if (name.ToLower() == "url" && !reader.IsDBNull(i))
                {
                    Url = reader.GetString(i);
                    continue;
                }
                if (name.ToLower() == "islog" && !reader.IsDBNull(i))
                {
                    IsLog = reader.GetBoolean(i);
                    continue;
                }
                if (name.ToLower() == "isvisible" && !reader.IsDBNull(i))
                {
                    IsVisible = reader.GetBoolean(i);
                    continue;
                }
                if (name.ToLower() == "operation" && !reader.IsDBNull(i))
                {
                    Operation = reader.GetInt32(i);
                    continue;
                }
                if (name.ToLower() == "sort" && !reader.IsDBNull(i))
                {
                    Sort = reader.GetInt32(i);
                    continue;
                }
                if (name.ToLower() == "msort" && !reader.IsDBNull(i))
                {
                    msort = reader.GetInt32(i);
                    continue;
                }

            }
            return this;
        }

        public override string TableName
        {
            get
            {
                return "CommonUserAcrions";
            }
        }

        public override bool IsIdentityPk
        {
            get { return false; }
        }

        private Dictionary<string, string> _props;

        public override Dictionary<string, string> Props
        {
            get
            {
                if (_props == null)
                {
                    _props = new Dictionary<string, string>();
                    _props.Add("MenuName", "MenuName");
                    _props.Add("CategoryName", "CategoryName");
                    _props.Add("Name", "Name");
                    _props.Add("Url", "Url");
                    _props.Add("IsLog", "IsLog");
                    _props.Add("IsVisible", "IsVisible");
                    _props.Add("Operation", "Operation");
                    _props.Add("Sort", "Sort");
                    _props.Add("msort", "msort");
                }
                return _props;
            }
        }

        public override string GetQuerySQL(string @where = "")
        {
            var sql = @"select distinct 
	m.Name MenuName,
	a.CategoryName,
	a.Name,
	a.Url, 
	a.IsLog, 
	a.IsVisible, 
	a.Operation, 
	a.Sort,m.Sort as msort
from 
Common_UserRole ur
	inner join Common_RoleAction ra on ur.RoleId = ra.RoleId
	inner join Common_ActionInfo a on ra.ActionId = a.Id
	inner join Common_MenuInfo m on a.MenuId = m.Id
where m.IsEnable = 'true'
	and a.IsEnable = 'true' ";

            if (!string.IsNullOrEmpty(@where))
                sql += "and " + @where;

            return sql;
        }

        #endregion
    }
}