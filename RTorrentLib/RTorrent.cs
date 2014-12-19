using RTorrentLib.Logger;
using RTorrentLib.RTorrentInterface.Item;
using RTorrentLib.RTorrentInterface.Method;
using RTorrentLib.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib
{
    public class RTorrent
    {
        private readonly string url;
        private ILogger logger;

        public RTorrent(string url, ILogger logger)
        {
            this.logger = new LoggerDecorator(logger);
            this.url = url;
            logger.Debug("Logger is under using.");
        }

        public IList<TorrentItem> MainTorrents()
        {
            TorrentsMainMethod torrentsMethod = new TorrentsMainMethod(url);
            return torrentsMethod.CallMethod();
        }

        public void DownloadStart(TorrentItem torrentItem)
        {

        }

        public bool DownloadStop(TorrentItem torrent)
        {
            StopTorrent stopTorrent = new StopTorrent(url, torrent);
            return stopTorrent.CallMethod();
        }
    }
}
