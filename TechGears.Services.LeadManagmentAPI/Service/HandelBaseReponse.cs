using System.Net;
using Newtonsoft.Json;
using TechGears.Services.LeadManagmentAPI.Models.DTO;


namespace TechGears.Services.LeadManagmentAPI.Service
{
    public class HandelBaseReponse
    {

        public static async Task<ResponseDTO?> HandleResponse(HttpResponseMessage apiResponse)
        {

            try
            {
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "NotFound" };

                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };

                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };

                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };

                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return apiResponseDTO;
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO() { Message = ex.Message };
            }
        }
    }
}