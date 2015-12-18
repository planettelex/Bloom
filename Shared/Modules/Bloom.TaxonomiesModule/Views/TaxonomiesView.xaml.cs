using Bloom.Domain.Models;
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

            if (State != null && State.Connections != null && State.Connections.Count > 0)
                foreach (var libraryConnection in State.Connections)
                    AddLibrary(libraryConnection);

            _eventAggregator.GetEvent<ConnectionAddedEvent>().Subscribe(AddLibrary);
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
            var library = new Library { Id = libraryConnection.LibraryId, Name = libraryConnection.LibraryName }; // Todo: Make this a data call
            var libraryViewModel = new LibraryViewModel(library, _eventAggregator);
            TaxonomiesLibraries.Children.Add(new LibraryView(libraryViewModel));
        }
    }
}
