using RtorrentClientWP8.Model;
using RtorrentClientWP8.Util;
using RTorrentLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace RtorrentClientWP8.ViewModel
{
    public class StartedTorrents
    {
        public ObservableCollection<TorrentItem> TorrentItems { get; set; }

        //public ICommand RefreshCommand { set; get; }

        public StartedTorrents()
        {
            TorrentItems = new ObservableCollection<TorrentItem>();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();


            //RefreshCommand = new CommandHandler(() => RefreshList(), true);
        }
        void timer_Tick(object sender, EventArgs e)
        {
            RefreshList();
        }

        public async void RefreshList()
        {
            RTorrentXmlRpc torrent = new RTorrentXmlRpc("http://raspberrypi.lan/scgi");
            var torrents = await torrent.StartedTorrents();
            var comparer = new TorrentItemHashEqualityComparer();
            foreach (var item in torrents)
            {
                var torrentItem = ConvertTorrentItem(item);

                if (TorrentItems.Contains(torrentItem, comparer))
                {
                    TorrentItem single = TorrentItems.Single(x => comparer.Equals(x, torrentItem));
                    single.Refresh(torrentItem);
                }
                else
                {
                    TorrentItems.Add(torrentItem);
                }
            }
            var contained = TorrentItems.Where(x => torrents.Select(y => y.Hash).Contains(x.Hash)).ToList();
            for (int i = TorrentItems.Count - 1; i >= 0; i--)
            {
                if (!contained.Contains(TorrentItems[i], comparer))
                {
                    TorrentItems.Remove(TorrentItems[i]);
                }
            }
        }

        private TorrentItem ConvertTorrentItem(RTorrentLib.RTorrentInterface.Item.TorrentItem torrentItem)
        {
            TorrentItem ret = new TorrentItem();

            ret.Hash = torrentItem.Hash;
            ret.TorrentName = torrentItem.TorrentName;
            ret.Started = torrentItem.Started;
            ret.CompletedBytes = torrentItem.CompletedBytes;
            ret.UpTotal = torrentItem.UpTotal;
            ret.PeersComplete = torrentItem.PeersComplete;
            ret.PeersAccounted = torrentItem.PeersAccounted;
            ret.DownRate = torrentItem.DownRate;
            ret.UpRate = torrentItem.UpRate;
            ret.Message = torrentItem.Message;
            ret.Priority = torrentItem.Priority;
            ret.SizeBytes = torrentItem.SizeBytes;
            ret.HashChecking = torrentItem.HashChecking;
            ret.Label = torrentItem.Label;

            return ret;
        }
    }
}
