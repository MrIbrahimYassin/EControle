using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EControle.Core.Data.Models;
using EControle.Data.SDK.Lib;
using EControle.Data.SDK.Lib.ViewModels;
using EControle.UI.Web.Controllers.Core;

namespace EControle.UI.Web.Controllers.API
{
    public class AccountController : _Controller
    {
        public async Task<JsonResult> GetAccounts()
        {
            try
            {
                List<AccountVM> state = await _sdk.AccountManager.Accounts();
                return Json(state, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
        }

        public async Task<JsonResult> ToggleActivateAccount(long id)
        {
            try
            {
                User user = await base.GetUser();

                try
                {
                    var state = await _sdk.AccountManager.ToggleActivate(id, userId: user.Id);
                    return Json(state, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetStatement()
        {
            try
            {
                // User user = await base.GetUser();
                try
                {
                    return Json(await _sdk.AccountManager.AccountVM(base.GetUserId()), JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetStatement(long id)
        {
            try
            {
                // User user = await base.GetUser();
                try
                {
                    return Json(await _sdk.AccountManager.AccountVM(id), JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}