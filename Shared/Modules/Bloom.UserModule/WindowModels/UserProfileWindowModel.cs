using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.UserModule.WindowModels
{
    /// <summary>
    /// Window model for NewLibraryWindow.xaml
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Mvvm.BindableBase" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    public class UserProfileWindowModel : BindableBase, IDataErrorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileWindowModel"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public UserProfileWindowModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            State = (ApplicationState) regionManager.Regions[Settings.MenuRegion].Context;

            if (State.User != null && State.User.PersonId != User.Anonymous.PersonId)
            {
                Header = "Edit Profile";
                SaveButtonText = "Save Changes";
                ProfileImagePath = State.User.ProfileImagePath;
                UserName = State.User.Name;
                Birthday = State.User.Birthday;
                Twitter = State.User.Twitter ?? "@";
            }
            else
            {
                Header = "New User";
                SaveButtonText = "Save New User";
                Twitter = "@";
            }
        }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public ApplicationState State { get; private set; }

        /// <summary>
        /// Gets or sets the set profile image command.
        /// </summary>
        public ICommand SetProfileImageCommand { get; set; }

        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Gets or sets the save changes command.
        /// </summary>
        public ICommand SaveChangesCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        public bool IsLoading { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header
        {
            get { return _header; }
            set { SetProperty(ref _header, value); }
        }
        private string _header;

        /// <summary>
        /// Gets or sets the save button text.
        /// </summary>
        public string SaveButtonText
        {
            get { return _saveButtonText; }
            set { SetProperty(ref _saveButtonText, value); }
        }
        private string _saveButtonText;

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
        /// Gets or sets the profile image path.
        /// </summary>
        public string ProfileImagePath
        {
            get { return _profileImagePath; }
            set { SetProperty(ref _profileImagePath, value); }
        }
        private string _profileImagePath;

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _userName;

        /// <summary>
        /// Gets or sets the user's birthday.
        /// </summary>
        public DateTime? Birthday
        {
            get { return _birthday; }
            set { SetProperty(ref _birthday, value); }
        }
        private DateTime? _birthday;

        /// <summary>
        /// Gets or sets the user's Twitter username.
        /// </summary>
        public string Twitter
        {
            get { return _twitter; }
            set { SetProperty(ref _twitter, value); }
        }
        private string _twitter;

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>Null for no error, otherwise the error string.</returns>
        public string this[string columnName]
        {
            get
            {
                IsValid = false;
                if (IsLoading)
                    return null;

                if (columnName == "UserName")
                {
                    if (string.IsNullOrEmpty(UserName))
                        return "User name is required";
                }
                if (columnName == "ProfileImagePath")
                {
                    if (!string.IsNullOrEmpty(ProfileImagePath) && !File.Exists(ProfileImagePath))
                        return "Invalid profile image path";
                }
                if (columnName == "Twitter")
                {
                    var twitter = Twitter.TrimStart('@');
                    if (!string.IsNullOrWhiteSpace(twitter) && !Regex.IsMatch(twitter, RegExPattern.AlphaNumericWithUnderscore))
                        return "Invalid Twitter username, only letters, numbers, and underscores are allowed";
                }

                IsValid = !string.IsNullOrEmpty(UserName) &&
                          (string.IsNullOrEmpty(ProfileImagePath) || File.Exists(ProfileImagePath));

                return null;
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error { get { return null; } }

        /// <summary>
        /// Determines whether the user profile window has changes.
        /// </summary>
        public bool HasChanges()
        {
            if (State.User == null)
                return true;

            var twitter = Twitter == "@" ? null : Twitter;
            var profileImagePath = string.IsNullOrEmpty(ProfileImagePath) ? null : ProfileImagePath;

            return !UserName.Equals(State.User.Name, StringComparison.CurrentCultureIgnoreCase) ||
                   (profileImagePath == null && State.User.ProfileImagePath != null) ||
                   (profileImagePath != null && !profileImagePath.Equals(State.User.ProfileImagePath, StringComparison.CurrentCultureIgnoreCase)) ||
                   (twitter == null && State.User.Twitter != null) ||
                   (twitter != null && !twitter.Equals(State.User.Twitter, StringComparison.CurrentCultureIgnoreCase)) ||
                   (Birthday == null && State.User.Birthday != null) ||
                   (Birthday != null && Birthday != State.User.Birthday);
        }

        /// <summary>
        /// Saves the user profile window changes.
        /// </summary>
        public void SaveChanges()
        {
            if (State.User == null)
                State.User = User.Anonymous;

            if (State.User.PersonId == User.Anonymous.PersonId)
            {
                var newUser = User.Create(Person.Create(UserName));
                newUser.ProfileImagePath = ProfileImagePath;
                newUser.Birthday = Birthday;
                newUser.Twitter = Twitter == "@" ? null : Twitter;

                EventAggregator.GetEvent<ChangeUserEvent>().Publish(newUser);
            }
            else
            {
                State.User.Name = UserName;
                State.User.ProfileImagePath = ProfileImagePath;
                State.User.Birthday = Birthday;
                State.User.Twitter = Twitter == "@" ? null : Twitter;

                EventAggregator.GetEvent<UserUpdatedEvent>().Publish(null);
                EventAggregator.GetEvent<SaveStateEvent>().Publish(State);
            }
        }
    }
}
