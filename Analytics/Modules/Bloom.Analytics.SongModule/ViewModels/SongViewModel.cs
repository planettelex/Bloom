using System;

namespace Bloom.Analytics.SongModule.ViewModels
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
