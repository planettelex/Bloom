using System;
using Bloom.Browser.Common;
using Bloom.Domain.Models;

namespace Bloom.Browser.LibraryModule.ViewModels
{
    public class LibraryViewModel
    {
        public LibraryViewModel(Library library, ViewType viewType)
        {
            ViewType = viewType;
        }

        public Guid TabId { get; set; }

        public Library Library { get; set; }

        public ViewType ViewType { get; set; }
    }
}
