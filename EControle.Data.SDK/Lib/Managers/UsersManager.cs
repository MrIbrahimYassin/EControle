using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EControle.Core.Data.Models;
using EControle.Core.Data.Types;
using EControle.Data.SDK.Lib.ViewModels;
using EControle.Data.SDK.Utilites;

namespace EControle.Data.SDK.Lib.Managers
{
    public class UsersManager
    {
        private Worker _manager;
        public UsersManager(Worker _worker)
        {
            _manager = _worker;
        }

        /// <summary>
        /// Get user from the database.
        /// </summary>
        /// <param name="name">user name</param>
        /// <param name="pass">user password</param>
        /// <returns></returns>
        public async Task<User> Login(string name) => await _manager.Users.SingleOrDefault
            (x => (x.UserName == name || x.Phone == name));

        /// <summary>
        /// Add new user.
        /// </summary>
        /// <param name="user">user obj</param>
        /// <returns></returns>
        public async Task<bool> AddUser(User user, long creatorId)
        {
            //Check if the user is exist first.
            var isExist = await _manager.Users.SingleOrDefault(x =>
                x.UserName == user.UserName || x.Phone == user.Phone);
            var creator = await _manager.Users.GetSingle(creatorId);
            if (isExist == null && creator != null)
            {
                //Add the user.  
                user.Serial = Guid.NewGuid().ToString();
                user.CreateDate = DateTime.Now;
                user.SetNewPassword = true;
                user.CreatedBy = creator;
                _manager.Users.Add(user);
            }
            //will return true if it has been added.
            //await _manager.Complete();
            ////Create Account
            //var us = await _manager.Users.SingleOrDefault(x =>
            //    x.UserName == user.UserName || x.Phone == user.Phone);
            //var creator = await _manager.Users.GetSingle(creatorId);

            //if (us!= null && creator != null)
            //{
            //    var account = await _manager.Accounts.SingleOrDefault(c => c.Owner.Id == us.Id);
            //    if (account == null)
            //    {
            //        _manager.Accounts.Add(new Account
            //        {
            //            AccountType = us.Role, CreateDate = DateTime.Now, CreatedBy = creator, Owner = us, Serial = Guid.NewGuid()
            //        });
            //    }
            //}
            return await _manager.Complete();
        }

        /// <summary>
        /// Flag user as Deleted, It can be recovered.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> RemoveUser(User user)
        {
            //Check the user.
            var isExist = await _manager.Users.SingleOrDefault(x =>
                x.UserName == user.UserName || x.Phone == user.Phone);
            if (isExist != null)
            {
                //Flag as Deleted user.
                user.IsDeleted = true;
                _manager.Users.Update(user);
            }
            return await _manager.Complete();
        }

        /// <summary>
        /// Update user info.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(User user)
        {
            var isExist = await _manager.Users.SingleOrDefault(x =>
                (x.UserName == user.UserName || x.Phone == user.Phone) && x.Id != user.Id);
            if (isExist == null)
            {
                var newUser = await _manager.Users.GetSingle(user.Id);
                if (newUser != null)
                {
                    newUser.Name = user.Name;
                    newUser.Phone = user.Phone;
                    newUser.UserName = user.UserName;
                    _manager.Users.Update(newUser);
                }
            }
            return await _manager.Complete();
        }

        /// <summary>
        /// Add new password to the user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> SetPassword(User user)
        {
            //check the user first.
            var isExist = await _manager.Users.SingleOrDefault(x =>
                x.UserName == user.UserName || x.Phone == user.Phone);
            if (isExist != null)
            {
                //set password.
                isExist.Password = EncryptionManager.Encrypt(user.Password);
                isExist.SetNewPassword = false;
                _manager.Users.Update(isExist);
            }
            return await _manager.Complete();
        }

        //GET User for layout page from cookies 
        public User GetSingleUserStatic(long id) => _manager.Users.Get(id);

        public async Task<User> GetSingleUser(long id)
        {
            var user = await _manager.Users.GetSingle(id);
            // user.Password = "";
            return user;
        }


        public async Task<bool> ConfiremPassword(User us, string pass)
        {
            //Encrypting the password first.
            pass = EncryptionManager.Encrypt(pass);
            var user = await _manager.Users.SingleOrDefault(x => x.Id == us.Id);
            return ((user != null) && user.Password == pass);
        }

        public async Task<List<UserVM>> GetUsers(int role)
        {
            var us = await _manager.Users.Find(x => x.Role == (Role)role);
            var users = await (from s in _manager._context.Users
                               where
                                   !s.IsDeleted && s.Role == (Role)role
                               select new UserVM
                               {
                                   Id = s.Id,
                                   CreateDate = s.CreateDate,
                                   Gender = s.Gender,
                                   Img = s.Img,
                                   IsActive = s.IsActive,
                                   UserName = s.UserName,
                                   Phone = s.Phone,
                                   Name = s.Name,
                                   Serial = s.Serial
                               }).ToListAsync();
            List<User> reList = new List<User>();
            //foreach (var user in users)
            //{
            //    user.Password = "";
            //    user.Transactions = null;
            //    reList.Add(user);
            //}

            return users.OrderByDescending(x => x.Name).ToList();
        }

        public async Task<bool> ResetPassword(User user)
        {
            user.Password = "";
            user.SetNewPassword = true;
            _manager.Users.Update(user);
            return await _manager.Complete();
        }
    }
}