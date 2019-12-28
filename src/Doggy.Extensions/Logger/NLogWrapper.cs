using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace Doggy.Extensions.Logger
{
    public class NLogWrapper : ILogWrapper
    {
        private readonly ILogger _logger;

        public NLogWrapper()
        {
            _logger = LogManager.GetLogger(Constants.Logger.ConsoleLogger);
        }

        public void Exception(Exception ex, params (string, object)[] args)
        {
            ExceptionWithMessage(string.Empty, ex, args);
        }

        public void ExceptionWithMessage(string message, Exception ex, params (string, object)[] args)
        {
            var (keys, values) = args.AddCategoryAndMessage(Constants.Category.ExceptionHandler, message).ConvertToNLog();
            _logger.Error(ex, keys, values);
        }

        public void TraceRequest(params (string, object)[] args)
        {
            TraceRequestWithMessage(string.Empty, args);
        }

        public void TraceRequestWithMessage(string message, params (string, object)[] args)
        {
            var (keys, values) = args.AddCategoryAndMessage(Constants.Category.RequestTracker, message).ConvertToNLog();
            _logger.Info(keys, values);
        }
    }
}