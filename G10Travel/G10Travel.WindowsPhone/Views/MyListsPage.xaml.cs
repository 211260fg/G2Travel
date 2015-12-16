using G10Travel.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace G10Travel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyListsPage : Page
    {
        private MobileServiceCollection<ListItem, ListItem> items;
        private IMobileServiceTable<ListItem> listItemTable = App.MobileService.GetTable<ListItem>();
        public MyListsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await refreshListItems();
        }

        private async Task refreshListItems()
        {
            try
            {
                List<ListItem> items = await listItemTable
                        .ToListAsync();
                lvMyLists.ItemsSource = items;
            } catch(Exception ex)
            {
                await new MessageDialog(ex.Message, "Error loading items").ShowAsync();
            }
        }

        private void lvMyLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (ListView)sender;
            
            ListItem selectedItem = lv.SelectedItem as ListItem;
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(ListDetailPage), selectedItem);
        }
    }
}
