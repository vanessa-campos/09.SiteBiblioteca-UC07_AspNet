using System.Text;
using System.Security.Cryptography;

namespace Biblioteca.Models
{
    public class Encrypt
    {
        public static string TextEncrypted(string simpleText){

            MD5 MD5Hasher = MD5.Create();

            byte[] By = Encoding.Default.GetBytes(simpleText);
            byte[] bytesEncrypted = MD5Hasher.ComputeHash(By);

            StringBuilder SB = new StringBuilder();

            foreach (byte b in bytesEncrypted){
                string DebugB = b.ToString("x2");
                SB.Append(DebugB);
            }

            return SB.ToString();
        }
    }
}