using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Bloom.Common.ExtensionMethods;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Bloom.UserModule.Services;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.LibraryModule.WindowModels
{
    /// <summary>
    /// Window model for NewLibraryWindow.xaml.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Mvvm.BindableBase" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    public class NewLibraryWindowModel : BindableBase, IDataErrorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewLibraryWindowModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="sharedUserService">The shared user service.</param>
        public NewLibraryWindowModel(IRegionManager regionManager, IEventAggregator eventAggregator, ISharedUserService sharedUserService)
        {
            _sharedUserService = sharedUserService;
            EventAggregator = eventAggregator;
            var potentialOwners = _sharedUserService.ListUsers();
            State = (BrowserState) regionManager.Regions[Bloom.Common.Settings.MenuRegion].Context;

            if (State.User != null && State.UserId != User.Anonymous.PersonId)
                OwnerName = State.User.Name;

            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            PotentialOwners = new ObservableCollection<User>();
            foreach (var potentialOwner in potentialOwners)
                PotentialOwners.Add(potentialOwner);
        }
        private readonly ISharedUserService _sharedUserService;

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }

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

        /// <summary>
        /// Gets or sets the name of the library.
        /// </summary>
        public string LibraryName
        {
            get { return _libraryName; }
            set { SetProperty(ref _libraryName, value); }
        }
        private string _libraryName;

        /// <summary>
        /// Gets or sets the name of the library owner.
        /// </summary>
        public string OwnerName
        {
            get { return _ownerName; }
            set { SetProperty(ref _ownerName, value); }
        }
        private string _ownerName;

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        public Guid OwnerId
        {
            get { return _ownerId; }
            set { SetProperty(ref _ownerId, value); }
        }
        private Guid _ownerId;

        /// <summary>
        /// Gets or sets the folder path for the new library.
        /// </summary>
        public string FolderPath
        {
            get { return _folderPath; }
            set { SetProperty(ref _folderPath, value); }
        }
        private string _folderPath;

        /// <summary>
        /// Gets or sets the potential owners of the new library.
        /// </summary>
        public ObservableCollection<User> PotentialOwners { get; set; }

        /// <summary>
        /// Gets or sets the browse folders command.
        /// </summary>
        public ICommand BrowseFoldersCommand { get; set; }

        /// <summary>
        /// Gets or sets the create new library command.
        /// </summary>
        public ICommand CreateNewLibraryCommand { get; set; }

        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Gets the library owner.
        /// </summary>
        public Person GetOwner()
        {
            Person owner;
            var ownerUser = PotentialOwners.FirstOrDefault(user => user.Name.Equals(OwnerName, StringComparison.InvariantCultureIgnoreCase));
            if (ownerUser == null)
            {
                owner = Person.Create(OwnerName);
                ownerUser = User.Create(owner);
                _sharedUserService.AddUser(ownerUser);
            }
            else
                owner = ownerUser.AsPerson();

            // If state user is null or anonymous, set it to the owner.
            if (State.User == null || State.UserId ==  User.Anonymous.PersonId)
                EventAggregator.GetEvent<ChangeUserEvent>().Publish(ownerUser); 

            return owner;
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        public string this[string columnName]
        {
            get
            {
                IsValid = false;
                if (IsLoading)
                    return null;

                if (columnName == "FolderPath")
                {
                    if (string.IsNullOrEmpty(FolderPath))
                        return "Folder path is required";
                    if (!Directory.Exists(FolderPath))
                        return "Specified folder does not exist";

                    // This forces a re-evaluation of library name validation.
                    LibraryName += " ";
                    LibraryName = LibraryName.Trim();
                }
                var libraryFolderPath = FolderPath + "\\" + LibraryName;
                var filePath = libraryFolderPath + "\\" + LibraryName + Bloom.Common.Settings.LibraryFileExtension;
                if (columnName == "LibraryName")
                {
                    if (string.IsNullOrEmpty(LibraryName))
                        return "Library name is required";
                    if (!LibraryName.IsValidFileName())
                        return "Library cannot contain the characters <, >, :, \", /, \\, |, ?, *";
                    if (File.Exists(filePath))
                        return "A library named \"" + LibraryName + "\" already exists at this location";
                }
                if (columnName == "OwnerName")
                {
                    if (string.IsNullOrEmpty(OwnerName))
                        return "Owner name is required";
                }

                IsValid = !string.IsNullOrEmpty(FolderPath) && 
                          !string.IsNullOrEmpty(LibraryName) &&
                          !string.IsNullOrEmpty(OwnerName) &&
                          Directory.Exists(FolderPath) &&
                          !File.Exists(filePath);

                return null;
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error { get { return null; } }
    }
}
