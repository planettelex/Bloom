using Bloom.Player.Playing.ViewModels;

namespace Bloom.Player.Playing.Views
{
    /// <summary>
    /// Interaction logic for PlayingView.xaml
    /// </summary>
    public partial class PlayingView
    {
        public PlayingView(PlayingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
