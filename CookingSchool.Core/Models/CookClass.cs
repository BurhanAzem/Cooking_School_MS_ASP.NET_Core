using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Core.Models
{
    public class CookClass : Audit
    {
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [ForeignKey (nameof(Chef))]
        public int ChefId { get; set; } 
        public virtual Chef Chef { get; set; }
        public TimeOnly StartingAt { get; set; }
        public TimeOnly EndingAt { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<ApplicationT> Applications { get; set; }
        public virtual ICollection<ClassDays> ClassDays { get; set; }

    }
}
