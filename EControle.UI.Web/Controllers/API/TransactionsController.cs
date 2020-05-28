using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EControle.Core.Data.Models;
using EControle.Core.Data.Types;
using EControle.Data.SDK.Lib;
using EControle.Data.SDK.Lib.ViewModels;
using EControle.Data.SDK.Utilites;
using EControle.UI.Web.App_Start.Resources;
using EControle.UI.Web.Controllers.Core;
using Newtonsoft.Json;

namespace EControle.UI.Web.Controllers.API
{
    public class TransactionsController : _Controller
    {
        //private SDK _sdk = new SDK();


        [HttpPost]
        public async Task<JsonResult> CompleteTransaction(long id)
        {
            try
            {
                User user = await base.GetUser();
                HttpCookie userCookie = Request.Cookies["User"];
                try
                {
                    var state = await _sdk.AccountManager.CompleteTransaction(id, userId: user.Id);
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

        [HttpPost]
        public async Task<JsonResult> RollBackTransaction(long id)
        {
            try
            {
                User user = await base.GetUser();
                try
                {
                    var state = await _sdk.AccountManager.RollBack(id, user.Id);
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

        public async Task<JsonResult> GetTransactionsSearch(DateTime from, DateTime to, long? id)
        {
            try
            {
                long idd = (id != null) ? id.Value : this.GetUserId();

                var data = await _sdk.AccountManager.TransactionsFromUser(from, to,idd);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetTransactions(long id)
        {
            try
            {
                var data = await _sdk.AccountManager.TransactionsFromUser(id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> SaveTransaction(decimal amount, string ToId, string cpass)
        {
            try
            {
                User user = await base.GetUser();
                try
                {
                    var state = await _sdk.AccountManager.NewTransaction(amount, ToId, user.Id, cpass);
                    switch (state)
                    {
                        case GenerateTransState.Error:
                            return Json(new { state = false, msg = Msg.Error }, JsonRequestBehavior.AllowGet);
                        case GenerateTransState.Success:
                            return Json(new { state = true, msg = Msg.Success }, JsonRequestBehavior.AllowGet);
                        case GenerateTransState.AccountNotExist:
                            return Json(new { state = false, msg = Msg.AccountNotExist }, JsonRequestBehavior.AllowGet);
                        case GenerateTransState.AmountIsHi:
                            return Json(new { state = false, msg = Msg.AmountIsHi }, JsonRequestBehavior.AllowGet);
                        default:
                            return Json(new { state = false, msg = Msg.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception e)
            {
                return Json(new { state = false, msg = Msg.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetMyTransactions()
        {
            try
            {
                try
                {
                    var data = await _sdk.AccountManager.TransactionsFromUser(base.GetUserId());
                    return Json(data, JsonRequestBehavior.AllowGet);
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
        public async Task<JsonResult> Recharge(decimal amount, string cpass)
        {
            try
            {
                User user = await base.GetUser();
                try
                {
                    if (user.Role == Role.SuperAdmin)
                    {
                        var state = await _sdk.AccountManager.NewTransaction(amount, "", user.Id, cpass,true);
                        switch (state)
                        {
                            case GenerateTransState.Error:
                                return Json(new { state = false, msg = Msg.Error }, JsonRequestBehavior.AllowGet);
                            case GenerateTransState.Success:
                                return Json(new { state = true, msg = Msg.Success }, JsonRequestBehavior.AllowGet);
                            case GenerateTransState.AccountNotExist:
                                return Json(new { state = false, msg = Msg.AccountNotExist }, JsonRequestBehavior.AllowGet);
                            case GenerateTransState.AmountIsHi:
                                return Json(new { state = false, msg = Msg.AmountIsHi }, JsonRequestBehavior.AllowGet);
                            default:
                                return Json(new { state = false, msg = Msg.Error }, JsonRequestBehavior.AllowGet);
                        }
                    }

                   
                }
                catch (Exception e)
                {
                   
                }

                return Json(new { state = false, msg = Msg.Error }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { state = false, msg = Msg.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}