using System;
using System.Linq;
using NLog;

namespace Doggy.Extensions.Logger
{
    public class NLogWrapper : ILogWrapper
    {
        private readonly ILogger _exceptionLogger;
        private readonly ILogger _requestLogger;

        public NLogWrapper()
        {
            _exceptionLogger = LogManager.GetLogger(Constants.ExceptionHandler);
            _requestLogger = LogManager.GetLogger(Constants.RequestTracker);
        }

        public void Exception(Exception ex)
        {
            _exceptionLogger.Error(ex);
        }

        public void ExceptionWithMessage(Exception ex, string message)
        {
            _exceptionLogger.Error(ex, message);
        }

        public void TraceRequest(params (string, object)[] args)
        {
            TraceRequestWithMessage(string.Empty, args);
        }

        public void TraceRequestWithMessage(string message, params (string, object)[] args)
        {
            if (args.Length == 0)
            {
                _requestLogger.Info(message);
            }
            else
            {
                var keys = string.Join("", args.Select(x => $"{{@{x.Item1}}}").ToList());
                var values = args.Select(x => x.Item2).ToList();
                if (!string.IsNullOrEmpty(message))
                {
                    keys = $"{message} {keys}";
                }

                _requestLogger.Info(keys, values.ToArray());
            }
        }
    }
}