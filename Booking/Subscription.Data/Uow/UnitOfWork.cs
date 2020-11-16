using System;
using Subscription.Core.Uow;
using System.Threading.Tasks;
using Subscription.Core.Repositories;
using Subscription.Data.Repositories;

namespace Subscription.Data.Uow
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext _dbContext = null;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext as ApplicationDbContext;

        }


        #region Properties
        public IBookRepository Books
        {
            get
            {
                return new BookRepository(_dbContext);
            }
        }

        public IAuthorRepository Authors
        {
            get
            {
                return new AuthorRepository(_dbContext);
            }
        }

        public IBookAuthorRepository BookAuthors
        {
            get
            {
                return new BookAuthorRepository(_dbContext);
            }
        }

        public IPublisherRepository Publishers
        {
            get
            {
                return new PublisherRepository(_dbContext);
            }
        }

        public IShoppingCartRepository ShoppingCarts
        {
            get
            {
                return new ShoppingCartRepository(_dbContext);
            }
        }

        public ISubscriptionTypeRepository SubscriptionTypes
        {
            get
            {
                return new SubscriptionTypeRepository(_dbContext);
            }
        }

        public IUserSubscriptionTypeRepository UserSubscriptionTypes
        {
            get
            {
                return new UserSubscriptionTypeRepository(_dbContext);
            }
        }
        #endregion

        #region Method(s)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }
        }
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
