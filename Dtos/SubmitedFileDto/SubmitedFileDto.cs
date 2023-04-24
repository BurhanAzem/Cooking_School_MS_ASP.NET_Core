using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using System.Reflection.Metadata;

namespace Cooking_School_ASP.NET.Dtos.CookClassDto
{
    public class SubmitedFileDto : CreateSubmitedFileDto
    {
        public int Id { get; set; }
        public Trainee Trainee { get; set; }
        public DateTime SubmitedDate { get; set; }

    }
}
