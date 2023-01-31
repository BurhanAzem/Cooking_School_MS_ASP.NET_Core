using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET_.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly ILogger<TraineeController> _logger;
        public ProjectController(ILogger<TraineeController> logger)
        {
            _logger = logger;   
        }

        [HttpPost("~/api/chefs/{chefId}/cook-classes/{classsId}/projects")]
        [Authorize]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto projectDto, int classId)
        {
            return Ok();
        }


        [HttpPut("~/api/chefs/{chefId}/cook-classes/{classsId}/projects/{projectId}")]
        [Authorize]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDto projectDto, int classId, int projectId)
        {
            return Ok();
        }

        [HttpDelete("~/api/chefs/{chefId}/cook-classes/{classsId}/projects/{projectId}")]
        [Authorize]
        public async Task<IActionResult> DeleteProject(int classId, int projectId)
        {
            return Ok();
        }

        [HttpGet("~/api/trainees/{traineeId}/courses/{coursesId}/cook-classes/{cookclassId}/projects")]
        public async Task<IActionResult> GetAllProjectTrainee()
        {
            return Ok();
        }


        [HttpGet("~/api/trainees/{traineeId}/courses/{coursesId}/cook-classes/{cookclassId}/projects/{projectId}")]
        public async Task<IActionResult> GetAllProject()
        {
            return Ok();
        }
    }
}
