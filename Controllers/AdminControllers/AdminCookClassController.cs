using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Controllers.AdminControllers
{
    [Route("api/admin/cook-classes")]
    [ApiController]
    public class AdminCookClassController : ControllerBase
    {
        private readonly ILogger<AdminCookClassController> _logger;

        public AdminCookClassController(ILogger<AdminCookClassController> logger)
        {
            _logger = logger;
        }
    }
}
