namespace TechGears.Web.Utility
{
    public class SD 
    {
        public static string AuthAPIBase { get; set; } 
        public static string CustomerAPIBase { get; set; } 

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}