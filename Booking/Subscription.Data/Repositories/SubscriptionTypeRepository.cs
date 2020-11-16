using Subscription.Core.Domain;
using Subscription.Core.Repositories;

namespace Subscription.Data.Repositories
{
    public class SubscriptionTypeRepository : Repository<SubscriptionType>, ISubscriptionTypeRepository
    {
        public SubscriptionTypeRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public ApplicationDbContext LibraryContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
