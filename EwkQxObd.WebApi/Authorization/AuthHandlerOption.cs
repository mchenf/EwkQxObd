using Microsoft.AspNetCore.Authentication;

namespace EwkQxObd.WebApi.Authorization
{
    public class AuthHandlerOption : AuthenticationSchemeOptions
    {
        public string TokenEndpoint { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;


    }
}
