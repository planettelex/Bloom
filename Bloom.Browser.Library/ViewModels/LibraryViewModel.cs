using System;
using Bloom.Browser.Common;

namespace Bloom.Browser.Library.ViewModels
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
