using System;
using Bloom.Controls.Helpers;
using Bloom.Browser.Common;
using Bloom.Browser.Menu.ViewModels;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace Bloom.Browser.Menu.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuView" /> class.
        /// </summary>
        /// <param name="viewModel">The menu view model.</param>
        public MenuView(MenuViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // Check the current skin.
            foreach (RadMenuItem menuItem in Skins.Items)
            {
                var skinName = (string) menuItem.CommandParameter;
                menuItem.IsChecked = skinName.Equals(viewModel.State.Skin, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        /// <summary>
        /// Gets the application state.
        /// </summary>
        public State State
        {
            get { return ((MenuViewModel) DataContext).State; }
        }

        private void OnItemClick(object sender, RadRoutedEventArgs e)
        {
            var currentItem = e.OriginalSource as RadMenuItem;
            if (currentItem == null || !currentItem.IsCheckable || currentItem.Tag == null) 
                return;
            
            if ((string) currentItem.CommandParameter == State.Skin)
            {
                currentItem.IsChecked = true;
                return;
            }

            var siblingItems = MenuControlHelper.GetSiblingGroupItems(currentItem);
            if (siblingItems == null) 
                return;

            foreach (var item in siblingItems)
            {
                if (!Equals(item, currentItem))
                    item.IsChecked = false;
            }
        }
    }
}
