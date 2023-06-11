using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Core.Models
{
    public class ProjectFile : Audit
    {
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string FilePath { get; set; }
    }
}
