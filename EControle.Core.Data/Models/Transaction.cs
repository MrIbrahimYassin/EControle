using System;

namespace EControle.Core.Data.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public string TransactionId { get; set; }
        
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public double KWht { get; set; }
        public virtual User  User { get; set; }
        public string ServerResponse { get; set; }
    }
}