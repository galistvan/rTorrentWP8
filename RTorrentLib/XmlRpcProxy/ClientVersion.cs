using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib.XmlRpcProxy
{
    public class ClientVersion : XmlRpcClientProtocol
    {
        public ClientVersion(String serviceUrl)
        {
            this.Url = serviceUrl;
        }

        [XmlRpcBegin("system.client_version")]
        public IAsyncResult BeginInvoke(AsyncCallback acb, object o)
        {
            return this.BeginInvoke(MethodBase.GetCurrentMethod(), new object[] { }, acb, null);
        }

        [XmlRpcEnd]
        public string EndInvoke(IAsyncResult iasr)
        {
            string ret = (string)base.EndInvoke(iasr);
            return ret;
        }

    }
}
