using EwkQxObd.WebApi.Data.Encryption;

namespace EwkQxObd.WebApi.Authorization
{
    public static class HttpContextExtension
    {
        public static string GetAccessToken(this HttpContext context, ITokenProtector protector)
        {
            var encrypted = context.Request.Cookies["Auth0.AccessToken"];
            if (string.IsNullOrEmpty(encrypted))
            {
                return string.Empty;
            }

            return protector.Unprotect(encrypted);
        }

    }
}
