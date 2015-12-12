using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.PersonModule.Services
{
    public interface IPersonService
    {
        void NewPersonTab(Buid personBuid);

        void RestorePersonTab(Tab tab);

        void DuplicatePersonTab(Guid tabId);
    }
}
