using System;
using Bloom.Browser.Common;

namespace Bloom.Browser.ArtistModule.ViewModels
{
    public class ArtistViewModel
    {
        public ArtistViewModel(ViewType viewType)
        {
            TabId = Guid.NewGuid();
            ViewType = viewType;
        }

        public Guid TabId { get; set; }

        public ViewType ViewType { get; set; }
    }
}
