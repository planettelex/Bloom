using System;
using Bloom.Domain.Models;

namespace Bloom.Browser.AlbumModule.ViewModels
{
    public class AlbumViewModel
    {
        public AlbumViewModel(Album album, Guid tabId)
        {
            Album = album;
            TabId = tabId;
        }

        public Guid TabId { get; set; }

        public Album Album { get; set; }
    }
}
