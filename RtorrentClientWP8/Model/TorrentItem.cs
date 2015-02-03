using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtorrentClientWP8.Model
{
    public class TorrentItem : INotifyPropertyChanged
    {
        // http://msdn.microsoft.com/en-us/library/windows/apps/gg521153%28v=vs.105%29.aspx
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string hash;
        public string Hash
        {
            get { return hash; }
            set
            {
                hash = value;
                RaisePropertyChanged("Hash");
            }
        }

        private string torrentName;
        public string TorrentName
        {
            get { return torrentName; }
            set
            {
                torrentName = value;
                RaisePropertyChanged("TorrentName");
            }
        }

        private bool started;
        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                RaisePropertyChanged("Started");
            }
        }

        private long completedBytes;
        public long CompletedBytes
        {
            get { return completedBytes; }
            set
            {
                completedBytes = value;
                RaisePropertyChanged("CompletedBytes");
            }
        }

        private long upTotal;
        public long UpTotal
        {
            get { return upTotal; }
            set
            {
                upTotal = value;
                RaisePropertyChanged("UpTotal");
            }
        }

        private long peersComplete;
        public long PeersComplete
        {
            get { return peersComplete; }
            set
            {
                peersComplete = value;
                RaisePropertyChanged("PeersComplete");
            }
        }

        private long peersAccounted;
        public long PeersAccounted
        {
            get { return peersAccounted; }
            set
            {
                peersAccounted = value;
                RaisePropertyChanged("PeersAccounted");
            }
        }

        private long downRate;
        public long DownRate
        {
            get { return downRate; }
            set
            {
                downRate = value;
                RaisePropertyChanged("DownRate");
            }
        }

        private long upRate;
        public long UpRate
        {
            get { return upRate; }
            set
            {
                upRate = value;
                RaisePropertyChanged("UpRate");
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }

        private long priority;
        public long Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                RaisePropertyChanged("Priority");
            }
        }

        private long sizeBytes;
        public long SizeBytes
        {
            get { return sizeBytes; }
            set
            {
                sizeBytes = value;
                RaisePropertyChanged("SizeBytes");
            }
        }

        private bool hashChecking;
        public bool HashChecking
        {
            get { return hashChecking; }
            set
            {
                hashChecking = value;
                RaisePropertyChanged("HashChecking");
            }
        }

        private string label;
        public string Label
        {
            get { return label; }
            set
            {
                label = value;
                RaisePropertyChanged("Label");
            }
        }


        internal void Refresh(TorrentItem other)
        {
            Started = other.Started;
            CompletedBytes = other.CompletedBytes;
            UpTotal = other.UpTotal;
            PeersComplete = other.PeersComplete;
            PeersAccounted = other.PeersAccounted;
            DownRate = other.DownRate;
            UpRate = other.UpRate;
            Message = other.Message;
            Priority = other.Priority;
            SizeBytes = other.SizeBytes;
            HashChecking = other.HashChecking;
            Label = other.Label;
        }
    }
}
