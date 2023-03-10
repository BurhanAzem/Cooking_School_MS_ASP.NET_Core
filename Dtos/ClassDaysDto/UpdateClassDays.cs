using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.ClassDaysDto
{
    public class UpdateClassDays : CreateClassDaysDto
    {
        public string? Day { get; set; }
        public int? CookClassId { get; set; }
    }
}
