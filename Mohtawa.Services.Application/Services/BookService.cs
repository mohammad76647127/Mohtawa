using AutoMapper;
using Mohtawa.Services.Application.Interfaces;
using Mohtawa.Services.Application.Models.DTOs;
using Mohtawa.Services.Application.Models.Requests.Book;
using Mohtawa.Services.Application.Models.Responses.Book;
using Mohtawa.Services.Domain.Contracts;
using Mohtawa.Services.Domain.Models.Entities;

namespace Mohtawa.Services.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IUnitOfWork unitOfWork, IBookRepository bookRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<GetAllBooksResponse> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return new()
            {
                Books = _mapper.Map<List<BookDTO>>(books)
            };
        }
        public async Task<GetBookResponse> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return new()
                {
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                    ErrorMessage = "Invalid Book ID"
                };
            }

            return new()
            {
                Book = _mapper.Map<BookDTO>(book)
            };

        }
        public async Task<AddBookResponse> AddBook(AddBookRequest request)
        {
            var bookRepository = _unitOfWork.Repository<Book>();
            //check if isbn already exists
            var isExists = await bookRepository.IsExists(x => x.ISBN == request.ISBN);
            if (isExists)
            {
                return new()
                {
                    Result = false,
                    ErrorMessage = "ISBN Already exists",
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                };
            }
            var book = _mapper.Map<Book>(request);
            book.CreatedDate = DateTime.Now;
            book.CreatedBy = Guid.NewGuid();
            await bookRepository.AddAsync(book);
            var result = await _unitOfWork.Save();
            if (result <= 0)
            {
                return new()
                {
                    Result = false,
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = "Technicall Error."
                };
            }
            return new()
            {
                Result = true
            };
        }
        public async Task<UpdateBookResponse> UpdateBook(int id, UpdateBookRequest book)
        {
            var bookRepository = _unitOfWork.Repository<Book>();
            var dbBook = await bookRepository.GetByIdAsync(id);
            if (dbBook == null)
            {
                return new()
                {
                    Result = false,
                    ErrorMessage = "Invalid Book ID",
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                };
            }
            dbBook.Title = book.Title;
            dbBook.Author = book.Author;
            dbBook.ISBN = book.ISBN;
            dbBook.PublishedDate = book.PublishedDate;

            bookRepository.Update(dbBook);
            var result = await _unitOfWork.Save();
            if (result <= 0)
            {
                return new()
                {
                    Result = false,
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = "Technicall Error."
                };
            }
            return new()
            {
                Result = true
            };
        }
        public async Task<DeleteBookResponse> DeleteBook(int bookId)
        {
            var bookRepository = _unitOfWork.Repository<Book>();
            var book = await bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                return new()
                {
                    Result = false,
                    ErrorMessage = "Invalid Book ID",
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                };
            }
            bookRepository.Delete(book);
            var result = await _unitOfWork.Save();
            if (result <= 0)
            {
                return new()
                {
                    Result = false,
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = "Technicall Error."
                };
            }
            return new()
            {
                Result = true
            };
        }
    }
}
