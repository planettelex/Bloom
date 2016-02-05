using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Bloom.UserModule.Services;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.UserModule.WindowModels
{
    public class ChangeUserWindowModel : BindableBase, IDataErrorInfo
    {
        public ChangeUserWindowModel(IRegionManager regionManager, IEventAggregator eventAggregator, ISharedUserService sharedUserService)
        {
            _sharedUserService = sharedUserService;
            EventAggregator = eventAggregator;
            var potentialOwners = _sharedUserService.ListUsers();
            State = (ApplicationState) regionManager.Regions[Common.Settings.MenuRegion].Context;
            if (State.User != null)
                UserName = State.User.Name;

            PotentialUsers = new ObservableCollection<User>();
            foreach (var potentialOwner in potentialOwners)
                PotentialUsers.Add(potentialOwner);
        }
        private readonly ISharedUserService _sharedUserService;

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public ApplicationState State { get; private set; }

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

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _userName;

        public ObservableCollection<User> PotentialUsers { get; set; }

        public ICommand ChangeUserCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public void ChangeUser()
        {
            var newUser = PotentialUsers.FirstOrDefault(user => user.Name.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
            var changeUser = false;
            if (newUser == null)
            {
                var newPerson = Person.Create(UserName);
                newUser = User.Create(newPerson);
                _sharedUserService.AddUser(newUser);
                changeUser = true;
            }
            else if (State.User == null || State.User.PersonId != newUser.PersonId)
                changeUser = true;
            
            if (changeUser)
            {
                newUser.LastLogin = DateTime.Now;
                EventAggregator.GetEvent<ChangeUserEvent>().Publish(newUser);
            }
        }

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

                IsValid = !string.IsNullOrEmpty(UserName) &&
                          (State.User != null && !State.User.Name.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));

                return null;
            }
        }

        public string Error { get { return null; } }
    }
}
