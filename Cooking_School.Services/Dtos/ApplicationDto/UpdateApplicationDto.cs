using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Services.Dtos.ApplicationDto
{
    public class UpdateApplicationDto 
    {
        public int? TraineeId { get; set; }
        public int? CookClassId { get; set; }
    }
}
