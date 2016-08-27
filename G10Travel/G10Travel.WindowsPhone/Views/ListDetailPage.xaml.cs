using G10Travel.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Collections;
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
        private IMobileServiceTable<Category> CategoryTable = App.MobileService.GetTable<Category>();
        private string ListId;


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
            ListId = listItem.Id;
            //ListName.Text = listItem.Name;
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
            List<Item> items = await App.MobileService.InvokeApiAsync<List<Item>>("Item/GetItems", HttpMethod.Get, new Dictionary<string, string>() { { "listId", id } });

            // de volgende 2 lijnen in commentaar zetten en hetgene in commentaar zet uit commentaar halen om die groepen te testen.
            items = items.OrderBy(item => item.Type).ToList();
            lvMyLists.ItemsSource = items;

            /*
            IEnumerable<ItemGroup> groups =
                from item in items
                group item by item.Type into itemGroup
                select new ItemGroup(itemGroup)
                {
                    Type = itemGroup.Key
                };
            var cvs = CollectionViewSource;
            cvs.Source = groups.ToList();
         */
        }
        private class ItemGroup : ObservableCollection<Item>
        {
            public ItemGroup(IEnumerable<Item> items) : base(items) { }
            public string Type { get; set; }
        }
        private async void btnAddListItem_Click(object sender, RoutedEventArgs e)
        {
            List<Category> Categories = await CategoryTable.ToListAsync();
            Categories.Insert(0, new Category { Name = "Add new Category" });
            AddItemDialog d = new AddItemDialog(Categories);

            await d.ShowAsync();

            if (d.IsResultOK())
            {
                String name = d.GetItemName();
                Category cat = d.GetCategory();
                String type = d.GetItemType();

                Item item;
                if (Categories.Contains(cat))
                {
                    item = new Item { ItemName = name, ListItemId = ListId, ItemChecked = false, CategoryId = cat.Id, Type = type };
                }
                else
                {
                    await CategoryTable.InsertAsync(cat);
                    item = new Item { ItemName = name, ListItemId = ListId, ItemChecked = false, CategoryId = cat.Id, Type = type };

                }
                await listItemTable.InsertAsync(item);
                await getItemsForList(ListId);
            }
        }
        private async void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            Item item = (Item)((CheckBox)sender).DataContext;
            await listItemTable.UpdateAsync(item);
        }
    }
}
