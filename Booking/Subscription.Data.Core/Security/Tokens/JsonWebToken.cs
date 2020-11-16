using Subscription.Infrastructure.Exceptions;
using Subscription.Infrastructure.Exceptions.Model.Enum;
using System;

namespace Subscription.Core.Security.Tokens
{
    public abstract class JsonWebToken
    {
        public string Token { get; protected set; }
        public long Expiration { get; protected set; }

        public JsonWebToken(string token, long expiration)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ApiException((int)ErrorEnum.InvalidToken, nameof(ErrorEnum.InvalidToken));

            if (expiration <= 0)
                throw new ApiException((int)ErrorEnum.InvalidExpiration, nameof(ErrorEnum.InvalidExpiration));

            Token = token;
            Expiration = expiration;
        }

        public bool IsExpired() => DateTime.UtcNow.Ticks > Expiration;
    }
}
