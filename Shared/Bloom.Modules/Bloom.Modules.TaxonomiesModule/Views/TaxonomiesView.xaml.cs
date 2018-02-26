using System;
using Bloom.Events;
using Bloom.Modules.TaxonomiesModule.ViewModels;
using Bloom.State.Domain.Models;

namespace Bloom.Modules.TaxonomiesModule.Views
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
        public TaxonomiesView(TaxonomiesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            ViewModel.EventAggregator.GetEvent<ConnectionAddedEvent>().Subscribe(AddLibrary);
            ViewModel.EventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(RemoveLibrary);
            ViewModel.EventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SyncWithState);
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        private TaxonomiesViewModel ViewModel => (TaxonomiesViewModel) DataContext;

        /// <summary>
        /// Adds the library.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        private void AddLibrary(LibraryConnection libraryConnection)
        {
            var libraryViewModel = new LibraryViewModel(libraryConnection.Library, ViewModel.EventAggregator);
            TaxonomiesLibraries.Children.Insert(0, new LibraryView(libraryViewModel));
        }

        /// <summary>
        /// Removes the library.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        private void RemoveLibrary(Guid libraryId)
        {
            LibraryView toRemove = null;
            foreach (LibraryView libraryView in TaxonomiesLibraries.Children)
                if (libraryView.ViewModel.Library.Id == libraryId)
                    toRemove = libraryView;
            
            if (toRemove == null)
                return;

            TaxonomiesLibraries.Children.Remove(toRemove);
        }

        /// <summary>
        /// Synchronizes with state data.
        /// </summary>
        private void SyncWithState(object nothing)
        {
            ViewModel.SetState();
            if (ViewModel.State.HasConnections())
            {
                TaxonomiesLibraries.Children.Clear();
                foreach (var libraryConnection in ViewModel.State.Connections)
                {
                    var libraryViewModel = new LibraryViewModel(libraryConnection.Library, ViewModel.EventAggregator);
                    TaxonomiesLibraries.Children.Add(new LibraryView(libraryViewModel));
                }
                ViewModel.EventAggregator.GetEvent<ShowSidebarEvent>().Publish(null);
            }
            else
                ViewModel.EventAggregator.GetEvent<HideSidebarEvent>().Publish(null);
        }
    }
}
