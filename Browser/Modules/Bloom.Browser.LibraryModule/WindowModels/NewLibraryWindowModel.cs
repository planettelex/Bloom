using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Bloom.Common.ExtensionMethods;
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
            State = (BrowserState) regionManager.Regions["DocumentRegion"].Context;
            PotentialOwners = new ObservableCollection<Person>();
            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        public bool IsLoading { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        public bool IsValid
        {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }
        private bool _isValid;

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
                IsValid = false;
                if (IsLoading)
                    return null;

                if (columnName == "LibraryName")
                {
                    if (string.IsNullOrEmpty(LibraryName))
                        return "Library name is required";
                    if (!LibraryName.IsValidFilename())
                        return "Library cannot contain the characters <, >, :, \", /, \\, |, ?, *";
                }
                if (columnName == "OwnerName")
                {
                    if (string.IsNullOrEmpty(OwnerName))
                        return "Owner name is required";
                }
                if (columnName == "FolderPath")
                {
                    if (string.IsNullOrEmpty(FolderPath))
                        return "Folder path is required";
                    if (!Directory.Exists(FolderPath))
                        return "Specified folder does not exist.";
                }

                IsValid = true;
                return null;
            }
        }

        public string Error { get { return null; } }
    }
}
