using Cooking_School.Core.Models;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.AdminService;
using Cooking_School.Services.TraineeService;
using Cooking_School_ASP.NET.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School.Controllers
{
    [Route("api/admin/trainees")]
    [ApiController]
    public class AdminTraineeController : ControllerBase
    {
        private readonly ILogger<AdminCookClassController> _logger;
        private readonly IAdminService _adminService;
        private readonly ITraineeService _traineeService;
        public AdminTraineeController(IAdminService adminService, ITraineeService traineeService, ILogger<AdminCookClassController> logger)
        {
            _adminService = adminService;
            _traineeService = traineeService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllTrainees([FromQuery] RequestParam requestParams)
        {
            var result = await _traineeService.GetAllUsers(requestParams);
            return Ok(result.ListDto);
        }

        [HttpDelete("{traineeId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTrainee(int traineeId)
        {
            _logger.LogInformation($"Attempt To Delete {nameof(Trainee)}");
            if (traineeId < 0)
            {
                _logger.LogInformation($"Invalid Attempt To Delete {nameof(Trainee)}");
                return BadRequest();
            }
            var result = await _traineeService.DeleteUser(traineeId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }
    }
}
