namespace RsaSecureToken
{
    public class ProfileDao : IProfileDao
    {
        public string GetPassword(string account)
        {
            return Context.GetPassword(account);
        }
    }
}