using CookComputing.XmlRpc;
using RTorrentLib.RTorrentInterface.Item;
using RTorrentLib.XmlRpcProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib
{
    public class RTorrentXmlRpc : IRTorrent
    {
        private String serviceUrl;

        public RTorrentXmlRpc(String serviceUrl)
        {
            this.serviceUrl = serviceUrl;
        }

        public async Task<string> ClientVersion()
        {
            var proxy = new ClientVersion(this.serviceUrl);
            return await Task<string>.Factory.FromAsync(proxy.BeginInvoke, proxy.EndInvoke, this);
        }

        public async Task<List<TorrentItem>> StartedTorrents()
        {
            var proxy = new TorrentList(this.serviceUrl, "started");
            return await Task<List<TorrentItem>>.Factory.FromAsync(proxy.BeginInvoke, proxy.EndInvoke, this);
        }

        public async Task<List<TorrentItem>> StoppedTorrents()
        {
            var proxy = new TorrentList(this.serviceUrl, "stopped");
            return await Task<List<TorrentItem>>.Factory.FromAsync(proxy.BeginInvoke, proxy.EndInvoke, this);
        }

        public async Task<string> StopTorrent(TorrentItem torrentItem)
        {
            var proxy = new StopTorrent(this.serviceUrl);
            return await Task<string>.Factory.FromAsync(proxy.BeginInvoke, proxy.EndInvoke, torrentItem.Hash);
        }

        public async Task<string> StartTorrent(TorrentItem torrentItem)
        {
            var proxy = new StartTorrent(this.serviceUrl);
            return await Task<string>.Factory.FromAsync(proxy.BeginInvoke, proxy.EndInvoke, torrentItem.Hash);
        }
    }
}
