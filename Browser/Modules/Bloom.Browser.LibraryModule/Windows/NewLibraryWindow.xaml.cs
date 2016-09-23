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
            windowModel.BrowseFoldersCommand = new DelegateCommand<object>(BrowserFolders, CanBrowserFolders);
            windowModel.CreateNewLibraryCommand = new DelegateCommand<object>(CreateNewLibrary, CanCreateNewLibrary);
            windowModel.CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
            DataContext = windowModel;
        }
        private readonly FolderBrowserDialog _folderBrowserDialog;

        private NewLibraryWindowModel WindowModel { get { return (NewLibraryWindowModel) DataContext; } }

        protected override void OnRender(DrawingContext drawingContext)
        {
 	        base.OnRender(drawingContext);
            WindowModel.IsLoading = false;
        }

        private bool CanBrowserFolders(object nothing)
        {
            return true;
        }

        private void BrowserFolders(object nothing)
        {
            _folderBrowserDialog.SelectedPath = WindowModel.FolderPath;
            _folderBrowserDialog.ShowNewFolderButton = true;
            _folderBrowserDialog.Description = "Select a folder for the new library.";
            if (!string.IsNullOrEmpty(WindowModel.LibraryName))
                _folderBrowserDialog.Description += "\r\nA folder named \"" + WindowModel.LibraryName + "\" will be created at this location.";
                
            var result = _folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                WindowModel.FolderPath = _folderBrowserDialog.SelectedPath;
        }

        private bool CanCreateNewLibrary(object nothing)
        {
            return true;
        }

        private void CreateNewLibrary(object nothing)
        {
            var libraryOwner = WindowModel.GetOwner();
            var libraryFolder = WindowModel.FolderPath + "\\" + WindowModel.LibraryName;
            var newLibrary = Library.Create(libraryOwner, WindowModel.LibraryName, libraryFolder);
            WindowModel.EventAggregator.GetEvent<CreateNewLibraryEvent>().Publish(newLibrary);
            Close();
        }

        private bool CanCancel(object nothing)
        {
            return true;
        }

        private void Cancel(object nothing)
        {
            Close();
        }
    }
}
