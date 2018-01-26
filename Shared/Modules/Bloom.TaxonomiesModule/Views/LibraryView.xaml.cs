using Bloom.Modules.TaxonomiesModule.ViewModels;

namespace Bloom.Modules.TaxonomiesModule.Views
{
    /// <summary>
    /// Interaction logic for LibraryView.xaml
    /// </summary>
    public partial class LibraryView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public LibraryView(LibraryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        public LibraryViewModel ViewModel => (LibraryViewModel) DataContext;
    }
}
