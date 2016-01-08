using Bloom.TaxonomiesModule.ViewModels;

namespace Bloom.TaxonomiesModule.Views
{
    /// <summary>
    /// Interaction logic for LibraryView.xaml
    /// </summary>
    public partial class LibraryView
    {
        public LibraryView(LibraryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public LibraryViewModel Model
        {
            get { return (LibraryViewModel) DataContext; }
        }
    }
}
