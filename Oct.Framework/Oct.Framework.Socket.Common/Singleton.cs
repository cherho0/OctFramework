namespace Oct.Framework.Socket.Common
{
    public class Singleton<T> where T : new()
    {
        private static T _current;

        protected Singleton()
        {
        }

        public static T Current
        {
            get
            {
                if (_current == null)
                    _current = new T();
                return _current;
            }
        }
    }
}
