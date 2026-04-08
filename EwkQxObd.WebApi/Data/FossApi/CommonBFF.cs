using System.Net.Http;

namespace EwkQxObd.WebApi.Data.FossApi
{
    public class CommonBFF
    {
        private static string UrlBase = "https://app.iqx.net/iqx/common-bff-api/v1";
        private static string UrlGetUserOrg = "users/getuserorganisations";
        private readonly IHttpClientFactory _httpClientFactory;

        public CommonBFF(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        public async Task<string> GetUserOrgsAsync(string AccessToken)
        {

            Uri urib = new Uri(UrlBase);
            Uri urif = new Uri(urib, UrlGetUserOrg);

            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, urif);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Bearer " + AccessToken);

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
