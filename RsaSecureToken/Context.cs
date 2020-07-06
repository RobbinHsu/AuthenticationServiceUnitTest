using System.Collections.Generic;

namespace RsaSecureToken
{
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