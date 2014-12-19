using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib.Logger
{
    public class LoggerDecorator : ILogger
    {
        private readonly ILogger _logger;

        public LoggerDecorator(ILogger logger)
        {
            _logger = logger;
        }

        public void Debug(string message, Exception ex = null)
        {
            if (_logger != null)
            {
                _logger.Debug(message, ex);
            }
        }

        public void Error(string message, Exception ex = null)
        {
            if (_logger != null)
            {
                _logger.Error(message, ex);
            }
        }
    }
}
