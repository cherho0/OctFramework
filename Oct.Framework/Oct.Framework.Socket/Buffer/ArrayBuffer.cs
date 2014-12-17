namespace Oct.Framework.Socket.Buffer
{
    /// <summary>
    /// Defines a wrapper for a chunk of memory that may be split into smaller, logical segments.
    /// </summary>
    public class ArrayBuffer
    {
        //private readonly int BufferSize;

        public readonly byte[] Array;
        private readonly BufferManager m_mgr;

        /// <summary>
        /// Creates an ArrayBuffer that is wrapping a pre-existing buffer.
        /// </summary>
        /// <param name="arr">the buffer to wrap</param>
        internal ArrayBuffer(byte[] arr)
        {
            Array = arr;
            // BufferSize = arr.Length;
        }

        /// <summary>
        /// Creates an ArrayBuffer and allocates a new buffer for usage.
        /// </summary>
        /// <param name="mgr">the <see cref="BufferManager" /> which allocated this array</param>
        internal ArrayBuffer(BufferManager mgr, int bufferSize)
        {
            m_mgr = mgr;
            // BufferSize = bufferSize;
            Array = new byte[bufferSize];
        }

        protected internal void CheckIn(BufferSegment segment)
        {
            if (m_mgr != null)
            {
                m_mgr.CheckIn(segment);
            }
        }
    }
}