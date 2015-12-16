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
        
        private async Task addList(string name, string location, string startdate, string enddate)
        {
            var newList = new JObject();
            newList.Add("name", name);
            newList.Add("location", location);
            newList.Add("startdate", startdate);
            newList.Add("enddate", enddate);
            newList.Add("itemstobring", "");

            JToken jToken = JToken.FromObject(newList);

            await App.MobileService.InvokeApiAsync("Tables", jToken, HttpMethod.Post, new Dictionary<string, string>() { { "name", "name" } });
        }


        private void btnAddListItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void btnAddList_Click_1(object sender, RoutedEventArgs e)
        {
            var currentUser = App.MobileService.CurrentUser;
            await addList(this.tfName.Text, this.tfLocation.Text, this.tfStartDate.Text, this.tfEndDate.Text);
        }
    }
}
