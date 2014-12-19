using RTorrentLib.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib.RTorrentInterface.Method
{
    internal abstract class MethodBase<T>
    {
        private string url;
        protected MethodBase(string url)
        {
            this.url = url;
        }

        protected abstract object[] Parameters { get; }

        internal T CallMethod()
        {
            XmlRpcClient xmlRpcClient = new XmlRpcClient(url);

            XmlRpcResponse response = PostDataAndReadResponse(xmlRpcClient);

            return ProcessResponse(response);
        }


        protected abstract string MethodName { get; }

        protected abstract T ProcessResponse(XmlRpcResponse response);

        protected XmlRpcResponse PostDataAndReadResponse(XmlRpcClient xmlRpcClient)
        {
            XmlRpcRequest request = new XmlRpcRequest(MethodName);
            foreach (object parameter in Parameters)
            {
                request.AddParameter(parameter);
            }
            return xmlRpcClient.Call(request);
        }
    }
}
