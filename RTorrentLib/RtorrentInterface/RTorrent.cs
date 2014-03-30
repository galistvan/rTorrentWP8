using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTorrentLib.RtorrentInterface
{
    using Attributes;
    using System.Diagnostics;
    public class RTorrent : IRTorrent
    {
        private XmlRpc xmlRpcClient;

        public RTorrent(string url)
        {
            this.xmlRpcClient = new XmlRpc(url);
        }

        private T MethodCaller<T>(params object[] pars)
            where T : class, new()
        {
            var interfaces = this.GetType().GetInterfaces();
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            string currentMethodName = methodBase.Name;
            XmlRpcMethodAttribute attr = interfaces[0].GetMethod(currentMethodName).GetCustomAttribute<XmlRpcMethodAttribute>();
            Task<object> ret = null;
            if (attr != null)
            {
                ret = xmlRpcClient.Call(attr.Method, pars);
            }
            else
            {
                throw new NotImplementedException("nincs method attribute-ja");
            }
            ret.Wait();

            return (T)ret.Result;
        }

        public List<string> ListMethods()
        {
            var ret = MethodCaller<List<string>>();
            return ret;
        }

        public List<TorrentItem> DownloadList()
        {
            List<TorrentItem> ret = new List<TorrentItem>();
            var hashlist = MethodCaller<List<string>>();
            hashlist.ForEach(x => ret.Add(new TorrentItem(x)));

            return ret;
        }

        public List<TorrentItem> DownloadListIncomplete()
        {
            List<TorrentItem> ret = new List<TorrentItem>();
            var hashlist = MethodCaller<List<string>>("incomplete");
            hashlist.ToList().ForEach(x => ret.Add(new TorrentItem(x)));

            return ret;
        }


        public List<TorrentItem> DownloadListStopped()
        {
            List<TorrentItem> ret = new List<TorrentItem>();
            var hashlist = MethodCaller<List<string>>("stopped");
            hashlist.ForEach(x => ret.Add(new TorrentItem(x)));

            return ret;
        }

        public List<TorrentItem> MulticallTest()
        {
            List<TorrentItem> ret = new List<TorrentItem>();
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
            var hashlist = MethodCaller<List<string>>(pars.ToArray());

            hashlist.ForEach(x => ret.Add(new TorrentItem(x)));

            return ret;
        }

    }
}
