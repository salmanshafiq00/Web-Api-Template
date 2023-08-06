using Microsoft.AspNetCore.Identity;

namespace WebApiUdemy.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}