


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
            if (!Request.HasFormContentType)
            {
                return AuthenticateResult.Fail("Invalid Request");
            }

            string? username = Request.Form["username"];
            string? password = Request.Form["password"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return AuthenticateResult.Fail("Missing Credentials");
            }

            var client = _httpClientFactory.CreateClient();

            string? grant_type = _config["authBody:grant_type"];
            string? client_id = _config["authBody:client_id"];
            string? scope = _config["authBody:scope"];
            string? client_secret = _config["authBody:client_secret"];
            string? audience = _config["authBody:audience"];

            string? auth0 = _config["appUrl:auth0"];



            if (
                string.IsNullOrEmpty(auth0) ||
                string.IsNullOrEmpty(grant_type) ||
                string.IsNullOrEmpty(client_id) ||
                string.IsNullOrEmpty(scope) ||
                string.IsNullOrEmpty(client_secret) ||
                string.IsNullOrEmpty(audience)

            ) 
            {
                throw new InvalidConfigurationException("Check your configuration, some or more items are missing for the call.");
            }

            var tokenPayload = new Dictionary<string, string>
            {
                [nameof(grant_type)] = grant_type,
                [nameof(client_id)] = client_id,
                [nameof(scope)] = scope,
                [nameof(client_secret)] = client_secret,
                [nameof(audience)] = audience,
                [nameof(username)] = username,
                [nameof(password)] = password
            };

            try
            {
                var response = await client.PostAsync(auth0, new FormUrlEncodedContent(tokenPayload));

                if (!response.IsSuccessStatusCode)
                {
                    return AuthenticateResult.Fail("Foss API Authentication Failed");
                }

                var tokenResponse = await response.Content.ReadFromJsonAsync<AuthResponseBody>();

                if (tokenResponse is null)
                {
                    throw new ArgumentNullException(nameof(tokenResponse));
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.AuthenticationMethod, "FossApi"),
                    new Claim("access_token", tokenResponse.AccessToken),
                    new Claim("token_type", tokenResponse.TokenType),
                    new Claim("expires_in", tokenResponse.ExpiresIn.ToString())
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);

            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Failed to authenticate {ex.Message}");
            }


        }
    }
}
