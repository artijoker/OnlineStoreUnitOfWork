using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HttpModels;
using Microsoft.IdentityModel.Tokens;

namespace HttpApiServer;

public class TokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;
    
    public TokenService(JwtConfig jwtConfig)
    {
        _jwtConfig = jwtConfig;
    }
    
    public string GenerateToken(Account account, string role)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim("roles", role)
            }),
            Expires = DateTime.UtcNow.Add(_jwtConfig.LifeTime),
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}