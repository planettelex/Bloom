using System.Windows;
using Bloom.Modules.UserModule.WindowModels;
using Bloom.Modules.UserModule.Windows;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using ChangeUserWindow = Bloom.Modules.UserModule.Windows.ChangeUserWindow;

namespace Bloom.Modules.UserModule.Services
{
    /// <summary>
    /// Service for shared user operations.
    /// </summary>
    /// <seealso cref="Bloom.Services.UserBaseService" />
    /// <seealso cref="ISharedUserService" />
    public class SharedUserService : UserBaseService, ISharedUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedUserService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="fileSystemService">The file system service.</param>
        public SharedUserService(IEventAggregator eventAggregator, IRegionManager regionManager, IUserRepository userRepository, IFileSystemService fileSystemService)
            : base(eventAggregator, regionManager, userRepository, fileSystemService)
        {
            // Subscribe to events
            EventAggregator.GetEvent<ShowChangeUserModalEvent>().Subscribe(ShowChangeUserModal);
            EventAggregator.GetEvent<ShowUserProfileModalEvent>().Subscribe(ShowUserProfileModal);
        }

        /// <summary>
        /// Shows the change user modal window.
        /// </summary>
        public void ShowChangeUserModal(object nothing)
        {
            ShowChangeUserModal();
        }

        /// <summary>
        /// Shows the change user modal window.
        /// </summary>
        public void ShowChangeUserModal()
        {
            var changeUserWindowModel = new ChangeUserWindowModel(RegionManager, EventAggregator, this);
            var changeUserWindow = new ChangeUserWindow(changeUserWindowModel)
            {
                Owner = Application.Current.MainWindow
            };
            changeUserWindow.ShowDialog();
        }

        /// <summary>
        /// Shows the user profile modal window.
        /// </summary>
        public void ShowUserProfileModal(object nothing)
        {
            ShowUserProfileModal();
        }

        /// <summary>
        /// Shows the user profile modal window.
        /// </summary>
        public void ShowUserProfileModal()
        {
            var userProfileWindowModel = new UserProfileWindowModel(RegionManager, EventAggregator);
            var userProfileWindow = new UserProfileWindow(userProfileWindowModel, FileSystemService)
            {
                Owner = Application.Current.MainWindow
            };
            userProfileWindow.ShowDialog();
        }
    }
}
