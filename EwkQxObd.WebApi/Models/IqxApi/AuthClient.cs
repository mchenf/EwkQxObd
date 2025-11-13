using EwkQxObd.Api.Authentication;
using EwkQxObd.Api.Authentication.ObjectModel;

namespace EwkQxObd.WebApi.Models.IqxApi
{
    public class AuthClient
    {
        private IConfiguration Configuration { get; set; }
        private Client AuthenticationClient { get; set; } = new();

        public AuthClient(IConfiguration Config)
        {
            Configuration = Config;
            AuthenticationClient.AuthUrl = Configuration["Url:Auth"] 
                ?? throw new Exception("Url:Auth Not In Configuration");
        }

        public void AttachRequestBody(AuthRequestBody Body)
        {
            Body.Scope = Configuration["Authentication:Scope"]
                ?? throw new Exception("Authentication:Scope Not In Configuration");

            Body.Audience = Configuration["Authentication:Audience"]
                ?? throw new Exception("Authentication:Audience Not In Configuration");

            Body.ClientSecret = Configuration["Authentication:ClientSecret"]
                ?? throw new Exception("Authentication:ClientSecret Not In Configuration");

            Body.ClientId = Configuration["Authentication:ClientId"]
                ?? throw new Exception("Authentication:ClientId Not In Configuration");

            Body.GrantType = Configuration["Authentication:GrantType"]
                ?? throw new Exception("Authentication:GrantType Not In Configuration");

            AuthenticationClient.Request = Body;

        }

        public async Task<LoginResultPageModel> Authenticate()
        {

            var Result = new LoginResultPageModel();

            await AuthenticationClient.Authenticate();

            Result.LoginSuccess = AuthenticationClient.IsAuthenticated;


            return Result;
        }

    }
}
