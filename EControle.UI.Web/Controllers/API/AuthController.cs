using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EControle.Core.Data.Models;
using EControle.Core.Data.Types;
using EControle.Data.SDK.Lib;
using EControle.UI.Web.App_Start.Resources;
using EControle.UI.Web.Controllers.Core;

namespace EControle.UI.Web.Controllers.API
{
    public class AuthController : _Controller
    {

        //public SDK _sdk = new SDK();

        public async Task<JsonResult> Login(string name, string pass, bool rememberMe)
        {
            HttpCookie userInfoCookies;
            var us = await _sdk.UsersManager.Login(name);
            try
            {
                if (us != null)
                {
                    Session["user"] = us;
                    if (us.SetNewPassword)
                    {
                        return Json(new { state = true, url = RedirectToAction("CreateNewPassword", "Home") },
                            JsonRequestBehavior.AllowGet);
                    }

                    var passConfirm = await _sdk.UsersManager.ConfiremPassword(us, pass);
                    if (passConfirm)
                    {
                        if (rememberMe)
                        {
                            userInfoCookies = new HttpCookie("User")
                            {
                                ["UserName"] = us.UserName,
                                ["UserId"] = us.Id.ToString(),
                                Expires = DateTime.Now.AddDays(5)
                            };
                            Response.Cookies.Add(userInfoCookies);
                        }
                        else
                        {
                            userInfoCookies = new HttpCookie("User")
                            {
                                ["UserName"] = us.UserName,
                                ["UserId"] = us.Id.ToString(),
                            };
                            Response.Cookies.Add(userInfoCookies);
                        }

                        switch (us.Role)
                        {
                            case Role.SuperAdmin:
                                return Json(new { state = true, url = RedirectToAction("Index", "SuperAdministrator") },
                                    JsonRequestBehavior.AllowGet);
                            case Role.Admin:
                                return Json(new { state = true, url = RedirectToAction("Index", "Admin") },
                                    JsonRequestBehavior.AllowGet);
                            case Role.Supervisor:
                                return Json(new { state = true, url = RedirectToAction("Index", "Supervisor") },
                                    JsonRequestBehavior.AllowGet);
                            case Role.Accountant:
                                return Json(new { state = true, url = RedirectToAction("Index", "Accountant") },
                                    JsonRequestBehavior.AllowGet);
                            case Role.Agent:
                                return Json(new { state = true, url = RedirectToAction("Index", "Agent") },
                                    JsonRequestBehavior.AllowGet);
                            case Role.SealsAgent:
                                return Json(new { state = true, url = RedirectToAction("Index", "SealsAgent") },
                                    JsonRequestBehavior.AllowGet);
                            default:
                                return Json(new { state = false, msg = us, url = RedirectToAction("Index", "Home") },
                                    JsonRequestBehavior.AllowGet);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { state = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { state = false, msg = us }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SetNewPassword(string pass, string cpass)
        {
            var us = Session["user"] as User;
            if (us != null && (pass == cpass))
            {
                us.Password = pass;
                var state = await _sdk.UsersManager.SetPassword(us);
                if (state)
                {
                    HttpCookie userInfoCookies = new HttpCookie("User")
                    {
                        ["UserName"] = us.UserName,
                        ["UserId"] = us.Id.ToString(),
                        Expires = DateTime.Now.AddDays(5)
                    };
                    Response.Cookies.Add(userInfoCookies);
                    //Session["user"] = us;
                    switch (us.Role)
                    {
                        case Role.SuperAdmin:
                            return Json(new { state = true, url = RedirectToAction("Index", "SuperAdministrator") },
                                JsonRequestBehavior.AllowGet);
                        case Role.Admin:
                            return Json(new { state = true, url = RedirectToAction("Index", "Admin") },
                                JsonRequestBehavior.AllowGet);
                        case Role.Supervisor:
                            return Json(new { state = true, url = RedirectToAction("Index", "Supervisor") },
                                JsonRequestBehavior.AllowGet);
                        case Role.Accountant:
                            return Json(new { state = true, url = RedirectToAction("Index", "Accountant") },
                                JsonRequestBehavior.AllowGet);
                        case Role.Agent:
                            return Json(new { state = true, url = RedirectToAction("Index", "Agent") },
                                JsonRequestBehavior.AllowGet);
                        case Role.SealsAgent:
                            return Json(new { state = true, url = RedirectToAction("Index", "SealsAgent") },
                                JsonRequestBehavior.AllowGet);
                        default:
                            return Json(new { state = false, msg = Msg.errLogin, url = RedirectToAction("Index", "Home") },
                                JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { state = false, msg = Msg.errLogin, url = RedirectToAction("Index", "Home") },
                JsonRequestBehavior.AllowGet);
        }

    }
}