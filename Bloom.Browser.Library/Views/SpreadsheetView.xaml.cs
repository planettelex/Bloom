using Bloom.Browser.Library.ViewModels;

namespace Bloom.Browser.Library.Views
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
