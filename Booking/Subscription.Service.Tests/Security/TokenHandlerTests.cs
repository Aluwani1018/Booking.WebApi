﻿

using Microsoft.Extensions.Options;
using Moq;
using Subscription.Core.Domain;
using Subscription.Core.Domain.Enums;
using Subscription.Core.Security.Tokens;
using Subscription.Data.Configurations;
using Subscription.Data.Security.Tokens;
using System;
using System.Collections.ObjectModel;
using Xunit;

namespace Subscription.Service.Tests.Security
{
    public class TokenHandlerTests
    {
        private Mock<IOptions<TokenOptions>> _tokenOptions;
        private SigningConfig _signingConfigurations;
        private User _user;

        private ITokenHandler _tokenHandler;
        private string _testKey = "5q3ZpUQtPUviVTNAsvQqzrR_YsFjdAdjCnBJuLu6dlyW9";

        public TokenHandlerTests()
        {
            SetupMocks();
            _tokenHandler = new TokenHandler(_tokenOptions.Object, _signingConfigurations);
        }

        private void SetupMocks()
        {
            _tokenOptions = new Mock<IOptions<TokenOptions>>();
            _tokenOptions.Setup(to => to.Value).Returns(new TokenOptions
            {
                Audience = "Testing",
                Issuer = "Testing",
                AccessTokenExpiration = 30,
                RefreshTokenExpiration = 60
            });

            _signingConfigurations = new SigningConfig(_testKey);

            _user = new User
            {
                Id = 1,
                FirstName = "Aluwani",
                LastName = "Mathode",
                Email = "test@test.com",
                PasswordHash = "123",
                Roles = new Collection<UserRole>
                {
                    new UserRole
                    {
                        Role = new Role
                        {
                            Id = 1,
                            Name = ApplicationRolesEnum.Common.ToString()
                        }
                    }
                }
            };
        }

        [Fact]
        public void Should_Create_Access_Token()
        {
            var accessToken = _tokenHandler.CreateAccessToken(_user);

            Assert.NotNull(accessToken);
            Assert.NotNull(accessToken.RefreshToken);
            Assert.NotEmpty(accessToken.Token);
            Assert.NotEmpty(accessToken.RefreshToken.Token);
            Assert.True(accessToken.Expiration > DateTime.UtcNow.Ticks);
            Assert.True(accessToken.RefreshToken.Expiration > DateTime.UtcNow.Ticks);
            Assert.True(accessToken.RefreshToken.Expiration > accessToken.Expiration);
        }

        [Fact]
        public void Should_Take_Existing_Refresh_Token()
        {
            var accessToken = _tokenHandler.CreateAccessToken(_user);
            var refreshToken = _tokenHandler.TakeRefreshToken(accessToken.RefreshToken.Token);

            Assert.NotNull(refreshToken);
            Assert.Equal(accessToken.RefreshToken.Token, refreshToken.Token);
            Assert.Equal(accessToken.RefreshToken.Expiration, refreshToken.Expiration);
        }

        [Fact]
        public void Should_Return_Null_For_Empty_Refresh_Token_When_Trying_To_Take()
        {
            var refreshToken = _tokenHandler.TakeRefreshToken("");
            Assert.Null(refreshToken);
        }

        [Fact]
        public void Should_Return_Null_For_Invalid_Refresh_Token_When_Trying_To_Take()
        {
            var refreshToken = _tokenHandler.TakeRefreshToken("invalid_token");
            Assert.Null(refreshToken);
        }

        [Fact]
        public void Should_Not_Take_Refresh_Token_That_Was_Already_Taken()
        {
            var accessToken = _tokenHandler.CreateAccessToken(_user);
            var refreshToken = _tokenHandler.TakeRefreshToken(accessToken.RefreshToken.Token);
            var refreshTokenSecondTime = _tokenHandler.TakeRefreshToken(accessToken.RefreshToken.Token);

            Assert.NotNull(refreshToken);
            Assert.Null(refreshTokenSecondTime);
        }

        [Fact]
        public void Should_Revoke_Refresh_Token()
        {
            var accessToken = _tokenHandler.CreateAccessToken(_user);
            _tokenHandler.RevokeRefreshToken(accessToken.RefreshToken.Token);
            var refreshToken = _tokenHandler.TakeRefreshToken(accessToken.RefreshToken.Token);

            Assert.Null(refreshToken);
        }
    }
}
