using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib
{
    public class TorrentItem
    {
        public string Hash { get;  set; }
        public string TorrentName
        {
            get;
            set;
        }

        
        public override string ToString()
        {
            return TorrentName;
        }
    }
}
