using System;

namespace Bloom.Analytics.PersonModule.Services
{
    public interface IPersonService
    {
        void NewPersonTab(Guid personId);

        void DuplicatePersonTab(Guid tabId);
    }
}
