using Bloom.Browser.Library.ViewModels;

namespace Bloom.Browser.Library.Views
{
    /// <summary>
    /// Interaction logic for SpinesView.xaml
    /// </summary>
    public partial class SpinesView
    {
        public SpinesView(SpinesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
