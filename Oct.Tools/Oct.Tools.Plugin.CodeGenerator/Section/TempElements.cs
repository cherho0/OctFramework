using System.Configuration;

namespace Oct.Tools.Plugin.CodeGenerator.Section
{
    public class TempElements : ConfigurationElementCollection
    {
        #region 索引器

        public TempElement this[int index]
        {
            get
            {
                return (TempElement)this.BaseGet(index);
            }
            set
            {
                if (this.BaseGet(index) != null)
                    this.BaseRemoveAt(index);

                this.BaseAdd(index, value);
            }
        }

        #endregion

        #region ConfigurationElementCollection 成员

        protected override ConfigurationElement CreateNewElement()
        {
            return new TempElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TempElement)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "temp";
            }
        }

        #endregion
    }
}
