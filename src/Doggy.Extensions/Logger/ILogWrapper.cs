using System;

namespace Doggy.Extensions.Logger
{
    public interface ILogWrapper
    {
        void Exception(Exception ex);
        void ExceptionWithMessage(Exception ex, string message);

        void TraceRequest(params (string, object)[] args);
        void TraceRequestWithMessage(string message, params (string, object)[] args);
    }
}