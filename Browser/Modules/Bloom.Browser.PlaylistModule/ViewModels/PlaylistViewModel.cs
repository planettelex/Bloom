using System;

namespace Bloom.Browser.PlaylistModule.ViewModels
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
