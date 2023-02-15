using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services;
using Cooking_School_ASP.NET_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Controllers.AdminControllers
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
        public async Task<IActionResult> GetAllTrainees([FromQuery] RequestParam requestParams)
        {
            var result = await _traineeService.GetAllUsers(requestParams);
            return Ok(result);
        }

        [HttpDelete("{traineeId}")]
        public async Task<IActionResult> DeleteTrainee(int traineeId)
        {
            _logger.LogInformation($"Attempt To Delete {nameof(ProjectFile)}");
            if (traineeId < 0)
            {
                _logger.LogInformation($"Invalid Attempt To Delete {nameof(ProjectFile)}");
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
