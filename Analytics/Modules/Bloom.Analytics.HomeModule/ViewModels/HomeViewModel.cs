using System;

namespace Bloom.Analytics.HomeModule.ViewModels
{
    /// <summary>
    /// View model for HomeView.xaml
    /// </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        public HomeViewModel(Guid tabId)
        {
            TabId = tabId;
        }

        /// <summary>
        /// The tab identifier.
        /// </summary>
        public Guid TabId { get; set; }
    }
}
