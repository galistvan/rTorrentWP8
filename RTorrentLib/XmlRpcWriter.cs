using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RTorrentLib
{
    internal class XmlRpcWriter
    {

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

        private XmlWriterSettings xmlWriterSettings;
        private XmlWriter xmlWriter;
        internal XmlRpcWriter(Stream stream)
        {
            xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = new System.Text.UTF8Encoding(false);
            xmlWriter = XmlWriter.Create(stream, xmlWriterSettings);
        }

        internal void WriteRequest(XmlRpcRequest xmlRpcRequest)
        {
            xmlWriter.WriteStartDocument();
            using (xmlWriter.CreateNode(methodCallNode))
            {
                using (xmlWriter.CreateNode(methodNameNode))
                {
                    xmlWriter.WriteString(xmlRpcRequest.MethodName);
                }

                WriteParametersToXmlWriter(xmlRpcRequest.Parameters);
            }
            xmlWriter.Flush();
        }

        private void WriteParametersToXmlWriter(object[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                using (xmlWriter.CreateNode(paramsNode))
                {
                    foreach (object par in parameters)
                    {
                        using (xmlWriter.CreateNode(paramNode))
                        {
                            using (xmlWriter.CreateNode(valueNode))
                            {
                                using (xmlWriter.CreateNode(stringNode))
                                {
                                    xmlWriter.WriteString(par.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    internal static class XmlWriterExtension
    {
        internal static NodeWriter CreateNode(this XmlWriter xmlWriter, String nodeName)
        {
            return new NodeWriter(xmlWriter, nodeName);
        }
    }

    internal class NodeWriter : IDisposable
    {
        XmlWriter xmlWriter;

        internal NodeWriter(XmlWriter xmlWriter, string nodeName)
        {
            this.xmlWriter = xmlWriter;
            xmlWriter.WriteStartElement(nodeName);
        }

        public void Dispose()
        {
            xmlWriter.WriteEndElement();
        }
    }

}
