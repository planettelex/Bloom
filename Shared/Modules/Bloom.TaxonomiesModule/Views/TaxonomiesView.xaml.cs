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
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SyncWithState);
        }
        private readonly IEventAggregator _eventAggregator;

        private TaxonomiesViewModel Model { get { return (TaxonomiesViewModel) DataContext; } }

        private void AddLibrary(LibraryConnection libraryConnection)
        {
            var libraryViewModel = new LibraryViewModel(libraryConnection.Library, _eventAggregator);
            TaxonomiesLibraries.Children.Insert(0, new LibraryView(libraryViewModel));
        }

        private void RemoveLibrary(Guid libraryId)
        {
            LibraryView toRemove = null;
            foreach (LibraryView libraryView in TaxonomiesLibraries.Children)
                if (libraryView.Model.Library.Id == libraryId)
                    toRemove = libraryView;
            
            if (toRemove == null)
                return;

            TaxonomiesLibraries.Children.Remove(toRemove);
        }

        private void SyncWithState(object nothing)
        {
            Model.SetState();
            if (Model.State.Connections == null || Model.State.Connections.Count == 0)
                _eventAggregator.GetEvent<HideSidebarEvent>().Publish(null);
            else
            {
                TaxonomiesLibraries.Children.Clear();
                foreach (var libraryConnection in Model.State.Connections)
                {
                    var libraryViewModel = new LibraryViewModel(libraryConnection.Library, _eventAggregator);
                    TaxonomiesLibraries.Children.Add(new LibraryView(libraryViewModel));
                }
                _eventAggregator.GetEvent<ShowSidebarEvent>().Publish(null);
            }
        }
    }
}
