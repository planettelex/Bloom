using System;
using Bloom.Analytics.Common;
using Bloom.Domain.Models;

namespace Bloom.Analytics.PersonModule.ViewModels
{
    /// <summary>
    /// View model for PersonView.xaml
    /// </summary>
    public class PersonViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonViewModel"/> class.
        /// </summary>
        /// <param name="person">A person.</param>
        /// <param name="viewType">The view type.</param>
        /// <param name="tabId">The tab identifier.</param>
        public PersonViewModel(Person person, ViewType viewType, Guid tabId)
        {
            ViewType = viewType;
            Person = person;
            TabId = tabId;
        }

        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        public Guid TabId { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the view type.
        /// </summary>
        public ViewType ViewType { get; set; }
    }
}
