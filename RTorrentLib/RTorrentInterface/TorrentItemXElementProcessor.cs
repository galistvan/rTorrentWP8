using RTorrentLib.RTorrentInterface.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RTorrentLib.XmlRpc
{
    public class TorrentItemXElementProcessor
    {

        public List<TorrentItem> Process(XElement xElement)
        {
            var temp = xElement; //METHODRESPONSE
            temp = temp.Element(XmlRpcTags.PARAMS);
            temp = temp.Element(XmlRpcTags.PARAM);
            temp = temp.Element(XmlRpcTags.VALUE);
            temp = temp.Element(XmlRpcTags.ARRAY);
            temp = temp.Element(XmlRpcTags.DATA);

            var values = temp.Elements(XmlRpcTags.VALUE);
            List<TorrentItem> ret = new List<TorrentItem>();
            foreach (XElement valueElement in values){
                ret.Add(TorrentItemProcess(valueElement));
            }
            return ret;
        }

        public TorrentItem TorrentItemProcess(XElement xElement)
        {
            var temp = xElement; //VALUE
            temp = temp.Element(XmlRpcTags.ARRAY);
            temp = temp.Element(XmlRpcTags.DATA);

            var values = temp.Elements(XmlRpcTags.VALUE).ToArray();
            TorrentItem torrent = new TorrentItem();
            torrent.Hash = values[0].Element("string").Value;
            torrent.TorrentName = values[1].Element("string").Value;

            return torrent;
        }

        public List<XElement> GetValueList(XElement xElement)
        {
            var temp = xElement; //METHODRESPONSE
            temp = temp.Element(XmlRpcTags.PARAMS);
            temp = temp.Element(XmlRpcTags.PARAM);
            temp = temp.Element(XmlRpcTags.VALUE);
            temp = temp.Element(XmlRpcTags.ARRAY);
            temp = temp.Element(XmlRpcTags.DATA);

            var values = temp.Elements(XmlRpcTags.VALUE).ToList();
            return values;
        }

        public List<XElement> ProcessValueList(XElement xElement)
        {
            var temp = xElement; //VALUE
            temp = temp.Element(XmlRpcTags.ARRAY);
            temp = temp.Element(XmlRpcTags.DATA);

            var values = temp.Elements(XmlRpcTags.VALUE).ToList();
            return values;
        }
    }
}
