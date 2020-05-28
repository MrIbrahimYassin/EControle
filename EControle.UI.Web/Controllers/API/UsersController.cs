using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EControle.Core.Data.Models;
using EControle.Core.Data.Types;
using EControle.Data.SDK.Lib;
using EControle.UI.Web.Controllers.Core;

namespace EControle.UI.Web.Controllers.API
{
    public class UsersController : _Controller
    {

        //public SDK _sdk = new SDK();


        public async Task<JsonResult> GetUsers(int role) => Json(await _sdk.UsersManager.GetUsers(role), JsonRequestBehavior.AllowGet);


        [HttpPost]
        public async Task<JsonResult> SaveUser(string name, string phone, Gender gender, Role role, string userName)
        {

            try
            {
                User user = await base.GetUser();
                return Json(await _sdk.UsersManager.AddUser(new User
                {
                    Gender = gender,
                    Role = role,
                    Name = name,
                    UserName = userName,
                    Phone = phone
                }, user.Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<JsonResult> GetSingleUser(long id) =>
            Json(await _sdk.UsersManager.GetSingleUser(id), JsonRequestBehavior.AllowGet);

        [HttpPost]
        public async Task<JsonResult> UpdateUser(long id, string name, string phone, Gender gender, Role role, string userName) => Json(await _sdk.UsersManager.UpdateUser(new User
        {
            Id = id,
            Gender = gender,
            Role = role,
            Name = name,
            UserName = userName,
            Phone = phone
        }), JsonRequestBehavior.AllowGet);


        public async Task<JsonResult> DeleteUser(long id)
        {
            var user = await _sdk.UsersManager.GetSingleUser(id);
            return Json(await _sdk.UsersManager.RemoveUser(user), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ResetUserPassword(long id)
        {
            var user = await _sdk.UsersManager.GetSingleUser(id);
            return Json(await _sdk.UsersManager.ResetPassword(user), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetSingleSedcAccount(long id)
        {
            return Json(await _sdk.SEDCManager.GetSingleAccount(id), JsonRequestBehavior.AllowGet);
        }
    }
}