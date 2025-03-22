using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechGears.Services.Auth.Models;
using TechGears.Services.Auth.Service.IService;


namespace TechGears.Services.Auth.Service
{
    public class JwtTokenGeneratorImpl : IJwtTokenGenerator
    {

        private const int EXPIRE_DAYS = 1;

        // To have all the key values that are assignd in the (appsettings.json).
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGeneratorImpl(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(IdentityUser user)
        {

            // We want to generate token based on the user
            JwtSecurityTokenHandler tokenHandler = new();

            SecurityTokenDescriptor tokenDescriptor = ConfigTokenProp(user);

            // Generate the token.
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private byte[]? ExtractSecretKey() => Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        private List<Claim> TokenClaims(IdentityUser user) => [new("Email", user.Email), new("Sub", user.Id)];

        private SecurityTokenDescriptor ConfigTokenProp(IdentityUser user) => 
            new()
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(TokenClaims(user)),
                Expires = DateTime.UtcNow.AddDays(EXPIRE_DAYS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(ExtractSecretKey()), SecurityAlgorithms.HmacSha256Signature)
            };
    }
}