using System;

namespace EControle.Core.Data.Models
{
    public class BalanceSheet
    {
        public long Id { get; set; }
        public int State { get; set; }
        public double Balance { get; set; }
        public DateTime CheckDate { get; set; }
        public string Username { get; set; }
    }
}