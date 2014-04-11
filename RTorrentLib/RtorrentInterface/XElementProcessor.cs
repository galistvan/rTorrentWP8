using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RTorrentLib.RtorrentInterface
{
    public class XElementProcessor
    {
        //const string METHODRESPONSE = "methodResponse";
        const string PARAMS = "params";
        const string PARAM = "param";
        const string VALUE = "value";
        const string ARRAY = "array";
        const string DATA = "data";

        public List<TorrentItem> Process(XElement xElement)
        {
            var temp = xElement; //METHODRESPONSE
            temp = temp.Element(PARAMS);
            temp = temp.Element(PARAM);
            temp = temp.Element(VALUE);
            temp = temp.Element(ARRAY);
            temp = temp.Element(DATA);

            var values = temp.Elements(VALUE);
            List<TorrentItem> ret = new List<TorrentItem>();
            foreach (XElement valueElement in values){
                ret.Add(TorrentItemProcess(valueElement));
            }
            return ret;
        }

        public TorrentItem TorrentItemProcess(XElement xElement)
        {
            var temp = xElement; //VALUE
            temp = temp.Element(ARRAY);
            temp = temp.Element(DATA);

            var values = temp.Elements(VALUE).ToArray();
            TorrentItem torrent = new TorrentItem();
            torrent.Hash = values[0].Element("string").Value;
            torrent.TorrentName = values[1].Element("string").Value;

            return torrent;
        }

        public List<XElement> GetValueList(XElement xElement)
        {
            var temp = xElement; //METHODRESPONSE
            temp = temp.Element(PARAMS);
            temp = temp.Element(PARAM);
            temp = temp.Element(VALUE);
            temp = temp.Element(ARRAY);
            temp = temp.Element(DATA);

            var values = temp.Elements(VALUE).ToList();
            return values;
        }

        public List<XElement> ProcessValueList(XElement xElement)
        {
            var temp = xElement; //VALUE
            temp = temp.Element(ARRAY);
            temp = temp.Element(DATA);

            var values = temp.Elements(VALUE).ToList();
            return values;
        }
    }
}
