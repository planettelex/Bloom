using System.Windows;
using System.Windows.Input;
using Bloom.Browser.Common;
using Bloom.Services;

namespace Bloom.Browser
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        /// <param name="skinningService">The skinning service.</param>
        public Shell(ISkinningService skinningService)
        {
            InitializeComponent();
            var state = new State();
            DataContext = state;

            skinningService.SetSkin(state.Skin);

            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                MaximizeButton.Visibility = Visibility.Collapsed;
            else
                RestoreButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// The toggle window state command.
        /// </summary>
        public static RoutedCommand ToggleWindowStateCommand = new RoutedCommand();

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            var state = (State) DataContext;
            state.Save();
        }

        private void ToggleWindowState(object sender, ExecutedRoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                RestoreWindow();
            else
                MaximizeWindow();
        }

        private void CanToggleWindowState(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            MinimizeWindow();
        }

        private void MaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            MaximizeWindow();
        }

        private void RestoreButtonClick(object sender, RoutedEventArgs e)
        {
            RestoreWindow();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            ExitApplication();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            MaximizeButton.Visibility = Visibility.Collapsed;
            RestoreButton.Visibility = Visibility.Visible;
        }

        private void RestoreWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            MaximizeButton.Visibility = Visibility.Visible;
            RestoreButton.Visibility = Visibility.Collapsed;
        }

        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
