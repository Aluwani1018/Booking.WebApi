
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Subscription.Core.Domain;
using Subscription.Infrastructure.Exceptions;
using Subscription.Infrastructure.Exceptions.Model.Enum;
using Subscription.Core.Security.Tokens;
using Subscription.Service.Services.UserService;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Subscription.Service.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string Sub => "sub";

        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler, IHttpContextAccessor httpContextAccessor)
        {
            this._tokenHandler = tokenHandler;
            this._userService = userService;
            this._httpContextAccessor = httpContextAccessor;

        }

        public async Task<AccessToken> LogInAsync(string email, string password)
        {
            User user = await _userService.FindByEmailAsync(email);

            if (user == null)
            {
                throw new ApiException((int)ErrorEnum.InvalidCredentials, nameof(ErrorEnum.InvalidCredentials));
            }

            bool isPawordValid = await _userService.CheckPasswordAsync(user, password);

            if (!isPawordValid)
            {
                throw new ApiException((int)ErrorEnum.InvalidCredentials, nameof(ErrorEnum.InvalidCredentials));
            }
            AccessToken accessToken = _tokenHandler.CreateAccessToken(user);

            return accessToken;
        }

        public async Task<AccessToken> RefreshTokenAsync(string refreshToken, string userEmail)
        {
            var token = _tokenHandler.TakeRefreshToken(refreshToken);

            if (token == null)
            {
               throw new ApiException((int)ErrorEnum.InvalidRefreshToken, nameof(ErrorEnum.InvalidRefreshToken), null);
            }

            if (token.IsExpired())
            {
                throw new ApiException((int)ErrorEnum.ExpiredRefreshToken, nameof(ErrorEnum.ExpiredRefreshToken), null);
            }

            var user = await _userService.FindByEmailAsync(userEmail);
            if (user == null)
            {
                throw new ApiException((int)ErrorEnum.InvalidRefreshToken, nameof(ErrorEnum.InvalidRefreshToken), null); ;
            }

            var accessToken = _tokenHandler.CreateAccessToken(user);
            return accessToken;
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }

        public string GetUserEmail()
        {
            AuthenticationHeaderValue authorizationHeader = AuthenticationHeaderValue.Parse(_httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization]);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenS = handler.ReadJwtToken(authorizationHeader.Parameter) as JwtSecurityToken;

            return tokenS?.Claims.First(claim => claim.Type == Sub).Value;

        }
    }
}
