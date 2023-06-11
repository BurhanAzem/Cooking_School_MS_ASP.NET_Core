using Backend_Controller_Burhan.Models;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.AuthenticationServices;
using Cooking_School.Services.CourseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages;

namespace Cooking_School.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly ICourseService _courseServer;
        private readonly IAuthenticationServices _authenticationServices;
        public CourseController(ILogger<CourseController> logger, ICourseService courseServer, IAuthenticationServices authenticationServices)
        {
            _logger = logger;
            _courseServer = courseServer;
            _authenticationServices = authenticationServices;
        }



        [HttpPost("{courseId}/favorite")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> FavoriteCourse(int courseId)
        {
            var trainee = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _courseServer.FavoriteCourse(courseId, trainee.Id);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }


        [HttpDelete("{courseId}/favorite")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> UnFavoriteCourse(int courseId)
        {
            var trainee = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _courseServer.UnFavoriteCourse(courseId, trainee.Id);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }
    }
}
