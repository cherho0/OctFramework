using System.Collections.Generic;
using System.Configuration;

namespace Oct.Tools.Plugin.CodeGenerator.Section
{
    public class SectionMgr
    {
        #region 属性

        public static IDictionary<string, TempElement> TempDict
        {
            get;
            private set;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载配置项
        /// </summary>
        public static void LoadSection()
        {
            TempDict = new Dictionary<string, TempElement>();

            var section = ConfigurationManager.GetSection("tempSection") as TempSection;

            foreach (TempElement temp in section.Temps)
            {
                TempDict.Add(temp.Name, temp);
            }
        }

        /// <summary>
        /// 获取模板配置项信息
        /// </summary>
        /// <param name="tempName">模板名称</param>
        /// <returns></returns>
        public static TempElement GetTempElement(string tempName)
        {
            if (TempDict.ContainsKey(tempName))
                return TempDict[tempName];
            else
                return null;
        }

        #endregion
    }
}
