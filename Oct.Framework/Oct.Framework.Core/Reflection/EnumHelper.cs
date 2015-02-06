using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Oct.Framework.Core.Reflection
{
    public class EnumHelper
    {
        private static Dictionary<Enum, string> _enumValueDic = new Dictionary<Enum, string>();

        /// <summary>
        /// 根据枚举获取数据
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum enumValue)
        {
            if (_enumValueDic.ContainsKey(enumValue))
            {
                return _enumValueDic[enumValue];
            }
            if (enumValue == null)
            {
                return "";
            }
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            if (field == null)
            {
                _enumValueDic.Add(enumValue, str);
                return str;
            }
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            _enumValueDic.Add(enumValue, da.Description);
            return da.Description;
        }
        private static Dictionary<string, IList<EnumModel>> enumCache = new Dictionary<string, IList<EnumModel>>();

        /// <summary>
        /// index 为0 获取全部 为页数 获取相关页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static EnumPageResult<EnumModel> GetEnumModels<T>(int index = 0, int size = 15) where T : struct
        {
            var typename = typeof(T).ToString();
            IList<EnumModel> items = new List<EnumModel>();
            if (!enumCache.ContainsKey(typename))
            {
                var types = typeof(T);
                var ms = types.GetFields();
                foreach (FieldInfo fieldInfo in ms)
                {
                    try
                    {
                        DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo,
                    typeof(DescriptionAttribute), false) as DescriptionAttribute;
                        if (attr == null)
                        {
                            continue;
                        }
                        var desc = attr.Description;
                        var v = (int)Enum.Parse(types, fieldInfo.Name);
                        var item = new EnumModel()
                        {
                            Key = fieldInfo.Name,
                            Value = v,
                            Desc = desc
                        };
                        items.Add(item);


                    }
                    catch (Exception)
                    {
                    }
                }
                enumCache.Add(typename, items);
            }

            items = enumCache[typename];

            if (index != 0)
            {
                items = items.Skip((index - 1) * size).Take(size).ToList();
            }

            return new EnumPageResult<EnumModel>(items, items.Count);
        }

    }

    public class EnumModel
    {
        public string Key;
        public string Desc;
        public int Value;
    }

    public class EnumPageResult<T> where T : new()
    {
        public IList<T> Models { get; private set; }
        public int TotalCount { get; private set; }

        public EnumPageResult(IList<T> models, int total)
        {
            Models = models;
            TotalCount = total;
        }
    }
}
