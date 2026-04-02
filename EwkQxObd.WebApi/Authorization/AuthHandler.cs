


using EwkQxObd.Api.Authentication.ObjectModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.Configuration;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace EwkQxObd.WebApi.Authorization
{
    public class AuthHandler : AuthenticationHandler<AuthHandlerOption>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public AuthHandler(
            IOptionsMonitor<AuthHandlerOption> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IConfiguration Configuration,
            IHttpClientFactory HttpClientFactory
        ) : base(options, logger, encoder)
        {
            _httpClientFactory = HttpClientFactory;
            _config = Configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return AuthenticateResult.Fail(new NotImplementedException());

        }
    }
}
