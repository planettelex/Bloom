using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.SongModule.Services
{
    public interface ISongService
    {
        void NewSongTab(Buid songId);

        void RestoreSongTab(Tab tab);

        void DuplicateSongTab(Guid tabId);
    }
}
