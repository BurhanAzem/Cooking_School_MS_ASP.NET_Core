using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Core.Models
{
    public class CookClass : Audit
    {
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [ForeignKey (nameof(Chef))]
        public int ChefId { get; set; } 
        public Chef Chef { get; set; }
        public TimeOnly StartingAt { get; set; }
        public TimeOnly EndingAt { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<ApplicationT> Applications { get; set; }
        public ICollection<ClassDays> ClassDays { get; set; }

    }
}
