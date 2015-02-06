using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Oct.Framework.Core.Xml
{
    public class XmlHelper
    {
        /// <summary>
        /// 序列化xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string Serialize<T>(T o)
        {
            using (StringUTF8Writer sw = new StringUTF8Writer())
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                //Add an empty namespace and empty value
                ns.Add("", "");
                XmlSerializer xz = new XmlSerializer(o.GetType());
                xz.Serialize(sw, o, ns);
                return sw.ToString();
            }
        }

        /// <summary>
        /// 序列化xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Serialize<T>(T o, Encoding encoding)
        {
            using (StringEncodingWriter sw = new StringEncodingWriter(encoding))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                //Add an empty namespace and empty value
                ns.Add("", "");
                XmlSerializer xz = new XmlSerializer(o.GetType());
                xz.Serialize(sw, o,ns);
                return sw.ToString();
            }
        }

        /// <summary>
        /// 反序列化xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(string xml)
        {
            try
            {
                XmlSerializer xmls = new XmlSerializer(typeof(T));
                TextReader tr = new StringReader(xml);
                XmlReader xr = new XmlTextReader(tr);
                return (T)xmls.Deserialize(xr);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }

    public class StringUTF8Writer : System.IO.StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }

    public class StringEncodingWriter : System.IO.StringWriter
    {
        private Encoding _encoding;

        public StringEncodingWriter(Encoding encoding)
        {
            _encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return _encoding; }
        }
    }
}
