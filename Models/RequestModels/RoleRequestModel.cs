using System.ComponentModel.DataAnnotations;

namespace JWTAuthDotNet7.Models.RequestModels
{
    public class RoleRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Role Name is required.")]
        public string Name { get; set; }
    }
}
