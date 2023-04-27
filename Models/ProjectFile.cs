using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Models
{
    public class ProjectFile : Audit
    {
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string ContentPath { get; set; }
    }
}
