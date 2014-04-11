using RTorrentLib.RtorrentInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib
{
    public class RTorrent
    {
        string url;

        public RTorrent(string url)
        {
            this.url = url;
        }

        public IList<TorrentItem> TorrentList()
        {
            TorrentsList torrentList = new TorrentsList(url);
            return torrentList.CallMethod();
        }

        public void DownloadStart(TorrentItem torrentItem)
        {

        }
    }
}
