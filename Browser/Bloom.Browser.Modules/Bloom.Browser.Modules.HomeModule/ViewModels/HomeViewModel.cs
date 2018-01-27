using System;

namespace Bloom.Browser.Modules.HomeModule.ViewModels
{
    /// <summary>
    /// View model for HomeView.xaml
    /// </summary>
    public class HomeViewModel
    {
        public HomeViewModel(Guid tabId)
        {
            TabId = tabId;
        }

        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        public Guid TabId { get; set; }
    }
}
