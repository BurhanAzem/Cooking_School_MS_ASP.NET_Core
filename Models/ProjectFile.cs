using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Models
{
    public class ProjectFile : Audit
    {
        public Project Project { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public string ContentPath { get; set; }
    }
}
