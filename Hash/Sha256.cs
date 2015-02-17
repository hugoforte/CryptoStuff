using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Crypto
{
    public class Sha256 : IGenerateHash
    {
        public string GetHash(object toHash)
        {
            var json = JsonConvert.SerializeObject(toHash);
            return GetHash(json);
        }

        public string GetHash(string toHash)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(toHash);
            var hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return hash.Aggregate(string.Empty, (current, x) => current + String.Format("{0:x2}", x));
        }
    }

    public interface IGenerateHash
    {
        string GetHash(object toHash);
        string GetHash(string toHash);
    }
}
