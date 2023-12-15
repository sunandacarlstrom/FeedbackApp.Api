

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FeedbackApp.Api.Models;

public class JwtSettings
{
    public string? Key { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }

    public string GenerateJwt(User user, IList<string> userRole)
    {
        var secretKey = Key;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        foreach (var role in userRole)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        var serializedToken = tokenHandler.WriteToken(token);
        return serializedToken;
    }

    public string? GetClaim(string token, string claim)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if (!tokenHandler.CanReadToken(token))
        {
            return null;
        }
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var subClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == claim);


        return subClaim?.Value;
    }
}
