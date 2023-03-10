using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
