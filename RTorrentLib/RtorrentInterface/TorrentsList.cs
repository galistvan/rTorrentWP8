using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RTorrentLib.RtorrentInterface
{
    public class TorrentsList
    {
        private string url;
        public TorrentsList(string url)
        {
            this.url = url;
        }

        internal object[] GetParams()
        {
            List<object> pars = new List<object>();
            pars.Add(String.Empty);
            //pars.Add("incomplete");
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

        public List<TorrentItem> CallMethod()
        {
            XmlRpc xmlRpc = new XmlRpc(url);

            var xElement = PostDataAndReadResponse(xmlRpc);

            return new XElementProcessor().Process(xElement);
        }

        internal XElement PostDataAndReadResponse(XmlRpc xmlRpc)
        {
            var task = xmlRpc.Call(GetMethodName(), GetParams());
            task.Wait();
            return XElement.Load(task.Result);
        }
    }
}
