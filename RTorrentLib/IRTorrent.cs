using RTorrentLib.RTorrentInterface.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib
{
    public interface IRTorrent
    {
        Task<string> ClientVersion();
        Task<List<TorrentItem>> StartedTorrents();
        Task<List<TorrentItem>> StoppedTorrents();
        Task<string> StopTorrent(TorrentItem torrentItem);
        Task<string> StartTorrent(TorrentItem torrentItem);
    }
}
