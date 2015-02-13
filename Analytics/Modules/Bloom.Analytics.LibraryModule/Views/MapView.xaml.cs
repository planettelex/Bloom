using Bloom.Analytics.LibraryModule.ViewModels;

namespace Bloom.Analytics.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView
    {
        public MapView(MapViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
