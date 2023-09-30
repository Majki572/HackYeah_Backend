using System.Security.Cryptography;
using System.Text;

namespace Backend;

public static class Utils
{
    public static string Hash(string password)
    {
        using var sha = new HMACSHA256(Encoding.UTF8.GetBytes("itwillbechanged"));
        
        byte[] textBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashBytes = sha.ComputeHash(textBytes);
        
        return BitConverter
            .ToString(hashBytes)
            .Replace("-", string.Empty);
    }

    public static bool IsPasswordEqualHash(string password, string hash)
    {
        return Utils.Hash(password) == hash;
    }
}