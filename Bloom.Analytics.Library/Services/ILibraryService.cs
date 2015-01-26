using System;

namespace Bloom.Analytics.Library.Services
{
    public interface ILibraryService
    {
        void NewLibraryTab();

        void DuplicateLibraryTab(Guid tabId);
    }
}
