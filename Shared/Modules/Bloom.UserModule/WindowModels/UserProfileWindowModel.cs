﻿using System;
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
    public class UserProfileWindowModel : BindableBase, IDataErrorInfo
    {
        public UserProfileWindowModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            State = (ApplicationState) regionManager.Regions[Settings.MenuRegion].Context;

            if (State.User != null)
            {
                ProfileImagePath = State.User.ProfileImagePath;
                UserName = State.User.Name;
                Birthday = State.User.Birthday;
                Twitter = State.User.Twitter ?? "@";
            }
            else
                Twitter = "@";
        }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public ApplicationState State { get; private set; }

        public ICommand SetProfileImageCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ICommand SaveChangesCommand { get; set; }

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

        public string ProfileImagePath
        {
            get { return _profileImagePath; }
            set { SetProperty(ref _profileImagePath, value); }
        }
        private string _profileImagePath;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _userName;

        public DateTime? Birthday
        {
            get { return _birthday; }
            set { SetProperty(ref _birthday, value); }
        }
        private DateTime? _birthday;

        public string Twitter
        {
            get { return _twitter; }
            set { SetProperty(ref _twitter, value); }
        }
        private string _twitter;

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

        public string Error { get { return null; } }

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

        public void SaveChanges()
        {
            if (State.User == null)
                State.User = User.Create(Person.Create(UserName));

            State.User.Name = UserName;
            State.User.ProfileImagePath = ProfileImagePath;
            State.User.Birthday = Birthday;
            State.User.Twitter = Twitter == "@" ? null : Twitter;

            EventAggregator.GetEvent<UserUpdatedEvent>().Publish(null);
            EventAggregator.GetEvent<SaveStateEvent>().Publish(null);
        }
    }
}