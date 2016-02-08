using System.Windows;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Bloom.UserModule.WindowModels;
using Bloom.UserModule.Windows;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.UserModule.Services
{
    public class SharedUserService : UserBaseService, ISharedUserService
    {
        public SharedUserService(IEventAggregator eventAggregator, IRegionManager regionManager, IUserRepository userRepository)
            : base(eventAggregator, regionManager, userRepository)
        {
            // Subscribe to events
            EventAggregator.GetEvent<ShowChangeUserModalEvent>().Subscribe(ShowChangeUserModal);
            EventAggregator.GetEvent<ShowUserProfileModalEvent>().Subscribe(ShowUserProfileModal);
        }

        public void ShowChangeUserModal(object nothing)
        {
            ShowChangeUserModal();
        }

        public void ShowChangeUserModal()
        {
            var changeUserWindowModel = new ChangeUserWindowModel(RegionManager, EventAggregator, this);
            var changeUserWindow = new ChangeUserWindow(changeUserWindowModel)
            {
                Owner = Application.Current.MainWindow
            };
            changeUserWindow.ShowDialog();
        }

        public void ShowUserProfileModal(object nothing)
        {
            ShowUserProfileModal();
        }

        public void ShowUserProfileModal()
        {
            var userProfileWindowModel = new UserProfileWindowModel(RegionManager, EventAggregator);
            var userProfileWindow = new UserProfileWindow(userProfileWindowModel)
            {
                Owner = Application.Current.MainWindow
            };
            userProfileWindow.ShowDialog();
        }
    }
}
