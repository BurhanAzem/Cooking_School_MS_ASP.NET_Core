using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School.Core.Models
{
    public class ProjectTraineeFile : Audit
    {
        [ForeignKey(nameof(ProjectTrainee))]
        public int ProjectTraineeId { get; set; }
        public virtual ProjectTrainee ProjectTrainee { get; set; }
        public string FilePath { get; set; }

    }
}
