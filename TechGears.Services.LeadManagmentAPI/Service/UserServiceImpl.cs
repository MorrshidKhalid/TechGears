using Newtonsoft.Json;
using TechGears.Services.LeadManagmentAPI.Models.DTO;
using TechGears.Services.LeadManagmentAPI.SerializDeserializ;
using TechGears.Services.LeadManagmentAPI.Service.IService;
using TechGears.Services.LeadManagmentAPI.Utility;

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

        public async Task<bool> IsUserExists(string username)
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