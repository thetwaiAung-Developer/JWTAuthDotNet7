using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuthDotNet7.Models.DataModels
{
    [Table("Role")]
    public class RoleModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
