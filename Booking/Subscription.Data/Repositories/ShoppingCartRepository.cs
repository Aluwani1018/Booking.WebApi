
using Subscription.Core.Domain;
using Subscription.Core.Repositories;

namespace Subscription.Data.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public ApplicationDbContext LibraryContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
