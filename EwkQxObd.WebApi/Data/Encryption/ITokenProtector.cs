namespace EwkQxObd.WebApi.Data.Encryption
{
    public interface ITokenProtector
    {
        string Protect(string PlainText);
        string Unprotect(string ProtectedText);
    }
}
