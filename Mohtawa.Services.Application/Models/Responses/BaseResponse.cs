using System.Net;
using System.Text.Json;

namespace Mohtawa.Services.Application.Models.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
        public string ErrorMessage { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
