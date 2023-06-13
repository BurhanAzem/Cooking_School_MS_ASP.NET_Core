
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Web.WebPages;
using Cooking_School.Services.SubmitedFileService;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.Dtos.CookClassDto;
using Cooking_School.Core.Models;
using Cooking_School.Services.Dtos.SubmitedFileDto;
using Cooking_School.Services.ProjectTraineeFileService;

namespace Cooking_School.Controllers
{
    [Route("api/project-trainee-files")]
    [ApiController]
    public class ProjectTraineeFileController : ControllerBase
    {
        private readonly ILogger<ProjectTraineeFileController> _logger;
        private readonly IProjectTraineeFileService _ProjectTraineeFileService;
        public ProjectTraineeFileController(ILogger<ProjectTraineeFileController> logger, IProjectTraineeFileService ProjectTraineeFileService)
        {
            _logger = logger;
            _ProjectTraineeFileService = ProjectTraineeFileService;
        }



        [HttpPost("download/{fileName}")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> DownloadProjectTraineeFile(string fileName)
        {
            _logger.LogInformation($"Attempt To Download  {nameof(ProjectTraineeFile)}");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Attempt To Download {nameof(ProjectTraineeFile)}");
                return BadRequest();
            }
            var result = await _ProjectTraineeFileService.DownloadProjectTraineeFile(fileName);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return File(result.Dto.Contant, result.Dto.ContantType, result.Dto.Name);
        }


        [HttpPost("")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> UploudProjectTraineeFile([FromForm] CreateSubmitedFileDto submitedFilesDto)
        {
            _logger.LogInformation($"Attempt To Uploud of {nameof(ProjectTraineeFile)}");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Attempt To Uploud of {nameof(ProjectTraineeFile)}");
                return BadRequest();
            }
            var result = await _ProjectTraineeFileService.UploadProjectTraineeFile(submitedFilesDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpPut("")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> UpdateProjectTraineeFile([FromForm] UpdateSubmitedFileDto submitedFilesDto)
        {
            _logger.LogInformation($"Attempt To Update of {nameof(ProjectTraineeFile)}");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Attempt To Update of {nameof(ProjectTraineeFile)}");
                return BadRequest();
            }
            var result = await _ProjectTraineeFileService.UpdateProjectTraineeFile(submitedFilesDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpGet("{projectId}")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> GetAllProjectTraineeFile(int projectId)
        {
            var result = await _ProjectTraineeFileService.GetAllProjectTraineeFile(projectId);
            return Ok(result);
        }

        [HttpDelete("{submitedFileId}")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> DeleteProjectTraineeFile(int submitedFileId)
        {
            _logger.LogInformation($"Attempt To Delete {nameof(ProjectTraineeFile)}");
            if (submitedFileId < 0)
            {
                _logger.LogInformation($"Invalid Attempt To Delete {nameof(ProjectTraineeFile)}");
                return BadRequest();
            }
            var result = await _ProjectTraineeFileService.DeleteProjectTraineeFile(submitedFileId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

    }
}
