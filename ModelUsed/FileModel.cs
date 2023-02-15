using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cooking_School_ASP.NET.ModelUsed
{
    public class BufferedSingleFileUploadDbModel : PageModel
    {

        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }

    }

    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
