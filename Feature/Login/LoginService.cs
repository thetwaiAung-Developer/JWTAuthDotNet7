using JWTAuthDotNet7.Helper;
using JWTAuthDotNet7.Models.RequestModels;
using JWTAuthDotNet7.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimeZoneConverter;

namespace JWTAuthDotNet7.Feature.Login
{
    public class LoginService
    {
        private readonly AppDbContext _context;
        private readonly EncryptionService _encryptionService;
        private readonly IConfiguration _configuration;

        public LoginService(AppDbContext context, EncryptionService encryptionService, IConfiguration configuration)
        {
            _context = context;
            _encryptionService = encryptionService;
            _configuration = configuration;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel loginViewModel)
        {
            LoginResponseModel loginResponseModel = new();
            var responseMessage = string.Empty;

            var userModel = await _context.RegisterModel
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.UserName == loginViewModel.UserName);

            if (userModel == null)
            {
                return new LoginResponseModel { ResponseMessage = "User does not exist" };
            }

            var IsCorrectPassword = loginViewModel.Password ==
                                    await _encryptionService.DecryptAsync(userModel.Password);

            if (!IsCorrectPassword)
            {
                return new LoginResponseModel { ResponseMessage = "Password is incorrect." };
            }

            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Convert.ToString(userModel.Id)),
                new Claim(ClaimTypes.Name , userModel.Name),
                new Claim("JWTID",Guid.NewGuid().ToString())
            };

            var token = GenerateNewJWTToken(authClaim);
            loginResponseModel.Token = token;
            return loginResponseModel;
        }

        private string GenerateNewJWTToken(List<Claim> claims)
        {
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var tokenObject = new JwtSecurityToken(
                                        issuer: _configuration["JWT:ValidateIssuer"],
                                        audience: _configuration["JWT:ValidateAudience"],
                                        //expires: DateTimeHelper.ToMyanmarDateTime().AddSeconds(100),
                                        expires: DateTime.Now.AddSeconds(20),
                                        claims: claims,
                                        signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }
    }
}
