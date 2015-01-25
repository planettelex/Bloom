using Bloom.Taxonomies.ViewModels;

namespace Bloom.Taxonomies.Views
{
    /// <summary>
    /// Interaction logic for TaxonomiesView.xaml
    /// </summary>
    public partial class TaxonomiesView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomiesView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public TaxonomiesView(TaxonomiesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
