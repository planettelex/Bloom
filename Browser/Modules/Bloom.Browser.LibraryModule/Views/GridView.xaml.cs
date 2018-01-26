using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public GridView(GridViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
