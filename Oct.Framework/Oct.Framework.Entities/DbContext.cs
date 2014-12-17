using Oct.Framework.Core;
using Oct.Framework.Core.IOC;
using Oct.Framework.DB;
using Oct.Framework.DB.Core;
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
                return Bootstrapper.GetRepository<IDBContext<TestTs>>();
            }
        }

        public IDBContext<UcUserMsg> UserMsgContext
        {
            get
            {
                return Bootstrapper.GetRepository<IDBContext<UcUserMsg>>();
            }
        }



        public IDBContext<CommonUserRole> CommonUserRoleContext
        {
            get
            {
                return Bootstrapper.GetRepository<IDBContext<CommonUserRole>>();
            }
        }

        public IDBContext<CommonUser> CommonUserContext
        {
            get
            {
                return Bootstrapper.GetRepository<IDBContext<CommonUser>>();
            }
        }

        public IDBContext<CommonRoleInfo> CommonRoleInfoContext
        {
            get
            {
                return Bootstrapper.GetRepository<IDBContext<CommonRoleInfo>>();
            }
        }

        public IDBContext<CommonRoleAction> CommonRoleActionContext
        {
            get
            {
                return Bootstrapper.GetRepository<IDBContext<CommonRoleAction>>();
            }
        }

        public IDBContext<CommonMenuActions> CommonMenuActionsContext
        {
            get
            {
                return Bootstrapper.GetRepository<IDBContext<CommonMenuActions>>();
            }
        }

        public IDBContext<CommonMenuInfo> CommonMenuInfoContext
        {
            get
            {
                return Bootstrapper.GetRepository<IDBContext<CommonMenuInfo>>();
            }
        }

        public IDBContext<CommonActionInfo> CommonActionInfoContext
        {
            get
            {
                return Bootstrapper.GetRepository<IDBContext<CommonActionInfo>>();
            }
        }

        public IDBContext<CommonUserAcrions> CommonUserAcrionsContext
        {
            get
            {
                return Bootstrapper.GetRepository<IDBContext<CommonUserAcrions>>();
            }
        }
    }
}
