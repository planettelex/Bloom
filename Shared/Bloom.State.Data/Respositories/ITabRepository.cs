using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    public interface ITabRepository
    {
        Tab GetTab(Guid tabId);

        List<Tab> ListTabs();
    }
}
