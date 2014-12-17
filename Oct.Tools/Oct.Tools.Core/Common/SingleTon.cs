namespace Oct.Tools.Core.Common
{
    public class SingleTon<T> where T : class, new()
    {
        #region 变量

        private static T _instance;
        private static object o = new object();

        #endregion

        #region 属性

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (o)
                    {
                        _instance = new T();
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region 构造函数

        protected SingleTon()
        {

        }

        #endregion
    }
}
