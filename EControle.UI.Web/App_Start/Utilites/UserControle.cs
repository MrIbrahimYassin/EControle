using EControle.Core.Data.Models;

namespace EControle.UI.Web.Utilites
{
    public static class UserControle
    {
        public static User GetUser(long id)
        {
            Data.SDK.Lib.SDK sdk = new Data.SDK.Lib.SDK();
            return sdk.UsersManager.GetSingleUserStatic(id);
        }

    }
}