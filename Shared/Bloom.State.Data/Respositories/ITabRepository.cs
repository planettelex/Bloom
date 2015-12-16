using System;
using System.Collections.Generic;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    public interface ITabRepository
    {
        Tab GetTab(Guid tabId);

        List<Tab> ListTabs(ProcessType process, Guid userId);

        void AddTab(Tab tab);

        void RemoveTab(Tab tab);
    }
}
