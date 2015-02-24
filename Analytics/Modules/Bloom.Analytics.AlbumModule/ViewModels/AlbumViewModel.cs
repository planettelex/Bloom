using System;

namespace Bloom.Analytics.AlbumModule.ViewModels
{
    public class AlbumViewModel
    {
        public AlbumViewModel()
        {
            TabId = Guid.NewGuid();
        }

        public Guid TabId { get; set; }
    }
}
