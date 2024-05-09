using JWTAuthDotNet7.Helper;
using JWTAuthDotNet7.Models.DataModels;
using JWTAuthDotNet7.Models.RequestModels;
using JWTAuthDotNet7.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthDotNet7.Feature.User
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly EncryptionService _encryptionService;

        public UserService(AppDbContext context, EncryptionService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
        }

        public async Task<string> Save(RegisterRequestModel requestModel)
        {
            var IsExistUser = _context.RegisterModel.Any(x => x.UserName == requestModel.UserName);

            if (IsExistUser)
            {
                return "User Name Already exist.";
            }

            RegisterModel register = new()
            {
                Name = requestModel.Name,
                UserName = requestModel.UserName,
                Password = await _encryptionService.EncryptAsync(requestModel.Password),
                PhoneNumber = requestModel.PhoneNumber,
            };

            await _context.RegisterModel.AddAsync(register);

            var result = _context.SaveChanges();
            var responseMessage = result > 0 ? "Register Success." : "Register Fail.";
            return responseMessage;
        }

        public async Task<RegisterResponeModel> GetUserById(int id)
        {
            RegisterResponeModel responseModel = new();

            var isExistUser = _context.RegisterModel.Any(x => x.Id == id);
            if (!isExistUser)
            {
                responseModel.ResponseMessage = "User does not exist.";
                return responseModel;
            }

            var userModel = await _context.RegisterModel.FirstOrDefaultAsync(x => x.Id == id);

            responseModel.Name = userModel.Name;
            responseModel.UserName = userModel.UserName;
            responseModel.Password = await _encryptionService.DecryptAsync(userModel.Password);
            responseModel.PhoneNumber = userModel.PhoneNumber;
            responseModel.ResponseMessage = "Success";
            return responseModel;
        }

    }
}
