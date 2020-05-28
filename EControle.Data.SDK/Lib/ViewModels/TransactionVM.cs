using System.Collections.Generic;
using System.Linq;
using EControle.Core.Data.Models;
using EControle.Core.Data.Types;

namespace EControle.Data.SDK.Lib.ViewModels
{
    public class TransactionVM : InternalTransaction
    {
        public User User { get; set; }
        public User Account { get; set; }
        public string CreateDate2 { get; set; }
        public List<TransactionVM> TransactionVms { get; set; }
        public TransactionVM() { }
        public TransactionVM(InternalTransaction tr, User u)
        {
            Id = tr.Id;
            UserId = tr.UserId;
            Amount = tr.Amount;
            Comment = tr.Comment;
            CompleteDate = tr.CompleteDate;
            CreateDate = tr.CreateDate;
            //
            CreateDate2 = (tr.CreateDate.HasValue)
                ? $"{tr.CreateDate.Value.ToShortDateString()} {tr.CreateDate.Value.ToShortTimeString()}"
                : "";
            //
            CompletedBy = tr.CompletedBy;
            CreatorName = tr.CreatorName;
            Direction = tr.Direction;
            CreatedBy = tr.CreatedBy;
            Flag = tr.Flag;
            RollBackDate = tr.RollBackDate;
            TransState = tr.TransState;
            Account =new User { Name = tr.Account.Name };
            User = new User{Name = u.Name};

        }

        //public TransactionVM(List<InternalTransaction> transactions, User u)
        //{
        //    TransactionVms = new List<TransactionVM>();
        //    foreach (var tr in transactions)
        //    {
        //        TransactionVms.Add(new TransactionVM
        //        {
        //            User = new User
        //            {
        //                Name = u.Name
        //            },
        //            Id = tr.Id,
        //            UserId = tr.UserId,
        //            Amount = tr.Amount,
        //            Comment = tr.Comment,
        //            CompleteDate = tr.CompleteDate,
        //            CreateDate = tr.CreateDate,
        //            CompletedBy = tr.CompletedBy,
        //            CreatorName = tr.CreatorName,
        //            Direction = tr.Direction,
        //            CreatedBy = tr.CreatedBy,
        //            Flag = tr.Flag,
        //            RollBackDate = tr.RollBackDate,
        //            TransState = tr.TransState,
        //            Account = transactions.SingleOrDefault(x=>x.Flag == tr.Flag && x.Id != tr.Id).UserId.
        //        });
        //    }
            //Id = tr.Id;
            //UserId = tr.UserId;
            //Amount = tr.Amount;
            //Comment = tr.Comment;
            //CompleteDate = tr.CompleteDate;
            //CreateDate = tr.CreateDate;
            //CompletedBy = tr.CompletedBy;
            //CreatorName = tr.CreatorName;
            //Direction = tr.Direction;
            //CreatedBy = tr.CreatedBy;
            //Flag = tr.Flag;
            //RollBackDate = tr.RollBackDate;
            //TransState = tr.TransState;
            ////

            //User = u;

        //}
    }
}