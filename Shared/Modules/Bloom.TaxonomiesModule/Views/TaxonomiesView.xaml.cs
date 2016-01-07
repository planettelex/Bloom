using System;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Bloom.TaxonomiesModule.ViewModels;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.TaxonomiesModule.Views
{
    /// <summary>
    /// Interaction logic for TaxonomiesView.xaml
    /// </summary>
    public partial class TaxonomiesView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomiesView" /> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public TaxonomiesView(TaxonomiesViewModel viewModel, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            DataContext = viewModel;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ConnectionAddedEvent>().Subscribe(AddLibrary);
            _eventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(RemoveLibrary);

            SyncWithState();
        }
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public TabbedApplicationState State
        {
            get { return ((TaxonomiesViewModel) DataContext).State; }
        }

        private void AddLibrary(LibraryConnection libraryConnection)
        {
            SyncWithState();
        }

        private void RemoveLibrary(Guid libraryId)
        {
            SyncWithState();
        }

        private void SyncWithState()
        {
            if (State == null || State.Connections == null || State.Connections.Count == 0)
                _eventAggregator.GetEvent<HideSidebarEvent>().Publish(null);
            else
            {
                TaxonomiesLibraries.Children.Clear();
                foreach (var libraryConnection in State.Connections)
                {
                    var libraryViewModel = new LibraryViewModel(libraryConnection.Library, _eventAggregator);
                    TaxonomiesLibraries.Children.Add(new LibraryView(libraryViewModel));
                }
                _eventAggregator.GetEvent<ShowSidebarEvent>().Publish(null);
            }
        }
    }
}
