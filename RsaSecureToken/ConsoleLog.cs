using System;

namespace RsaSecureToken
{
    public class ConsoleLog : ILog
    {
        public void Save(string content)
        {
            Console.WriteLine(content);
        }
    }
}