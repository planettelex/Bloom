using System;
using Bloom.Browser.Common;
using Bloom.Domain.Models;

namespace Bloom.Browser.PersonModule.ViewModels
{
    public class PersonViewModel
    {
        public PersonViewModel(Person person, ViewType viewType, Guid tabId)
        {
            ViewType = viewType;
            Person = person;
            TabId = tabId;
        }

        public Guid TabId { get; set; }

        public Person Person { get; set; }

        public ViewType ViewType { get; set; }
    }
}
