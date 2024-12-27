namespace Mohtawa.Services.Domain.Models
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
