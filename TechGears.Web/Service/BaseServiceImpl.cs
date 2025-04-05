using Newtonsoft.Json;
using TechGears.Web.Service.IService;
using TechGears.Web.Models;
using System.Text;
using static TechGears.Web.Utility.SD;

namespace TechGears.Web.Service
{
    public class BaseServiceImpl : IBaseService
    {
        
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseServiceImpl(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<ResponseDTO?> SendAsync(Request request)
        {
            HttpClient client = _httpClientFactory.CreateClient("TechGears");
            
            // It used to config aspects such verb(GET...etc), content, and more.
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");

            message.RequestUri = new Uri(request.Url);

            // To SerializeObject when we have data.
            if (request.Data != null)
                message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");

            // To config message method.
            message.Method = request.ApiType switch
            {
                ApiType.POST => HttpMethod.Post,
                ApiType.PUT => HttpMethod.Put,
                ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get
            };      
            
             HttpResponseMessage apiResponse = await client.SendAsync(message);

            // Send this and get response back.
            return await HandelBaseReponse.HandleResponse(apiResponse);
        }
    }
}