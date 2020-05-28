using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EControle.Core.Data.Models;
using EControle.UI.Web.Controllers.Core;

namespace EControle.UI.Web.Controllers.API
{
    public class ConfigController : _Controller
    {
        
        public async Task<JsonResult> GetSEDCAccounts()
        {
            return Json(await _sdk.SEDCManager.SedcAccounts(),JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public async Task<JsonResult> SaveSedcUser(string name, string pass, string http, string server, string port)
        {
            try
            {
                bool state = await _sdk.SEDCManager.SaveAccount(new SedcAccount
                {
                    HttpType = http, Password = pass, Port = (port != "") ? port : "80", Server = server, UserName = name
                });
                return Json(state, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateSedcAccount(long id, string name, string pass, string http, string server, string port)
        {
            bool state = await _sdk.SEDCManager.UpdateAccount(new SedcAccount
            {
                Id = id,
                HttpType = http,
                Password = pass,
                Port = (port != "") ? port : "80",
                Server = server,
                UserName = name
            });
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSedcUser(long id)
        {
            bool state = await _sdk.SEDCManager.DeleteAccount(id);
            return Json(state, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> SetToDefault(long id)
        {
            bool state = await _sdk.SEDCManager.SetDefault(id);
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetBalanceSheet()
        {
            return Json(await _sdk.SEDCManager.GetBalanceSheet(), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateBalanceSheet()
        {
            return Json(await _sdk.SEDCManager.UpdateBalanceSheet(), JsonRequestBehavior.AllowGet);
        }
    }
}