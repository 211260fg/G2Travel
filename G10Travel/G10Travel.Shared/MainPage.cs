using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using G10Travel.Views;
using Windows.Security.Authentication.Web;
using Facebook;
using System.Diagnostics;

// To add offline sync support, add the NuGet package Microsoft.WindowsAzure.MobileServices.SQLiteStore
// to your project. Then, uncomment the lines marked // offline sync
// For more information, see: http://aka.ms/addofflinesync
//using Microsoft.WindowsAzure.MobileServices.SQLiteStore;  // offline sync
//using Microsoft.WindowsAzure.MobileServices.Sync;         // offline sync

namespace G10Travel
{
    sealed partial class MainPage: Page
    {
        //private IMobileServiceSyncTable<TodoItem> todoTable = App.MobileService.GetSyncTable<TodoItem>(); // offline sync

        // Define a member variable for storing the signed-in user. 
        private MobileServiceUser user;
        

        // Define a method that performs the authentication process
        // using a Facebook sign-in. 
        private async Task<MobileServiceUser> AuthenticateAsync(String username, String password)
        {
            var user = await App.MobileService
       .InvokeApiAsync<LoginRequest, MobileServiceUser>(
       "CustomLogin", new LoginRequest()
       {
           username = username,
           password = password
       });
            this.user = user;
            return user;

        }

        private async Task<MobileServiceUser> RegisterAsync(String username, String password)
        {
            var user = await App.MobileService
       .InvokeApiAsync<RegistrationRequest, MobileServiceUser>(
       "CustomRegistration", new RegistrationRequest()
       {
           username = username,
           password = password
       });
            this.user = user;
            return await AuthenticateAsync(username, password);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //await InitLocalStoreAsync(); // offline sync
            //await RefreshTodoItems();
        }

        #region Offline sync

        //private async Task InitLocalStoreAsync()
        //{
        //    if (!App.MobileService.SyncContext.IsInitialized)
        //    {
        //        var store = new MobileServiceSQLiteStore("localstore.db");
        //        store.DefineTable<TodoItem>();
        //        await App.MobileService.SyncContext.InitializeAsync(store);
        //    }
        //
        //    await SyncAsync();
        //}

        //private async Task SyncAsync()
        //{
        //    await App.MobileService.SyncContext.PushAsync();
        //    await todoTable.PullAsync("todoItems", todoTable.CreateQuery());
        //}

        #endregion 
    }
}
