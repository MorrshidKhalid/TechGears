namespace TechGears.Services.Auth.Models.DTO
{
    public class LoginResponse
    {
        public UserDTO? User { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
