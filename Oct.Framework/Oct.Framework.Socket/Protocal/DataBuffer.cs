using System;
using System.Data;
using System.IO;

namespace Oct.Framework.Socket.Protocal
{
    public class DataBuffer : IDisposable
    {
        public byte[] Data;
        internal int mCount;

        private int mPostion;

        public DataBuffer(int count)
        {
            Data = new byte[count];
        }

        public int Count
        {
            get { return mCount; }
        }

        public int Postion
        {
            get { return mPostion; }
        }

        public DataBufferPool Pool { get; set; }

        #region IDisposable 成员

        public void Dispose()
        {
            SetPostion(0);
            SetCount(0);
            if (Pool != null)
                Pool.Push(this);
        }

        #endregion

        public void Write(byte data)
        {
            mCount++;
            if (mCount > Data.Length)
                throw new DataException();
            Data[mPostion] = data;
            mPostion++;

            //Write(new byte[] { data});
        }

        public void Write(byte[] data)
        {
            if (data == null || data.Length == 0)
                return;
            mCount += data.Length;
            if (mCount > Data.Length)
                throw new DataException();
            data.CopyTo(Data, mPostion);
            mPostion += data.Length;
        }

        public void WriteStream(Stream stream)
        {
            int length = Convert.ToInt32(stream.Length);
            if (stream.Length > (Data.Length - Count))
                throw new DataException();
            stream.Read(Data, Count, length);
            mCount += length;
        }

        internal void SetPostion(int value)
        {
            mPostion = value;
        }

        public void SetCount(int value)
        {
            mCount = value;
        }

        public void Clear()
        {
            mCount = 0;
            mPostion = 0;
        }
    }
}