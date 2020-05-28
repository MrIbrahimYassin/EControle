using System.Collections.Generic;
using System.Xml.Serialization;

namespace EControle.Data.SDK.Lib.Responses
{
    [XmlRoot(ElementName = "result")]
    public class PurchaseResp
    {

        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }

        [XmlAttribute(AttributeName = "code")] public string Code { get; set; }

        [XmlAttribute(AttributeName = "transTime")]
        public string TransTime { get; set; }

        [XmlAttribute(AttributeName = "regCode")]
        public string RegCode { get; set; }

        [XmlAttribute(AttributeName = "refCode")]
        public string RefCode { get; set; }

        [XmlAttribute(AttributeName = "meterNum")]
        public string MeterNum { get; set; }

        [XmlAttribute(AttributeName = "customerName")]
        public string CustomerName { get; set; }

        [XmlAttribute(AttributeName = "tariffCode")]
        public string TariffCode { get; set; }

        [XmlAttribute(AttributeName = "buyTimes")]
        public string BuyTimes { get; set; }

        [XmlAttribute(AttributeName = "calcQty")]
        public string CalcQty { get; set; }

        [XmlAttribute(AttributeName = "vendQty")]
        public string VendQty { get; set; }

        [XmlAttribute(AttributeName = "vendAMT")]
        public string VendAmt { get; set; }

        [XmlAttribute(AttributeName = "supplyAMT")]
        public string SupplyAmt { get; set; }

        [XmlAttribute(AttributeName = "arrearAMT")]
        public string ArrearAmt { get; set; }

        [XmlAttribute(AttributeName = "feeAMT")]
        public string FeeAmt { get; set; }

        [XmlAttribute(AttributeName = "AMT")] public string Amt { get; set; }

        [XmlAttribute(AttributeName = "VAT")] public string Vat { get; set; }

        [XmlAttribute(AttributeName = "stampTax")]
        public string StampTax { get; set; }

        [XmlAttribute(AttributeName = "netAMT")]
        public string NetAmt { get; set; }

        [XmlAttribute(AttributeName = "commAMT")]
        public string CommAmt { get; set; }

        [XmlAttribute(AttributeName = "token")]
        public string Token { get; set; }

        [XmlAttribute(AttributeName = "invoice")]
        public string Invoice { get; set; }

        [XmlAttribute(AttributeName = "verifyCode")]
        public string VerifyCode { get; set; }

        [XmlAttribute(AttributeName = "checkCode")]
        public string CheckCode { get; set; }

        [XmlAttribute(AttributeName = "transID")]
        public string TransId { get; set; }


        [XmlElement("item")]
        public List<ItemNode> Items { get; set; }
    }


    [XmlRoot("item")]
    public class ItemNode
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "kwh")]
        public string Kwh { get; set; }
        [XmlAttribute(AttributeName = "amt")]
        public string Amt { get; set; }
        [XmlAttribute(AttributeName = "price")]
        public string Price { get; set; }
    }
}