using System;

namespace Bloom.Analytics.SongModule.Services
{
    public interface ISongService
    {
        void NewSongTab();

        void DuplicateSongTab(Guid tabId);
    }
}
