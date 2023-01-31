using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Models
{
    public class Audit
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int Deleted { get; set; } = 0;    
    }
}
