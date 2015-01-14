using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RtorrentClientWP8.Logger;
using RtorrentClientWP8.Resources;
using RTorrentLib.XmlRpc;
using RTorrentLib;
using System.Windows.Threading;
using System.Threading.Tasks;
using RTorrentLib.RTorrentInterface.Item;
using RTorrentLib.RTorrentInterface;

namespace RtorrentClientWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly IRTorrent _torrent;

        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            MetroLogger logger = new Logger.MetroLogger();
            _torrent = new RTorrentXmlRpc("http://raspberrypi.lan/scgi");
            Update();
        }

        private async void Update()
        {
            while (true)
            {
                await Task.Delay(1000);
                Load();
            }
        }
        //TODO give back the list to the lib, and do that the refresh.
        // The list should be binded to the xaml app
        private async void Load()
        {
            await Task.Run(() =>
            {
                var ret = _torrent.StartedTorrents();
                ret.Wait();
                Dispatcher.BeginInvoke(() => RefreshTorrentList(ret.Result));
            });
        }

        private void RefreshTorrentList(IEnumerable<TorrentItem> torrentsList )
        {
            if (torrentList.ItemsSource == null)
            {
                torrentList.ItemsSource = new ObservableCollection<TorrentItem>(torrentsList);
            }
            else
            {
                var items = (ObservableCollection<TorrentItem>) torrentList.ItemsSource;
                var comparer = new TorrentItemHashEqualityComparer();
                foreach (var torrentItem in items)
                {

                    if (items.Contains(torrentItem, comparer))
                    {
                        TorrentItem single = items.Single(item => comparer.Equals(item, torrentItem));
                        single.Refresh(torrentItem);
                    }
                    else
                    {
                        items.Add(torrentItem);
                    }
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            var item = torrentList.ItemContainerGenerator.ContainerFromItem(((MenuItem)sender).DataContext) as ListBoxItem;
            TorrentItem torrent = (TorrentItem)item.Content;
            _torrent.StartTorrent(torrent);
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            var item = torrentList.ItemContainerGenerator.ContainerFromItem(((MenuItem)sender).DataContext) as ListBoxItem;
            TorrentItem torrent = (TorrentItem)item.Content;
            _torrent.StopTorrent(torrent);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}