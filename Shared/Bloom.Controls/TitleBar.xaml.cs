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
            if (Application.Current.MainWindow != null && Application.Current.MainWindow.WindowState == WindowState.Maximized)
                RestoreWindow();
            else
                MaximizeWindow();
        }

        /// <summary>
        /// Determines whether this instance can toggle window state.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void CanToggleWindowState(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// A minimize button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            MinimizeWindow();
        }

        /// <summary>
        /// A maximize button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            MaximizeWindow();
        }

        /// <summary>
        /// A restore button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RestoreButtonClick(object sender, RoutedEventArgs e)
        {
            RestoreWindow();
        }

        /// <summary>
        /// A close application button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            ExitApplication();
        }

        /// <summary>
        /// Drags the window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.DragMove();
        }

        /// <summary>
        /// Minimizes the window.
        /// </summary>
        private static void MinimizeWindow()
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Maximizes the window.
        /// </summary>
        private void MaximizeWindow()
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;

            MaximizeButton.Visibility = Visibility.Collapsed;
            RestoreButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Restores the window.
        /// </summary>
        private void RestoreWindow()
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.WindowState = WindowState.Normal;

            MaximizeButton.Visibility = Visibility.Visible;
            RestoreButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        private static void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
