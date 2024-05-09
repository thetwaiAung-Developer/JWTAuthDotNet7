using JWTAuthDotNet7.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthDotNet7
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RegisterModel> RegisterModel { get; set; }
        public DbSet<RoleModel> RoleModel { get; set; }
    }
}
