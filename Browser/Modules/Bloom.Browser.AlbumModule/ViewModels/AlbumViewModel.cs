using System;

namespace Bloom.Browser.AlbumModule.ViewModels
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
