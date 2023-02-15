using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Dtos.ClassDaysDto
{
    public class CreateClassDaysDto
    {
        [Required]
        public string Day { get; set; }
        public int CookClassId { get; set; }
    }
}
