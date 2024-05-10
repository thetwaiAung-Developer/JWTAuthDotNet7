using System.ComponentModel.DataAnnotations;

namespace JWTAuthDotNet7.Models.RequestModels
{
    public class RegisterRequestModel : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
    }
}
