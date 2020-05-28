using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using EControle.Data.SDK.Lib.ViewModels;
using EControle.UI.Web.Controllers.Core;

namespace EControle.UI.Web.Controllers.API
{
    public class SEDCController : _Controller
    {
        // GET: SEDC
        public async Task<JsonResult> GetTransactions()
        {
            try
            {
                var data = await _sdk.SEDCManager.GetTransactions();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult NewTransaction()
        {
            return PartialView();
        }


       [HttpPost]
       public async Task<JsonResult> SaveTransaction(string amount, string meter,string cpass)
        {
            try
            {
                var data = await _sdk.SEDCManager.NewTransaction(new PurchaseVM
                {
                    amount = (amount),meterNum = meter, 
                },cpass,GetUserId());
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}