using static TechGears.Web.Utility.SD;

namespace TechGears.Web.Models
{
    public class Request
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public required string Url { get; set; }
        public object? Data { get; set; } = null;
        public string AccessToken { get; set; } = string.Empty;
    }
}
