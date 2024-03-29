﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cooking_School.Core.ModelUsed
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
