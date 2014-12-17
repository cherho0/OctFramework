using System;

namespace Oct.Framework.Socket.Common
{
    public class BasicCfg
    {
        //连接/断开日志
        public bool WatchConnectAndDisconnect { get; set; }

        //查看通讯数据       
        public bool WatchData { get; set; }

        //查看线程
        public bool WatchThreads { get; set; }

        //最大连接数
        public Int32 MaxNumberOfConnections { get; set; }

        public string IP { get; set; }

        //通讯端口
        public Int32 Port { get; set; }

        //Buffer大小
        //byte
        public Int32 BufferSize { get; set; }

        /// <summary>
        /// 包头大小
        /// </summary>
        public int HeadSize { get; set; }

        //异步同时处理请求数
        public Int32 MaxSimultaneousAcceptOps { get; set; }

        //排队大小
        public Int32 Backlog { get; set; }

        /// <summary>
        /// Buffer分区 1 receive 2 send
        /// </summary>
        public Int32 OpsToPreAlloc { get; set; }

        //允许超过对象池个数
        public Int32 ExcessSaeaObjectsInPool { get; set; }

        /// <summary>
        /// 包头前缀长度
        /// </summary>
        public Int32 ReceivePrefixLength { get; set; }

        public Int32 SendPrefixLength { get; set; }

        //延迟时间
        public Int32 MSDelayAfterGettingMessage { get; set; }

        public Int32 UpdateFrequency { get; set; }

        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        public string ConnHost { get; set; }

        /// <summary>
        /// 连接端口
        /// </summary>
        public int ConnPort { get; set; }

        public string DBName { get; set; }

        /// <summary>
        /// 大数据包缓冲区个数
        /// </summary>
        public int BigBufferCount { get; set; }

        /// <summary>
        /// 消息中参数个数
        /// </summary>
        public int BigArgsCount { get; set; }

        /// <summary>
        /// 延迟拼包个数
        /// </summary>
        public int SendDelayCount { get; set; }
    }
}