using System;
using Bloom.Domain.Models;

namespace Bloom.Analytics.SongModule.ViewModels
{
    public class SongViewModel
    {
        public SongViewModel(Song song, Guid tabId)
        {
            Song = song;
            TabId = tabId;
        }

        public Guid TabId { get; set; }

        public Song Song { get; set; }
    }
}
