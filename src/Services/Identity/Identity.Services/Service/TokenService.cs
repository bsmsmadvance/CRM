using System;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using Database.Models;
using Database.Models.USR;
using ErrorHandling;
using Identity.Params.Inputs;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly IJwtHandler JwtHandler;
        private readonly DatabaseContext DB;

        public TokenService(IJwtHandler jwtHandler, DatabaseContext db)
        {
            this.JwtHandler = jwtHandler;
            this.DB = db;
        }

        public async Task<ClientJsonWebToken> ClientLoginAsync(ClientLoginParam input)
        {
            var user = await DB.Users.FirstOrDefaultAsync(o => o.ClientID == input.client_id && o.ClientSecret == input.client_secret && o.IsClient);
            if (user == null)
            {
                throw new UnauthorizedException("Client ID or Secret is incorrect");
            }
            return JwtHandler.ClientCreate(user.ID, user.DisplayName);
        }

        public async Task<JsonWebToken> LoginAsync(LoginParam input)
        {
            if (input.grant_type == "password")
            {
                var user = await DB.Users.FirstOrDefaultAsync(o => o.EmployeeNo == input.username);
                if (user == null)
                {
                    throw new UnauthorizedException("Username or password is incorrect");
                }

                var refreshToken = await GenerateNewRefreshTokenAsync(user.ID);
                return JwtHandler.Create(user.ID, user.DisplayName, refreshToken);

                // TODO : เช็ค Password กับ AD.
            }
            else if (input.grant_type == "refresh_token")
            {
                var token = await DB.RefreshTokens.Include(o => o.User).FirstOrDefaultAsync(o => o.Token == input.refresh_token);
                if (token == null)
                {
                    throw new UnauthorizedException("Refresh token not found");
                }

                if (token.ExpireDate < DateTime.UtcNow)
                {
                    throw new UnauthorizedException("Refresh token has been expired");
                }

                var refreshToken = await GenerateNewRefreshTokenAsync(token.UserID);
                DB.Remove(token);
                await DB.SaveChangesAsync();
                return JwtHandler.Create(token.UserID, token.User.DisplayName, refreshToken);
            }
            else
            {
                throw new UnauthorizedException("Invalid grant type (must be \"password\" or \"refresh_token\")");
            }
        }

        public async Task LogoutAsync(LogoutParam input)
        {
            var token = await DB.RefreshTokens.FirstOrDefaultAsync(o => o.Token == input.RefreshToken);
            if (token != null)
            {
                DB.Remove(token);
                await DB.SaveChangesAsync();
            }
        }

        private async Task<string> GenerateNewRefreshTokenAsync(Guid userID)
        {
            RefreshToken token = new RefreshToken();
            token.Token = Guid.NewGuid().ToString("N");
            token.UserID = userID;
            token.ExpireDate = DateTime.UtcNow.AddDays(14);
            await DB.AddAsync(token);
            await DB.SaveChangesAsync();
            return token.Token;
        }
    }
}
