using Bloom.Browser.Library.WindowModels;
using Microsoft.Practices.Prism.Commands;

namespace Bloom.Browser.Library.Windows
{
    /// <summary>
    /// Interaction logic for NewLibraryWindow.xaml
    /// </summary>
    public partial class NewLibraryWindow
    {
        public NewLibraryWindow(NewLibraryWindowModel windowModel)
        {
            InitializeComponent();

            windowModel.BrowseFoldersCommand = new DelegateCommand<object>(BrowserFolders, CanBrowserFolders);
            windowModel.CreateNewLibraryCommand = new DelegateCommand<object>(CreateNewLibrary, CanCreateNewLibrary);
            windowModel.CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
            DataContext = windowModel;
        }

        private bool CanBrowserFolders(object nothing)
        {
            return true;
        }

        private void BrowserFolders(object nothing)
        {

        }

        private bool CanCreateNewLibrary(object nothing)
        {
            return true;
        }

        private void CreateNewLibrary(object nothing)
        {
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
