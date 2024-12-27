using System.ComponentModel.DataAnnotations;

namespace Mohtawa.Services.Application.Models.Requests.Book
{
    public class UpdateBookRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
