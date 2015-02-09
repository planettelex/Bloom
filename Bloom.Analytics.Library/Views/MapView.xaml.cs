using Bloom.Analytics.Library.ViewModels;

namespace Bloom.Analytics.Library.Views
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
