using System;
using Bloom.Browser.Common;
using Bloom.Domain.Models;

namespace Bloom.Browser.ArtistModule.ViewModels
{
    public class ArtistViewModel
    {
        public ArtistViewModel(Artist artist, ViewType viewType, Guid tabId)
        {
            ViewType = viewType;
            Artist = artist;
            TabId = tabId;
        }

        public Guid TabId { get; set; }

        public Artist Artist { get; set; }

        public ViewType ViewType { get; set; }
    }
}
