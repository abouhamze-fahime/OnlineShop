using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utility;

public class EncryptionUtility
{
    private readonly AppConfigurations appConfig;
    public EncryptionUtility(IOptions<AppConfigurations> options)
    {
        this.appConfig = options.Value;
    }
    public string GetSHA256(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes =sha256.ComputeHash(Encoding.UTF8.GetBytes(password +salt));
            var result = BitConverter.ToString(bytes).Replace("-", "").ToLower(); 
            return result;
        }
    }

    public string GetNewSalt()
    {
        return Guid.NewGuid().ToString();
    }


    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
    public string GenerateNewToken(Guid userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(appConfig.TokenKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim("userId", userId.ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(appConfig.TokenTimeOut),
            SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }

}