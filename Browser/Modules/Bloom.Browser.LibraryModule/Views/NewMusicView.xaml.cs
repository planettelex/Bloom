using System.Windows.Media;
using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for NewMusicView.xaml
    /// </summary>
    public partial class NewMusicView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewMusicView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public NewMusicView(NewMusicViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        public NewMusicViewModel ViewModel => (NewMusicViewModel) DataContext;

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations that are directed by the layout system. The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            ViewModel.StartImport();
        }
    }
}
