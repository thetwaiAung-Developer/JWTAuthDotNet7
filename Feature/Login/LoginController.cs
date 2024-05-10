using JWTAuthDotNet7.Models.RequestModels;
using JWTAuthDotNet7.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthDotNet7.Feature.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel request)
        {
            LoginResponseModel responseModel = new();
            try
            {
                responseModel = await _loginService.Login(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok(responseModel);
        }
    }
}
