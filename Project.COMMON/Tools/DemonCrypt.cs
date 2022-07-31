using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.COMMON.Tools
{
    public static class DemonCrypt
    {
        public static string Crypt(string a)
        {
            string hashedCode = "";
            
            foreach (char item in a)
            {
                hashedCode += Convert.ToChar(item + 1).ToString();
            }
            return hashedCode;
        }

        public static string DeCrypt(string a)
        {
            string decryptedCode = "";

            foreach (char item in a)
            {
                decryptedCode += Convert.ToChar(item - 1).ToString();
            }
            return decryptedCode;
        }
    }
}
