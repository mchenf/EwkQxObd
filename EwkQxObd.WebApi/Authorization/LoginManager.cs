using Azure.Core;
using EwkQxObd.Api.Authentication.ObjectModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.Configuration;
using System.Net.Http;
using System.Security.Claims;

namespace EwkQxObd.WebApi.Authorization
{
    public class LoginManager
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public LoginManager(IHttpClientFactory httpClientFactory,
            IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<AuthResponseBody?> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new UserOrPassNullException("username or password is missing");
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

            var response = await client.PostAsync(auth0, new FormUrlEncodedContent(tokenPayload));

            if (!response.IsSuccessStatusCode)
            {
                throw new UnauthorizedAccessException();
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<AuthResponseBody>();

            if (tokenResponse is null)
            {
                throw new ArgumentNullException(nameof(tokenResponse));
            }

            return tokenResponse;
        }
    }
}
