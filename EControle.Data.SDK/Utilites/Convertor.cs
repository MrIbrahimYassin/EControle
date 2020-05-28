using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using EControle.Core.Data.Models;
using EControle.Data.SDK.Lib.Responses;
using EControle.Data.SDK.Lib.ViewModels;

namespace EControle.Data.SDK.Utilites
{
    public static class Convertor
    {
       
        public static XElement GetXML(PurchaseVM purchase)
        {
            try
            {
               
                XElement xml = new XElement("xml",
                    new XAttribute("userName", purchase.userName),
                    new XAttribute("userPass", purchase.userPass),
                    new XAttribute("transID", purchase.transID),
                    new XAttribute("meterNum", purchase.meterNum),
                    new XAttribute("calcMode", purchase.calcMode),
                    new XAttribute("amount", purchase.amount),
                    new XAttribute("verifyCode", purchase.verifyCode),
                    new XAttribute("verifyData", purchase.verifyData));
                return xml;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static XElement GetXML(SearchVM searchVm)
        {
            XElement xml = new XElement("xml",
                new XAttribute("userName", searchVm.userName),
                new XAttribute("userPass", searchVm.userPass),
                new XAttribute("transID", searchVm.transID),
                new XAttribute("meterNum", searchVm.meterNum));
            return xml;
        }

        public static XElement GetXML(BalanceVM searchVm)
        {
            XElement xml = new XElement("xml",
                new XAttribute("userName", searchVm.userName),
                new XAttribute("userPass", searchVm.userPass));
            return xml;
        }

        public static XElement GetXML(HistoryVM historyVm)
        {
            XElement xml = new XElement("xml",
                new XAttribute("userName", historyVm.userName),
                new XAttribute("meterNum", historyVm.meterNum),
                new XAttribute("userPass", historyVm.userPass));
            return xml;
        }

        public static BalanceResp GetBalanceResp(XmlDocument xml)
        {
            BalanceResp resp = new BalanceResp();

            try
            {
                var serilize = new XmlSerializer(typeof(BalanceResp));
                using (var reader = new XmlNodeReader(xml))
                {
                    resp = (BalanceResp) serilize.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                resp = null;
            }

            return resp;
        }

        public static PurchaseResp GetPurchaseResp(XmlDocument xml)
        {
            PurchaseResp resp = new PurchaseResp();

            try
            {
                var serilize = new XmlSerializer(typeof(BalanceResp));
                using (var reader = new XmlNodeReader(xml))
                {
                    resp = (PurchaseResp)serilize.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                resp = null;
            }

            return resp;
        }
    }
}