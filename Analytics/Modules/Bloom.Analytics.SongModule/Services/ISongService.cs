using System;

namespace Bloom.Analytics.SongModule.Services
{
    public interface ISongService
    {
        void NewSongTab(Guid songId);

        void DuplicateSongTab(Guid tabId);
    }
}
