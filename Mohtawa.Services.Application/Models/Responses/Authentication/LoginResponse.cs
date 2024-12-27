using Mohtawa.Services.Application.Models.DTOs;

namespace Mohtawa.Services.Application.Models.Responses.Authentication
{
    public class LoginResponse : BaseResponse
    {
        public TokenDTO Token { get; set; }
    }
}
