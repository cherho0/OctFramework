using System;
using System.Diagnostics;
using System.IO;
using NyMQ.Core.Socket;

namespace Oct.Framework.Socket.Socket
{
    public class PackageHead : BinaryWriter
    {
        private const ushort h1 = 250;
        private const ushort h2 = 251;
        private const ushort h3 = 252;
        private const ushort h4 = 253;
        private readonly byte[] _buf;
        private byte[] _ubuf;
        public int BODYLENGTH = 4;
        public int HEADSIZE = 4;
        private int _length;
        private PackageHeadPool _pool;

        public PackageHeadPool Pool
        {
            get { return _pool; }
            set { _pool = value; }
        }

        public PackageHead()
        {
            _buf = new byte[4];
            _ubuf = new byte[2];
        }

        public int MessageLength
        {
            get { return _length; }
        }

        public void GetHeadBytes(Stream stream)
        {
            Write(stream, h1);
            Write(stream, h2);
            Write(stream, h3);
            Write(stream, h4);
        }

        private unsafe void Write(Stream s, ushort val)
        {
            fixed (byte* numRef = _buf)
            {
                *((ushort*) numRef) = val;
            }

            s.Write(_buf, 0, 1);
        }

        public unsafe void Write(Stream s, int val)
        {
            fixed (byte* numRef = _buf)
            {
                *((int*) numRef) = val;
            }

            s.Write(_buf, 0, 4);
        }

        /// <summary>
        /// 验证包头
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool CheckPackageHead(Stream s)
        {
            var  ret = ReadUshot(s)== h1 && ReadUshot(s) == h2 && ReadUshot(s) == h3 && ReadUshot(s) == h4;
            if (ret)
            {
                _length = GetMessageLength(s);
            }
            return ret;
        }

        /// <summary>
        /// 读取无符号整数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public ushort ReadUshot(Stream s)
        {
            Trace.Assert(s.Read(_ubuf, 0, 1) == 1, "len == 1");
            return BitConverter.ToUInt16(_ubuf, 0);
        }

        /// <summary>
        /// 读取消息长度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private int GetMessageLength(Stream s)
        {
            Trace.Assert(s.Read(_buf, 0, 4) == 4, "len == 4");
            return BitConverter.ToInt32(_buf, 0);
        }

        //public void Dispose()
        //{
        //    for (int i = 0; i < _buf.Length; i++)
        //    {
        //        _buf[i] = 0;
        //    }

        //    for (int i = 0; i < _ubuf.Length; i++)
        //    {
        //        _ubuf[i] = 0;
        //    }

        //    if (_pool != null)
        //    {
        //        _pool.Push(this);
        //    }
        //}
    }
}