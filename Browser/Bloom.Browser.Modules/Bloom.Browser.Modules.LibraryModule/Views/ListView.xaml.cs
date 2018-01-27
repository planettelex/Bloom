using Bloom.Browser.Modules.LibraryModule.ViewModels;

namespace Bloom.Browser.Modules.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public ListView(ListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
