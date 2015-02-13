using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for SpreadsheetView.xaml
    /// </summary>
    public partial class SpreadsheetView
    {
        public SpreadsheetView(SpreadsheetViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
