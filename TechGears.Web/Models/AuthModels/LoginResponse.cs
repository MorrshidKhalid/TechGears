namespace TechGears.Web.Models.AuthModels
{
    public class LoginResponse
    {
        public UserDTO? User { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
