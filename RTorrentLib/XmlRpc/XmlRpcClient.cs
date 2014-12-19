using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RTorrentLib.XmlRpc
{
    internal class XmlRpcClient
    {
       
        private readonly string contentType;
        private readonly string httpMethod;
        private readonly string url;

        private readonly string username;
        private readonly string password;

        internal XmlRpcClient(string url) : this(url, null, null)
        {
        }

        internal XmlRpcClient(string url, string username, string password)
        {
            this.contentType = "text/xml";
            this.httpMethod = "POST";
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
            var hwr = (HttpWebRequest)System.Net.WebRequest.CreateHttp(url);
            hwr.ContentType = contentType;
            hwr.Method = httpMethod;
            hwr.Credentials = GetCredentials();
            return hwr;
        }
        private ICredentials GetCredentials()
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                return new NetworkCredential()
                {
                    Password = password,
                    UserName = username
                };
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
                    var xmlRpcWriter = new XmlRpcWriter(requestStream);
                    xmlRpcWriter.WriteRequest(xmlRpcRequest);
                    requestStream.Close();
                }
            });
            //TODO try-catch
            task.Wait();
        }
        private XmlRpcResponse ReceiveRepsonse(HttpWebRequest hwr, XmlRpcRequest xmlRpcRequest)
        {
            Debug.WriteLine(xmlRpcRequest.ToString());
            Task<XmlRpcResponse> task = Task.Run<XmlRpcResponse>(async () =>
            {
                using (WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(hwr.BeginGetResponse, hwr.EndGetResponse, hwr))
                {
                    Debug.WriteLine(responseObject.ToString());
                    var responseStream = responseObject.GetResponseStream();

                    Debug.WriteLine(responseStream.ToString());
                    return new XmlRpcResponse(xmlRpcRequest, responseStream);


                }
            });
            return task.Result;
        }
    }
}
