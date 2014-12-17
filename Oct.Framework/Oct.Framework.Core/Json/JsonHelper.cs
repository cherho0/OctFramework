using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Oct.Framework.Core.Json
{
    public class JsonHelper
    {
        /// <summary>
        /// 序列化json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            var body = JsonConvert.SerializeObject(obj);

            return body;
        }

        /// <summary>
        /// 反序列化json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string json) where T : class
        {
            var reader = new JsonTextReader(new StringReader(json));

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var s = JsonSerializer.Create(settings);
            T body = s.Deserialize<T>(reader);
            return body;
        }
    }
}
