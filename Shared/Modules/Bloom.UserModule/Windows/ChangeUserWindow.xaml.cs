using Bloom.Services;
using Bloom.UserModule.WindowModels;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.UserModule.Windows
{
    /// <summary>
    /// Interaction logic for ChangeUserWindow.xaml
    /// </summary>
    public partial class ChangeUserWindow
    {
        public ChangeUserWindow(ChangeUserWindowModel windowModel, IEventAggregator eventAggregator, IUserBaseService sharedUserService)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            _sharedUserService = sharedUserService;

            DataContext = windowModel;
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserBaseService _sharedUserService;

        protected ChangeUserWindowModel Model
        {
            get { return (ChangeUserWindowModel) DataContext; }
        }
    }
}
