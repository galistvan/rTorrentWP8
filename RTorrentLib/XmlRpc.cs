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
            WriteXmlRpcRequest(xmlWriter, methodName, pars);
            
            s.Close();
        }

        #region request
        private const string methodCallNode = "methodCall";
        private const string methodNameNode = "methodName";
        #endregion

        #region response
        private const string methodResponseNode = "methodResponse";
        #endregion

        private const string paramsNode = "params";
        private const string paramNode = "param";
        private const string valueNode = "value";
        private const string arrayNode = "array";
        private const string dataNode = "data";

        //types
        private const string stringNode = "string";
        private const string base64Node = "base64";

        private void WriteXmlRpcRequest(XmlWriter xmlWriter, string methodName, params object[] parameters)
        {
            //Encoding enc = Encoding.UTF8;
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement(methodCallNode);
            xmlWriter.WriteStartElement(methodNameNode);

            xmlWriter.WriteString(methodName);

            xmlWriter.WriteEndElement();
            if (parameters != null && parameters.Length > 0)
            {
                xmlWriter.WriteStartElement(paramsNode);
                foreach (object par in parameters)
                {
                    xmlWriter.WriteStartElement(paramNode);
                    xmlWriter.WriteStartElement(valueNode);
                    xmlWriter.WriteStartElement(stringNode);

                    xmlWriter.WriteString(par.ToString());

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();

            xmlWriter.Flush();
        }

    }
}
