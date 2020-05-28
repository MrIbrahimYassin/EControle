using System;
using System.Collections.Generic;
using System.Linq;
using EControle.Core.Data.Models;
using EControle.Core.Data.Types;
using EControle.Data.SDK.Utilites;

namespace EControle.Data.SDK.Lib.ViewModels
{
    public sealed class AccountVM 
    {
        public User User { get; set; }
        public List<InternalTransaction> InternalTransactions { get; set; }
       
        public AccountVM(User thisAccount, List<InternalTransaction> tsList)
        {
            User = thisAccount;
            TotalTransaction = tsList.Count;
            InternalTransactions = tsList;
            LastActivity = InternalTransactions.OrderBy(x => x.CreateDate).FirstOrDefault()?.CreateDate;
            LastActivity2 = (LastActivity.HasValue)
                ? $"{LastActivity.Value.Date.ToShortDateString()}|{LastActivity.Value.Date.ToShortTimeString()}"
                : ""; 
            TotalIncome = InternalTransactions.Where(x => x.Direction == Direction.In && x.TransState == TransState.Completed).Sum(x => x.Amount);
            TotalOutCome = InternalTransactions.Where(x => x.Direction == Direction.Out && x.TransState == TransState.Completed).Sum(x => x.Amount);
            Balance = TotalIncome - TotalOutCome;
        }
        public AccountVM( List<InternalTransaction> tsList)
        {
            User thisAccount = new User();
            thisAccount.Password = "";
            thisAccount.Role = Role.SealsAgent;
            thisAccount.IsActive = false;
            thisAccount.IsDeleted = true;
            thisAccount.UserName = "";
            thisAccount.CreateDate = DateTime.MaxValue;
            InternalTransactions = tsList;//Convertor.GetTemplate(tsList);
            User = thisAccount;
            TotalTransaction = InternalTransactions.Count;
            LastActivity = InternalTransactions.OrderBy(x => x.CreateDate).FirstOrDefault()?.CreateDate;
            TotalIncome = InternalTransactions.Where(x => x.Direction == Direction.In && x.TransState == TransState.Completed).Sum(x => x.Amount);
            TotalOutCome = InternalTransactions.Where(x => x.Direction == Direction.Out && x.TransState == TransState.Completed).Sum(x => x.Amount);
            Balance = TotalIncome - TotalOutCome;
        }

        public int TotalTransaction { get; set; }
        public DateTime? LastActivity { get; set; }
        public string LastActivity2 { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalOutCome { get; set; }
        public decimal Balance { get; set; }
    }
}