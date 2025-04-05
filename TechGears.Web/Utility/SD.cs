namespace TechGears.Web.Utility
{
    public class SD 
    {
        public static string AuthAPIBase { get; set; } 
        public static string CustomerAPIBase { get; set; } 
        public static string LeadManagmentAPIBase { get; set; } 
        public static string TokenCookia = "JWTToken"; 
        public const string RoleAdmin = "ADMIN";
        public const string RoleSeles = "SEL";

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}