using Cinemapart.UI.DataProviders;
using Cinemapart.UI.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Cinemapart.UI
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            LoadMultiplexes();
            //LoadMovies();
            //LoadSchedules();
        }

        private async void LoadMultiplexes()
        {
            var data = new MultiplexDataProvider();
            var items = await data.LoadMultiplexes();

            Multiplexes.Items.Clear();
            foreach (var item in items)
            {
                ListBoxItem b = new ListBoxItem();
                b.Content = item.City + " - " + item.Title;
                Multiplexes.Items.Add(b);
            }
        }

        private async void LoadMovies()
        {
            var data = new MultiplexDataProvider();
            var items = await data.LoadMovies();

            Multiplexes.Items.Clear();
            foreach (var item in items)
            {
                ListBoxItem b = new ListBoxItem();
                var sp = new StackPanel();
                sp.Orientation = System.Windows.Controls.Orientation.Horizontal;
                var img = new Image();
                BitmapImage bi = new BitmapImage(new Uri(string.Format(MultiplexDataProvider.PosterUri, item.Id.ToString().Substring(0, 4))));
                img.Source = bi;
                sp.Children.Add(img);
                var tx = new TextBlock();
                tx.Text = item.Title;
                sp.Children.Add(tx);
                b.Content = sp;
                Multiplexes.Items.Add(b);
            }
        }

        private async void LoadSchedules()
        {
            var data = new MultiplexDataProvider();
            var items = await data.LoadSchedules();

            Multiplexes.Items.Clear();
            foreach (var item in items)
            {
                ListBoxItem b = new ListBoxItem();
                b.Content = string.Format("{0} - Hall {1} - Price {2} - Time {3}", item.Id, item.Hall, item.Price, item.Time.ToLongDateString());
                Multiplexes.Items.Add(b);
            }
        }


        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsVisible = true;

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarButton.Click += AppBarButton_Click;
            appBarButton.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            appBarMenuItem.Click += AppBarMenuItem_Click;
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        private void AppBarMenuItem_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void AppBarButton_Click(object sender, EventArgs e)
        {
            Load();
        }
    }
}
