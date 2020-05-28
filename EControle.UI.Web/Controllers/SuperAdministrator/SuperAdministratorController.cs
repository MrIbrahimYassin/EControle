using System.Web.Mvc;
using EControle.Core.Data.Types;

namespace EControle.UI.Web.Controllers.SuperAdministrator
{
    public class SuperAdministratorController : Controller
    {
        // GET: SuperAdministrator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users(Role role)
        {
            ViewBag.Role = role;
            return View();
        }

        public ActionResult NewUser()
        {
            return PartialView();
        }

        public ActionResult EditUserForm()
        {
            return PartialView();
        }

        public ActionResult Accounts()
        {
            return View();
        }


        public ActionResult Transactions(long id)
        {
            ViewBag.Id = id;
            return View();
        }

       

        public ActionResult NewTransaction()
        {
            return PartialView();
        }

        public ActionResult MyTransactions()
        {
            return View();
        }


        public ActionResult SedcConfig()
        {
            return View();
        }

        public ActionResult NewRecharge()
        {
            return PartialView();
        }

        public ActionResult NewSedcAccount()
        {
            return PartialView();
        }

        public ActionResult EditSedcAccount()
        {
            return PartialView();
        }

        public ActionResult SedcTransactions()
        {
            return View();
        }
    }
}