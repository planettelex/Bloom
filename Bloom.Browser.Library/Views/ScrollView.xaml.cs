using Bloom.Browser.Library.ViewModels;

namespace Bloom.Browser.Library.Views
{
    /// <summary>
    /// Interaction logic for ScrollView.xaml
    /// </summary>
    public partial class ScrollView
    {
        public ScrollView(ScrollViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
