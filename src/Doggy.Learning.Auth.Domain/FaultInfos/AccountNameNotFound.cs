using System.Net;
using Doggy.Extensions.Exceptions;

namespace Doggy.Learning.Auth.Domain.FaultInfos
{
    public class AccountNameNotFound : FaultInfoBase
    {
        private const HttpStatusCode SelfHttpStatusCode = HttpStatusCode.NotFound;
        
        public AccountNameNotFound(string value) : base(SelfHttpStatusCode)
        {
            ErrorMessage = $"AccountName [{value}] not found";
        }

        public AccountNameNotFound(string value, string message) : base(SelfHttpStatusCode)
        {
            if (!string.IsNullOrEmpty(message))
                ErrorMessage = $"{message} (value = ${value})";
        }
    }
}