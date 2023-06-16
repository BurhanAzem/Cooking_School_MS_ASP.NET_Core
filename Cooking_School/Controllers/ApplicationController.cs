using Cooking_School.Core.Models;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.ApplicationService;
using Cooking_School.Services.AuthenticationServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Cooking_School.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IApplicationSevice _applicationSevice;
        private readonly IAuthenticationServices _authenticationService;
        public ApplicationController(ILogger<ApplicationController> logger, IApplicationSevice applicationSevice, IAuthenticationServices authenticationService)
        {
            _logger = logger;
            _applicationSevice = applicationSevice;
            _authenticationService = authenticationService;
        }

        [HttpGet("~/api/chefs/{chefId}/applications")]
        [Authorize(Roles = "Administrator, Chef")]
        public async Task<IActionResult> GetAllApplicationsToChef(int chefId)
        {

            _logger.LogInformation($"Attempt To GetAll {nameof(Application)}");
            var res = await _authenticationService.GetCurrentUser(HttpContext);
            if (res.Id < 0)
            {
                _logger.LogInformation($"Invalid chefId");
                return BadRequest();
            }
            var result = await _applicationSevice.GetAllApplicationsToChef(res.Id);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ListDto);
        }


        [HttpPost("{applicationId}/accept")]
        [Authorize(Roles = "Administrator, Chef")]
        public async Task<IActionResult> AcceptApplication(int applicationId)
        {
            _logger.LogInformation($"Attempt To Accept Application {nameof(Application)}");
            var user = HttpContext.User;

            var result = await _applicationSevice.AcceptApplication(applicationId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }
        

        [HttpPost("{applicationId}/reject")]
        [Authorize(Roles = "Administrator, Chef")]
        public async Task<IActionResult> RejectApplication(int applicationId)
        {
            _logger.LogInformation($"Attempt To Reject Application {nameof(Application)}");
            var user = HttpContext.User;
            var result = await _applicationSevice.RejectApplication(applicationId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [HttpGet("~/api/cook-classes/{classId}/applications")]
        [Authorize(Roles = "Administrator, Chef")]
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
            return Ok(result.ListDto);
        }


        [HttpPost("~/api/cook-classes/{classId}/applications")]
        [Authorize(Roles = "Administrator, Trainee")]
        public async Task<IActionResult> ApplayTraineeToclass(int classId, [FromQuery] int traineeId)
        {
            var user = HttpContext.User;

            if (user.IsInRole("Trainee"))
            {
                var trainee = await _authenticationService.GetCurrentUser(HttpContext);
                traineeId = trainee.Id;
            }

            _logger.LogInformation($"Attempt Sinup for {nameof(Application)} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt for {nameof(Application)}");
                return BadRequest(ModelState);
            }
            var result = await _applicationSevice.CreateApplication(traineeId, classId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }
    }
}
