using System;

namespace RsaSecureToken
{
    public class RsaTokenDao : ITokenDao
    {
        public string GetRandom(string account)
        {
            var seed = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
            var result = seed.Next(0, 999999).ToString("000000");
            Console.WriteLine("randomCode:{0}", result);

            return result;
        }
    }
}