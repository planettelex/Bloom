using System;
using Bloom.Controls.Helpers;
using Bloom.Player.Modules.MenuModule.ViewModels;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace Bloom.Player.Modules.MenuModule.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuView" /> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public MenuView(MenuViewModel viewModel, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            DataContext = viewModel;

            eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetSkin);
        }

        private MenuViewModel Model => (MenuViewModel) DataContext;

        private void SetSkin(object nothing)
        {
            Model.SetState();
            // Check the current skin.
            foreach (RadMenuItem menuItem in Skins.Items)
            {
                var skinName = (string)menuItem.CommandParameter;
                menuItem.IsChecked = skinName.Equals(Model.State.SkinName, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        private void OnItemClick(object sender, RadRoutedEventArgs e)
        {
            var currentItem = e.OriginalSource as RadMenuItem;
            if (currentItem == null || !currentItem.IsCheckable || currentItem.Tag == null)
                return;

            if ((string) currentItem.CommandParameter == Model.State.SkinName)
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
