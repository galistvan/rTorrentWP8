using RTorrentLib.RtorrentInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RTorrentLib
{
    internal class XmlRpcResponse
    {
        private XElement xElement;
        private XmlRpcRequest xmlRpcRequest;

        internal XmlRpcResponse(XmlRpcRequest xmlRpcRequest, Stream responseStream)
        {
            this.xmlRpcRequest = xmlRpcRequest;
            xElement = XElement.Load(responseStream);
        }

        internal List<TorrentItem> GetTorrents()
        {
            List<TorrentItem> ret = new List<TorrentItem>();
            XElementProcessor xElementProcessor = new XElementProcessor();
            var values = xElementProcessor.GetValueList(xElement);
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
