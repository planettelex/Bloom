using System;

namespace Bloom.Browser.SongModule.Services
{
    public interface ISongService
    {
        void NewSongTab();

        void DuplicateSongTab(Guid tabId);
    }
}
