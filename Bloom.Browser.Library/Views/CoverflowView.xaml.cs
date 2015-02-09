using Bloom.Browser.Library.ViewModels;

namespace Bloom.Browser.Library.Views
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
