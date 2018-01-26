using System;
using Bloom.Browser.Common;
using Bloom.Domain.Models;

namespace Bloom.Browser.LibraryModule.ViewModels
{
    /// <summary>
    /// View model for LibraryView.xaml
    /// </summary>
    public class LibraryViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryViewModel"/> class.
        /// </summary>
        /// <param name="library">A library.</param>
        /// <param name="viewType">The view type.</param>
        /// <param name="tabId">The tab identifier.</param>
        public LibraryViewModel(Library library, ViewType viewType, Guid tabId)
        {
            ViewType = viewType;
            Library = library;
            TabId = tabId;
        }

        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        public Guid TabId { get; set; }

        /// <summary>
        /// Gets or sets the library.
        /// </summary>
        public Library Library { get; set; }

        /// <summary>
        /// Gets or sets the view type.
        /// </summary>
        public ViewType ViewType { get; set; }
    }
}
