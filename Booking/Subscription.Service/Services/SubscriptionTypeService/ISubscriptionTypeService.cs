using Subscription.Core.Domain;
using System.Threading.Tasks;

namespace Subscription.Service.Services.SubscriptionTypeService
{
    public interface ISubscriptionTypeService
    {
        Task<SubscriptionType> GetSubscriptionTypeById(int id);

        Task<UserSubscriptionType> AddUserSubscriptionType(UserSubscriptionType userSubscriptionType);
    }
}
