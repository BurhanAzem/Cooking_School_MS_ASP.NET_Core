using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace Cooking_School_ASP.NET.Models
{
    public class ApplicationT : Audit
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Trainee))]
        public int TraineeId { get; set; }
        public virtual Trainee Trainee { get; set; }

        [ForeignKey(nameof(CookClass))]
        public int CookClassId { get; set; }    
        public CookClass CookClass { get; set; }
        public DateTime DateOfApplay { get; set; }
        public status_apply status { get; set; } = status_apply.inprocess;
    }
}
