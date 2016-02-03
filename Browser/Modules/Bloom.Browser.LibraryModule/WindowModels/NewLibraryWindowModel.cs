using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Bloom.Common.ExtensionMethods;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.LibraryModule.WindowModels
{
    public class NewLibraryWindowModel : BindableBase, IDataErrorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewLibraryWindowModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public NewLibraryWindowModel(IRegionManager regionManager, IUserBaseService userService, IEventAggregator eventAggregator)
        {
            _userService = userService;
            EventAggregator = eventAggregator;
            var potentialOwners = _userService.ListUsers();
            State = (BrowserState) regionManager.Regions[Bloom.Common.Settings.MenuRegion].Context;
            if (State.User != null)
                OwnerName = State.User.Name;

            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            PotentialOwners = new ObservableCollection<User>();
            foreach (var potentialOwner in potentialOwners)
                PotentialOwners.Add(potentialOwner);
        }
        private readonly IUserBaseService _userService;

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

        public ObservableCollection<User> PotentialOwners { get; set; }

        public ICommand BrowseFoldersCommand { get; set; }

        public ICommand CreateNewLibraryCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public Person GetOwner()
        {
            Person owner;
            var ownerUser = PotentialOwners.FirstOrDefault(user => user.Name.Equals(OwnerName, StringComparison.InvariantCultureIgnoreCase));
            if (ownerUser == null)
            {
                owner = Person.Create(OwnerName);
                ownerUser = User.Create(owner);
                _userService.AddUser(ownerUser);
            }
            else
                owner = ownerUser.AsPerson();

            // If state user is null, set it to the owner.
            if (State.User == null)
            {
                State.SetUser(ownerUser);
                EventAggregator.GetEvent<UserChangedEvent>().Publish(null);
            } 

            return owner;
        }

        public string this[string columnName]
        {
            get
            {
                IsValid = false;
                if (IsLoading)
                    return null;

                var filePath = FolderPath + "\\" + LibraryName + Bloom.Common.Settings.LibraryFileExtension;

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

        public string Error { get { return null; } }
    }
}
