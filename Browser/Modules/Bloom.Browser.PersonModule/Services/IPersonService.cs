using System;

namespace Bloom.Browser.PersonModule.Services
{
    public interface IPersonService
    {
        void NewPersonTab(Guid personId);

        void DuplicatePersonTab(Guid tabId);
    }
}
