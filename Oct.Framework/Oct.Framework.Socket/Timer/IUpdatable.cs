namespace Oct.Framework.Socket.Timer
{
    /// <summary>
    /// ��ʱ���нӿ�
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// Ψһ��ʾ
        /// </summary>
        int Id { get; }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        bool IsRunning { get; set; }

        /// <summary>
        /// ���¶���
        /// </summary>
        /// <param name="dt">�������һ�θ��µ�ʱ��</param>
        void Update(float dt);
    }
}