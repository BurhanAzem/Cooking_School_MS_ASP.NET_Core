
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Cooking_School_ASP.NET.Controllers.AdminControllers
{
    [Route("api/admin/courses")]
    [ApiController]
    public class AdminCourseController : ControllerBase
    {
        private readonly ILogger<AdminCourseController> _logger;
        private readonly ICourseService _courseService;
        private readonly IAuthenticationServices _authentication;
        public AdminCourseController(ILogger<AdminCourseController> logger, ICourseService courseService, IAuthenticationServices authentication)
        {
            _logger = logger;
            _courseService = courseService;
            _authentication = authentication;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateProject([FromBody] CreateCourseDto courseDto)
        {
            _logger.LogInformation($"Attempt Sinup for {nameof(courseDto)} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt for {nameof(courseDto)}");
                return BadRequest(ModelState);
            }
            //var adminId = await _authentication.GetCurrentUser(HttpContext).Id;
            var result = await _courseService.CreateCourse(courseDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }


        [HttpPut("{CourseId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateCourseDto courseDto, int CourseId)
        {
            _logger.LogInformation($"Attempt Sinup for {nameof(courseDto)} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Put attempt for {nameof(courseDto)}");
                return BadRequest(ModelState);
            }
            var result = await _courseService.UpdateCourse(courseDto, CourseId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }


        [HttpDelete("{courseId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteProject(int courseId)
        {
            _logger.LogInformation($"Attempt Delete for {nameof(Project)} ");
            var result = await _courseService.DeleteCourse(courseId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }


        [HttpGet()]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllCourses([FromQuery] RequestParam requestParams)
        {
            _logger.LogInformation($"Attempt GetAll of {nameof(Project)} ");
            var result = await _courseService.GetAllCourses(requestParams);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ListDto);
        }


        [HttpGet("{courseId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            _logger.LogInformation($"Attempt GetBy Id of {nameof(Course)}");
            var result = await _courseService.GetCourseById(courseId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }


        [HttpGet("favorite")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllFavoriteCourses()
        {
            _logger.LogInformation($"Attempt GetBy Id of {nameof(Course)}");
            var result = await _courseService.GetAllFavoriteCourses();
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ListDto);
        }

    }

}

