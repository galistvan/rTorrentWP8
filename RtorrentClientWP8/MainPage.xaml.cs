using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RtorrentClientWP8.Resources;
using RTorrentLib.RtorrentInterface;
using RTorrentLib;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace RtorrentClientWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

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
        private async void Load()
        {
            await Task.Run(() =>
            {
                RTorrent torrent = new RTorrent("http://raspberrypi.lan/rpc");
                var ret = torrent.TorrentList();
                Dispatcher.BeginInvoke(() =>
                {
                    torrentList.ItemsSource = ret;
                });
            });
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            var item = torrentList.ItemContainerGenerator.ContainerFromItem(((MenuItem)sender).DataContext) as ListBoxItem;
            TorrentItem torrent = (TorrentItem)item.Content;

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