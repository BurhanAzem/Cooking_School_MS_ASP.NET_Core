using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services;
using Cooking_School_ASP.NET_.Controllers;
using Cooking_School_ASP.NET_.Models;
using CookingSchoolASP.NET.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Web.WebPages;

namespace Cooking_School_ASP.NET.Controllers
{
    [Route("api/projects-files")]
    [ApiController]
    public class ProjectFileController : ControllerBase
    {
        private readonly ILogger<ProjectFileController> _logger;
        private readonly IProjectFileService _projectFileService;
        public ProjectFileController(ILogger<ProjectFileController> logger, IProjectFileService projectFileService)
        {
            _logger = logger;
            _projectFileService = projectFileService;
        }


        [HttpPost("~/api/chefs/{chefId}/cook-classes/{classsId}/projects/{projectId}/project-files/fileId/evaluate")]
        public async Task<IActionResult> EvaluateTraineeProject([FromBody] decimal mark, int projectFileId)
        {
            _logger.LogInformation($"Attempt To Evaluate Id of {nameof(ProjectFile)}");
            if (mark < 0 && mark > 100)
            {
                _logger.LogInformation($"Mark Out of Range");
                return BadRequest("Mark Out of Range");
            }
            var result = await _projectFileService.EvaluateTraineeProject(mark, projectFileId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpPut("~/api/trainees/{traineeId}/project-files")]
        public async Task<IActionResult> UploudProjectFile([FromForm] CreateProjectFileDto projectFilesDto)
        {
            _logger.LogInformation($"Attempt To Uploud of {nameof(ProjectFile)}");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Attempt To Uploud of {nameof(ProjectFile)}");
                return BadRequest();
            }
            var result = await _projectFileService.UploadeProjectFile(projectFilesDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpGet("~/api/trainees/{traineeId}/project-files")]
        public async Task<IActionResult> GetAllProjectFile()
        {
            var result = await _projectFileService.GetAllProjectFile();
            return Ok(result);
        }

        [HttpDelete("~/api/trainees/{traineeId}/project-files/{projectFileId}")]
        public async Task<IActionResult> DeleteProjectFile(int projectFileId)
        {
            _logger.LogInformation($"Attempt To Delete {nameof(ProjectFile)}");
            if (projectFileId < 0)
            {
                _logger.LogInformation($"Invalid Attempt To Delete {nameof(ProjectFile)}");
                return BadRequest();
            }
            var result = await _projectFileService.DeleteProjectFile(projectFileId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

    }
}
