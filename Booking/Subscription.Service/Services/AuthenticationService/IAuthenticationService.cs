using System.Threading.Tasks;
using Subscription.Core.Security.Tokens;

namespace Subscription.Service.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<AccessToken> LogInAsync(string email, string password);
        Task<AccessToken> RefreshTokenAsync(string refreshToken, string userEmail);
        void RevokeRefreshToken(string refreshToken);

        string GetUserEmail();
    }
}
