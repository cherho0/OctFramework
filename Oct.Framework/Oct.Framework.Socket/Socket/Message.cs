using System;
using System.Threading;

namespace Oct.Framework.Socket.Socket
{
    /// <summary>
    /// 消息接口
    /// </summary>
    public interface IMessage
    {
        void Execute();
    }

    public class Message : IMessage
    {
        public Message()
        {
        }


        public Message(Action callback)
        {
            Callback = callback;
        }

        public Action Callback { get; private set; }

        #region IMessage Members
        //Stopwatch sw = new Stopwatch();
        public virtual void Execute()
        {
            //sw.Restart();
            Action cb = Callback;
            //Csl.Wl(cb.Target.ToString() + "Enter");
            if (cb != null)
            {
                cb();
            }
            //Csl.Wl(cb.Target.ToString() + "Exit");

            //sw.Stop();

            //if (sw.ElapsedMilliseconds > 100)
            //{
            //    Csl.Wl("Msg:" + cb.Target + cb.Method.Name);
            //}
        }

        #endregion

        public static Message Obtain(Action callback)
        {
            return new Message(callback);
        }

        public static implicit operator Message(Action dele)
        {
            return new Message(dele);
        }

        public override string ToString()
        {
            return Callback.ToString();
        }
    }

    public class WaitMessage : Message
    {
        private bool m_executed;

        public override void Execute()
        {
            try
            {
                base.Execute();
            }
            finally
            {
                lock (this)
                {
                    m_executed = true;
                    Monitor.PulseAll(this);
                }
            }
        }

        public void Wait()
        {
            if (!m_executed)
            {
                lock (this)
                {
                    Monitor.Wait(this);
                }
            }
        }
    }

    #region Message1

    public class Message1<T1> : IMessage
    {
        public Message1()
        {
        }

        public Message1(Action<T1> callback)
        {
            Callback = callback;
        }


        public Message1(T1 param1, Action<T1> callback)
        {
            Callback = callback;
            Parameter1 = param1;
        }

        public Action<T1> Callback { get; set; }

        public T1 Parameter1 { get; set; }

        #region IMessage Members
        //Stopwatch sw = new Stopwatch();
        public virtual void Execute()
        {
            //sw.Restart();

            Action<T1> cb = Callback;
            //Csl.Wl(cb.Target.ToString() + "Enter");
            if (cb != null)
            {
                cb(Parameter1);
            }
            //Csl.Wl(cb.Target.ToString() + "Exit");
           // sw.Stop();

            //if (sw.ElapsedMilliseconds > 100)
            //{
            //    Csl.Wl("Msg:" + cb.Target + cb.Method.Name);
            //}
        }

        #endregion

        public static explicit operator Message1<T1>(Action<T1> dele)
        {
            return new Message1<T1>(dele);
        }
    }

    #endregion

    #region Message2

    public class Message2<T1, T2> : IMessage
    {
        public Message2()
        {
        }

        public Message2(Action<T1, T2> callback)
        {
            Callback = callback;
        }

        public Message2(T1 param1, T2 param2, Action<T1, T2> callback)
        {
            Callback = callback;
            Parameter1 = param1;
            Parameter2 = param2;
        }

        public Message2(T1 param1, T2 param2)
        {
            Parameter1 = param1;
            Parameter2 = param2;
        }


        public Action<T1, T2> Callback { get; set; }


        public T1 Parameter1 { get; set; }


        public T2 Parameter2 { get; set; }

        #region IMessage Members

        //Stopwatch sw = new Stopwatch();
        public virtual void Execute()
        {
            //sw.Restart();
            Action<T1, T2> cb = Callback;
            //Csl.Wl(cb.Target.ToString() + Parameter2 + "Enter");
            if (cb != null)
            {
                cb(Parameter1, Parameter2);
                //cb.BeginInvoke(Parameter1, Parameter2, null, null);
            }
            //Csl.Wl(cb.Target.ToString() + "Exit");
            //sw.Stop();

            //if (sw.ElapsedMilliseconds > 1000)
            //{
            //    Csl.Wl("Msg:" + cb.Target + cb.Method.Name);
            //}
        }

        #endregion

        public static explicit operator Message2<T1, T2>(Action<T1, T2> dele)
        {
            return new Message2<T1, T2>(dele);
        }
    }

    #endregion

    #region Message3

    public class Message3<T1, T2, T3> : IMessage
    {
        public Message3()
        {
        }


        public Message3(Action<T1, T2, T3> callback)
        {
            Callback = callback;
        }

        public Message3(T1 param1, T2 param2, T3 param3, Action<T1, T2, T3> callback)
        {
            Callback = callback;
            Parameter1 = param1;
            Parameter2 = param2;
            Parameter3 = param3;
        }


        public Action<T1, T2, T3> Callback { get; set; }


        public T1 Parameter1 { get; set; }


        public T2 Parameter2 { get; set; }


        public T3 Parameter3 { get; set; }

        #region IMessage Members

        //Stopwatch sw = new Stopwatch();
        public virtual void Execute()
        {
            //sw.Restart();
            Action<T1, T2, T3> cb = Callback;
            //Csl.Wl(cb.Target.ToString() + "Enter");
            if (cb != null)
            {
                cb(Parameter1, Parameter2, Parameter3);
            }
            //Csl.Wl(cb.Target.ToString() + "Exit");
            //sw.Stop();

            //if (sw.ElapsedMilliseconds > 1000)
            //{
            //    Csl.Wl("Msg:" + cb.Target + cb.Method.Name);
            //}
        }

        #endregion

        public static explicit operator Message3<T1, T2, T3>(Action<T1, T2, T3> dele)
        {
            return new Message3<T1, T2, T3>(dele);
        }
    }

    #endregion

    #region Message4

    public class Message4<T1, T2, T3, T4> : IMessage
    {
        public Message4()
        {
        }


        public Message4(Action<T1, T2, T3, T4> callback)
        {
            Callback = callback;
        }


        public Message4(Action<T1, T2, T3, T4> callback, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            Callback = callback;
            Parameter1 = param1;
            Parameter2 = param2;
            Parameter3 = param3;
            Parameter4 = param4;
        }


        public Action<T1, T2, T3, T4> Callback { get; set; }


        public T1 Parameter1 { get; set; }


        public T2 Parameter2 { get; set; }


        public T3 Parameter3 { get; set; }


        public T4 Parameter4 { get; set; }

        #region IMessage Members

        //Stopwatch sw = new Stopwatch();
        public virtual void Execute()
        {
            //sw.Restart();
            Action<T1, T2, T3, T4> cb = Callback;
            //Csl.Wl(cb.Target.ToString() + "Enter");
            if (cb != null)
            {
                cb(Parameter1, Parameter2, Parameter3, Parameter4);
            }
            //Csl.Wl(cb.Target.ToString() + "Exit");
            //sw.Stop();

            //if (sw.ElapsedMilliseconds > 1000)
            //{
            //    Csl.Wl("Msg:" + cb.Target + cb.Method.Name);
            //}
        }

        #endregion

        public static explicit operator Message4<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback)
        {
            return new Message4<T1, T2, T3, T4>(callback);
        }
    }

    #endregion
}