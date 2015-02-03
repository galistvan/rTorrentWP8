using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Devices;
using RtorrentClientWP8.Model;

namespace RtorrentClientWP8.Util
{
    public class TorrentItemHashEqualityComparer : IEqualityComparer<TorrentItem>
    {
        public bool Equals(TorrentItem first, TorrentItem second)
        {
            if (first == second) { return true; }
            if (first.Hash.Equals(second.Hash))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(TorrentItem obj)
        {
            return 3 * obj.Hash.GetHashCode();
        }
    }
}
