using System;
using System.Collections.Generic;
using Bloom.PubSubEvents;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Services
{
    /// <summary>
    /// Base class for user services.
    /// </summary>
    public class UserBaseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserBaseService" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="fileSystemService">The file system service.</param>
        public UserBaseService(IEventAggregator eventAggregator, IRegionManager regionManager, IUserRepository userRepository, IFileSystemService fileSystemService)
        {
            FileSystemService = fileSystemService;
            UserRepository = userRepository;
            EventAggregator = eventAggregator;
            RegionManager = regionManager;

            // Subscribe to events
            EventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
        }
        
        /// <summary>
        /// Gets the application state.
        /// </summary>
        public ApplicationState ApplicationState { get; private set; }

        /// <summary>
        /// Gets or sets the file system service.
        /// </summary>
        protected IFileSystemService FileSystemService { get; set; }

        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        protected IEventAggregator EventAggregator { get; set; }

        /// <summary>
        /// Gets or sets the region manager.
        /// </summary>
        protected IRegionManager RegionManager { get; set; }

        /// <summary>
        /// Gets or sets the user repository.
        /// </summary>
        protected IUserRepository UserRepository { get; set; }

        /// <summary>
        /// Sets the application state in the menu region context.
        /// </summary>
        /// <param name="nothing">Unused object.</param>
        private void SetState(object nothing = null)
        {
            ApplicationState = (ApplicationState) RegionManager.Regions[Common.Settings.MenuRegion].Context;
        }

        /// <summary>
        /// Initializes the user to the last one to use the suite.
        /// </summary>
        public User InitializeUser()
        {
            return UserRepository.GetLastUser();
        }

        /// <summary>
        /// Gets a user.
        /// </summary>
        /// <param name="personId">The user's person identifier.</param>
        public User GetUser(Guid personId)
        {
            return UserRepository.GetUser(personId);
        }

        /// <summary>
        /// Lists the users.
        /// </summary>
        public List<User> ListUsers()
        {
            return UserRepository.ListUsers();
        }

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void AddUser(User user)
        {
            UserRepository.AddUser(user);
        }

        /// <summary>
        /// Checks to see if the suite user has changed.
        /// </summary>
        public void CheckUser()
        {
            if (ApplicationState == null)
                return;

            var lastUserToLogin = UserRepository.GetLastUser();
            EventAggregator.GetEvent<ChangeUserEvent>().Publish(lastUserToLogin);
        }
    }
}
