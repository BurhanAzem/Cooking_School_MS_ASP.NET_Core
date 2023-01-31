using Cooking_School_ASP.NET.Dtos.ApplicationDto;
using Cooking_School_ASP.NET_.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        public ApplicationController(ILogger<ApplicationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("~/api/chefs/{cheefId}/applications")]
        public async Task<IActionResult> GetAllApplicationsToChef()
        {
            return Ok();
        }


        [HttpPost("~/api/chefs/{cheefId}/cook-classes/{classsId}/applications/{applicationId}/accept")]
        [Authorize]
        public async Task<IActionResult> AcceptApplication(int classId, int applicationId)
        {
        return Ok();
        }
        

        [HttpPost("~/api/chefs/{cheefId}/cook-classes/{classsId}/applications/{applicationId}/reject")]
        [Authorize]
        public async Task<IActionResult> RejectApplication(int classId, int applicationId)
        {
            return Ok();
        }

        [HttpGet("~api/cook-classes/{classId}/applications")]
        public async Task<IActionResult> GetAllApplicationToClass()
        {
            return Ok();
        }


        [HttpPost("~api/cook-classes/{classId}/applications")]
        public async Task<IActionResult> ApplayTraineeToclass([FromBody] CreateApplicationDto applicationDto)
        {
            return Ok();
        }
    }
}
