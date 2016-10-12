using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Bloom.UserModule.Services;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.UserModule.WindowModels
{
    /// <summary>
    /// Window model for ChangeUserWindow.xaml
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Mvvm.BindableBase" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    public class ChangeUserWindowModel : BindableBase, IDataErrorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeUserWindowModel"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="sharedUserService">The shared user service.</param>
        public ChangeUserWindowModel(IRegionManager regionManager, IEventAggregator eventAggregator, ISharedUserService sharedUserService)
        {
            _sharedUserService = sharedUserService;
            EventAggregator = eventAggregator;
            var potentialOwners = _sharedUserService.ListUsers();
            State = (ApplicationState) regionManager.Regions[Settings.MenuRegion].Context;

            if (State.User != null)
            {
                ButtonText = Header = "Change User";
                UserName = State.User.Name;
            }
            else
            {
                Header = "Login User";
                ButtonText = "Login";
            }
            
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
        /// Gets or sets the header.
        /// </summary>
        public string Header
        {
            get { return _header; }
            set { SetProperty(ref _header, value); }
        }
        private string _header;

        /// <summary>
        /// Gets or sets the button text.
        /// </summary>
        public string ButtonText
        {
            get { return _buttonText; }
            set { SetProperty(ref _buttonText, value); }
        }
        private string _buttonText;

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
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _userName;

        /// <summary>
        /// Gets or sets a list of potential users.
        /// </summary>
        public ObservableCollection<User> PotentialUsers { get; set; }

        /// <summary>
        /// Gets or sets the change user command.
        /// </summary>
        public ICommand ChangeUserCommand { get; set; }

        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        public ICommand CancelCommand { get; set; }

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

                IsValid = !string.IsNullOrEmpty(UserName) &&
                          ((State.User != null && !State.User.Name.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)) ||
                          State.User == null);

                return null;
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error { get { return null; } }

        /// <summary>
        /// Changes the active user.
        /// </summary>
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
    }
}
