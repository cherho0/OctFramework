using System;

namespace Oct.Framework.Services
{
    public class ServiceException : Exception
    {
        public ServiceException()
            : base("服务层异常")
        {

        }

        public ServiceException(string message)
            : base(message)
        {

        }

        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
