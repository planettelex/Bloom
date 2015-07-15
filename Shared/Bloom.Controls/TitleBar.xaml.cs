using System.Windows;
using System.Windows.Input;

namespace Bloom.Controls
{
    /// <summary>
    /// Interaction logic for TitleBar.xaml
    /// </summary>
    public partial class TitleBar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TitleBar"/> class.
        /// </summary>
        public TitleBar()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Sets the button visibilties according to the main window state.
        /// </summary>
        public void SetButtonVisibilties()
        {
            if (Application.Current.MainWindow != null && Application.Current.MainWindow.WindowState == WindowState.Maximized)
                MaximizeButton.Visibility = Visibility.Collapsed;
            else
                RestoreButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// The title dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TitleBar), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// The toggle window state command.
        /// </summary>
        public static RoutedCommand ToggleWindowStateCommand = new RoutedCommand();

        /// <summary>
        /// Toggles the state of the window between normal and maximized.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
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
            Application.Current.MainWindow.DragMove();
        }

        private static void MinimizeWindow()
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

        private static void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
