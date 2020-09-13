using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace StockaProSSO.Utilities
{
    public static class AppUtilities
    {
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            var validator = new EmailAddressAttribute();
            return validator.IsValid(email);

        }

        public static string Hash512(string text)
        {
            string hash = "";
            using (SHA512 alg = new SHA512Managed())
            {
                byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(text));
                hash = HexStrFromBytes(result).ToUpper();
            }
            return hash;
        }

        private static string HexStrFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        public static string HashSHA1(string data)
        {
            string hash = "";
            using (SHA1 alg = new SHA1Managed())
            {
                byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(data));
                hash = HexStrFromBytes(result);
            }
            return hash;
        }

        public static string HashMD5(string data)
        {
            string hash = "";
            using (MD5 alg = MD5.Create())
            {
                byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(data));
                hash = HexStrFromBytes(result);
            }
            return hash;
        }

        public static IDictionary<string, object> OwinEnvironmentVariables
        {
            get
            {
                return OwinRequestScopeContext.Current.Environment;

            }
        }

        public static string RemoteIp
        {
            get
            {
                return OwinEnvironmentVariables != null ? OwinEnvironmentVariables.FirstOrDefault(x => x.Key == "server.RemoteIpAddress").Value as string
                               : string.Empty;
            }
        }

        public static string LocalIp
        {
            get
            {
                return OwinEnvironmentVariables != null ? OwinEnvironmentVariables.FirstOrDefault(x => x.Key == "server.LocalIpAddress").Value as string
                    : string.Empty;
            }
        }
    }

    public class StringItemComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}
