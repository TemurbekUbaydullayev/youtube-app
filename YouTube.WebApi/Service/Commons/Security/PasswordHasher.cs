using System.Text;
using XSystem.Security.Cryptography;

namespace YouTube.WebApi.Service.Commons.Security;

public class PasswordHasher
{
    private const string _key = "2d0f1460-033f-43a1-88d7-cdf57898e23e";

    public static string Hash(string password)
    {
        string _password = password + _key;

        var tmpSource = ASCIIEncoding.ASCII.GetBytes(_password);
        var hash = Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(tmpSource));

        return hash;
    }

    public static bool Verify(string password, string hash)
    {
        string _password = password + _key;

        byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(_password);
        var _hash = Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(tmpSource));

        return _hash == hash;
    }
}
