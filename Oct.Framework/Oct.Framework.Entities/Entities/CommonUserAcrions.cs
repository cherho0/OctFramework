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

        #endregion

        #region 重载

        public override CommonUserAcrions GetEntityFromDataRow(DataRow row)
        {
            if (row["MenuName"] != null)
            {
                this.MenuName = row["MenuName"].ToString();
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
            if (row["Operation"] != null && row["Operation"].ToString() != "")
            {
                this.Operation = int.Parse(row["Operation"].ToString());
            }
            if (row["Sort"] != null && row["Sort"].ToString() != "")
            {
                this.Sort = int.Parse(row["Sort"].ToString());
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

        public override List<string> Props
        {
            get { return null; }
        }

        public override string GetQuerySQL(string @where = "")
        {
            var sql = @" select
	m.Name MenuName,
	a.CategoryName,
	a.Name,
	a.Url, 
	a.IsLog, 
	a.IsVisible, 
	a.Operation, 
	a.Sort
from 
Common_UserRole ur
	inner join Common_RoleAction ra on ur.RoleId = ra.RoleId
	inner join Common_ActionInfo a on ra.ActionId = a.Id
	inner join Common_MenuInfo m on a.MenuId = m.Id
where 1=1 and m.IsEnable = 'true'
	and a.IsEnable = 'true'
";

            if (!string.IsNullOrEmpty(@where))
                sql += "and " + @where;

            return sql;
        }

        #endregion
    }
}