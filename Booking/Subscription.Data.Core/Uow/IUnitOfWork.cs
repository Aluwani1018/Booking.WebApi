using System;
using System.Threading.Tasks;
using Subscription.Core.Repositories;

namespace Subscription.Core.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        IBookAuthorRepository BookAuthors { get; }
        IPublisherRepository Publishers { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        ISubscriptionTypeRepository SubscriptionTypes { get; }
        IUserSubscriptionTypeRepository UserSubscriptionTypes { get; }

        Task CommitAsync();
    }
}
