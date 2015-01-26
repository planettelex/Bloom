using System;

namespace Bloom.Browser.Song.Services
{
    public interface ISongService
    {
        void NewSongTab();

        void DuplicateSongTab(Guid tabId);
    }
}
