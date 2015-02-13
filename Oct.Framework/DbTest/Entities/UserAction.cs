using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.DB.Attrbuites;
using Oct.Framework.DB.Base;

namespace DbTest.Entities
{
    [Entity(@"select distinct 
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
	and a.IsEnable = 'true' ",true)]
    public class UserAction : BaseEntity<UserAction>
    {
        #region	属性
        [PrimaryKey]
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
    }
}
