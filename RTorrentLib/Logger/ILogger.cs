using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib.Logger
{
    public interface ILogger
    {
        void Debug(String message, Exception ex = null);
        void Error(String message, Exception ex = null);
    }
}
