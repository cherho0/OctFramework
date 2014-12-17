namespace Oct.Framework.Socket.Timer
{
    public class TimerHelper
    {
        #region Fields

        private static int _id;

        #endregion

        #region Properties

        public static int Id
        {
            get { return _id++; }
        }

        #endregion
    }
}