using TechGears.Web.Service.IService;
using TechGears.Web.Utility;

namespace TechGears.Web.Service
{
    public class TokenProviderImpl : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContext;

        public TokenProviderImpl(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void ClearToken() => _httpContext.HttpContext?.Response.Cookies.Delete(SD.TokenCookia);

        public string? GetToken()
        {
            bool hasToken = _httpContext.HttpContext.Request.Cookies.TryGetValue(SD.TokenCookia, out string? token);
            return hasToken ? token : null;
        }

        public void SetToken(string token) => _httpContext.HttpContext?.Response.Cookies.Append(SD.TokenCookia, token);
    }
}