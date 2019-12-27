using System;
using Doggy.Learning.Infrastructure.Constants;
using Doggy.Learning.Infrastructure.Interfaces;
using NLog;

namespace Doggy.Learning.Infrastructure.Entities
{
    public class NLogWrapper : ILogWrapper
    {
        private readonly ILogger _exceptionLogger;
        private readonly ILogger _requestLogger;
        
        public NLogWrapper()
        {
            _exceptionLogger = LogManager.GetLogger(LoggerConstants.ExceptionHandler);
            _requestLogger = LogManager.GetLogger(LoggerConstants.RequestTracker);
        }

        public void Exception(Exception ex)
        {
            _exceptionLogger.Error(ex);
        }

        public void ExceptionWithMessage(Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void TraceRequest(params (string, object)[] args)
        {
            throw new NotImplementedException();
        }

        public void TraceRequestWithMessage(string message, params (string, object)[] args)
        {
            _requestLogger.Info(message);
        }
    }
}