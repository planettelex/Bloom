using System;

namespace Bloom.Analytics.Song.Services
{
    public interface ISongService
    {
        void NewSongTab();

        void DuplicateSongTab(Guid tabId);
    }
}
