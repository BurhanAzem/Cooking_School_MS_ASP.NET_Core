
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

        public AdminCourseController(ILogger<AdminCourseController> logger)
        {
            _logger = logger;
        }
        // POST /api/users/login

    }
}
