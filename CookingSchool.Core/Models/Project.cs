using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Core.Models
{
    public class Project : Audit
    {

        [ForeignKey(nameof(CookClass))]
        public int CookClassId { get; set; }
        public CookClass CookClass { get; set; }
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public DateTime ExpirDate { get; set; }
        public ICollection<ProjectTrainee>? ProjectTrainees { get; set; }
        public ICollection<ProjectFile>? ProjectFiles { get; set; }
    }
}
