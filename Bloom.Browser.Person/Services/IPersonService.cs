using System;

namespace Bloom.Browser.PersonModule.Services
{
    public interface IPersonService
    {
        void NewPersonTab();

        void DuplicatePersonTab(Guid tabId);
    }
}
