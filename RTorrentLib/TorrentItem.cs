using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib
{
    public class TorrentItem
    {
        public string Hash { get; set; }
        public string TorrentName { get; set; }


        public override string ToString()
        {
            return TorrentName;
        }

        public bool Started { get; set; }

        public long CompletedBytes { get; set; }

        public long UpTotal { get; set; }

        public long PeersComplete { get; set; }

        public long PeersAccounted { get; set; }

        public long DownRate { get; set; }

        public long UpRate { get; set; }

        public string Message { get; set; }

        public long Priority { get; set; }

        public long SizeBytes { get; set; }

        public bool HashChecking { get; set; }

        public string Label { get; set; }
    }
}
