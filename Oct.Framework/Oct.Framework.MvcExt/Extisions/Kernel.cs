using System;
using System.Web;
using System.Web.Mvc;

namespace Oct.Framework.MvcExt.Extisions
{
    public interface IKernel
    {
        Controller Controller { get; }
        HttpRequestBase Request { get; }
        HttpResponseBase Response { get; }
    }

    public class Kernel : IKernel
    {
        private readonly Controller _controller;

        public Kernel(Controller controller)
        {
            _controller = controller;
        }

        public Controller Controller
        {
            get
            {
                if (_controller == null)
                {
                    throw new ArgumentNullException("当前Controller为空");
                }
                return _controller;
            }
        }

        public HttpRequestBase Request
        {
            get { return Controller.Request; }
        }

        public HttpResponseBase Response
        {
            get { return Controller.Response; }
        }
    }
}
