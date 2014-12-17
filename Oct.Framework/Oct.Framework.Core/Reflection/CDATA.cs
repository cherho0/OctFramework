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
            this._outerXml = xml;
        }
        private string _outerXml;
        public string OuterXml
        {
            get
            {
                return _outerXml;
            }
        }
        private string _innerXml;
        public string InnerXml
        {
            get
            {
                return _innerXml;
            }
        }
        private string _innerSourceXml;
        public string InnerSourceXml
        {
            get
            {
                return _innerXml;
            }
        }
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
            this._innerSourceXml = s;
            this._innerXml = s.Trim(trims);
        }
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteCData(this._outerXml);
        }
    }
}
