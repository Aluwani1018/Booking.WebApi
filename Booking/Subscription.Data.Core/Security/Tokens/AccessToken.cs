using Subscription.Infrastructure.Exceptions;
using Subscription.Infrastructure.Exceptions.Model.Enum;
using System;

namespace Subscription.Core.Security.Tokens
{
    public class AccessToken : JsonWebToken
    {
        public RefreshToken RefreshToken { get; private set; }

        public AccessToken(string token, long expiration, RefreshToken refreshToken) : base(token, expiration)
        {
            if (refreshToken == null)
                throw new ApiException((int)ErrorEnum.InvalidRefreshToken, nameof(ErrorEnum.InvalidRefreshToken));

            RefreshToken = refreshToken;
        }
    }
}
