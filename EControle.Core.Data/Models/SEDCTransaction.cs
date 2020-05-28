using System;
using System.Collections.Generic;
using EControle.Core.Data.Models.SEDC_Trans;

namespace EControle.Core.Data.Models
{
    public class SEDCTransaction
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public string state { get; set; }
        public string code { get; set; }
        public string transTime { get; set; }
        public string regCode { get; set; }
        public string refCode { get; set; }
        public string meterNum { get; set; }
        public string customerName { get; set; }
        public string tariffCode { get; set; }
        public string buyTimes { get; set; }
        public string calcQty { get; set; }
        public string vendQty { get; set; }
        public string vendAMT { get; set; }
        public string supplyAMT { get; set; }
        public string arrearAMT { get; set; }
        public string feeAMT { get; set; }
        public string AMT { get; set; }
        public string VAT { get; set; }
        public string stampTax { get; set; }
        public string netAMT { get; set; }
        public string commAMT { get; set; }
        public string token { get; set; }
        public string invoice { get; set; }
        public string verifyCode { get; set; }
        public string checkCode { get; set; }
        public string transID { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Power> Powers { get; set; }
        public virtual ICollection<Fee> Fees{ get; set; }
        public virtual ICollection<Arrear> Arrears { get; set; }
    }
}