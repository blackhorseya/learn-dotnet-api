using System;

namespace Doggy.Extensions.Logger
{
    public interface ILogWrapper
    {
        void Exception(Exception ex, params (string, object)[] args);
        void ExceptionWithMessage(string message, Exception ex, params (string, object)[] args);

        void TraceRequest(params (string, object)[] args);
        void TraceRequestWithMessage(string message, params (string, object)[] args);
    }
}