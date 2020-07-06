using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsaSecureToken;

namespace ContextSample
{
    #region Log Implementation

    public class ConsoleLog : ILog
    {
        public void Save(string content)
        {
            Console.WriteLine(string.Format("{0}，log內容：{1}", DateTime.Now.ToString(), content));
        }
    }

    public class FileLog : ILog
    {
        public void Save(string content)
        {
            throw new NotImplementedException();
        }
    }

    #endregion Log Implementation

    internal class Program
    {
        private static AuthenticationService GetAuthenticationService()
        {
            var profile = new ProfileDao();
            var token = new RsaTokenDao();

            // 你可以隨時抽換 log 的紀錄方式，只需要實作 ILog
            var log = GetLog();

            var result = new AuthenticationService(profile, token, log);
            return result;
        }

        private static ILog GetLog()
        {
            var logType = GetLogType();
            switch (logType.ToString())
            {
                case "B":
                    return new FileLog();

                case "A":
                default:
                    return new ConsoleLog();
            }
        }

        private static string GetLogType()
        {
            // 可以從config來，可以從DB來，可以依據條件來決定，要使用哪一種類型的 log
            Console.WriteLine("你想怎麼記log, 輸入A，Console.Writeline()，輸入B，寫到log檔案");
            var logType = Console.ReadLine();
            return logType;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("請輸入你的帳號:");
            var account = Console.ReadLine();

            Console.WriteLine("請輸入你的密碼:");
            var password = Console.ReadLine();

            var auth = GetAuthenticationService();
            var result = auth.IsValid(account, password);
            Console.WriteLine("你是否通過驗證：{0}", result ? "Yes" : "No");
        }
    }
}