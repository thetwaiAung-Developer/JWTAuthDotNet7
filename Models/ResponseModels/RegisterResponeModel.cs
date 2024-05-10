using System.ComponentModel.DataAnnotations;

namespace JWTAuthDotNet7.Models.ResponseModels
{
    public class RegisterResponeModel : BaseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
    }
}
