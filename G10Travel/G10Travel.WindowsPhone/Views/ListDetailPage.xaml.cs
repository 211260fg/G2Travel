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
using Windows.ApplicationModel.DataTransfer;
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
        private string ListId;
        private ListItem listItem;
        private IMobileServiceTable<Category> CategoryTable = App.MobileService.GetTable<Category>();

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
            DataTransferManager dtManager = DataTransferManager.GetForCurrentView();
            dtManager.DataRequested += dtManager_DataRequested;

            listItem = (ListItem)(e.Parameter);
            this.DataContext = listItem;
            ListId = listItem.Id;
            //ListName.Text = listItem.Name;
            pivotList.Title = listItem.Name;
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
            IEnumerable<Item> allItems = await App.MobileService.InvokeApiAsync<List<Item>>("Item/GetItems", HttpMethod.Get, new Dictionary<string, string>() { { "listId", id } });
            List<Item> itemsToBring = new List<Item>();
            List<Item> tasks = new List<Item>(); ;
            foreach (Item item in allItems)
            {

                //System.Diagnostics.Debug.WriteLine("type is null");

                if (item.Type!=null&& item.Type.ToLower().Equals("task"))
                {
                    tasks.Add(item);
                }
                else
                {
                    itemsToBring.Add(item);
                }
            }


            //IEnumerable<Item> itemsToBring = allItems.Where(item => item.Type.ToLower().Equals("item"));
            //IEnumerable<Item> tasks = allItems.Where(item => item.Type.ToLower().Equals("task"));

            lvMyLists.ItemsSource = itemsToBring;
            lvMyTasks.ItemsSource = tasks;

        }
        private async void btnAddListItem_Click(object sender, RoutedEventArgs e)
        {
            //TODO juiste lijst gebruiken
            /*System.Collections.IEnumerable Categories = new List<string>(){
            "New Category", "Clothing", "Bathroom", "Utensils", "Camping", "Financial" };*/
            /*List<Category> Categories = await CategoryTable.ToListAsync();
            Categories.Insert(0, new Category { Name = "Add new Category" });

            AddItemDialog d = new AddItemDialog(Categories);

            await d.ShowAsync();

            if (d.IsResultOK())
            {
                String name = d.GetItemName();
                Item item = new Item { ItemName = name, ListItemId = ListId, ItemChecked = false };
                await listItemTable.InsertAsync(item);
                await getItemsForList(ListId);
            }*/

            List<Category> Categories = await CategoryTable.ToListAsync();
            Categories.Insert(0, new Category { Name = "Add new Category" });

            AddItemDialog d = new AddItemDialog(Categories);

            await d.ShowAsync();

            if (d.IsResultOK())
            {
                String name = d.GetItemName();
                Category cat = d.GetCategory();
                String type = d.GetItemType();
                int amount = d.GetAmount();
                if (!Categories.Contains(cat))
                {
                    await CategoryTable.InsertAsync(cat);
                }
                Item item = new Item { ItemName = name, ListItemId = ListId, ItemChecked = false, CategoryId = cat.Id, Type = type, Amount = amount };
                await listItemTable.InsertAsync(item);
                await getItemsForList(ListId);
            }
        }

        private void btnShare_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void dtManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            e.Request.Data.Properties.Title = "My tracking list!";
            e.Request.Data.SetText("Already started packing for our trip to " + listItem.Location + " on " + listItem.startDate + "!");
            //e.Request.Data.Properties.Description = "Already started packing for our trip to " + listItem.Location + "on " + listItem.startDate;
            //e.Request.Data.SetWebLink(new Uri("https://g9ts.azurewebsites.net/"));
        }
    }
}
