using Bloom.Analytics.Modules.LibraryModule.ViewModels;

namespace Bloom.Analytics.Modules.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public MapView(MapViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
