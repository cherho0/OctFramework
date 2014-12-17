namespace Oct.Framework.Core.Common
{
    public class SingleTon<T> where T : class, new()
    {
        private static T _instance;
        protected SingleTon() { }

        private static object o = new object();

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

    }
}
