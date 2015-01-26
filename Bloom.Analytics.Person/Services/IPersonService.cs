using System;

namespace Bloom.Analytics.Person.Services
{
    public interface IPersonService
    {
        void NewPersonTab();

        void DuplicatePersonTab(Guid tabId);
    }
}
