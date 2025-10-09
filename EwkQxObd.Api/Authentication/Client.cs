using EwkQxObd.Api.Authentication.ObjectModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EwkQxObd.Api.Authentication
{
    public class Client : ClientBase
    {

        public string AuthUrl = string.Empty;

        public Client() { }

        public AuthResponseBody? Response { get; private set; }
        public DateTime? AuthenticatedOn { get; private set; }
        public DateTime? ExpiresOn { 
            get
            {
                if (AuthenticatedOn == null || Response == null)
                {
                    return null;
                }
                return AuthenticatedOn.Value.AddSeconds(Response.ExpiresIn);
            }
        }

        public bool HasResponse { get => Response != null; }
        public bool IsNotExpired { get => ExpiresOn > DateTime.Now; }
        public bool IsAuthenticated { get => HasResponse && IsNotExpired; }

        public async Task Authenticate(string User, string Password)
        {
            using var clientAlt = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, AuthUrl);


            request.Content = ComposeContent(User, Password);

            using var response = await clientAlt.SendAsync(request);

            response.EnsureSuccessStatusCode();

            AuthenticatedOn = DateTime.Now;

            using var result = await response.Content.ReadAsStreamAsync();

            Response = await JsonSerializer.DeserializeAsync<AuthResponseBody>(result);

        }


        public AuthRequestBody Request { get; set; } = new();
        private List<KeyValuePair<string, string>> Collection { get; set; } = [];
        private FormUrlEncodedContent ComposeContent(string user, string pass)
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
