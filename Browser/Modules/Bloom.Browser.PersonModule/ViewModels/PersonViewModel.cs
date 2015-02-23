using System;
using Bloom.Browser.Common;

namespace Bloom.Browser.PersonModule.ViewModels
{
    public class PersonViewModel
    {
        public PersonViewModel(ViewType viewType)
        {
            TabId = Guid.NewGuid();
            ViewType = viewType;
        }

        public Guid TabId { get; set; }

        public ViewType ViewType { get; set; }
    }
}
