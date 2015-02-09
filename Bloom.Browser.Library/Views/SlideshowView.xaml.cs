using Bloom.Browser.Library.ViewModels;

namespace Bloom.Browser.Library.Views
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
