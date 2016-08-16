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
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

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
            //HardwareButtons.BackPressed += HardwareButtons_BackPressed;
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
                /*IEnumerable<string> test = await listItemTable.Where(list => list.Name == "test").Select(list => list.Name).ToEnumerableAsync();
                Debug.WriteLine(test);*/

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

        /*void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }*/

    }
}
