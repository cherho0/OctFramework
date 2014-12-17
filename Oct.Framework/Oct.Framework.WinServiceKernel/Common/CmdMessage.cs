namespace Oct.Framework.WinServiceKernel.Common
{
    public class CmdMessage
    {
        private string _msg;

        public string Msg
        {
            get { return _msg; }
        }

        internal CmdMessage(string cmd)
        {
            _msg = cmd;
        }
    }
}
