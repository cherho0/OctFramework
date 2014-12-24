using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
