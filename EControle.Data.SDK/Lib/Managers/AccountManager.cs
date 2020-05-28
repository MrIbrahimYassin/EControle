using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EControle.Core.Data.Models;
using EControle.Core.Data.Types;
using EControle.Data.SDK.Lib.ViewModels;
using EControle.Data.SDK.Utilites;

namespace EControle.Data.SDK.Lib.Managers
{
    public class AccountManager
    {
        private Worker _manager;
        public AccountManager(Worker _worker)
        {
            _manager = _worker;
        }

        public async Task<List<AccountVM>> Accounts()
        {
            var users = await _manager.Users.Find(x => !x.IsDeleted);
            List<AccountVM> accountVms = new List<AccountVM>();
            try
            {
                foreach (var user in users)
                {
                    try
                    {
                        var transes = await _manager.InternalTransactions.Find(x => x.UserId == user.Id);
                        accountVms.Add(new AccountVM(user, transes.ToList()));
                    }
                    catch (Exception e)
                    {
                    }
                }

            }
            catch (Exception e)
            {
                return null;
            }
            return accountVms;
        }

        public async Task<bool> ToggleActivate(long id, long userId)
        {
            var acc = await _manager.Users.GetSingle(id);
            var user = await _manager.Users.GetSingle(userId);
            if (acc != null && user != null)
            {
                acc.IsActive = !acc.IsActive;
                _manager.Users.Update(acc);
                //_manager.AccountActivateMonitor.Add(new AccountActivateMonitor
                //{
                //    Account = acc,
                //    By = user,
                //    CreateDate = DateTime.Now,
                //    IsActive = acc.IsActive
                //});
            }

            return await _manager.Complete();
        }

        public async Task<bool> CompleteTransaction(long id, long userId)
        {
            var transaction = await _manager.InternalTransactions.GetSingle(id);
            var user = await _manager.Users.GetSingle(userId);

            if (transaction != null)
            {
                if (transaction.Direction == Direction.Out)
                {
                    return false;
                }
                else
                {
                    if (transaction.TransState == TransState.Pending && user != null)
                    {
                        var allTrans = await _manager.InternalTransactions.Find(x => x.Flag == transaction.Flag);
                        if (allTrans.ToList().Count == 2)
                        {
                            foreach (var tran in allTrans)
                            {
                                tran.TransState = TransState.Completed;
                                tran.CompleteDate = DateTime.Now;
                                tran.CompletedBy = user.Serial;
                                _manager.InternalTransactions.Update(tran);
                            }
                        }

                    }
                }

            }
            return await _manager.Complete();
        }

        public async Task<bool> RollBack(long id, long userId)
        {
            var transaction = await _manager.InternalTransactions.GetSingle(id);
            var user = await _manager.Users.GetSingle(userId);

            if (transaction != null && transaction.TransState == TransState.Pending && user != null)
            {
                transaction.TransState = TransState.RollBack;
                transaction.RollBackDate = DateTime.Now;
                transaction.CompletedBy = user.Serial;
                _manager.InternalTransactions.Update(transaction);
            }

            return await _manager.Complete();
        }

        public async Task<List<InternalTransaction>> Transactions(long id)
        {
            var owner = _manager.Users.Get(id);
            //var ac = await _manager.InternalTransactions.Find(x => x.Sender.Id == id);
            //return ac as List<InternalTransaction>;
            var transes = await _manager.InternalTransactions.Find(x => x.UserId == owner.Id);
            return transes.ToList().OrderBy(x => x.CreateDate).ToList();
        }



