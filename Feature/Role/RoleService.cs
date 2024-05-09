using JWTAuthDotNet7.Models.DataModels;
using JWTAuthDotNet7.Models.RequestModels;

namespace JWTAuthDotNet7.Feature.Role
{
    public class RoleService
    {
        private readonly AppDbContext _context;

        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Save(RoleRequestModel roleViewModel)
        {
            RoleModel role = new()
            {
                Name = roleViewModel.Name,
            };

            await _context.RoleModel.AddAsync(role);
            var result = _context.SaveChanges();
            var responseMessage = result > 0 ? "Role Create Success." : "Role Create Fail.";

            return responseMessage;
        }
    }
}
