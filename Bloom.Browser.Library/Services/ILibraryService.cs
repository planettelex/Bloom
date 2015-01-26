using System;

namespace Bloom.Browser.Library.Services
{
    public interface ILibraryService
    {
        void NewLibraryTab();

        void DuplicateLibraryTab(Guid tabId);
    }
}
