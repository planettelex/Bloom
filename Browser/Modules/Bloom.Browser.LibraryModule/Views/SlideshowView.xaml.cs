using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for SlideshowView.xaml
    /// </summary>
    public partial class SlideshowView
    {
        public SlideshowView(SlideshowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
