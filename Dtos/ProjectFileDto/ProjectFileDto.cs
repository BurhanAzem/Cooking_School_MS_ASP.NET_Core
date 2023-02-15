using Cooking_School_ASP.NET.Models;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class ProjectFileDTO
    {
        public int Id { get; set; }
        public Trainee Trainee { get; set; }
        public DateTime SubmitedDate { get; set; }


    }
}
