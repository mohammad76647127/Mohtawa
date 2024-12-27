using System.ComponentModel.DataAnnotations;

namespace Mohtawa.Services.Application.Models.Requests.Book
{
    public class AddBookRequest
    {
        /// <summary>
        /// Computer
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
