using Mohtawa.Services.Application.Models.DTOs;

namespace Mohtawa.Services.Application.Models.Responses.Book
{
    public class GetAllBooksResponse : BaseResponse
    {
        public List<BookDTO> Books { get; set; }
    }
}
