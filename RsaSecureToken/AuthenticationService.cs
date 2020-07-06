using System.Collections.Generic;

namespace RsaSecureToken
{
    public class AuthenticationService
    {
        private ILog _log;
        private IProfileDao _profileDao;
        private ITokenDao _rsaToken;

        public AuthenticationService()
        {
            _profileDao = new ProfileDao();
            _rsaToken = new RsaTokenDao();
            _log = new ConsoleLog();
        }

        public AuthenticationService(IProfileDao profileDao, ITokenDao rsaToken, ILog consoleLog)
        {
            _profileDao = profileDao;
            _rsaToken = rsaToken;
            _log = consoleLog;
        }

        public bool IsValid(string account, string password)
        {
            // 根據 account 取得自訂密碼
            var passwordFromDao = _profileDao.GetPassword(account);

            // 根據 account 取得 RSA token 目前的亂數
            var randomCode = _rsaToken.GetRandom(account);

            // 將發生錯誤的 account 寫入 log

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
                _log.Save(content);
                return false;
            }
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