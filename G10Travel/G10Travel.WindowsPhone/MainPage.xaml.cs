using System;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Xaml.Controls;
using G10Travel.Views;
using Windows.Security.Authentication.Web;
using Facebook;
using System.Diagnostics;

namespace G10Travel
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            checkIfLoggedIn();
        }


        private async void checkIfLoggedIn()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            Object username = localSettings.Values["username"];
            Object userpassword = localSettings.Values["userpassword"];


            if (username != null || userpassword != null)
            {
                try { 
                App.MobileService.CurrentUser = await AuthenticateAsync(username.ToString(), userpassword.ToString());
                Frame.Navigate(typeof(HomePage));
                }
                catch (MobileServiceInvalidOperationException ignored)
                {
                    this.InitializeComponent();
                }
            }
            else
            {
                this.InitializeComponent();
            }
        }


        private async void btnLogin_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtUsername.Text)
        || string.IsNullOrEmpty(txtPassword.Password))
            {
                this.txtPassword.Password = "testertje";
                this.txtUsername.Text= "testertje";

                // return;
            }

            try
            {
                // Sign-in and set the returned user on the context,
                // then load data from the mobile service.

                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                // Create a simple setting

                localSettings.Values["username"] = this.txtUsername.Text;
                localSettings.Values["userpassword"] = txtPassword.Password;


                App.MobileService.CurrentUser = await AuthenticateAsync(this.txtUsername.Text, txtPassword.Password);
                Frame.Navigate(typeof(HomePage));

            }
            catch (MobileServiceInvalidOperationException ex)
            {
                loginMessage.Text = ex.Message;
            }
            finally
            {
                ResetLoginUI();
            }
        }

        private void ResetLoginUI()
        {
            txtPassword.Password = "";
            //txtUsername.Text = "";
        }

        private async void btnRegister_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text)
        || string.IsNullOrEmpty(txtPassword.Password))
            {
               // return;
                 this.txtPassword.Password = "testertje";
                this.txtUsername.Text= "testertje";
            }

            try
            {
                // Sign-in and set the returned user on the context,
                // then load data from the mobile service.

                App.MobileService.CurrentUser = await RegisterAsync(this.txtUsername.Text, txtPassword.Password);
                Frame.Navigate(typeof(HomePage));
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                loginMessage.Text = ex.Message;
            }
            finally
            {
                ResetLoginUI();
            }
        }

        private void btnFbLogin_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Facebook app id
            String clientId = "235751480157972";
            //Facebook permissions
            String scope = "public_profile, email";

            //var redirectUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();
            String redirectUri = "https://g9ts.azurewebsites.net";

            String FacebookURL = "https://www.facebook.com/dialog/oauth?client_id=" + Uri.EscapeDataString(clientId) + "&redirect_uri=" + Uri.EscapeDataString(redirectUri) + "&scope=" + Uri.EscapeDataString(scope) + "&display=popup&response_type=token";

            Uri startUri = new Uri(FacebookURL);
            //Uri startUri = loginUrl;
            Uri endUri = new Uri(redirectUri, UriKind.Absolute);

            WebAuthenticationBroker.AuthenticateAndContinue(startUri, endUri, null, WebAuthenticationOptions.None);
        }
    }
}
