using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ApplicationDto;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services;
using Cooking_School_ASP.NET_.Controllers;
using Cooking_School_ASP.NET_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Cooking_School_ASP.NET.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IApplicationSevice _applicationSevice;
        public ApplicationController(ILogger<ApplicationController> logger, IApplicationSevice applicationSevice)
        {
            _logger = logger;
            _applicationSevice = applicationSevice;
        }

        [HttpGet("~/api/chefs/{cheefId}/applications")]
        public async Task<IActionResult> GetAllApplicationsToChef(int chefId)
        {
            _logger.LogInformation($"Attempt To GetAll {nameof(Application)}");
            if (chefId < 0)
            {
                _logger.LogInformation($"Invalid Attempt To Delete {nameof(Application)}");
                return BadRequest();
            }
            var result = await _applicationSevice.GetAllApplicationsToChef(chefId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.applicationDTOs);
        }


        [HttpPost("~/api/chefs/{cheefId}/cook-classes/{classsId}/applications/{applicationId}/accept")]
        [Authorize]
        public async Task<IActionResult> AcceptApplication(int applicationId,[FromForm] RequestParam requestParams = null)
        {
            _logger.LogInformation($"Attempt To Accept Application {nameof(Application)}");
            if (applicationId < 0)
            {
                _logger.LogInformation($"Invalid Attempt To Accept Application {nameof(Application)}");
                return BadRequest();
            }
            var result = await _applicationSevice.AcceptApplication(applicationId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }
        

        [HttpPost("~/api/chefs/{cheefId}/cook-classes/{classsId}/applications/{applicationId}/reject")]
        [Authorize]
        public async Task<IActionResult> RejectApplication(int applicationId)
        {
            _logger.LogInformation($"Attempt To Reject Application {nameof(Application)}");
            if (applicationId < 0)
            {
                _logger.LogInformation($"Invalid Reject To Accept Application {nameof(Application)}");
                return BadRequest();
            }
            var result = await _applicationSevice.RejectApplication(applicationId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpGet("~api/cook-classes/{classId}/applications")]
        public async Task<IActionResult> GetAllApplicationToClass(int classId)
        {
            _logger.LogInformation($"Attempt To Accept Application {nameof(Application)}");
            if (classId < 0)
            {
                _logger.LogInformation($"Invalid Attempt To Accept Application {nameof(Application)}");
                return BadRequest();
            }
            var result = await _applicationSevice.GetAllApplicationToClass(classId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.applicationDTOs);
        }


        [HttpPost("~api/cook-classes/{classId}/applications")]
        public async Task<IActionResult> ApplayTraineeToclass([FromBody] CreateApplicationDto applicationDto, int classId)
        {
            _logger.LogInformation($"Attempt Sinup for {nameof(Application)} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt for {nameof(Application)}");
                return BadRequest(ModelState);
            }
            var result = await _applicationSevice.CreateApplication(applicationDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }
    }
}
