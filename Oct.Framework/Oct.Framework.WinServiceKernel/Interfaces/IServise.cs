namespace Oct.Framework.WinServiceKernel.Interfaces
{
    public interface IServise
    {
        string Name { get; }

        void Cmd(string cmd);
    }
}
