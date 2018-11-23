using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HashPassword
    {
        public static string Hash(string password)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] HashData = sha1.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<HashData.Length;i++)
            {
                sb.Append(HashData[i].ToString());
            }
            return sb.ToString();
        }

        public static bool UnHash(string password,string hashpassword)
        {
            string SHA1Password = Hash(password);
            if (string.Compare(SHA1Password, hashpassword)==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
