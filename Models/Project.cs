using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET_.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking_School_ASP.NET.Models
{
    public class Project : Audit
    {

        [ForeignKey(nameof(CookClass))]
        public int CookClassId { get; set; }
        public CookClass CookClass { get; set; }
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public DateTime ExpirDate { get; set; }
        public virtual ICollection<ProjectFile> projectFiles { get; set; }  
    }
}
