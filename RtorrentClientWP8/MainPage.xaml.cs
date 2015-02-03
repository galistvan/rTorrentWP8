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
using RtorrentClientWP8.ViewModel;

namespace RtorrentClientWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        private StartedTorrents st;

        public MainPage()
        {
            InitializeComponent();
            this.st = new StartedTorrents();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            st.RefreshList();

            startedTorrents.DataContext = st;
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