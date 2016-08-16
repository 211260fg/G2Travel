using G10Travel.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ListDetailPage : Page
    {
        private IMobileServiceTable<Item> listItemTable = App.MobileService.GetTable<Item>();
        public ListDetailPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            ListItem listItem = (ListItem)(e.Parameter);
            this.DataContext = listItem;

            try
            {
                await getItemsForList(listItem.Id);
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
            }
            base.OnNavigatedTo(e);
        }

        private async Task getItemsForList(string id)
        {
            lvMyLists.ItemsSource = await App.MobileService.InvokeApiAsync<List<Item>>("Item/GetItems", HttpMethod.Get, new Dictionary<string, string>() { { "listId", id } });
        }
    }
}
