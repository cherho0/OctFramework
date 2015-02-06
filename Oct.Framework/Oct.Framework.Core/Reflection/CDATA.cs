using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Oct.Framework.Core.Reflection
{
    public class CDATA : IXmlSerializable
    {
        public CDATA()
        {
        }
        public CDATA(string xml)
        {
            this.OuterXml = xml;
        }

        public string OuterXml { get; set; }
        public string InnerXml
        { get; set; }
        public string InnerSourceXml
        { get; set; }
        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            string s = reader.ReadInnerXml();
            string startTag = "<![CDATA[";
            string endTag = "]]>";
            char[] trims = new char[] { '\r', '\n', '\t', ' ' };
            s = s.Trim(trims);
            if (s.StartsWith(startTag) && s.EndsWith(endTag))
            {
                s = s.Substring(startTag.Length, s.LastIndexOf(endTag) - startTag.Length);
            }
            this.InnerSourceXml = s;
            this.InnerXml = s.Trim(trims);
        }
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteCData(this.OuterXml);
        }
    }
}
