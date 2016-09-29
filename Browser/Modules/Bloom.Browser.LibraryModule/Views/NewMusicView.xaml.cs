using System.Windows.Controls;
using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for NewMusicView.xaml
    /// </summary>
    public partial class NewMusicView
    {
        public NewMusicView(NewMusicViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
