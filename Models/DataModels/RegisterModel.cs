using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuthDotNet7.Models.DataModels
{
    [Table("User")]
    public class RegisterModel
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }

        public int RoleId { get; set; }
    }
}
