using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking_School.Core.Models
{
    public class ProjectTrainee : Audit
    {
        [ForeignKey(nameof(Trainee))]
        public int TraineeId { get; set; }
        public virtual Trainee Trainee { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public decimal? Evalution { get; set; }
        public status_project status { get; set; } = status_project.notSubmited;
    }
}
