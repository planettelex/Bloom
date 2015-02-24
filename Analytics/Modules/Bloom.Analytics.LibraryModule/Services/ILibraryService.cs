using System;
using Bloom.Analytics.Common;

namespace Bloom.Analytics.LibraryModule.Services
{
    public interface ILibraryService
    {
        void NewLibraryTab();

        void DuplicateLibraryTab(Guid tabId);

        void ChangeLibraryTabView(Guid tabId, ViewType viewType);
    }
}
