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

        private async Task addList(string name, string location, string startdate, string enddate, ItemCollection items)
        {
            string message;
            ListItem listItem = new ListItem { Name = name, Location = location, startDate = startdate, endDate = enddate, itemsToBring = itemstobring, Id = "test"};
            try
            {
                await listItemTable.InsertAsync(listItem);
                Frame.Navigate(typeof(HomePage));
            } catch(Exception ex)
            {
                String name = d.GetItemName();
                lvItemList.Items.Add(name);
            }
        }

        private async void btnAddList_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentUser = App.MobileService.CurrentUser;
                await addList(this.tfName.Text, this.tfLocation.Text, this.tfStartDate.Text, this.tfEndDate.Text, lvItemList.Items);

                Frame.Navigate(typeof(HomePage));
                ContentDialog cd = new ContentDialog()
        {
                    Title = "List added",
                    Content = this.tfName.Text + " list has been added",
                    PrimaryButtonText = "OK"

                };
                await cd.ShowAsync();

        }
            catch (ArgumentException ex)
            {
                tbError.Text = "Geef een geldige waarde in";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));
        }
    }
}
