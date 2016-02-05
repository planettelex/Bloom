using System;
using System.Collections.Generic;
using Bloom.PubSubEvents;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Services
{
    public class UserBaseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserBaseService" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="userRepository">The user repository.</param>
        public UserBaseService(IEventAggregator eventAggregator, IRegionManager regionManager, IUserRepository userRepository)
        {
            UserRepository = userRepository;
            EventAggregator = eventAggregator;
            RegionManager = regionManager;

            // Subscribe to events
            EventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
        }
        
        /// <summary>
        /// Gets the state.
        /// </summary>
        public ApplicationState ApplicationState { get; private set; }

        protected IEventAggregator EventAggregator { get; set; }

        protected IRegionManager RegionManager { get; set; }

        protected IUserRepository UserRepository { get; set; }

        private void SetState(object nothing)
        {
            ApplicationState = (ApplicationState) RegionManager.Regions[Common.Settings.MenuRegion].Context;
        }

        public User InitializeUser()
        {
            return UserRepository.GetLastUser();
        }

        public User GetUser(Guid personId)
        {
            return UserRepository.GetUser(personId);
        }

        public List<User> ListUsers()
        {
            return UserRepository.ListUsers();
        }

        public void AddUser(User user)
        {
            UserRepository.AddUser(user);
        }
    }
}
