using System.IO;
using System.Windows.Media;
using System.Windows.Forms;
using Bloom.Browser.LibraryModule.WindowModels;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.Commands;

namespace Bloom.Browser.LibraryModule.Windows
{
    /// <summary>
    /// Interaction logic for NewLibraryWindow.xaml
    /// </summary>
    public partial class NewLibraryWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewLibraryWindow" /> class.
        /// </summary>
        /// <param name="windowModel">The window model.</param>
        public NewLibraryWindow(NewLibraryWindowModel windowModel)
        {
            InitializeComponent();
            _folderBrowserDialog = new FolderBrowserDialog();
            windowModel.IsLoading = true;
            windowModel.BrowseFoldersCommand = new DelegateCommand<object>(BrowseFolders, CanBrowseFolders);
            windowModel.CreateNewLibraryCommand = new DelegateCommand<object>(CreateNewLibrary, CanCreateNewLibrary);
            windowModel.CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
            DataContext = windowModel;
        }
        private readonly FolderBrowserDialog _folderBrowserDialog;

        /// <summary>
        /// Gets the window model.
        /// </summary>
        private NewLibraryWindowModel WindowModel => (NewLibraryWindowModel) DataContext;

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations that are directed by the layout system. The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
 	        base.OnRender(drawingContext);
            WindowModel.IsLoading = false;
        }

        /// <summary>
        /// Determines whether this instance can browse folders.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private bool CanBrowseFolders(object nothing)
        {
            return true;
        }

        /// <summary>
        /// Opens a folder browser dialogue.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private void BrowseFolders(object nothing)
        {
            _folderBrowserDialog.SelectedPath = WindowModel.FolderPath;
            _folderBrowserDialog.ShowNewFolderButton = true;
            _folderBrowserDialog.Description = @"Select a folder for the new library.";
            if (!string.IsNullOrEmpty(WindowModel.LibraryName))
                // ReSharper disable LocalizableElement
                _folderBrowserDialog.Description += "\r\nA folder named \"" + WindowModel.LibraryName + "\" will be created at this location.";
                // ReSharper restore LocalizableElement
                
            var result = _folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                WindowModel.FolderPath = _folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// Determines whether this instance can create a new library.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private bool CanCreateNewLibrary(object nothing)
        {
            return true;
        }

        /// <summary>
        /// Creates a new library.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private void CreateNewLibrary(object nothing)
        {
            var libraryOwner = WindowModel.GetOwner();
            var libraryFolder = Path.Combine(WindowModel.FolderPath, WindowModel.LibraryName);
            var newLibrary = Library.Create(libraryOwner, WindowModel.LibraryName, libraryFolder);
            WindowModel.EventAggregator.GetEvent<CreateNewLibraryEvent>().Publish(newLibrary);
            Close();
        }

        /// <summary>
        /// Determines whether this instance can be cancelled.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private bool CanCancel(object nothing)
        {
            return true;
        }

        /// <summary>
        /// Closes the window without taking any action.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private void Cancel(object nothing)
        {
            Close();
        }
    }
}
