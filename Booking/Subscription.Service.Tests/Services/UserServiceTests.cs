using Microsoft.AspNetCore.Identity;
using Moq;
using Subscription.Core.Domain;
using Subscription.Core.Domain.Enums;
using Subscription.Service.Services.SubscriptionTypeService;
using Subscription.Service.Services.UserService;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xunit;

namespace Subscription.Service.Tests.Services
{
    public class UserServiceTests
    {
        private Mock<UserManager<User>> _usermanager;
        private Mock<ISubscriptionTypeService> _subscriptionTypeService;
        private Mock<IUserStore<User>> _store;

        private IUserService _userService;

        public UserServiceTests()
        {
            SetupMocks();
            _userService = new UserService(_usermanager.Object, _subscriptionTypeService.Object);
        }

        private void SetupMocks()
        {
            _subscriptionTypeService = new Mock<ISubscriptionTypeService>();
            
            _store = new Mock<IUserStore<User>>();
            _usermanager = new Mock<UserManager<User>>(_store.Object, null, null, null, null, null, null, null, null);
            
            _usermanager.Setup(r => r.FindByEmailAsync("test@test.com")).ReturnsAsync(new User
            {
                Id = 1,
                Email = "test@test.com",
                Roles = new Collection<UserRole>()
            });

            _usermanager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Verifiable();
            _usermanager.Setup(r => r.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult<IdentityResult>(IdentityResult.Success)).Verifiable();

        }

        [Fact]
        public async Task Should_Create_Non_Existing_User()
        {
            var user = new User { Email = "mytestuser@mytestuser.com", PasswordHash = "123", Roles = new Collection<UserRole>() };

            var response = await _userService.CreateUserAsync(user, user.PasswordHash ,ApplicationRolesEnum.Common);

            Assert.NotNull(response);
            Assert.Equal(user.Email, response.Email);
        }

        [Fact]
        public async Task Should_Find_Existing_User_By_Email()
        {

            var user = await _userService.FindByEmailAsync("test@test.com");
            Assert.NotNull(user);
            Assert.Equal("test@test.com", user.Email);
        }

        [Fact]
        public async Task Should_Return_Null_When_Trying_To_Find_User_By_Invalid_Email()
        {
            var user = await _userService.FindByEmailAsync("secondtest@secondtest.com");
            Assert.Null(user);
        }
    }
}
