using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RTorrentLib
{
    public static class XmlRpcSerializer
    {

        #region request
        private const string methodCallNode =  "methodCall";
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


        public static void WriteXmlRpcRequest(XmlWriter xmlWriter, string methodName, params object[] parameters)
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

        public static object ReadFromXmlReader(XmlReader xmlReader)
        {
            var xElement = XElement.Load(xmlReader);
            var valueElements = xElement.Element("params").Element("param").Element("value").Element("array").Element("data").Elements("value");

            IEnumerable<String> lista = valueElements.Select(x => x.Element("string").Value).ToList();
            //dynamic root = new ExpandoObject();
            //XmlToDynamic.Parse(root, xElement);

            //StringBuilder sb = new StringBuilder();
            //            //root.xmethodResponse.xfault.xvalue.xstruct.xmember

            ////var name = xElement.Descendants(XName.Get("Name", @"http://demo.com/2011/demo-schema")).First().Value;


            //var array = root.xmethodResponse.xparams.xparam.xvalue.xarray.xdata.xvalue;
            //List<string> lista = new List<string>();
            //foreach (var item in array)
            //{
            //    lista.Add((string)item.xstring);
            //}

            return lista;
        }

    }
}
