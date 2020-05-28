using System;
using EControle.Core.Data.Types;

namespace EControle.Core.Data.Models
{
    public class InternalTransaction
    {
        public long Id { get; set; }
        public Guid Flag { get; set; }
        public string Comment { get; set; }
       // public virtual User User { get; set; }
        public Direction Direction { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CreateDate { get; set; }
        public TransState TransState { get; set; }

        public DateTime? CompleteDate { get; set; }
        public DateTime? RollBackDate { get; set; }
        public string CompletedBy { get; set; }
        public string CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public User Account { get; set; }
        public long UserId { get; set; }
        //public long  ToUserId { get; set; }
        //public long  CreatedByUserId { get; set; }

    }
}