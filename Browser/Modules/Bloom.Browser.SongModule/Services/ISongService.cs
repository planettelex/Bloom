using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.SongModule.Services
{
    public interface ISongService
    {
        void NewSongTab(Buid songBuid);

        void RestoreSongTab(Tab tab);

        void DuplicateSongTab(Guid tabId);
    }
}
