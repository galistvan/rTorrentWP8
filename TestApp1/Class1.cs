using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using RTorrentLib;
using RTorrentLib.XmlRpc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLibTest
{
    [TestClass]
    public class Class1
    {

        //[TestMethod]
        //public void test1()
        //{
        //    Stream stream = new MemoryStream();
        //    XmlRpcWriter writer = new XmlRpcWriter(stream);

        //    XmlRpcRequest request = new XmlRpcRequest("d.stop");
        //    request.AddParameter("5E69096FCE718D4550B12D9BA90D02CC8551948E");

        //    writer.WriteRequest(request);

        //    StreamReader sr = new StreamReader(stream);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    String actual = sr.ReadToEnd();

        //    Debug.WriteLine(actual);
        //    Assert.AreEqual("", actual);
        //}

        [TestMethod]
        public void test12()
        {
            RTorrentXmlRpc a = new RTorrentXmlRpc();
            var s   = a.StartedTorrents();
            
        a.StopTorrent(s[0][1]);
        }
    }
}
