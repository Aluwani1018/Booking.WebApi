using Subscription.Core.Domain;
using Subscription.Core.Repositories;

namespace Subscription.Data.Repositories
{
    public class UserSubscriptionTypeRepository : Repository<UserSubscriptionType>, IUserSubscriptionTypeRepository
    {
        public UserSubscriptionTypeRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public ApplicationDbContext LibraryContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
