using System;

namespace EControle.Data.SDK.Lib.ViewModels
{
    public class PurchaseVM
    {
        public PurchaseVM()
        {
            transID =
                $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Millisecond}";
        }
        public string userName { get; set; }
        public string userPass { get; set; }
        public string transID { get; set; }
        public string meterNum { get; set; }
        public string calcMode { get; set; }
        public string amount { get; set; }
        public string verifyCode { get; set; }
        public string verifyData { get; set; }
      
    }
}