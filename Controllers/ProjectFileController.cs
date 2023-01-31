using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET_.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Controllers
{
    [Route("api/projects-files")]
    [ApiController]
    public class ProjectFileController : ControllerBase
    {
        private readonly ILogger<ProjectFileController> _logger;
        public ProjectFileController(ILogger<ProjectFileController> logger)
        {
            _logger= logger;
        }


        [HttpPost("~/api/chefs/{chefId}/cook-classes/{classsId}/projects/{projectId}/project-files/fileId/evaluate")]
        [Authorize]
        public async Task<IActionResult> EvaluateTraineeProject(int classId, int projectId, int fileId)
        {
            return Ok();
        }

        [HttpPut("~/api/trainees/{traineeId}/project-files")]
        public async Task<IActionResult> UploudProjectFile([FromBody] UpdateProjectFileDto projectFilesDto)
        {
            return Ok();
        }

        [HttpGet("~/api/trainees/{traineeId}/project-files")]
        public async Task<IActionResult> GetAllProjectFile()
        {
            return Ok();
        }

        [HttpDelete("~/api/trainees/{traineeId}/project-files/{projectFileId}")]
        public async Task<IActionResult> DeleteProjectFile(int projectFileId)
        {
            return Ok();
        }

    }
}
