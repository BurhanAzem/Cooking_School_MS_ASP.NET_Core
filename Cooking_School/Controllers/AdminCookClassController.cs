using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.CookClassService;
using Cooking_School.Services.CookClassService;
using Cooking_School_ASP.NET.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School.Controllers
{

    [Route("api/admin/cook-classes")]
    [ApiController]
    public class AdminCookClassController : ControllerBase
    {
        private readonly ILogger<AdminCookClassController> _logger;
        private readonly ICookClassService _cookClassService;
        public AdminCookClassController(ILogger<AdminCookClassController> logger, ICookClassService cookClassService)
        {
            _logger = logger;
            _cookClassService = cookClassService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllCookClasses([FromQuery] RequestParam requestParam)
        {
            var result = await _cookClassService.GetAllCookClasses(requestParam);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ListDto);
        }
    }
}
