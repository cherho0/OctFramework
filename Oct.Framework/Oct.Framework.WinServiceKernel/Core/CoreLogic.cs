using Oct.Framework.WinServiceKernel.Interfaces;

namespace Oct.Framework.WinServiceKernel.Core
{
    public abstract class CoreLogic : IServise
    {
        private Kernel _knl;

        public abstract string Name { get; }

        internal void Ctor(Kernel knl)
        {
            _knl = knl;
        }

        internal void Load()
        {
            OnLoad(_knl);
        }

        internal void Close()
        {
            OnClose(_knl);
        }

        protected abstract void OnClose(Kernel knl);

        protected abstract void OnLoad(Kernel knl);

        public abstract void Cmd(string cmd);

        public virtual string ShowMsg()
        {
            return "";
        }

        
    }
}
