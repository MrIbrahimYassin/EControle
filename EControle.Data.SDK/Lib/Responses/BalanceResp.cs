using System.IO;
using System.Xml.Serialization;
    //using Newtonsoft.Json;

namespace EControle.Data.SDK.Lib.Responses
{
    [XmlRoot(ElementName = "result")]
    public class BalanceResp
    {
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "username")]
        public string Username { get; set; }
        [XmlAttribute(AttributeName = "balance")]
        public string Balance { get; set; }
    }
}