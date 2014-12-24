using Oct.Framework.Core;
using Oct.Framework.Core.IOC;
using Oct.Framework.DB;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Implementation;
using Oct.Framework.DB.Interface;
using Oct.Framework.Entities.Entities;

namespace Oct.Framework.Entities
{
    public class DbContext : DBContextBase
    {
        public DbContext()
            : base()
        {

        }

        public DbContext(string connectionString)
            : base(connectionString)
        {

        }

        public DbContext(DBOperationType dbType)
            : base(dbType)
        {

        }

        public IDBContext<TestTs> TestTsContext
        {
            get
            {
                return new SQLDBContext<TestTs>(Session);
            }
        }

        public IDBContext<UcUserMsg> UserMsgContext
        {
            get
            {
                return new SQLDBContext<UcUserMsg>(Session);
            }
        }

        public IDBContext<CommonUserRole> CommonUserRoleContext
        {
            get
            {
                return new SQLDBContext<CommonUserRole>(Session);
            }
        }

        public IDBContext<CommonUser> CommonUserContext
        {
            get
            {
                return new SQLDBContext<CommonUser>(Session);
            }
        }

        public IDBContext<CommonRoleInfo> CommonRoleInfoContext
        {
            get
            {
                return new SQLDBContext<CommonRoleInfo>(Session);
            }
        }

        public IDBContext<CommonRoleAction> CommonRoleActionContext
        {
            get
            {
                return new SQLDBContext<CommonRoleAction>(Session);
            }
        }

        public IDBContext<CommonMenuActions> CommonMenuActionsContext
        {
            get
            {
                return new SQLDBContext<CommonMenuActions>(Session);
            }
        }

        public IDBContext<CommonMenuInfo> CommonMenuInfoContext
        {
            get
            {
                return new SQLDBContext<CommonMenuInfo>(Session);
            }
        }

        public IDBContext<CommonActionInfo> CommonActionInfoContext
        {
            get
            {
                return new SQLDBContext<CommonActionInfo>(Session);
            }
        }

        public IDBContext<CommonUserAcrions> CommonUserAcrionsContext
        {
            get
            {
                return new SQLDBContext<CommonUserAcrions>(Session);
            }
        }
    }
}
