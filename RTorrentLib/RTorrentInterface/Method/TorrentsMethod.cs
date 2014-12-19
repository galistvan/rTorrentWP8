using RTorrentLib.RTorrentInterface.Item;
using RTorrentLib.RTorrentInterface.Method;
using RTorrentLib.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RTorrentLib.RTorrentInterface.Method
{
    internal abstract class TorrentsMethod : MethodBase<List<TorrentItem>>
    {
        internal TorrentsMethod(string url) : base(url) { }

        protected static readonly object[] parameters = new object[]{
                "d.get_hash=", "d.get_name=", "d.get_state=", "d.get_completed_bytes=",
                "d.get_up_total=", "d.get_peers_complete=", "d.get_peers_accounted=", "d.get_down_rate=",
                "d.get_up_rate=", "d.get_message=", "d.get_priority=", "d.get_size_bytes=", "d.is_hash_checking=",
                "d.get_custom1=",
            };

        protected override object[] Parameters
        {
            get { return parameters; }
        }

        protected override string MethodName
        {
            get
            {
                return "d.multicall";
            }
        }

        protected override List<TorrentItem> ProcessResponse(XmlRpcResponse response)
        {
            List<TorrentItem> ret = new List<TorrentItem>();
            TorrentItemXElementProcessor xElementProcessor = new TorrentItemXElementProcessor();
            var values = xElementProcessor.GetValueList(response.ResponseXElement);
            foreach (var value in values)
            {
                var torrentItemValue = xElementProcessor.ProcessValueList(value);
                TorrentItem item = new TorrentItem();

                item.Hash = torrentItemValue[0].Value;
                item.TorrentName = torrentItemValue[1].Value;
                item.Started = long.Parse(torrentItemValue[2].Value) == 1;
                item.CompletedBytes = long.Parse(torrentItemValue[3].Value);
                item.UpTotal = long.Parse(torrentItemValue[4].Value);
                item.PeersComplete = long.Parse(torrentItemValue[5].Value);
                item.PeersAccounted = long.Parse(torrentItemValue[6].Value);
                item.DownRate = long.Parse(torrentItemValue[7].Value);
                item.UpRate = long.Parse(torrentItemValue[8].Value);
                item.Message = torrentItemValue[9].Value;
                item.Priority = long.Parse(torrentItemValue[10].Value);
                item.SizeBytes = long.Parse(torrentItemValue[11].Value);
                item.HashChecking = long.Parse(torrentItemValue[12].Value) == 1;
                item.Label = torrentItemValue[13].Value;

                ret.Add(item);
            }
            return ret;
        }
    }
}
