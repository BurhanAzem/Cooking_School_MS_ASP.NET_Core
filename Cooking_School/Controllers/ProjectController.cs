using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages;
using Cooking_School.Services.AuthenticationServices;
using Cooking_School.Services.ProjectService;
using Cooking_School.Services.Dtos.CookClassDto;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Core.Models;
using Cooking_School.Services.SubmitedFileService;
using Cooking_School.Services.Dtos.ProjectDto;

namespace Cooking_School.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly ILogger<TraineeController> _logger;
        private readonly IProjectService _projectService;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IProjectFileService _submitedFileService;
        public ProjectController(ILogger<TraineeController> logger, IProjectService projectService, IAuthenticationServices authenticationServices, IProjectFileService submitedFileService)
        {
            _logger = logger;
            _projectService = projectService;
            _authenticationServices = authenticationServices;
            _submitedFileService = submitedFileService;
        }

        [HttpPost("~/api/cook-classes/{classId}/projects")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> CreateProject([FromForm] CreateProjectDto projectDto, int classId)
        {
            _logger.LogInformation($"Attempt Sinup for {nameof(projectDto)} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt for {nameof(projectDto)}");
                return BadRequest(ModelState);
            }
            var chef = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _projectService.CreateProject(projectDto, classId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }


        [HttpPut("{projectId}")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDto projectDto, int projectId)
        {
            _logger.LogInformation($"Attempt Sinup for {nameof(projectDto)} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Put attempt for {nameof(projectDto)}");
                return BadRequest(ModelState);
            }
            var result = await _projectService.UpdateProject(projectId, projectDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }

        [HttpPost("~/api/trainees/{traineeId}/projects/{projectId}/evaluate")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> EvaluateTraineeProject(int projectId, int traineeId, [FromHeader] decimal mark)
        {
            _logger.LogInformation($"Attempt To Evaluate Id of {nameof(ProjectTraineeFile)}");
            if (mark < 0 && mark > 100)
            {
                _logger.LogInformation($"Mark Out of Range");
                return BadRequest("Mark Out of Range");
            }
            var result = await _projectService.EvaluateTraineeProject(mark, projectId, traineeId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }


        [HttpDelete("{projectId}")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            _logger.LogInformation($"Attempt Delete for {nameof(Project)} ");
            var result = await _projectService.DeleteProject(projectId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }

        [HttpGet("~/api/cook-classes/{cookClassId}/projects")]
        [Authorize(Roles = "Chef, Trainee")]
        public async Task<IActionResult> GetAllProjectOfCookClass(int cookClassId, [FromQuery] RequestParam requestParams)
        {
            _logger.LogInformation($"Attempt GetAll of {nameof(Project)} ");
            var result = await _projectService.GetAllProjectOfCookClass(requestParams, cookClassId);
            return Ok(result.ListDto);
        }


        [HttpGet("{projectId}")]
        [Authorize(Roles = "Chef, Trainee")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            _logger.LogInformation($"Attempt GetBy Id of {nameof(Project)}");
            var result = await _projectService.GetProjectById(projectId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }
    }
}
