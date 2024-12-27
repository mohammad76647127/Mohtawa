
using Mohtawa.Services.Application.Models.Requests.Authentication;
using Mohtawa.Services.Application.Models.Responses.Authentication;

namespace Mohtawa.Services.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<RegisterResponse> Register(RegisterRequest registerRequest);
    }
}
