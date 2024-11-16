using CashFlow.Domain.Entities;
using CashFlow.Infrastucture.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashFlow.Infrastucture.Security.Token;
internal class JwtTokenGenarator : IAcessTokenGenarator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _singinKey;
    public JwtTokenGenarator(uint expirationTimeMinutes, string singinKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _singinKey = singinKey;
    }
    public string Generate(User user)
    {
        var claims = new List<Claim>()
        { 
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString())
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(),SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity()
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_singinKey);
        return new SymmetricSecurityKey(key);
    }
}
