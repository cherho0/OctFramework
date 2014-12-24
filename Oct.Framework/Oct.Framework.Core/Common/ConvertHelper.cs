using System;

namespace Oct.Framework.Core.Common
{
    /// <summary>
    /// 数据格式转换帮助类
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        /// 转换成int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int CInt(this string value)
        {
            int result = 0;

            if (int.TryParse(value, out result))
                return result;

            return result;
        }

        /// <summary>
        /// 转换成bool
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CBool(this string value)
        {
            bool result = false;

            if (bool.TryParse(value, out result))
                return result;

            return result;
        }

        /// <summary>
        /// 转换成DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime CDateTime(this string value)
        {
            DateTime result = DateTime.MinValue;

            if (DateTime.TryParse(value, out result))
                return result;

            return result;
        }

        /// <summary>
        /// 转换成decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal CDecimal(this string value)
        {
            decimal result = 0;

            if (decimal.TryParse(value, out result))
                return result;

            return result;
        }

        /// <summary>
        /// 转换成Guid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid CGuid(this string value)
        {
            Guid result = Guid.Empty;

            if (Guid.TryParse(value, out result))
                return result;

            return result;
        }

        /// <summary>
        /// 空或无
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 空或无
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBoolString(this bool value)
        {
            return value ? "是" : "否";
        }

        
        public static string ToYYYYMMDDString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 空或无
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToYYYYMMDDHHmmssString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 空或无
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string ToEnumString(this Enum enumValue)
        {
            if (enumValue == null)
            {
                return "";
            }
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            if (field == null)
            {
                return str;
            }
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }
    }
}
