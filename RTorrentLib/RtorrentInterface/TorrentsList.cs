using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RTorrentLib.RtorrentInterface
{
    internal class TorrentsList
    {
        private string url;
        internal TorrentsList(string url)
        {
            this.url = url;
        }

        private object[] GetParams()
        {
            List<object> pars = new List<object>();
            //pars.Add(String.Empty);
            pars.Add("incomplete");
            pars.Add("d.get_hash=");
            pars.Add("d.get_name=");
            pars.Add("d.get_state=");
            pars.Add("d.get_completed_bytes=");
            pars.Add("d.get_up_total=");
            pars.Add("d.get_peers_complete=");
            pars.Add("d.get_peers_accounted=");
            pars.Add("d.get_down_rate=");
            pars.Add("d.get_up_rate=");
            pars.Add("d.get_message=");
            pars.Add("d.get_priority=");
            pars.Add("d.get_size_bytes=");
            pars.Add("d.is_hash_checking=");
            pars.Add("d.get_custom1=");
            return pars.ToArray();
        }

        internal string GetMethodName()
        {
            return "d.multicall";
        }

        internal List<TorrentItem> CallMethod()
        {
            XmlRpc xmlRpc = new XmlRpc(url);

            var response = PostDataAndReadResponse(xmlRpc);

            return response.GetTorrents();
        }

        internal XmlRpcResponse PostDataAndReadResponse(XmlRpc xmlRpc)
        {
            XmlRpcRequest request = new XmlRpcRequest(GetMethodName());
            foreach (object param in GetParams())
            {
                request.AddParameter(param);
            }
            return xmlRpc.Call(request);
        }
    }
}
