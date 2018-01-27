using Bloom.Browser.Modules.LibraryModule.ViewModels;

namespace Bloom.Browser.Modules.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for SpreadsheetView.xaml
    /// </summary>
    public partial class SpreadsheetView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public SpreadsheetView(SpreadsheetViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
