using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Imoby.Domain.Interface.Service;

namespace Imoby.Auth;
public class JwtService
{
    private readonly string _secretKey;
    private readonly IServiceUsuario _user;

    public JwtService(string secretKey, IServiceUsuario user)
    {
        _secretKey = secretKey;
        _user = user;
    }

    public JwtService(string secretKey = null)
    {
        _secretKey = secretKey ?? GenerateRandomSecretKey();
    }
    private string GenerateRandomSecretKey()
    {
        var random = new Random();
        var bytes = new byte[32];
        random.NextBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public bool PadraoEmail(string email)
    {
        return Regex.IsMatch(email, @"^[a-z0-9\\.@]+$");
    }

    public bool PadraoSenha(string senha)
    {
        if (senha.Length <= 6)
            return false;

        var caracter = Regex.IsMatch(senha, @"[!""#$%&'()*+,-./:;?@[\\\]_`{|}~]");
        if (!caracter) return caracter;
       
        if (senha.ToLower() == senha)
            caracter = false;
        return caracter;
    }

    public string GenerateToken(string userId, string username, int expireMinutes = 60)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, username)
                }),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }
    
}
