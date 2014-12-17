using System.Configuration;
using Oct.Framework.Core.Common;

namespace Oct.Framework.WinServiceKernel.Util
{
    public class Cfg : SingleTon<Cfg>
    {
        public int Delay
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Delay"]);
            }
        }

        public int ExecHour
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["ExecHour"]);
            }
        }

        public string Listen
        {
            get
            {
                return ConfigurationManager.AppSettings["listen"];
            }
        }

        public int OrderCount
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["OrderCount"]);
            }
        }
    }
}
