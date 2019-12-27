using System;
using System.Threading.Tasks;
using Auth;
using Identity.Params.Inputs;

namespace Identity.Services
{
    public interface ITokenService
    {
        Task<ClientJsonWebToken> ClientLoginAsync(ClientLoginParam input);
        Task<JsonWebToken> LoginAsync(LoginParam input);
        Task LogoutAsync(LogoutParam input);
    }
}
