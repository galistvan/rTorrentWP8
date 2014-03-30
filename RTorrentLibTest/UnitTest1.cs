using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Text;
using System.Xml;
using RTorrentLib;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace RTorrentLibTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MethodNameTest1()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(sb);

            XmlRpcSerializer.WriteXmlRpcRequest(xmlWriter, "system.listMethods");

            string actual = sb.ToString();
            string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><methodCall><methodName>system.listMethods</methodName></methodCall>";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DownloadListTest1()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(sb);

            XmlRpcSerializer.WriteXmlRpcRequest(xmlWriter, "download_list", "started");

            string actual = sb.ToString();
            string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><methodCall><methodName>download_list</methodName><params><param><value><string>started</string></value></param></params></methodCall>";

            //<?xml version="1.0" encoding="utf-16"?><methodCall><methodName>download_list</methodName><params><param><value><string>started</string></value></param></params></methodCall>
            //<?xml version="1.0" encoding="utf-16"?><methodCall><methodName>download_list</methodName><params><param><value><base64>c3RhcnRlZA==</base64></value></param></params></methodCall>


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ResponseTest()
        {
            StringReader response = new StringReader(
                String.Concat("<?xml version=\"1.0\" encoding=\"UTF-8\"?>",
                    "<methodResponse>",
                    "<params>",
                    "<param><value><array><data>",
                    "<value><string>system.listMethods</string></value>",
                    "<value><string>system.methodExist</string></value>",
                    "<value><string>system.methodHelp</string></value>",
                    "</data></array></value></param>",
                    "</params>",
                    "</methodResponse>"));

            XmlReader xmlReader = XmlReader.Create(response);

            var actual = (ICollection)XmlRpcSerializer.ReadFromXmlReader(xmlReader);
            List<string> expected = new List<string>();
            expected.Add("system.listMethods");
            expected.Add("system.methodExist");
            expected.Add("system.methodHelp");

            CollectionAssert.AreEqual(expected, actual);
        }

        ////download list response parse test
        //[TestMethod]
        //public void DownloadListTest2()
        //{
        //    string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><methodCall><methodName>download_list</methodName><params><param><value><string>started</string></value></param></params></methodCall>";

        //    Assert.AreEqual(expected, actual);
        //}

    }
}
