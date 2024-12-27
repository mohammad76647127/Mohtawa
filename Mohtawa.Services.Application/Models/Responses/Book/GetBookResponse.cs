using Mohtawa.Services.Application.Models.DTOs;

namespace Mohtawa.Services.Application.Models.Responses.Book
{
    public class GetBookResponse :BaseResponse
    {
        public BookDTO Book { get; set; }
    }
}
