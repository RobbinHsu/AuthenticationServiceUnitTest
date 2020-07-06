using System;
using System.Collections.Generic;

namespace RsaSecureToken
{
    public class AuthenticationService
    {
        private IProfileDao _profileDao;
        private ITokenDao _rsaToken;

        public AuthenticationService()
        {
            _profileDao = new ProfileDao();
            _rsaToken = new RsaTokenDao();
        }

        public AuthenticationService(IProfileDao profileDao, ITokenDao rsaToken)
        {
            _profileDao = profileDao;
            _rsaToken = rsaToken;
        }

        public bool IsValid(string account, string password)
        {
            // 根據 account 取得自訂密碼
            var passwordFromDao = _profileDao.GetPassword(account);

            // 根據 account 取得 RSA token 目前的亂數
            var randomCode = _rsaToken.GetRandom(account);

            // 將發生錯誤的 account 寫入 log
            var log = new ConsoleLog();

            // 驗證傳入的 password 是否等於自訂密碼 + RSA token亂數
            var validPassword = passwordFromDao + randomCode;
            var isValid = password == validPassword;

            if (isValid)
            {
                return true;
            }
            else
            {
                var content = $"account:{account} try to login failed";
                log.Save(content);
                return false;
            }
        }
    }

    public class ConsoleLog
    {
        public void Save(string content)
        {
            Console.WriteLine(content);
        }
    }

    public static class Context
    {
        public static Dictionary<string, string> profiles;

        static Context()
        {
            profiles = new Dictionary<string, string>();
            profiles.Add("mei", "99");
            profiles.Add("robbin", "aaa222");
        }

        public static string GetPassword(string key)
        {
            return profiles[key];
        }
    }
}