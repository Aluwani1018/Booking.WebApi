using Subscription.Core.Domain;
using Subscription.Core.Domain.Enums;
using System.Threading.Tasks;

namespace Subscription.Service.Services.UserService
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user, string password, ApplicationRolesEnum applicationRole = ApplicationRolesEnum.Common);
        Task<User> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<User> SubscribeUserAsync(string email, SubscriptionTypeEnum subscriptionType);
    }
}
