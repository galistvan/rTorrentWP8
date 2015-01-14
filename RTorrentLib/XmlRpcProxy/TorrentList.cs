using CookComputing.XmlRpc;
using RTorrentLib.RTorrentInterface.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib.XmlRpcProxy
{
    public class TorrentList : XmlRpcClientProtocol
    {
        private String view;

        public TorrentList(String serviceUrl, String view)
        {
            this.Url = serviceUrl;
            this.view = view;
        }

        public IAsyncResult BeginInvoke(AsyncCallback acb, object o)
        {
            return PrivateInvoke(view, "d.get_hash=", "d.get_name=", "d.get_state=", "d.get_completed_bytes=",
            "d.get_up_total=", "d.get_peers_complete=", "d.get_peers_accounted=", "d.get_down_rate=",
            "d.get_up_rate=", "d.get_message=", "d.get_priority=", "d.get_size_bytes=", "d.is_hash_checking=",
            "d.get_custom1=", acb);
        }

        [XmlRpcBegin("d.multicall")] //TODO this many parameter is because the used lib. Check the serializer for other solution.
        private IAsyncResult PrivateInvoke(string par1, string par2, string par3, string par4, string par5,
            string par6, string par7, string par8, string par9, string par10, string par11, string par12,
            string par13, string par14, string par15, AsyncCallback acb)
        {
            return this.BeginInvoke(MethodBase.GetCurrentMethod(), new object[] {
                par1,  par2,  par3, par4, par5,  par6, par7, par8, par9, par10, par11, par12, par13, par14, par15 }, acb, null);
        }

        [XmlRpcEnd]
        public List<TorrentItem> EndInvoke(IAsyncResult iasr)
        {
            List<TorrentItem> torrentItems = new List<TorrentItem>();
            object invokeResult = (object)base.EndInvoke(iasr);

            if (invokeResult is object[][])
            {
                object[][] torrentItemArray = (object[][])invokeResult;

                foreach (object[] torrentItemValue in torrentItemArray)
                {
                    TorrentItem torrentItem = new TorrentItem();
                    torrentItem.Hash = torrentItemValue[0].ToString();
                    torrentItem.TorrentName = torrentItemValue[1].ToString();
                    torrentItem.Started = long.Parse(torrentItemValue[2].ToString()) == 1;
                    torrentItem.CompletedBytes = long.Parse(torrentItemValue[3].ToString());
                    torrentItem.UpTotal = long.Parse(torrentItemValue[4].ToString());
                    torrentItem.PeersComplete = long.Parse(torrentItemValue[5].ToString());
                    torrentItem.PeersAccounted = long.Parse(torrentItemValue[6].ToString());
                    torrentItem.DownRate = long.Parse(torrentItemValue[7].ToString());
                    torrentItem.UpRate = long.Parse(torrentItemValue[8].ToString());
                    torrentItem.Message = torrentItemValue[9].ToString();
                    torrentItem.Priority = long.Parse(torrentItemValue[10].ToString());
                    torrentItem.SizeBytes = long.Parse(torrentItemValue[11].ToString());
                    torrentItem.HashChecking = long.Parse(torrentItemValue[12].ToString()) == 1;
                    torrentItem.Label = torrentItemValue[13].ToString();
                    torrentItems.Add(torrentItem);
                }
            }
            return torrentItems;
        }

    }
}
