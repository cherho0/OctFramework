using System;
using System.Threading;
using NyMQ.Core.Socket;

namespace Oct.Framework.Socket.Socket
{
    /// <summary>
    /// 在一个线程当中处理一次处理所有消息并分发消息
    /// </summary>
    public interface IContextHandler
    {
        /// <summary>
        /// 当前线程是否属于该上下文
        /// </summary>
        bool IsInContext { get; }

        void AddMessage(IMessage message);

        void AddMessage(Action action);

        /// <summary>
        /// 在当前上下文执行
        /// </summary>
        bool ExecuteInContext(Action action);

        void EnsureContext();
    }

    public static class ContextUtil
    {
        /// <summary>
        /// Lets the given ContextHandler wait one Tick. Does nothing if within the given Handler's Context.
        /// </summary>
        /// <param name="contextHandler"></param>
        public static void WaitOne(this IContextHandler contextHandler)
        {
            var obj = new Object();

            if (!contextHandler.IsInContext)
            {
                lock (obj)
                {
                    contextHandler.AddMessage(() =>
                                                  {
                                                      lock (obj)
                                                      {
                                                          Monitor.PulseAll(obj);
                                                      }
                                                  });

                    Monitor.Wait(obj);
                }
            }
        }
    }
}