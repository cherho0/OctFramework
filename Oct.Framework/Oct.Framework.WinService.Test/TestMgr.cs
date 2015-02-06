using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.Core.MEF;
using Oct.Framework.WinServiceKernel;
using Oct.Framework.WinServiceKernel.Core;
using Oct.Framework.WinServiceKernel.Interfaces;

namespace Oct.Framework.WinService.Test
{
    [Export(typeof(IServise))]
    public class TestMgr:CoreLogic
    {
        public override string Name
        {
            get { return "TestMgr"; }
        }

        protected override void OnClose(Kernel knl)
        {
            //throw new NotImplementedException();
        }

        protected override void OnLoad(Kernel knl)
        {
            //throw new NotImplementedException();
        }

        public override void Cmd(string cmd)
        {
            //throw new NotImplementedException();
        }
    }
}
