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
        private XmlWriterSettings xmlWriterSettings;
        private string contentType;
        private string httpMethod;
        private string url;

        internal XmlRpc(string url)
        {
            xmlWriterSettings = new XmlWriterSettings();
            //xmlWriterSettings.Encoding = Encoding.GetEncoding("ISO-8859-1");
            xmlWriterSettings.Encoding = new System.Text.UTF8Encoding(false);
            contentType = "text/xml";
            httpMethod = "POST";
            this.url = url;
        }

        internal async Task<XmlReader> Call(string methodName, params object[] pars)
        {
            HttpWebRequest hwr = (HttpWebRequest)System.Net.WebRequest.CreateHttp(url);
            hwr.ContentType = contentType;
            hwr.Method = httpMethod;

            var factory = new TaskFactory();

            using (var requestStream = await Task<Stream>.Factory.FromAsync(hwr.BeginGetRequestStream, hwr.EndGetRequestStream, hwr))
            {
                XmlWrite(requestStream, methodName, pars);
            }


            WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(hwr.BeginGetResponse, hwr.EndGetResponse, hwr);
            var responseStream = responseObject.GetResponseStream();

            return XmlReader.Create(responseStream);
        }

        internal void XmlWrite(Stream s, string methodName, object[] pars)
        {
            XmlWriter xmlWriter = XmlWriter.Create(s, xmlWriterSettings);
            RTorrentLib.XmlRpcSerializer.WriteXmlRpcRequest(xmlWriter, methodName, pars);
            
            s.Close();
        }

    }
}
