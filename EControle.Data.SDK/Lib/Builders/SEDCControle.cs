using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using EControle.Core.Data.Models;
using EControle.Data.SDK.Lib.Responses;
using EControle.Data.SDK.Lib.ViewModels;
using EControle.Data.SDK.Utilites;
using Newtonsoft.Json;

namespace EControle.Data.SDK.Lib.Builders
{
    public class SEDCControle : IDisposable
    {
        private Worker _manager;
        public SEDCControle(Worker _worker)
        {
            _manager = _worker;
        }

        #region SEDCApiCalls

        public async Task<string> CallApi(XElement xmlElement, string uri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var content = new StringContent(xmlElement.ToString(), Encoding.UTF8, "application/xml");

                    var response = await client.PostAsync(uri, content);

                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }
        public async Task<PurchaseResp> Purchase(PurchaseVM purchase)
        {
            PurchaseResp response = new PurchaseResp();
            try
            {
                var sedcAccount = await _manager.SedcAccounts.SingleOrDefault(x => x.IsDefault);
                purchase.userName = sedcAccount.UserName;
                purchase.userPass = sedcAccount.Password;
                string port = (sedcAccount.Port == "80") ? "" : $":{sedcAccount.Port}";
                SEDCOprations.Core = $"{sedcAccount.HttpType}://{sedcAccount.Server}{port}";
                try
                {
                    purchase.userPass = EncryptionManager.MD5Encrypt(purchase.userName, purchase.userPass, sedcAccount.Key);
                    purchase.verifyCode= EncryptionManager.VDataMD5(purchase.userName, purchase.userPass, purchase.transID,
                        purchase.meterNum, purchase.calcMode, purchase.amount, sedcAccount.Key);
                    var content = Convertor.GetXML(purchase);
                    var resp = await CallApi(content, SEDCOprations.Core + SEDCOprations.PURCHASE);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(resp);
                    response = Convertor.GetPurchaseResp(doc);
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            catch (Exception ex)
            {


            }
            return response;
        }


        public async Task<string> Search(SearchVM searchVm)
        {
            string json = "";
            try
            {
                var content = Convertor.GetXML(searchVm);
                var resp = await CallApi(content, SEDCOprations.SEARCH);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(resp);

                json = JsonConvert.SerializeXmlNode(doc);

            }
            catch (Exception e)
            {

            }
            return json;
        }
        public async Task<BalanceResp> Balance(BalanceVM balanceVm)
        {
            BalanceResp response = new BalanceResp();
            try
            {
                var sedcAccount = await _manager.SedcAccounts.SingleOrDefault(x => x.IsDefault);
                balanceVm.userName = sedcAccount.UserName;
                balanceVm.userPass = sedcAccount.Password;
                string port = (sedcAccount.Port == "80") ? "" : $":{sedcAccount.Port}";
                SEDCOprations.Core = $"{sedcAccount.HttpType}://{sedcAccount.Server}{port}";
                try
                {
                    balanceVm.userPass = EncryptionManager.MD5Encrypt(balanceVm.userName, balanceVm.userPass,sedcAccount.Key);
                    var content = Convertor.GetXML(balanceVm);
                    var resp = await CallApi(content, SEDCOprations.Core+ SEDCOprations.BALANCE);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(resp);
                    response = Convertor.GetBalanceResp(doc);
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

              
            }
            return response;
        }
        private async Task<string> GetTrans(HistoryVM historyVm)
        {
            string json = "";
            try
            {
                var content = Convertor.GetXML(historyVm);
                var resp = await CallApi(content, SEDCOprations.SEARCH);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(resp);
                json = JsonConvert.SerializeXmlNode(doc);
            }
            catch (Exception e)
            {
            }
            return json;
        }

        #endregion

        public void Dispose()
        {
           _manager.Dispose();
        }
    }
}