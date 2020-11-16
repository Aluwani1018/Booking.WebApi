using Subscription.Core.Domain;
using Subscription.Core.Repositories;

namespace Subscription.Data.Repositories
{
    public class BookAuthorRepository : Repository<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public ApplicationDbContext LibraryContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
