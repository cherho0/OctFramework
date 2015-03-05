using Oct.Framework.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Mvc;

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
        public static int ToInt(this string value)
        {
            int result = 0;

            if (int.TryParse(value, out result))
                return result;

            return result;
        }

        /// <summary>
        /// 转换成long
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToLong(this string value)
        {
            long result = 0;

            if (long.TryParse(value, out result))
                return result;

            return result;
        }

        /// <summary>
        /// 转换成bool
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(this string value)
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
        public static DateTime ToDateTime(this string value)
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
        public static decimal ToDecimal(this string value)
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
        public static Guid ToGuid(this string value)
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
        /// 获取一天的开始时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBeginTimeByOneDay(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd 00:00:00");
        }

        /// <summary>
        /// 获取一天的结束时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEndTimeByOneDay(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd 23:59:59");
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
            return EnumHelper.GetEnumDescription(enumValue);
        }

        /// <summary>
        /// 转换为可访问的动态类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anonymousList"></param>
        /// <returns></returns>
        public static IEnumerable<ExpandoObject> ToExpando<T>(this IEnumerable<T> anonymousList)
        {
            foreach (var anonymous in anonymousList)
            {
                IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymous);
                IDictionary<string, object> expando = new ExpandoObject();
                foreach (var item in anonymousDictionary)
                    expando.Add(item);
                yield return (ExpandoObject)expando;
            }
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(this DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            return (int)(time - startTime).TotalSeconds;
        }
    }
}
