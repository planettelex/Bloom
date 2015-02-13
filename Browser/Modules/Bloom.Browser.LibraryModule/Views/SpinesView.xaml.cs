using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
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
