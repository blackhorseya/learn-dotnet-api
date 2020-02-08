using System.Net;
using System.Text;
using Doggy.Extensions.Exceptions;

namespace Doggy.Learning.Auth.Domain.FaultInfos
{
    public class AccountNameNotFound : FaultInfoBase
    {
        public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.NotFound;
        
        public AccountNameNotFound(string value)
        {
            ErrorMessage = $"AccountName [{value}] not found";
        }

        public AccountNameNotFound(string value, string message)
        {
            if (!string.IsNullOrEmpty(message))
                ErrorMessage = $"{message} (value = ${value})";
        }
    }
}