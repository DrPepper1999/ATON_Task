using System;
using Users.Domain;

namespace Users.Application.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(string key, string JwtIssuer, string JwtAudience, User user);
    }
}
