using Bloom.Browser.Library.ViewModels;

namespace Bloom.Browser.Library.Views
{
    /// <summary>
    /// Interaction logic for TilesView.xaml
    /// </summary>
    public partial class TilesView
    {
        public TilesView(TilesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
