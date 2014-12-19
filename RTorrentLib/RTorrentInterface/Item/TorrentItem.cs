namespace RTorrentLib.RTorrentInterface.Item
{
    public class TorrentItem
    {
        public string Hash { get; set; }
        public string TorrentName { get; set; }


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

        public override string ToString()
        {
            return TorrentName;
        }
        public void Refresh(TorrentItem other)
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