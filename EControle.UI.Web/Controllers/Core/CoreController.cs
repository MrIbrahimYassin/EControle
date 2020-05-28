using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EControle.Core.Data.Models;
using EControle.Core.Data.Types;
using EControle.Data.SDK.Lib;
using EControle.Data.SDK.Lib.ViewModels;
using EControle.Data.SDK.Utilites;
using EControle.UI.Web.App_Start.Resources;

namespace EControle.UI.Web.Controllers.Core
{
    public class CoreController : Controller
    {
       

        //public async Task<JsonResult> Branches()
        //{
        //    var branchies = await _sdk.BrachesManager.Braches();
        //    return Json(branchies.OrderBy(x => x.CreatedDate), JsonRequestBehavior.AllowGet);

        //}

        //public async Task<JsonResult> GetSingleBranch(long id)
        //{
        //    return Json(await _sdk.Worker.Branchies.GetSingle(id), JsonRequestBehavior.AllowGet);
        //}

        //public async Task<JsonResult> GetUsersForBranch()
        //{
        //    return Json(await _sdk.Worker.Users.Find(x => x.Role == Role.SuperAdmin || x.Role == Role.Admin || x.Role == Role.SealsAgent), JsonRequestBehavior.AllowGet);
        //}


        //[HttpPost]
        //public async Task<JsonResult> SaveBranch(string name, long owner)
        //{
        //    try
        //    {
        //        User user = null;
        //        HttpCookie userCookie = Request.Cookies["User"];
        //        if (userCookie != null)
        //        {
        //            user = await _sdk.UsersManager.GetSingleUser(long.Parse(userCookie["UserId"]));
        //        }
        //        if (Session["user"] is User && user == null)
        //        {
        //            user = Session["user"] as User;
        //        }
        //        if (user == null)
        //        {
        //            return Json(false, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            try
        //            {
        //                bool state = await _sdk.BrachesManager.AddBranch(name, owner, user.Id);

        //                return Json(state, JsonRequestBehavior.AllowGet);
        //            }
        //            catch (Exception e)
        //            {
        //                return Json(false, JsonRequestBehavior.AllowGet);

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }

        //}


        //[HttpPost]
        //public async Task<JsonResult> DeleteBranch(long id)
        //{
        //    try
        //    {
        //        bool state = await _sdk.BrachesManager.RemoveBranch(id);

        //        return Json(state, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);

        //    }
        //}

        //[HttpPost]
        //public async Task<JsonResult> UpdateBranch(long id, string name, long owner)
        //{
        //    try
        //    {
        //        User user = null;
        //        HttpCookie userCookie = Request.Cookies["User"];
        //        if (userCookie != null)
        //        {
        //            user = await _sdk.UsersManager.GetSingleUser(long.Parse(userCookie["UserId"]));
        //        }
        //        if (Session["user"] is User && user == null)
        //        {
        //            user = Session["user"] as User;
        //        }
        //        if (user == null)
        //        {
        //            return Json(false, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            try
        //            {
        //                bool state = await _sdk.BrachesManager.UpdateBranch(id, name, owner, user.Id);
        //                return Json(state, JsonRequestBehavior.AllowGet);
        //            }
        //            catch (Exception e)
        //            {
        //                return Json(false, JsonRequestBehavior.AllowGet);

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }

        //}

        //[HttpPost]
        //public async Task<JsonResult> RestoreBranch(long id)
        //{
        //    try
        //    {
        //        User user = null;
        //        HttpCookie userCookie = Request.Cookies["User"];
        //        if (userCookie != null)
        //        {
        //            user = await _sdk.UsersManager.GetSingleUser(long.Parse(userCookie["UserId"]));
        //        }
        //        if (Session["user"] is User && user == null)
        //        {
        //            user = Session["user"] as User;
        //        }
        //        if (user == null)
        //        {
        //            return Json(false, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            try
        //            {
        //                bool state = await _sdk.BrachesManager.RestoreBranch(id, user.Id);
        //                return Json(state, JsonRequestBehavior.AllowGet);
        //            }
        //            catch (Exception e)
        //            {
        //                return Json(false, JsonRequestBehavior.AllowGet);

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}


       
    }
}