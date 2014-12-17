using System;

namespace Oct.Framework.Core.Args
{
    public class DataEventArgs<TArg1, TArg2> : EventArgs
    {
        private TArg1 _arg1;
        private TArg2 _arg2;

        public DataEventArgs(TArg1 arg1, TArg2 arg2)
        {
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public TArg1 Arg1
        {
            get { return _arg1; }
            set { _arg1 = value; }
        }

        public TArg2 Arg2
        {
            get { return _arg2; }
            set { _arg2 = value; }
        }

        #region Destroyer Members

        public void Destroy()
        {
            Arg1 = default(TArg1);
            Arg2 = default(TArg2);
        }
        #endregion
    }
}