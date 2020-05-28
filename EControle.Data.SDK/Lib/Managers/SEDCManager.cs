using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EControle.Core.Data.Models;
using EControle.Core.Data.Models.SEDC_Trans;
using EControle.Data.SDK.Lib.Builders;
using EControle.Data.SDK.Lib.ViewModels;
using EControle.Data.SDK.Utilites;

namespace EControle.Data.SDK.Lib.Managers
{
    public class SEDCManager
    {
        string Baseurl = "http://192.168.95.1:5555/";

        private Worker _manager;
        public SEDCManager(Worker _worker)
        {
            _manager = _worker;
            //Baseurl = _worker.SedcAccounts.GetAll()
        }

        public async Task<List<SedcAccount>> SedcAccounts()
        {
            var sedcList = new List<SedcAccount>();
            try
            {
                var result = await _manager._context.SedcAccounts.ToListAsync();
                foreach (var r in result)
                {
                    r.Password = "";
                    sedcList.Add(r);
                }
            }
            catch (Exception e)
            {

            }

            return sedcList;
        }

        ////Hosted web API REST Service base url  

        //public async Task<bool> Index()
        //{
        //    var EmpInfo = new List<User>();

        //    using (var client = new HttpClient())
        //    {
        //        //Passing service base url  
        //        client.BaseAddress = new Uri(Baseurl);

        //        client.DefaultRequestHeaders.Clear();
        //        //Define request data format  
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
        //        HttpResponseMessage Res = await client.GetAsync("api/Employee/GetAllEmployees");

        //        //Checking the response is successful or not which is sent using HttpClient  
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            //Storing the response details recieved from web api   
        //            var EmpResponse = Res.Content.ReadAsStringAsync().Result;

        //            //Deserializing the response recieved from web api and storing into the Employee list  
        //            //EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
        //        }
        //        //returning the employee list to view  
        //        return true;
        //    }
        //}

        public async Task<SedcAccount> GetSingleAccount(long id)
        {
            return await _manager.SedcAccounts.GetSingle(id);
        }

        public async Task<bool> SaveAccount(SedcAccount sedcAccount)
        {
            try
            {
                var accounts = await _manager._context.SedcAccounts.ToListAsync();
                var old = await _manager._context.SedcAccounts.SingleOrDefaultAsync(x =>
                    x.Server == sedcAccount.Server && x.Port == sedcAccount.Port);
                if (old == null)
                {
                    sedcAccount.IsDefault = !accounts.Any();
                    sedcAccount.CreateDate = DateTime.Now;
                    _manager._context.SedcAccounts.Add(sedcAccount);
                }
                return await _manager.Complete();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAccount(SedcAccount sedcAccount)
        {
            var acc = await _manager.SedcAccounts.GetSingle(sedcAccount.Id);
            if (acc != null)
            {
                acc = sedcAccount;
                _manager.SedcAccounts.Update(acc);
            }
            return await _manager.Complete();
        }

        public async Task<bool> DeleteAccount(long id)
        {
            var acc = await _manager.SedcAccounts.GetSingle(id);
            if (acc != null)
            {
                _manager.SedcAccounts.Remove(acc);
            }
            return await _manager.Complete();
        }

        public async Task<bool> SetDefault(long id)
        {
            var acc = await _manager._context.SedcAccounts.ToListAsync();
            foreach (var account in acc)
            {
                account.IsDefault = (account.Id == id);
                _manager.SedcAccounts.Update(account);
            }
            return await _manager.Complete();
        }

        public async Task<List<BalanceSheet>> GetBalanceSheet() => (List<BalanceSheet>)await _manager.BalanceSheet.GetAll();

        public async Task<bool> UpdateBalanceSheet()
        {
            SEDCControle controle = new SEDCControle(_manager);
            var b = await controle.Balance(new BalanceVM());
            _manager._context.BalanceSheets.Add(new BalanceSheet
            {
                Balance = double.Parse(b.Balance),
                CheckDate = DateTime.Now,
                State = int.Parse(b.State),
                Username = b.Username
            });
            return await _manager.Complete();
        }

        public async Task<List<SEDCTransaction>> GetTransactions() => (List<SEDCTransaction>)await _manager.SEDCTransaction.GetAll();

        public async Task<bool> NewTransaction(PurchaseVM purchaseVm, string cpass, long userId)
        {
            var user = await _manager.Users.GetSingle(userId);
            if (user != null && user.Password == EncryptionManager.Encrypt(cpass))
            {
                using (var con = new SEDCControle(_manager))
                {

                    var result = await con.Purchase(purchaseVm);
                    if (result.State == "0")
                    {
                        List<Power> pws = new List<Power>();
                        foreach (var x in result.Items)
                        {
                            pws.Add(new Power
                            { Tid = x.Id, amt = x.Amt, kwh = x.Kwh, price = x.Price });
                        }

                        _manager.SEDCTransaction.Add(new SEDCTransaction
                        {
                            verifyCode = result.VerifyCode,
                            meterNum = result.MeterNum,
                            CreateDate = DateTime.Now,
                            transID = result.TransId,
                            AMT = result.Amt,
                            UserId = userId,
                            Powers = pws,
                            VAT = result.Vat,
                            arrearAMT = result.ArrearAmt,
                            buyTimes = result.BuyTimes,
                            vendQty = result.VendQty,
                            vendAMT = result.VendAmt,
                            transTime = result.TransTime,
                            token = result.Token,
                            tariffCode = result.TariffCode,
                            supplyAMT = result.SupplyAmt,
                            state = result.State,
                            stampTax = result.StampTax,
                            regCode = result.RegCode,
                            refCode = result.RefCode,
                            netAMT = result.NetAmt,
                            invoice = result.Invoice,
                            feeAMT = result.FeeAmt,
                            customerName = result.CustomerName,
                            commAMT = result.CommAmt,
                            code = result.Code,
                            checkCode = result.CheckCode,
                            calcQty = result.CalcQty
                        });
                        return await _manager.Complete();
                    }

                }


            }
            return false;

        }
    }
}