using JWTAuthDotNet7.Models.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthDotNet7.Feature.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(RoleRequestModel request)
        {
            var responseMessage = string.Empty;
            try
            {
                responseMessage = await _roleService.Save(request);
            }
            catch (Exception ex)
            {
                responseMessage = ex.Message;
            }
            return Ok(responseMessage);
        }
    }
}
