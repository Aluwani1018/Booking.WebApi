using Subscription.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Subscription.Service.Services.BookService
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks(string email);
        Task<Book> AddBookAsync(Book book);
    }
}
