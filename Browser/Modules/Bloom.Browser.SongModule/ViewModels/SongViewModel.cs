using System;

namespace Bloom.Browser.SongModule.ViewModels
{
    public class SongViewModel
    {
        public SongViewModel()
        {
            TabId = Guid.NewGuid();
        }

        public Guid TabId { get; set; }
    }
}
