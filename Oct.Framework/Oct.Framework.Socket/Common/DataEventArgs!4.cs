using System;

namespace Oct.Framework.Socket.Common
{
    public class DataEventArgs<TArg1, TArg2, TArg3, TArg4> : EventArgs
    {
        public TArg1 Arg1 { get; private set; }
        public TArg2 Arg2 { get; private set; }
        public TArg3 Arg3 { get; private set; }
        public TArg4 Arg4 { get; private set; }

        public DataEventArgs(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            Arg1 = arg1;
            Arg2 = arg2;
            Arg3 = arg3;
            Arg4 = arg4;
        }

        #region Members
        public void Destroy()
        {
            Arg1 = default(TArg1);
            Arg2 = default(TArg2);
            Arg3 = default(TArg3);
            Arg4 = default(TArg4);
        }
        #endregion
    }
}