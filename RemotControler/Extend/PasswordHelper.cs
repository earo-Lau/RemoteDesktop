using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemotControler.Extend
{
    public class PasswordHelper
    {
        public static string EncodePwd(string pwd)
        {
            byte[] pwd_byte = System.Text.Encoding.UTF8.GetBytes(pwd);
            int count=pwd_byte.Count()-1;
            byte[] temp = new byte[count + 1];
            for (int i = 0; i < pwd_byte.Count(); i++)
            {
                temp[count - i] = (byte)(pwd_byte[i] << i % 2);
            }
            string code = Convert.ToBase64String(temp);

            return code;
        }

        public static string DecodePwd(string pwd)
        {
            byte[] pwd_byte = Convert.FromBase64String(pwd);
            int count = pwd_byte.Count() - 1;
            byte[] temp = new byte[count + 1];
            for (int i = 0; i < pwd_byte.Count(); i++)
            {
                temp[count - i] = (byte)(pwd_byte[i] >> i % 2);
            }
            string code = System.Text.Encoding.UTF8.GetString(temp);

            return code;
        }
    }
}
