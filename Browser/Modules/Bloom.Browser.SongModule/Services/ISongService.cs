using System;

namespace Bloom.Browser.SongModule.Services
{
    public interface ISongService
    {
        void NewSongTab(Guid songId);

        void DuplicateSongTab(Guid tabId);
    }
}
