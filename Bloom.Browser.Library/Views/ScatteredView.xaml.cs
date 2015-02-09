using Bloom.Browser.Library.ViewModels;

namespace Bloom.Browser.Library.Views
{
    /// <summary>
    /// Interaction logic for ScatteredView.xaml
    /// </summary>
    public partial class ScatteredView
    {
        public ScatteredView(ScatteredViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