        public async Task<List<TransactionVM>> TransactionsFromUser(long user)
        {
            List<TransactionVM> transaction = new List<TransactionVM>();

            try
            {
                var owner = await _manager.Users.GetSingle(user);
                //var transes = await _manager.InternalTransactions.Find(x => x.UserId == user);
                var transes = await _manager._context.InternalTransactions.Where(x => x.UserId == user).ToListAsync();

                transes.ToList().ForEach(x => transaction.Add(new TransactionVM(x, owner)));
                return transaction.OrderBy(x => x.CreateDate).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<TransactionVM>> TransactionsFromUser(DateTime from, DateTime to, long user)
        {
            List<TransactionVM> transaction = new List<TransactionVM>();

            try
            {
                var owner = await _manager.Users.GetSingle(user);
                var transes = await _manager.InternalTransactions
                    .Find(x => x.UserId == user && 
                               (x.CreateDate >= DbFunctions.TruncateTime(from.Date)
                                || x.CreateDate <= DbFunctions.TruncateTime(to.Date)));

                transes.ToList().ForEach(x => transaction.Add(new TransactionVM(x, owner)));
                return transaction.OrderBy(x => x.CreateDate).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private async Task<bool> CreateTransaction(User senderUser, User toAccount, decimal amount, bool twoWay)
        {
            try
            {
                var flag = Guid.NewGuid();
                if (twoWay)
                {
                    _manager.InternalTransactions.Add(new InternalTransaction
                    {
                        CreateDate = DateTime.Now,
                        UserId = senderUser.Id,
                        Amount = amount,
                        Direction = Direction.In,
                        TransState = TransState.Pending,
                        CreatedBy = senderUser.Serial,
                        Flag = flag,
                        Comment = "NT",
                        CreatorName = senderUser.Name,
                        Account = toAccount
                    });
                    _manager.InternalTransactions.Add(new InternalTransaction
                    {
                        CreateDate = DateTime.Now,
                        UserId = toAccount.Id,
                        Amount = amount,
                        Direction = Direction.Out,
                        TransState = TransState.Pending,
                        CreatedBy = senderUser.Serial,
                        CompleteDate = DateTime.Now,
                        Flag = flag,
                        Comment = "NT",
                        CreatorName = senderUser.Name,
                        Account = senderUser
                    });
                }
                else
                {
                    _manager.InternalTransactions.Add(new InternalTransaction
                    {
                        CreateDate = DateTime.Now,
                        UserId = senderUser.Id,
                        Amount = amount,
                        Direction = Direction.In,
                        TransState = TransState.Completed,
                        CreatedBy = senderUser.Serial,
                        CompleteDate = DateTime.Now,
                        Flag = flag,
                        Comment = "INCH",
                        CreatorName = senderUser.Name,
                        Account = senderUser
                    });
                }

                return await _manager.Complete();
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<GenerateTransState> NewTransaction(decimal amount, string toId, long fromAccount, string cpass, bool recharge = false)
        {
            try
            {
                cpass = EncryptionManager.Encrypt(cpass);
                var senderUser = await _manager.Users.GetSingle(fromAccount);
                if (senderUser != null && cpass == senderUser.Password)
                {
                    if (recharge)
                    {

                        var state = await CreateTransaction(senderUser, senderUser, amount, false);
                        return (state) ? GenerateTransState.Success : GenerateTransState.Error;
                    }
                    else
                    {
                        var toAccount = await _manager.Users.SingleOrDefault(x => x.Serial == toId);
                        if (toAccount == null)
                            return GenerateTransState.AccountNotExist;

                        IEnumerable<InternalTransaction> senderTranses = await _manager.InternalTransactions.Find(x =>
                            x.UserId == senderUser.Id);
                        var vm = new AccountVM(senderTranses.ToList());

                        if (vm.Balance >= amount)
                        {
                            var state = await CreateTransaction(senderUser, toAccount, amount, true);
                            return (state) ? GenerateTransState.Success : GenerateTransState.Error;
                        }
                        else
                        {
                            return GenerateTransState.AmountIsHi;
                        }

                        //return (vm.Balance >= amount) ? ((await CreateTransaction(senderUser, toAccount, amount, true)) ? GenerateTransState.Success : GenerateTransState.Error) : GenerateTransState.AmountIsHi;

                    }

                }
                else
                {
                    return GenerateTransState.PasswordIsNotCurrect;
                }
            }
            catch (Exception e)
            {
                return GenerateTransState.Error;
            }
        }

        public async Task<AccountVM> AccountVM(long userId)
        {
            try
            {
                var user = await _manager.Users.GetSingle(userId);
                var transactions = await _manager.InternalTransactions.Find(x => x.UserId == userId) as List<InternalTransaction>;
                return new AccountVM(user, transactions);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> Recharge(User user, decimal amount)
        {
            try
            {
                return await CreateTransaction(user, user, amount, false);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}