using JWTAuthDotNet7.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthDotNet7.Helper
{
    public class AuthorizationHelper
    {
        private readonly IConfiguration _configuration;

        public AuthorizationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public BaseModel CheckUserAuthentication(string token)
        {
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
            {
                return new BaseModel { RespCode = "E001" };
            }

            return new BaseModel { RespCode = "I001" };
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;

                var expirationTime = jwtToken.ValidTo;
                //if (expirationTime < DateTimeHelper.ToMyanmarDateTime())
                //{
                //    return null;
                //}

                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = authKey
                };

                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                                                                       validationParameters,
                                                                       out securityToken);

                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
