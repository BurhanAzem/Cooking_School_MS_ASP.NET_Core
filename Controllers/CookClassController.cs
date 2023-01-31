using Cooking_School_ASP.NET.Controllers;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET_.Controllers
{
    [Route("api/cook-classes")]
    [ApiController]
    public class CookClassController : ControllerBase
    {
        private readonly ILogger<CookClassController> _logger;
        public CookClassController(ILogger<CookClassController> logger)
        {
            _logger = logger;
        }
    ///pi/chefs/{chefId}/cook-classes
    [HttpPost("~/api/chefs/{chefId}/cook-classes")]
    [Authorize]
    public async Task<IActionResult> CreateClass([FromBody] CreateCookClassDto classDto)
        {

            return Ok();
        }

        [HttpPut("~/api/chefs/{chefId}/cook-classes/{classId}")]
        [Authorize]
        public async Task<IActionResult> UpdateClass([FromBody] UpdateCookClassDto classDto)
        {           
            return Ok();
        }

        [HttpDelete("~/api/chefs/{chefId}/cook-classes/{classId}")]
        [Authorize]
        public async Task<IActionResult> DleteClass()
        {
            return Ok();
        }
    }





}
