using System;
using System.Net;

namespace Doggy.Extensions.Exceptions
{
    public abstract class FaultInfoBase : Exception
    {
        protected FaultInfoBase(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorCode { get; }
        public string ErrorMessage { get; set; }
    }
}