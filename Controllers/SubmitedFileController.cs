using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET_.Controllers;
using Cooking_School_ASP.NET_.Models;
using CookingSchoolASP.NET.Migrations;
using Microsoft.AspNetCore.Authorization;
using Cooking_School_ASP.NET.Services.ProjectFileService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Web.WebPages;

namespace Cooking_School_ASP.NET.Controllers
{
    [Route("api/projects-files")]
    [ApiController]
    public class SubmitedFileController : ControllerBase
    {
        private readonly ILogger<SubmitedFileController> _logger;
        private readonly ISubmitedFileService _submitedFileService;
        public SubmitedFileController(ILogger<SubmitedFileController> logger, ISubmitedFileService projectFileService)
        {
            _logger = logger;
            _submitedFileService = projectFileService;
        }



        [HttpPost("~/api/submited-files/download")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> UploudSubmitedFile([FromBody] string fileName)
        {
            _logger.LogInformation($"Attempt To Download  {nameof(SubmitedFile)}");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Attempt To Download {nameof(SubmitedFile)}");
                return BadRequest();
            }
            var result = await _submitedFileService.DownloadSubmitedFile(fileName);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return File(result.Dto.Contant, result.Dto.ContantType, result.Dto.Name);
        }



        [HttpPost("~/api/cook-classes/{classsId}/projects/{projectId}/submited-files/fileId/evaluate")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> EvaluateTraineeProject([FromBody] decimal mark, int projectFileId)
        {
            _logger.LogInformation($"Attempt To Evaluate Id of {nameof(SubmitedFile)}");
            if (mark < 0 && mark > 100)
            {
                _logger.LogInformation($"Mark Out of Range");
                return BadRequest("Mark Out of Range");
            }
            var result = await _submitedFileService.EvaluateTraineeProject(mark, projectFileId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpPost("~/api/submited-files")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> UploudSubmitedFile([FromForm] CreateSubmitedFileDto submitedFilesDto)
        {
            _logger.LogInformation($"Attempt To Uploud of {nameof(SubmitedFile)}");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Attempt To Uploud of {nameof(SubmitedFile)}");
                return BadRequest();
            }
            var result = await _submitedFileService.UploadSubmitedFile(submitedFilesDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpPut("~/api/submited-files")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> UpdateSubmitedFile([FromForm] UpdateSubmitedFileDto submitedFilesDto)
        {
            _logger.LogInformation($"Attempt To Update of {nameof(SubmitedFile)}");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Attempt To Update of {nameof(SubmitedFile)}");
                return BadRequest();
            }
            var result = await _submitedFileService.UpdateSubmitedFile(submitedFilesDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpGet("~/api/submited-files")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> GetAllSubmitedFile()
        {
            var result = await _submitedFileService.GetAllSubmitedFile();
            return Ok(result);
        }

        [HttpDelete("~/api/submited-files/{projectFileId}")]
        [Authorize(Roles = "Trainee, Chef")]
        public async Task<IActionResult> DeleteSubmitedFile(int submitedFileId)
        {
            _logger.LogInformation($"Attempt To Delete {nameof(SubmitedFile)}");
            if (submitedFileId < 0)
            {
                _logger.LogInformation($"Invalid Attempt To Delete {nameof(SubmitedFile)}");
                return BadRequest();
            }
            var result = await _submitedFileService.DeleteSubmitedFile(submitedFileId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

    }
}
