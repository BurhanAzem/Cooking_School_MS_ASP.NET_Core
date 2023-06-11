using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Dtos.ApplicationDto
{
    public class UpdateApplicationDto 
    {
        public int? TraineeId { get; set; }
        public int? CookClassId { get; set; }
    }
}
