using Subscription.Core.Domain;
using Subscription.Core.Repositories;

namespace Subscription.Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public ApplicationDbContext LibraryContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
