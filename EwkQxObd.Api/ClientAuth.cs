using EwkQxObd.Api.ObjectModel;
using System.Reflection;
using System.Text.Json.Serialization;

namespace EwkQxObd.Api
{
    public class ClientAuth : ClientBase
    {

        public string AuthUrl { get; set; } = string.Empty;

        public AuthRequestBody Request { get; set; } = new();


        private List<KeyValuePair<string, string>> Collection { get; set; } = new();

        public ClientAuth() { }

        public async Task Authenticate(string User, string Password)
        {
            using var clientAlt = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, "https://foss-prod.eu.auth0.com/oauth/token");


            request.Content = genContent(User, Password);

            using var response = await clientAlt.SendAsync(request);

            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);

        }


        private FormUrlEncodedContent genContent(string user, string pass)
        {
            
            TypeInfo t = typeof(AuthRequestBody).GetTypeInfo();

            var props = t.GetProperties();
            Request.UserName = user;
            Request.Password = pass;

            foreach (var p in props)
            {

                var atts = p.GetCustomAttribute<JsonPropertyNameAttribute>();

                object? valueRaw = p.GetValue(Request);

                if (atts != null && valueRaw != null)
                {
                    Collection.Add(new(atts.Name, valueRaw.ToString() ?? string.Empty));
                }

            }


            return new FormUrlEncodedContent(Collection);


        }
    }
}
