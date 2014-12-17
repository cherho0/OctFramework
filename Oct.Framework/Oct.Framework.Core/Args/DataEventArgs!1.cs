using System;

namespace Oct.Framework.Core.Args
{
    public class DataEventArgs<T> : EventArgs
    {
        private T _arg;

        public DataEventArgs(T arg)
        {
            _arg = arg;
        }

        public T Arg1
        {
            get { return _arg; }
            set { _arg = value; }
        }

        #region IObjDestroyer Members

        public void Destroy()
        {
            _arg = default(T);
        }

        #endregion
    }
}