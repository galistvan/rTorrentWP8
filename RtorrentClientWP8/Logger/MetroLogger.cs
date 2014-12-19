using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtorrentClientWP8.Logger
{
    public class MetroLogger : RTorrentLib.Logger.ILogger
    {
        private readonly MetroLog.ILogger _logger;

        public MetroLogger()
        {
            _logger = MetroLog.LogManagerFactory.CreateLogManager().GetLogger("log");
        }

        public void Debug(string message, Exception ex = null)
        {
            _logger.Debug(message, ex);
        }

        public void Error(string message, Exception ex = null)
        {
            _logger.Error(message, ex);
        }
    }
}
