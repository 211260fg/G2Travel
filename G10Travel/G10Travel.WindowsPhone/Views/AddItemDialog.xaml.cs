using System;
using System.Collections.Generic;
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


        public System.Collections.IEnumerable Categories = new List<string>()
{
 "Lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit", "Nullam", "felis", "dui", "gravida", "at"};

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

        private void OnCbCategoryContentChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {

        }
    }
        
}
