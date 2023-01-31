using Backend_Controller_Burhan.Models;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class ProjectFileDto
    {
        public int Id { get; set; }
        public Trainee Trainee { get; set; }
        public DateTime SubmitedDate { get; set; }


    }
}
