using G10Travel.DataModel;
using G10Travel.Requests;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
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
    public sealed partial class NewListPage : Page
    {
        private IMobileServiceTable<ListItem> listItemTable = App.MobileService.GetTable<ListItem>();
        private IMobileServiceTable<Item> ItemTable = App.MobileService.GetTable<Item>();
        private IMobileServiceTable<Category> CategoryTable = App.MobileService.GetTable<Category>();
        public NewListPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //myItemsList = new ObservableCollection<String>();
        }

        private async Task addList(string name, string location, string startdate, string enddate, List<Item> itemstobring)
        {
            ListItem listItem = new ListItem { Name = name, Location = location, startDate = startdate, endDate = enddate };
            //try
            //{
            await listItemTable.InsertAsync(listItem);

            for(int i = 0; i < itemstobring.Count; i++)
            {
                //Item item = new Item { ItemName = itemstobring.ElementAt(i).ItemName, ListItemId = listItem.Id, Type = itemstobring.ElementAt(i).Type};
                Item item = itemstobring.ElementAt(i);
                await ItemTable.InsertAsync(item);
            }
            //}
            //catch (Exception ex)
            //{
            //    await new MessageDialog(ex.Message, "Error adding items").ShowAsync();
            //}
        }

        private async void btnAddList_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentUser = App.MobileService.CurrentUser;
                //List<string> newItems = lvItemList.Items.Cast<string>().ToList();
                //List<Item> itemList = new List<Item>();
                List<Item> itemList = lvItemList.Items.Cast<Item>().ToList();

                /*for (int i = 0; i < newItems.Count; i++)
                {
                    Item item = new Item { ItemName = newItems[i] };
                    itemList.Add(item);
                }*/



                try
                {
                    await addList(this.tfName.Text, this.tfLocation.Text, this.tfStartDate.Date.ToString("dd/MM/yyyy"), this.tfEndDate.Date.ToString("dd/MM/yyyy"), itemList);


                    Frame.Navigate(typeof(HomePage));
                    ContentDialog cd = new ContentDialog()
                    {
                        Title = "List added",
                        Content = this.tfName.Text + " list has been added",
                        PrimaryButtonText = "OK"

                    };
                    await cd.ShowAsync();
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message, "Error adding items").ShowAsync();
                }
            }
            catch (ArgumentException ex)
            {
                tbError.Text = "Geef een geldige waarde in";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }else
            {
                Frame.Navigate(typeof(HomePage));
            }
        }

        private async void btnAddListItem_Click(object sender, RoutedEventArgs e)
        {
            List<Category> Categories = await CategoryTable.ToListAsync();
            Categories.Insert(0, new Category { Name = "Add new Category"});
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
                Item item = new Item { ItemName = name, CategoryId = cat.Id, Type = type, Amount=amount};
                lvItemList.Items.Add(item);
            }
        }
        
        

    }
}
