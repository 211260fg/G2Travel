using System;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Xaml.Controls;

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
    }
}
