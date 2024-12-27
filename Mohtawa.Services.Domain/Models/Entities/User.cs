namespace Mohtawa.Services.Domain.Models.Entities
{
    public class User :BaseEntity
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
    }
}
