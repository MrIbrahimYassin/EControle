using System;

namespace EControle.Core.Data.Models
{
    public class SedcAccount
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HttpType { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string Key { get; set; }
        public bool IsDefault { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}