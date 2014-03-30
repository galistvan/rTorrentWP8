using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using RTorrentLib;
using RTorrentLib.RtorrentInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RTorrentLibTest
{
    [TestClass]
    public class XElementProcessorTest
    {
        private XElement GetResponseXElement()
        {
            MemoryStream memStream = new MemoryStream();
            StreamWriter sw = new StreamWriter(memStream);

            sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sw.Write("<methodResponse>");
            sw.Write("<params>");
            sw.Write("<param><value><array><data>");
            sw.Write("<value><array><data>");
            sw.Write("<value><string>62E18A8AA7ECD642FDEA0D23CFA5B24E39300E69</string></value>");
            sw.Write("<value><string>asdf</string></value>");
            sw.Write("<value><i8>1</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><string>Tracker: [Failure reason \"unregistered torrent\"]</string></value>");
            sw.Write("<value><i8>2</i8></value>");
            sw.Write("<value><i8>1656832</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><string></string></value>");
            sw.Write("</data></array></value>");
            sw.Write("<value><array><data>");
            sw.Write("<value><string>7BD6AB46D04766866A6A7FB1CD9E79BD887C33AF</string></value>");
            sw.Write("<value><string>fdsa</string></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><string></string></value>");
            sw.Write("<value><i8>2</i8></value>");
            sw.Write("<value><i8>18207842691</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><string></string></value>");
            sw.Write("</data></array></value>");
            sw.Write("</data></array></value></param>");
            sw.Write("</params>");
            sw.Write("</methodResponse>");
            sw.Flush();
            memStream.Seek(0, SeekOrigin.Begin);
            XElement xelement = XElement.Load(memStream);
            return xelement;
        }

        private XElement GetTorrentXElement()
        {
            MemoryStream memStream = new MemoryStream();
            StreamWriter sw = new StreamWriter(memStream);
            sw.Write("<value><array><data>");
            sw.Write("<value><string>7BD6AB46D04766866A6A7FB1CD9E79BD887C33AF</string></value>");
            sw.Write("<value><string>fdsa</string></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><string></string></value>");
            sw.Write("<value><i8>2</i8></value>");
            sw.Write("<value><i8>18207842691</i8></value>");
            sw.Write("<value><i8>0</i8></value>");
            sw.Write("<value><string></string></value>");
            sw.Write("</data></array></value>");
            sw.Flush();
            memStream.Seek(0, SeekOrigin.Begin);
            XElement xelement = XElement.Load(memStream);
            return xelement;
        }

        [TestMethod]
        public void XElementProcessTest1()
        {
            XElement xelementResponse = GetResponseXElement();
            XElementProcessor processor = new XElementProcessor();
            var actual = processor.Process(xelementResponse);

            TorrentItem expected1 = new TorrentItem();
            expected1.Hash = "62E18A8AA7ECD642FDEA0D23CFA5B24E39300E69";
            expected1.TorrentName = "asdf";
            
            TorrentItem expected2 = new TorrentItem();
            expected2.Hash = "7BD6AB46D04766866A6A7FB1CD9E79BD887C33AF";
            expected2.TorrentName = "fdsa";

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(expected1.Hash, actual[0].Hash);
            Assert.AreEqual(expected1.TorrentName, actual[0].TorrentName);

            Assert.AreEqual(expected2.Hash, actual[1].Hash);
            Assert.AreEqual(expected2.TorrentName, actual[1].TorrentName);
        }

        [TestMethod]
        public void XElementProcessorTorrentItemProcessTest()
        {
            XElement xelementTorrentItem = GetTorrentXElement();
            XElementProcessor processor = new XElementProcessor();
            TorrentItem actual = processor.TorrentItemProcess(xelementTorrentItem);
            TorrentItem expected = new TorrentItem();
            expected.Hash = "7BD6AB46D04766866A6A7FB1CD9E79BD887C33AF";
            expected.TorrentName = "fdsa";

            Assert.AreEqual(expected.Hash, actual.Hash);
            Assert.AreEqual(expected.TorrentName, actual.TorrentName);
        }

    }
}
