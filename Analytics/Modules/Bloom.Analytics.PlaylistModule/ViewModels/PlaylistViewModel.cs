using System;
using Bloom.Domain.Models;

namespace Bloom.Analytics.PlaylistModule.ViewModels
{
    public class PlaylistViewModel
    {
        public PlaylistViewModel(Playlist playlist, Guid tabId)
        {
            Playlist = playlist;
            TabId = tabId;
        }

        public Guid TabId { get; set; }

        public Playlist Playlist { get; set; }
    }
}
