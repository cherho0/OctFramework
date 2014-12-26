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
    public partial class CommonMenuActions : BaseEntity<CommonMenuActions>
    {
        #region	属性

        public Guid Id
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

        public bool IsEnable
        {
            get;
            set;
        }

        public bool IsVisible
        {
            get;
            set;
        }

        public bool IsLog
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

        public DateTime CreateDate
        {
            get;
            set;
        }

        public DateTime ModifyDate
        {
            get;
            set;
        }

        public Guid MenuId
        {
            get;
            set;
        }

        public string MenuName
        {
            get;
            set;
        }

        #endregion

        #region 重载

        public override CommonMenuActions GetEntityFromDataRow(DataRow row)
        {
            if (row.Table.Columns.Contains("Id") && row["Id"] != null && row["Id"].ToString() != "")
            {
                this.Id = new Guid(row["Id"].ToString());
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
            if (row.Table.Columns.Contains("IsEnable") && row["IsEnable"] != null && row["IsEnable"].ToString() != "")
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
            if (row.Table.Columns.Contains("Operation") && row["Operation"] != null && row["Operation"].ToString() != "")
            {
                this.Operation = int.Parse(row["Operation"].ToString());
            }
            if (row.Table.Columns.Contains("Sort") && row["Sort"] != null && row["Sort"].ToString() != "")
            {
                this.Sort = int.Parse(row["Sort"].ToString());
            }
            if (row.Table.Columns.Contains("CreateDate") && row["CreateDate"] != null && row["CreateDate"].ToString() != "")
            {
                this.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
            }
            if (row.Table.Columns.Contains("ModifyDate") && row["ModifyDate"] != null && row["ModifyDate"].ToString() != "")
            {
                this.ModifyDate = DateTime.Parse(row["ModifyDate"].ToString());
            }
            if (row.Table.Columns.Contains("MenuId") && row["MenuId"] != null && row["MenuId"].ToString() != "")
            {
                this.MenuId = new Guid(row["MenuId"].ToString());
            }
            if (row.Table.Columns.Contains("MenuName") && row["MenuName"] != null)
            {
                this.MenuName = row["MenuName"].ToString();
            }

            return this;
        }

        public override CommonMenuActions GetEntityFromDataReader(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                if (name.ToLower() == "id" && !reader.IsDBNull(i))
                {
                    Id = reader.GetGuid(i);
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
                if (name.ToLower() == "isenable" && !reader.IsDBNull(i))
                {
                    IsEnable = reader.GetBoolean(i);
                    continue;
                }
                if (name.ToLower() == "isvisible" && !reader.IsDBNull(i))
                {
                    IsVisible = reader.GetBoolean(i);
                    continue;
                }
                if (name.ToLower() == "islog" && !reader.IsDBNull(i))
                {
                    IsLog = reader.GetBoolean(i);
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
                if (name.ToLower() == "createdate" && !reader.IsDBNull(i))
                {
                    CreateDate = reader.GetDateTime(i);
                    continue;
                }
                if (name.ToLower() == "modifydate" && !reader.IsDBNull(i))
                {
                    ModifyDate = reader.GetDateTime(i);
                    continue;
                }
                if (name.ToLower() == "menuid" && !reader.IsDBNull(i))
                {
                    MenuId = reader.GetGuid(i);
                    continue;
                }
                if (name.ToLower() == "menuname" && !reader.IsDBNull(i))
                {
                    MenuName = reader.GetString(i);
                    continue;
                }

            }
            return this;
        }

        public override string TableName
        {
            get
            {
                return "CommonMenuActions";
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
                    _props.Add("Id", "Id");
                    _props.Add("CategoryName", "CategoryName");
                    _props.Add("Name", "Name");
                    _props.Add("Url", "Url");
                    _props.Add("IsEnable", "IsEnable");
                    _props.Add("IsVisible", "IsVisible");
                    _props.Add("IsLog", "IsLog");
                    _props.Add("Operation", "Operation");
                    _props.Add("Sort", "Sort");
                    _props.Add("CreateDate", "CreateDate");
                    _props.Add("ModifyDate", "ModifyDate");
                    _props.Add("MenuId", "MenuId");
                    _props.Add("MenuName", "MenuName");
                }
                return _props;
            }
        }

        public override string GetQuerySQL(string @where = "")
        {
            var sql = @"select a.*,b.Name MenuName from Common_ActionInfo a
left join Common_MenuInfo b on a.MenuId = b.Id where 1=1 ";

            if (!string.IsNullOrEmpty(@where))
                sql += "and " + @where;

            return sql;
        }

        #endregion
    }
}