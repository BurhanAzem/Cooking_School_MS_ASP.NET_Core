using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET_.Models
{
    public class ProjectFile : Audit
    {
        public DateTime? SubmitedDate { get; set; }
        [ForeignKey(nameof(Trainee))]
        public int TraineeId { get; set; }
        public virtual Trainee Trainee { get; set; }
        public byte[] content { get; set; }
        public decimal? Evalution { get; set; }
        public status_project status { get; set; } = status_project.notSubmited;
    }
}
