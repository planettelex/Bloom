using System;
using Bloom.Browser.Common;

namespace Bloom.Browser.Library.Services
{
    public interface ILibraryService
    {
        void NewLibraryTab();

        void DuplicateLibraryTab(Guid tabId);

        void ChangeLibraryTabView(Guid tabId, LibraryViewType viewType);
    }
}
