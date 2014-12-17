using System;
using System.Collections.Generic;
using System.Data;
using Oct.Framework.DB.Base;

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
            if (row["MenuName"] != null)
            {
                this.MenuName = row["MenuName"].ToString();
            }

            return this;
        }

        public override bool IsIdentityPk
        {
            get { return false; }
        }

        public override List<string> Props
        {
            get { return null; }
        }

        public override string TableName
        {
            get
            {
                return "CommonMenuActions";
            }
        }

        public override string GetQuerySQL(string @where = "")
        {
            var sql = @" select a.*,b.Name MenuName from Common_ActionInfo a
left join Common_MenuInfo b on a.MenuId = b.Id where 1=1";

            if (!string.IsNullOrEmpty(@where))
                sql += "and " + @where;

            return sql;
        }

        #endregion
    }
}