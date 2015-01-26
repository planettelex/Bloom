using System;

namespace Bloom.Browser.Person.Services
{
    public interface IPersonService
    {
        void NewPersonTab();

        void DuplicatePersonTab(Guid tabId);
    }
}
