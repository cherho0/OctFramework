using System;
using System.IO;
using System.Xml.Serialization;

namespace Oct.Framework.Socket.Common
{
    public class CfgMgr
    {
        #region Fields

        //基础设置 
        private static BasicCfg _basicCfg;
        #endregion

        #region Initialize


        #endregion

        #region Properties

        public static BasicCfg BasicCfg
        {
            get
            {
                 if (_basicCfg == null)
                 {
                     using (var ste = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "../Res/BasicCfg.xml"))
                     {
                         _basicCfg = (BasicCfg)new XmlSerializer(typeof(BasicCfg)).Deserialize(ste);
                     }
                 }

                return _basicCfg;
            }
        }

        #endregion
    }
}