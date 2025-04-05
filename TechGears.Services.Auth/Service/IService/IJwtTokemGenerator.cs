using Microsoft.AspNetCore.Identity;

namespace TechGears.Services.Auth.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityUser user, IEnumerable<string> roles);
    }
}