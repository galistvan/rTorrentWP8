using RTorrentLib.XmlRpc;
using RTorrentLib.RTorrentInterface.Item;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RTorrentLib.XmlRpc
{
    public class XmlRpcResponse
    {
        private XmlRpcRequest xmlRpcRequest;

        internal XmlRpcResponse(XmlRpcRequest xmlRpcRequest, Stream responseStream)
        {
            this.xmlRpcRequest = xmlRpcRequest;
            this.ResponseXElement = XElement.Load(responseStream);
        }

        public XElement ResponseXElement
        {
            get;
            private set;
        }
    }
}
