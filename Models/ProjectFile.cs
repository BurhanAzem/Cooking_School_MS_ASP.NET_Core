using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET_.Models
{
    public class ProjectFile : Audit
    {
        [ForeignKey(nameof(Trainee))]
        public int TraineeId { get; set; }
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public virtual Trainee Trainee { get; set; }
        public string ContentPath { get; set; }
        public decimal? Evalution { get; set; }
        public status_project status { get; set; } = status_project.notSubmited;
    }
}
