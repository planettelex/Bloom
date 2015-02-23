using System;
using Bloom.Browser.Common;
using Bloom.Browser.LibraryModule.ViewModels;
using Bloom.Browser.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for LibraryView.xaml
    /// </summary>
    public partial class LibraryView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryView" /> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public LibraryView(LibraryViewModel viewModel, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            DataContext = viewModel;
            ShowView(viewModel.ViewType);

            eventAggregator.GetEvent<ChangeLibraryTabViewEvent>().Subscribe(ChangeLibraryTabView);
        }

        private LibraryViewModel ViewModel
        {
            get { return (LibraryViewModel) DataContext; }
        }

        private void ChangeLibraryTabView(Tuple<Guid, ViewType> libraryViewTuple)
        {
            if (ViewModel.TabId != libraryViewTuple.Item1)
                return;

            ShowView(libraryViewTuple.Item2);
        }

        private void ShowView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Album:
                    ShowAlbumView();
                    break;
                case ViewType.Coverflow:
                    ShowCoverflowView();
                    break;
                case ViewType.Grid:
                    ShowGridView();
                    break;
                case ViewType.List:
                    ShowListView();
                    break;
                case ViewType.Scattered:
                    ShowScatteredView();
                    break;
                case ViewType.Scroll:
                    ShowScrollView();
                    break;
                case ViewType.Slideshow:
                    ShowSlideshowView();
                    break;
                case ViewType.Spines:
                    ShowSpinesView();
                    break;
                case ViewType.Spreadsheet:
                    ShowSpreadsheetView();
                    break;
                case ViewType.Tiles:
                    ShowTilesView();
                    break;
            }
        }

        private void ShowAlbumView()
        {
            LibraryViewContainer.Children.Clear();
            var albumViewModel = new AlbumViewModel();
            var albumView = new AlbumView(albumViewModel);
            LibraryViewContainer.Children.Add(albumView);
        }

        private void ShowCoverflowView()
        {
            LibraryViewContainer.Children.Clear();
            var coverflowViewModel = new CoverflowViewModel();
            var coverflowView = new CoverflowView(coverflowViewModel);
            LibraryViewContainer.Children.Add(coverflowView);
        }

        private void ShowGridView()
        {
            LibraryViewContainer.Children.Clear();
            var gridViewModel = new GridViewModel();
            var gridView = new GridView(gridViewModel);
            LibraryViewContainer.Children.Add(gridView);
        }

        private void ShowListView()
        {
            LibraryViewContainer.Children.Clear();
            var listViewModel = new ListViewModel();
            var listView = new ListView(listViewModel);
            LibraryViewContainer.Children.Add(listView);
        }

        private void ShowScatteredView()
        {
            LibraryViewContainer.Children.Clear();
            var scatteredViewModel = new ScatteredViewModel();
            var scatteredView = new ScatteredView(scatteredViewModel);
            LibraryViewContainer.Children.Add(scatteredView);
        }

        private void ShowScrollView()
        {
            LibraryViewContainer.Children.Clear();
            var scrollViewModel = new ScrollViewModel();
            var scrollView = new ScrollView(scrollViewModel);
            LibraryViewContainer.Children.Add(scrollView);
        }

        private void ShowSlideshowView()
        {
            LibraryViewContainer.Children.Clear();
            var slideshowViewModel = new SlideshowViewModel();
            var slideshowView = new SlideshowView(slideshowViewModel);
            LibraryViewContainer.Children.Add(slideshowView);
        }

        private void ShowSpinesView()
        {
            LibraryViewContainer.Children.Clear();
            var spinesViewModel = new SpinesViewModel();
            var spinesView = new SpinesView(spinesViewModel);
            LibraryViewContainer.Children.Add(spinesView);
        }

        private void ShowSpreadsheetView()
        {
            LibraryViewContainer.Children.Clear();
            var spreadsheetViewModel = new SpreadsheetViewModel();
            var spreadsheetView = new SpreadsheetView(spreadsheetViewModel);
            LibraryViewContainer.Children.Add(spreadsheetView);
        }

        private void ShowTilesView()
        {
            LibraryViewContainer.Children.Clear();
            var tilesViewModel = new TilesViewModel();
            var tilesView = new TilesView(tilesViewModel);
            LibraryViewContainer.Children.Add(tilesView);
        }
    }
}
