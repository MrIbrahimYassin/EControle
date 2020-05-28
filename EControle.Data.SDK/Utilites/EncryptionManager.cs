using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EControle.Data.SDK.Utilites
{

    public static class EncryptionManager
    {
        public static string Encrypt(string rawData)
        {
            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        public static byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
        public static byte[] ConvertToBytes(HttpPostedFile image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();

        }

        public static string MD5Encrypt(string name,string pass, string key)
        {
            var res = MD5Hash(name.ToUpper() + pass);
            res = MD5Hash($"{res}{key}");
            return MD5Hash($"{res}{key}");
        }

        public static string VDataMD5(string purchaseUserName, string purchaseUserPass, string purchaseTransId, string purchaseMeterNum, string purchaseCalcMode, string purchaseAmount, string sedcAccountKey)
        {
            purchaseUserPass = MD5Hash($"{purchaseUserPass}");
           return MD5Hash(purchaseUserName.ToUpper() + purchaseUserPass + purchaseTransId + purchaseMeterNum + purchaseCalcMode + purchaseAmount + sedcAccountKey);
        }
    }
}