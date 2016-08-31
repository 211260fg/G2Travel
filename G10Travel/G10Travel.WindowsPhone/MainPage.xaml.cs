using System;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Xaml.Controls;
using G10Travel.Views;
using Windows.Security.Authentication.Web;
using Facebook;
using System.Diagnostics;
using Windows.ApplicationModel.Activation;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace G10Travel
{
    public sealed partial class MainPage : Page, IWebAuthenticationContinuable
    {
        private string AccessToken { get; set; }
        private DateTime TokenExpiry { get; set; }

        public MainPage()
        {
            checkIfLoggedIn();
        }

        public async void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
        {
            await ParseAuthenticationResult(args.WebAuthenticationResult);
        }

        public async Task ParseAuthenticationResult(WebAuthenticationResult result)
        {
            switch (result.ResponseStatus)
            {
                case WebAuthenticationStatus.ErrorHttp:
                    Debug.WriteLine("Error");
                    break;
                case WebAuthenticationStatus.Success:
                    var pattern = string.Format("{0}#access_token={1}&expires_in={2}", WebAuthenticationBroker.GetCurrentApplicationCallbackUri(), "(?<access_token>.+)", "(?<expires_in>.+)");
                    var match = Regex.Match(result.ResponseData, pattern);

                    var access_token = match.Groups["access_token"];
                    var expires_in = match.Groups["expires_in"];

                    AccessToken = access_token.Value;
                    TokenExpiry = DateTime.Now.AddSeconds(double.Parse(expires_in.Value));
                    FacebookClient client = new FacebookClient(AccessToken);
                    dynamic user = await client.GetTaskAsync("me");
                    App.MobileService.CurrentUser = await LoginFacebookAsync(user.name, user.id);
                    Frame.Navigate(typeof(HomePage));
                    break;
                case WebAuthenticationStatus.UserCancel:
                    Debug.WriteLine("Operation aborted");
                    break;
                default:
                    break;
            }
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
