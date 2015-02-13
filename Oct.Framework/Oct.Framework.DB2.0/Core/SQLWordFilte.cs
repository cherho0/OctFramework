using System.Text.RegularExpressions;

namespace Oct.Framework.DB.Core
{
    public class SQLWordFilte
    {
        public static bool CheckKeyWord(string sWord)
        {
            return false;
            //过滤关键字 \(|char
            string StrKeyWord =
                @"(|drop table|update|truncate\(|mid\(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user";
            //过滤关键字符
            string StrRegex = @"[-|;|,|/|\(|\)|\[|\]|}|{|%|*|!|']";
            if (Regex.IsMatch(sWord, StrKeyWord, RegexOptions.IgnoreCase))
                return true;
            return false;
        }

        public static bool CheckSql(string sWord)
        {
            //过滤关键字 \(|char
            string StrKeyWord =
                @"drop table|mid\(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user";
            //过滤关键字符
            string StrRegex = @"[-|;|,|/|\(|\)|\[|\]|}|{|%|*|!|']";
            if (Regex.IsMatch(sWord, StrKeyWord, RegexOptions.IgnoreCase))
                return true;
            return false;
        }
    }
}