using System;
using Bloom.Analytics.Common;

namespace Bloom.Analytics.LibraryModule.ViewModels
{
    public class LibraryViewModel
    {
        public LibraryViewModel(ViewType viewType)
        {
            TabId = Guid.NewGuid();
            ViewType = viewType;
        }
        
        public Guid TabId { get; set; }

        public ViewType ViewType { get; set; }
    }
}
