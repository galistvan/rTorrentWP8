using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RTorrentLib
{
    internal class XmlRpc
    {
        private string contentType;
        private string httpMethod;
        private string url;

        private string username;
        private string password;

        internal XmlRpc(string url)
        {
            contentType = "text/xml";
            httpMethod = "POST";
            this.url = url;
        }

        internal XmlRpc(string url, string username, string password)
        {
            contentType = "text/xml";
            httpMethod = "POST";
            this.url = url;
            this.username = username;
            this.password = password;
        }

        internal XmlRpcResponse Call(XmlRpcRequest xmlRpcRequest)
        {
            HttpWebRequest hwr = SetupHttpRequest();
            return SendAndReceive(hwr, xmlRpcRequest);
        }

        private HttpWebRequest SetupHttpRequest()
        {
            HttpWebRequest hwr = (HttpWebRequest)System.Net.WebRequest.CreateHttp(url);
            hwr.ContentType = contentType;
            hwr.Method = httpMethod;
            hwr.Credentials = GetCredentials();
            return hwr;
        }
        private ICredentials GetCredentials()
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                NetworkCredential nc = new NetworkCredential();
                nc.Password = password;
                nc.UserName = username;
                return nc;
            }
            return null;
        }
        private XmlRpcResponse SendAndReceive(HttpWebRequest hwr, XmlRpcRequest xmlRpcRequest)
        {
            SendRequest(hwr, xmlRpcRequest);
            return ReceiveRepsonse(hwr, xmlRpcRequest);
        }
        private void SendRequest(HttpWebRequest hwr, XmlRpcRequest xmlRpcRequest)
        {
            Task task = Task.Run(async () =>
            {
                using (var requestStream = await Task<Stream>.Factory.FromAsync(hwr.BeginGetRequestStream, hwr.EndGetRequestStream, hwr))
                {
                    XmlRpcWriter xmlRpcWriter = new XmlRpcWriter(requestStream);
                    xmlRpcWriter.WriteRequest(xmlRpcRequest);
                    requestStream.Close();
                }
            });
            task.Wait();
        }
        private XmlRpcResponse ReceiveRepsonse(HttpWebRequest hwr, XmlRpcRequest xmlRpcRequest)
        {
            Task<XmlRpcResponse> task = Task.Run<XmlRpcResponse>(async () =>
            {
                using (WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(hwr.BeginGetResponse, hwr.EndGetResponse, hwr))
                {
                    var responseStream = responseObject.GetResponseStream();
                    return new XmlRpcResponse(xmlRpcRequest, responseStream);
                }
            });
            return task.Result;
        }
    }
}
