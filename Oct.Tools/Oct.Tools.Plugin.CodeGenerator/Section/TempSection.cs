using System.Configuration;

namespace Oct.Tools.Plugin.CodeGenerator.Section
{
    public class TempSection : ConfigurationSection
    {
        #region 属性

        [ConfigurationProperty("temps", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(TempElement))]
        public TempElements Temps
        {
            get
            {
                return (TempElements)base["temps"];
            }
            set
            {
                base["temps"] = value;
            }
        }

        #endregion
    }
}
