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
        }

        public void ShowChangeUserModal(object nothing)
        {
            ShowChangeUserModal();
        }

        public void ShowChangeUserModal()
        {
            var changeUserWindowModal = new ChangeUserWindowModel(RegionManager, EventAggregator, this);
            var changeUserWindow = new ChangeUserWindow(changeUserWindowModal)
            {
                Owner = Application.Current.MainWindow
            };
            changeUserWindow.ShowDialog();
        }
    }
}
