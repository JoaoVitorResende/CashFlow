using BC = BCrypt.Net.BCrypt;
using CashFlow.Domain.Security.Cryptography;

namespace CashFlow.Infrastucture.Security.CryptographyPassWord;
public class Cryptography : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword("my password");
        return passwordHash;
    }
    public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
}
