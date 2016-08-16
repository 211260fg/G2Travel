using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace G10Travel.Views
{
    public sealed partial class AddItemDialog : ContentDialog
    {

        private Boolean ResultOK;


        public System.Collections.IEnumerable Categories = new ObservableCollection<string>
        {
            "a",
            "b",
            "c",
            "d"
        };
        public AddItemDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            if (string.IsNullOrEmpty(itemName.Text))
            {
                args.Cancel = true;
                errorTextBlock.Text = "Item name is required";
                ResultOK = false;
            }
            else
            {
                ResultOK = true;
            }

        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // You can set a threshold when to start looking for suggestions
                /*if (sender.Text.Length > 3)
                {*/
                System.Diagnostics.Debug.WriteLine("text changed");
                sender.ItemsSource = Categories;
                /*}
                else
                {
                    sender.ItemsSource = new List<String> { };
                }*/
            }
        }

        private String getSuggestions(String input)
        {
            return "test";
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        public String GetItemName()
        {
            return itemName.Text;
        }
        public Boolean IsResultOK()
        {
            return ResultOK;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
        }
    }
        
}
