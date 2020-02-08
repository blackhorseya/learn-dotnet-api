using System;
using System.Net;

namespace Doggy.Extensions.Exceptions
{
    public abstract class FaultInfoBase : Exception
    {
        public abstract HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorCode { get; }
        public string ErrorMessage { get; set; }
    }
}