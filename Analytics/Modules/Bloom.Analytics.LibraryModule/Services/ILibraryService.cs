using System;
using Bloom.Analytics.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.LibraryModule.Services
{
    public interface ILibraryService
    {
        void NewLibraryTab(Guid libraryId);

        void RestoreLibraryTab(Tab tab);

        void DuplicateLibraryTab(Guid tabId);

        void ChangeLibraryTabView(Guid tabId, ViewType viewType);
    }
}
