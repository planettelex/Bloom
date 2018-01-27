using System;
using Bloom.Browser.Modules.MenuModule.ViewModels;
using Bloom.Controls.Helpers;
using Bloom.PubSubEvents;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace Bloom.Browser.Modules.MenuModule.Views
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

            ViewModel.EventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetSkin);
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        private MenuViewModel ViewModel => (MenuViewModel) DataContext;

        /// <summary>
        /// Sets the skin.
        /// </summary>
        private void SetSkin(object nothing)
        {
            ViewModel.SetState();
            // Check the current skin.
            foreach (RadMenuItem menuItem in Skins.Items)
            {
                var skinName = (string) menuItem.CommandParameter;
                menuItem.IsChecked = skinName.Equals(ViewModel.State.SkinName, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        /// <summary>
        /// A menu item can fire this event to update mutually exclusive sibling option.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">The <see cref="RadRoutedEventArgs"/> instance containing the event data.</param>
        private void OnItemClick(object sender, RadRoutedEventArgs e)
        {
            var currentItem = e.OriginalSource as RadMenuItem;
            if (currentItem == null || !currentItem.IsCheckable || currentItem.Tag == null) 
                return;
            
            if ((string) currentItem.CommandParameter == ViewModel.State.SkinName)
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
