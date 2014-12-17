namespace Oct.Framework.Socket.Timer
{
    /// <summary>
    /// 定时更行接口
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// 唯一标示
        /// </summary>
        int Id { get; }

        /// <summary>
        /// 是否在运行
        /// </summary>
        bool IsRunning { get; set; }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="dt">距离最后一次更新的时间</param>
        void Update(float dt);
    }
}