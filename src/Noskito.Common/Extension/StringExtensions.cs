using System;
using System.Security.Cryptography;
using System.Text;

namespace Noskito.Common.Extension
{
    public static class StringExtensions
    {
        public static string ToSha512(this string value)
        {
            using (var sha512 = new SHA512Managed())
            {
                var hashed = sha512.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashed).Replace("-", "");
            }
        }
    }
}