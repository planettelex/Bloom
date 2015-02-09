using System;
using Bloom.Analytics.Common;

namespace Bloom.Analytics.Library.Services
{
    public interface ILibraryService
    {
        void NewLibraryTab();

        void DuplicateLibraryTab(Guid tabId);

        void ChangeLibraryTabView(Guid tabId, LibraryViewType viewType);
    }
}
