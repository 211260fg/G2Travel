using System;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Xaml.Controls;
using G10Travel.Views;
using Windows.Security.Authentication.Web;
using Facebook;

namespace G10Travel
{
    public sealed partial class MainPage : Page
    {
        private async void btnLogin_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtUsername.Text)
        || string.IsNullOrEmpty(txtPassword.Password))
            {
                return;
            }

            try
            {
                // Sign-in and set the returned user on the context,
                // then load data from the mobile service.

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
            txtUsername.Text = "";
        }

        private async void btnRegister_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text)
        || string.IsNullOrEmpty(txtPassword.Password))
            {
                return;
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
            var clientId = "235751480157972";
            //Facebook permissions
            var scope = "public_profile, email";

            var redirectUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = clientId,
                redirect_uri = redirectUri,
                response_type = "token",
                scope = scope
            });

            Uri startUri = loginUrl;
            Uri endUri = new Uri(redirectUri, UriKind.Absolute);

            WebAuthenticationBroker.AuthenticateAndContinue(startUri, endUri, null, WebAuthenticationOptions.None);
        }
    }
}
