namespace RsaSecureToken
{
    public interface ITokenDao
    {
        string GetRandom(string account);
    }
}