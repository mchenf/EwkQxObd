using Microsoft.AspNetCore.DataProtection;

namespace EwkQxObd.WebApi.Data.Encryption
{
    public class TokenProtector : ITokenProtector
    {
        private readonly IDataProtector _protector;

        public TokenProtector(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("AcessTokenEncryption");
        }

        public string Protect(string PlainText)
        {
            return _protector.Protect(PlainText);
        }

        public string Unprotect(string ProtectedText)
        {
            return _protector.Unprotect(ProtectedText);
        }
    }
}
