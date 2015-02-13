using System;
using Bloom.Analytics.Common;

namespace Bloom.Analytics.LibraryModule.ViewModels
{
    public class LibraryViewModel
    {
        public LibraryViewModel(LibraryViewType viewType)
        {
            ViewType = viewType;
        }
        
        public Guid TabId { get; set; }

        public LibraryViewType ViewType { get; set; }
    }
}
