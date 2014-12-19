using RTorrentLib.RTorrentInterface.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib.RTorrentInterface.Method
{
    internal class StopTorrent : MethodBase<bool>
    {
        private TorrentItem torrentItem;

        internal StopTorrent(string url, TorrentItem torrentItem) : base(url) {
            this.torrentItem = torrentItem;
        }

        protected override object[] Parameters
        {
            get { return new object[] { "d.try_close=", torrentItem.Hash }; }
        }

        protected override string MethodName
        {
            get { return "d.multicall"; }
        }

        protected override bool ProcessResponse(XmlRpc.XmlRpcResponse response)
        {
            return true;
        }
    }
}
