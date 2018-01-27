using System;
using Bloom.Analytics.Common;
using Bloom.Analytics.Modules.LibraryModule.ViewModels;
using Bloom.Analytics.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Analytics.Modules.LibraryModule.Views
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

        private LibraryViewModel ViewModel => (LibraryViewModel) DataContext;

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
                case ViewType.BarChart:
                    ShowBarChartView();
                    break;
                case ViewType.LineGraph:
                    ShowLineGraphView();
                    break;
                case ViewType.Map:
                    ShowMapView();
                    break;
                case ViewType.PieChart:
                    ShowPieChartView();
                    break;
                case ViewType.Stats:
                    ShowStatsView();
                    break;
                case ViewType.Timeline:
                    ShowTimelineView();
                    break;
            }
        }

        private void ShowBarChartView()
        {
            LibraryViewContainer.Children.Clear();
            var barChartViewModel = new BarChartViewModel();
            var barChartView = new BarChartView(barChartViewModel);
            LibraryViewContainer.Children.Add(barChartView);
        }

        private void ShowLineGraphView()
        {
            LibraryViewContainer.Children.Clear();
            var lineGraphViewModel = new LineGraphViewModel();
            var lineGraphView = new LineGraphView(lineGraphViewModel);
            LibraryViewContainer.Children.Add(lineGraphView);
        }

        private void ShowMapView()
        {
            LibraryViewContainer.Children.Clear();
            var mapViewModel = new MapViewModel();
            var mapView = new MapView(mapViewModel);
            LibraryViewContainer.Children.Add(mapView);
        }

        private void ShowPieChartView()
        {
            LibraryViewContainer.Children.Clear();
            var pieChartViewModel = new PieChartViewModel();
            var pieChartView = new PieChartView(pieChartViewModel);
            LibraryViewContainer.Children.Add(pieChartView);
        }

        private void ShowStatsView()
        {
            LibraryViewContainer.Children.Clear();
            var statsViewModel = new StatsViewModel();
            var statsChartView = new StatsView(statsViewModel);
            LibraryViewContainer.Children.Add(statsChartView);
        }

        private void ShowTimelineView()
        {
            LibraryViewContainer.Children.Clear();
            var timelineViewModel = new TimelineViewModel();
            var timelineChartView = new TimelineView(timelineViewModel);
            LibraryViewContainer.Children.Add(timelineChartView);
        }
    }
}
