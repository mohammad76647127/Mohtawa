using Mohtawa.Services.Application.Models.Requests.Book;
using Mohtawa.Services.Application.Models.Responses.Book;

namespace Mohtawa.Services.Application.Interfaces
{
    public interface IBookService
    {
        Task<AddBookResponse> AddBook(AddBookRequest book);
        Task<DeleteBookResponse> DeleteBook(int bookId);
        Task<GetAllBooksResponse> GetAllBooks();
        Task<GetBookResponse> GetBookById(int id);
        Task<UpdateBookResponse> UpdateBook(int id, UpdateBookRequest book);
    }
}
