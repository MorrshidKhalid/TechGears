using Newtonsoft.Json;
using TechGears.Services.CustomerAPI.Models.DTO;
using TechGears.Services.CustomerAPI.SerializDeserializ;
using TechGears.Services.CustomerAPI.Service.IService;
using TechGears.Services.CustomerAPI.Utility;

namespace TechGears.Services.LeadManagmentAPI.Service
{
    public class UserServiceImpl : TemplateResponse, IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISerializer _serializer;

        public UserServiceImpl(IHttpClientFactory httpClientFactory, ISerializer serializer)
        {
            _httpClientFactory = httpClientFactory;
            _serializer = serializer;
        }

        public async Task<bool> IsUserExists(string? username)
        {
            bool isExists = false;

            HttpClient client = _httpClientFactory.CreateClient("User");
            HttpResponseMessage response = await client.GetAsync($"api/users/exists/{username}");
            string content = await response.Content.ReadAsStringAsync();
            ResponseDTO? responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(content);

            string? json = responseDTO?.Result?.ToString();

            if (json != null)
                isExists = _serializer.Deserializ(json);

            return isExists;
        }
    }
}