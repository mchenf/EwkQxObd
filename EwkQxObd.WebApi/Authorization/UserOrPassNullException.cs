using System.Runtime.Serialization;

namespace EwkQxObd.WebApi.Authorization
{
    [Serializable]
    public class UserOrPassNullException : Exception
    {
        public UserOrPassNullException() { }

        public UserOrPassNullException(string message) : base(message) { }

        public UserOrPassNullException(string message,  Exception innerException) : base(message, innerException) { }

        public bool UsernameMissing { get; set; } = false;
        public bool UserPasswordMissing { get; set;} = false;
    }
}
