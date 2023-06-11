using Cooking_School.Core.Models;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.ApplicationService;
using Cooking_School.Services.AuthenticationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Cooking_School.Controllers
{
    [Route("api/admin/applications")]
    [ApiController]
    public class AdminControllers : ControllerBase
    {
        private readonly ILogger<AdminControllers> _logger;
        private readonly IApplicationSevice _applicationSevice;
        private readonly IAuthenticationServices _authenticationService;
        public AdminControllers(ILogger<AdminControllers> logger, IApplicationSevice applicationSevice, IAuthenticationServices authenticationService)
        {
            _logger = logger;
            _applicationSevice = applicationSevice;
            _authenticationService = authenticationService;
        }

        [HttpGet("")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllApplications()
        {

            _logger.LogInformation($"Attempt To GetAll {nameof(Application)}");
            var res = await _authenticationService.GetCurrentUser(HttpContext);
            if (res.Id < 0)
            {
                _logger.LogInformation($"Invalid chefId");
                return BadRequest();
            }
            var result = await _applicationSevice.GetAllApplications();
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ListDto);
        }
    }
}
