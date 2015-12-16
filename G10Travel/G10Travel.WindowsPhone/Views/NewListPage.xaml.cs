﻿using G10Travel.DataModel;
using G10Travel.Requests;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        private IMobileServiceTable<ListItem> listItemTable = App.MobileService.GetTable<ListItem>();
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
        }

        private async Task addList(string name, string location, string startdate, string enddate, List<string> itemstobring)
        {
            string message;
            var listItem = new ListItem();
            listItem.Name = name;
            listItem.Location = location;
            listItem.startDate = DateTime.ParseExact(startdate, "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture); 
            listItem.endDate = DateTime.ParseExact(enddate, "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            listItem.itemsToBring = itemstobring;
            try
            {
                await listItemTable.InsertAsync(listItem);
            } catch(Exception ex)
            {
                message = ex.Message;
            }
            
        }


        private void btnAddListItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnAddList_Click_1(object sender, RoutedEventArgs e)
        {
            var currentUser = App.MobileService.CurrentUser;
            List<string> itemstobring = new List<string>();
            await addList(this.tfName.Text, this.tfLocation.Text, this.tfStartDate.Text, this.tfEndDate.Text, itemstobring);
        }
    }
}
