using Backend_Controller_Burhan.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Models
{
    public class ClassDays : Audit
    {
        public WeekDays Day { get; set; }
        [ForeignKey(nameof(CookClass))]
        public int CookClassId { get; set; }
        public CookClass CookClass { get; set; }    
    }
}
