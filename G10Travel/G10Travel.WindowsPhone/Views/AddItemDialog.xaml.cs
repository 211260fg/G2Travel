using G10Travel.DataModel;
using System;
using System.Collections;
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

        public AddItemDialog(IEnumerable<Category> Categories)
        {
            this.InitializeComponent();
            createCategories(Categories);
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

        private void createCategories(IEnumerable<Category> Categories)
        {
            
            cbCategory.ItemsSource = Categories;
        }
       

        public String GetItemName()
        {
            return itemName.Text;
        }

        public Category GetCategory()
        {
            if(cbCategory.SelectedIndex == 0)
            {
                return new Category { Name = tbNewCategory.Text };  
            } else
            {
                return (Category)cbCategory.SelectedItem;
            }
        }

        public Boolean IsResultOK()
        {
            return ResultOK;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
        }

        private void OnCbCategorySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedIndex == 0)
            {
                cbCategory.Width = 146;
                tbNewCategory.Visibility = Visibility.Visible;
            }else
            {
                cbCategory.Width = 356;
                tbNewCategory.Visibility = Visibility.Collapsed;
            }
        }
    }
        
}
