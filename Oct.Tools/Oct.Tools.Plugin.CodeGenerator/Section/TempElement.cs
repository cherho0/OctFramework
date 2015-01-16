using System.Configuration;

namespace Oct.Tools.Plugin.CodeGenerator.Section
{
    public class TempElement : ConfigurationSection
    {
        #region 属性

        /// <summary>
        /// 模板名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        /// <summary>
        /// 类的扩展名称
        /// </summary>
        [ConfigurationProperty("classNameExtend", IsRequired = true)]
        public string ClassNameExtend
        {
            get
            {
                return (string)base["classNameExtend"];
            }
            set
            {
                base["classNameExtend"] = value;
            }
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        [ConfigurationProperty("fileName", IsRequired = true)]
        public string FileName
        {
            get
            {
                return (string)base["fileName"];
            }
            set
            {
                base["fileName"] = value;
            }
        }

        /// <summary>
        /// 文件的扩展名称
        /// </summary>
        [ConfigurationProperty("fileNameExtend", IsRequired = true)]
        public string FileNameExtend
        {
            get
            {
                return (string)base["fileNameExtend"];
            }
            set
            {
                base["fileNameExtend"] = value;
            }
        }

        /// <summary>
        /// 文件夹的名称
        /// </summary>
        [ConfigurationProperty("dirName", IsRequired = true)]
        public string DirName
        {
            get
            {
                return (string)base["dirName"];
            }
            set
            {
                base["dirName"] = value;
            }
        }

        /// <summary>
        /// 是否按照表名创建子文件夹
        /// </summary>
        [ConfigurationProperty("isCreateChildDirByTableName", IsRequired = true)]
        public bool IsCreateChildDirByTableName
        {
            get
            {
                return (bool)base["isCreateChildDirByTableName"];
            }
            set
            {
                base["isCreateChildDirByTableName"] = value;
            }
        }

        /// <summary>
        /// 批量生成时是否只运行一次
        /// </summary>
        [ConfigurationProperty("isRanOnlyOnceByBath", IsRequired = true)]
        public bool IsRanOnlyOnceByBath
        {
            get
            {
                return (bool)base["isRanOnlyOnceByBath"];
            }
            set
            {
                base["isRanOnlyOnceByBath"] = value;
            }
        }

        /// <summary>
        /// 模板文件路径
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        #endregion
    }
}
