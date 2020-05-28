using System.Collections.Generic;

namespace EControle.Data.SDK.Utilites
{
    public static class SEDCOprations
    {

        public static string Core = "";
        public static string Base = $"{Core}/tpService.php?ACTION="; 
        public static string PURCHASE = $"{Base}PURCHASE"; 
        public static string SEARCH = $"{Base}SEARCH";
        public static string BALANCE = $"{Base}BALANCE";
        public static string GETTRANS = $"{Base}GETTRANS";
    }

    public  class ErrorMsg
    {
        public string Message { get; set; }
        public int Code { get; set; }
    }
}