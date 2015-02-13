using System;

namespace Bloom.Analytics.PersonModule.Services
{
    public interface IPersonService
    {
        void NewPersonTab();

        void DuplicatePersonTab(Guid tabId);
    }
}
