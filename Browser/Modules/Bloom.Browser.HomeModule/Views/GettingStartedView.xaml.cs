using System.Windows.Controls;
using Bloom.Browser.HomeModule.ViewModels;

namespace Bloom.Browser.HomeModule.Views
{
    /// <summary>
    /// Interaction logic for GettingStartedView.xaml
    /// </summary>
    public partial class GettingStartedView : UserControl
    {
        public GettingStartedView(GettingStartedViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
