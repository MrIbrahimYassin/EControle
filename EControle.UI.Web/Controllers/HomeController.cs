using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EControle.Core.Data.Types;
using EControle.Data.SDK.Lib;
using EControle.UI.Web.Utilites;

namespace EControle.UI.Web.Controllers
{
    public class HomeController : Controller
    {

        private SDK sdk = new SDK();

        // GET: Home
        public async Task<ActionResult> Index()
        {
            HttpCookie user = Request.Cookies["User"];
            if (user != null)
            {
                var us = await sdk.UsersManager.GetSingleUser(long.Parse(user["UserId"]));
                if (us != null)
                {
                    Session["user"] = us;
                    if (us.SetNewPassword)
                    {
                        return RedirectToAction("CreateNewPassword", "Home");
                    }
                    switch (us.Role)
                    {
                        case Role.SuperAdmin:
                            return RedirectToAction("Index", "SuperAdministrator");
                        case Role.Admin:
                            return RedirectToAction("Index", "Admin");
                        case Role.Supervisor:
                            return RedirectToAction("Index", "Supervisor");
                        case Role.Accountant:
                            return RedirectToAction("Index", "Accountant");
                        case Role.Agent:
                            return RedirectToAction("Index", "Agent");
                        case Role.SealsAgent:
                            return RedirectToAction("Index", "SealsAgent");
                        default:
                            return View();
                    }
                }
            }
            return View();
        }

        public ActionResult ChangeLanguage(string lang, string url)
        {
            new LanguageMang().SetLanguage(lang);
            return Redirect(url);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            HttpCookie userInfoCookies = new HttpCookie("User") { Expires = DateTime.Now.AddDays(-1) };
            Response.Cookies.Add(userInfoCookies);
            return RedirectToAction("Index");
        }

        public ActionResult CreateNewPassword()
        {
            return View();
        }
    }
}