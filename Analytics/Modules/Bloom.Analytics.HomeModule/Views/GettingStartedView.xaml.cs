using System.Windows.Controls;
using Bloom.Analytics.HomeModule.ViewModels;

namespace Bloom.Analytics.HomeModule.Views
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
