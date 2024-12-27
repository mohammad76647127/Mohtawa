namespace Mohtawa.Services.Application.Models.DTOs
{
    public class BookDTO
    {
        /// <example>
        /// 1
        /// </example>
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
