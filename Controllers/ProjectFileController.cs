
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Web.WebPages;
using Cooking_School.Services.SubmitedFileService;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Dtos.CookClassDto;
using Cooking_School.Core.Models;
using Cooking_School.Dtos.SubmitedFileDto;

namespace Cooking_School.Controllers
{
    [Route("api/project-files")]
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



        [HttpPost("download/{fileName}")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> DownloadProjectFile(string fileName)
        {
            _logger.LogInformation($"Attempt To Download  {nameof(ProjectTraineeFile)}");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Attempt To Download {nameof(ProjectTraineeFile)}");
                return BadRequest();
            }
            var result = await _projectFileService.DownloadProjectFile(fileName);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return File(result.Dto.Contant, result.Dto.ContantType, result.Dto.Name);
        }


        [HttpPost("")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> UploudProjectFile([FromForm] CreateSubmitedFileDto submitedFilesDto)
        {
            _logger.LogInformation($"Attempt To Uploud of {nameof(ProjectTraineeFile)}");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Attempt To Uploud of {nameof(ProjectTraineeFile)}");
                return BadRequest();
            }
            var result = await _projectFileService.UploadProjectFile(submitedFilesDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpPut("")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> UpdateProjectFile([FromForm] UpdateSubmitedFileDto submitedFilesDto)
        {
            _logger.LogInformation($"Attempt To Update of {nameof(ProjectTraineeFile)}");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Attempt To Update of {nameof(ProjectTraineeFile)}");
                return BadRequest();
            }
            var result = await _projectFileService.UpdateProjectFile(submitedFilesDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpGet("{projectId}")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> GetAllProjectFile(int projectId)
        {
            var result = await _projectFileService.GetAllProjectFile(projectId);
            return Ok(result);
        }

        [HttpDelete("{submitedFileId}")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> DeleteProjectFile(int submitedFileId)
        {
            _logger.LogInformation($"Attempt To Delete {nameof(ProjectTraineeFile)}");
            if (submitedFileId < 0)
            {
                _logger.LogInformation($"Invalid Attempt To Delete {nameof(ProjectTraineeFile)}");
                return BadRequest();
            }
            var result = await _projectFileService.DeleteProjectFile(submitedFileId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

    }
}
