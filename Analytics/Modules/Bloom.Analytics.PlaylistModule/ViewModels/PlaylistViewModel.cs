using System;

namespace Bloom.Analytics.PlaylistModule.ViewModels
{
    public class PlaylistViewModel
    {
        public PlaylistViewModel()
        {
            TabId = Guid.NewGuid();
        }

        public Guid TabId { get; set; }
    }
}
