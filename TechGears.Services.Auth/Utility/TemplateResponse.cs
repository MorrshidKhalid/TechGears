using TechGears.Services.Auth.Models.DTO;

namespace TechGears.Services.CustomerAPI.Utility
{
    public class TemplateResponse
    {
        private readonly ResponseDTO _responseDTO;
        public TemplateResponse()
        {
            _responseDTO = new();
        }


        protected ResponseDTO Success(object? result, bool flag = true, string msg = "Success")
        {
            _responseDTO.IsSuccess = flag;
            _responseDTO.Message = msg;
            _responseDTO.Result = result;

            return _responseDTO;
        }

        protected ResponseDTO ErrorResponse(object? result = null, bool flag = false, string msg = "Error")
        {
            _responseDTO.IsSuccess = flag;
            _responseDTO.Message = msg;
            _responseDTO.Result = result;

            return _responseDTO;
        }

        protected void CleanResponse()
        {
            _responseDTO.IsSuccess = false;
            _responseDTO.Message = string.Empty;
            _responseDTO.Result = null;
        }
    }
}