namespace JWTAuthDotNet7.Models.RequestModels
{
    public class LoginRequestModel 
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
