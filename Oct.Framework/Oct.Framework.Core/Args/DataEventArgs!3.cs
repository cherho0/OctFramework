using System;

namespace Oct.Framework.Core.Args
{
    public class DataEventArgs<TArg1, TArg2, TArg3> : EventArgs
    {
        public DataEventArgs(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            Arg1 = arg1;
            Arg2 = arg2;
            Arg3 = arg3;
        }

        public TArg1 Arg1 { get; private set; }
        public TArg2 Arg2 { get; private set; }
        public TArg3 Arg3 { get; private set; }

        #region IObjDestroyer Members

        public void Destroy()
        {
            Arg1 = default(TArg1);
            Arg2 = default(TArg2);
            Arg3 = default(TArg3);
        }

        #endregion
    }
}