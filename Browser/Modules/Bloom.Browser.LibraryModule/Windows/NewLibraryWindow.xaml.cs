using Bloom.Browser.LibraryModule.WindowModels;
using Bloom.Browser.PubSubEvents;
using Bloom.Domain.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.LibraryModule.Windows
{
    /// <summary>
    /// Interaction logic for NewLibraryWindow.xaml
    /// </summary>
    public partial class NewLibraryWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewLibraryWindow"/> class.
        /// </summary>
        /// <param name="windowModel">The window model.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public NewLibraryWindow(NewLibraryWindowModel windowModel, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            windowModel.BrowseFoldersCommand = new DelegateCommand<object>(BrowserFolders, CanBrowserFolders);
            windowModel.CreateNewLibraryCommand = new DelegateCommand<object>(CreateNewLibrary, CanCreateNewLibrary);
            windowModel.CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
            DataContext = windowModel;
        }
        private readonly IEventAggregator _eventAggregator;

        private NewLibraryWindowModel WindowModel { get { return (NewLibraryWindowModel) DataContext; } }

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
            var libraryOwner = WindowModel.GetOwner();
            var newLibrary = Library.Create(libraryOwner, WindowModel.LibraryName, WindowModel.FolderPath);
            _eventAggregator.GetEvent<CreateNewLibraryEvent>().Publish(newLibrary);
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
