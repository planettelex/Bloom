using System;
using Bloom.Analytics.Common;

namespace Bloom.Analytics.PersonModule.ViewModels
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
