using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.LibraryModule.WindowModels
{
    public class NewLibraryWindowModel : BindableBase, IDataErrorInfo
    {
        public NewLibraryWindowModel(IRegionManager regionManager)
        {
            State = (BloomState) regionManager.Regions["DocumentRegion"].Context;
            PotentialOwners = new ObservableCollection<Person>();
            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BloomState State { get; private set; }

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

        public Person GetOwner()
        {
            var libraryOwner = PotentialOwners.FirstOrDefault(owner => owner.Name.Equals(OwnerName, StringComparison.InvariantCultureIgnoreCase));
            return libraryOwner ?? Person.Create(OwnerName);
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "LibraryName")
                {
                    // Validate property and return a string if there is an error
                    if (string.IsNullOrEmpty(LibraryName))
                        return "Library name is required";
                }

                // If there's no error, null gets returned
                return null;
            }
        }

        public string Error { get { return null; } }
    }
}
