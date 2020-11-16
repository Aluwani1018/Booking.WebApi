using Subscription.Core.Domain;
using Subscription.Core.Uow;
using Subscription.Infrastructure.Exceptions;
using Subscription.Infrastructure.Exceptions.Model.Enum;
using Subscription.Service.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.Service.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUserService userService, IUnitOfWork unitOfWork)
        {
            this._userService = userService;
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> GetAllBooks(string email)
        {
            User existingUser = await _userService.FindByEmailAsync(email);

            if (existingUser is null)
            {
                throw new ApiException((int)ErrorEnum.InvalidUser, nameof(ErrorEnum.InvalidUser));
            }

            if(existingUser.UserSubscriptions == null)
            {
                throw new ApiException((int)ErrorEnum.UserNotSubscribed, nameof(ErrorEnum.UserNotSubscribed));
            }    

            IEnumerable<Book> booksResponse = _unitOfWork.Books.GetAll();

            return booksResponse.ToList();
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            IEnumerable<Book> bookListReponse = _unitOfWork.Books.Find(x => x.ISBN == book.ISBN);


            if (bookListReponse.Any())
            {
                throw new ApiException((int)ErrorEnum.BookAlreadyExist, nameof(ErrorEnum.BookAlreadyExist));
            }

            _unitOfWork.Books.Add(book);

            await _unitOfWork.CommitAsync();

            return book;
        }
    }
}
