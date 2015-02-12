using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.Library.WindowModels
{
    public class NewLibraryWindowModel : BindableBase
    {
        public NewLibraryWindowModel(IRegionManager regionManager)
        {
            PotentialOwners = new ObservableCollection<Person>();
            State = (BrowserState) regionManager.Regions["DocumentRegion"].Context;
            
        }

        /// <summary>
        /// Gets the browser application state.
        /// </summary>
        public BrowserState State { get; private set; }

        public string LibraryName
        {
            get { return _libraryName; }
            set { SetProperty(ref _libraryName, value); }
        }
        private string _libraryName;

        public string OwnerName
        {
            get { return _ownerName; }
            set { SetProperty(ref _ownerName, value); }
        }
        private string _ownerName;

        public Guid OwnerId
        {
            get { return _ownerId; }
            set { SetProperty(ref _ownerId, value); }
        }
        private Guid _ownerId;

        public string FolderPath
        {
            get { return _folderPath; }
            set { SetProperty(ref _folderPath, value); }
        }
        private string _folderPath;

        public ObservableCollection<Person> PotentialOwners { get; set; }

        public ICommand BrowseFoldersCommand { get; set; }

        public ICommand CreateNewLibraryCommand { get; set; }

        public ICommand CancelCommand { get; set; }
    }
}
