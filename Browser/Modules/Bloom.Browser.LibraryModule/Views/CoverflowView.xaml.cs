using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for CoverflowView.xaml
    /// </summary>
    public partial class CoverflowView
    {
        public CoverflowView(CoverflowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
